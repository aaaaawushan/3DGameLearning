using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyController : MonoBehaviour
{
    ///<summary>
    ///当たり判定用コンポーネント
    /// </summary>
    private CollisionDetector dragonCollisionDetector;

    ///<summary>
    ///ドラゴンのパラメーター
    /// </summary>
    public GameCharacterParameter DragonParameter;

    ///<summary>
    ///プレイヤーのパラメーター
    /// </summary>
    public GameCharacterParameter PlayerParameter;

    ///<summary>
    ///現状の体力
    /// </summary>
    private float currentHealth;

    ///<summary>
    ///死んだ判定
    /// </summary>
    private bool isDead;
    //死亡判定を外からのゲットするためのアクセス
    public bool GetIsDead
    {
        get { return isDead; }
    }
    /// <summary>
    /// ダメージ判定
    /// </summary>
    public bool CanTakeDamage = false;
    /// <summary>
    /// ドラゴンのanimator
    /// </summary>
    private Animator dragonAnimator;
    private float groundCheckDistance = 0.1f;

    private float gravity = -9.81f;
    private float verticalVelocity = 0f;
    public bool isGrounded = false;
    private NavMeshAgent navMeshAgent;
    //eyeに配置されているCollisionDetector
    public CollisionDetector EyecollisionDetector;
    //enemeyの活動状態


    public enum EnemyMode
    {
        Invalid = -1,
        Normal,
        Attack,
        Find,
        Move,

    }
    /// <summary>
    /// ENEMYの活動状態を初期化
    /// </summary>
    public EnemyMode EnemyModes = EnemyMode.Normal;
    /// <summary>
    /// プレイヤーの位置
    /// </summary>
    public Transform PlayerTransform;
    /// <summary>
    /// 攻撃用の待ち時間
    /// </summary>
    private float attackWaitTime = 1f;
    private void CheckGroundStatus()
    {
        RaycastHit hit;
        if (Physics.Raycast(
            this.transform.position,
            Vector3.down,
            out hit,
            groundCheckDistance))
        {

            isGrounded = true;

        }
        else
        {
            isGrounded = false;
        }
    }

    private void Start()
    {
        dragonAnimator = GetComponent<Animator>();
        dragonCollisionDetector = GetComponentInChildren<CollisionDetector>();
        navMeshAgent = GetComponent<NavMeshAgent>();

        currentHealth = DragonParameter.MaxHealth;
    }
    private void Update()
    {
        if (isDead)
        {
            return;

        }
        //当たり判定があったとき、CanTakeDamageがtrueだった場合
        if (dragonCollisionDetector.GetIsCollided && CanTakeDamage)
        {
            //floatは0.0001だった場合、ここを通ってくれない
            if (Mathf.Floor(currentHealth) <= 0)
            {
                currentHealth = 0;
                isDead = true;
                CanTakeDamage = false;
                dragonAnimator.SetTrigger("IsDie");
                return;
            }
            currentHealth -= PlayerParameter.AttackPower;
            Debug.Log(currentHealth);
            CanTakeDamage = false;
            EnemyModes = EnemyMode.Attack;
        }
        CheckGroundStatus();
        //地面に設置していない場合は重力適用
        if (!isGrounded)
        {
            verticalVelocity += gravity * Time.deltaTime;
        }
        else
        {
            verticalVelocity = 0f;

        }
        if (verticalVelocity < 0f)
        {
            var pos = this.transform.position;
            pos.y = verticalVelocity;
            //落下処理
            this.transform.position = pos;
        }
        if (navMeshAgent != null)
        {
            //接地地点の調整を行う
            navMeshAgent.baseOffset += verticalVelocity * Time.deltaTime;

        }
        switch (EnemyModes)
        {
            case EnemyMode.Normal:
                //playerとの距離が1m以内だったら
                //if (Vector2.Distance(this.transform.position, PlayerTransform.position) < 1f)
                // {
                //    EnemyModes = EnemyMode.Find;
                // }
                //視界にRPG　characterがいる
                if (EyecollisionDetector.GetIsCollided)
                {
                    EnemyModes = EnemyMode.Find;
                }
                break;
            case EnemyMode.Find:
                //Find中に何かしたいことあれば
                //なければmovemodsに移動
                EnemyModes = EnemyMode.Move;
                break;
            case EnemyMode.Move:


                //playerの位置が設定されていて、navMeshがあれば
                if (PlayerTransform != null && navMeshAgent != null)
                {
                    //ターゲットに向かって移動
                    navMeshAgent.SetDestination(PlayerTransform.position);
                    //speedに現在の加速度を追加
                    dragonAnimator.SetFloat("Speed", navMeshAgent.acceleration);
                    Debug.Log(Vector2.Distance(this.transform.position, PlayerTransform.position));

                }
                if (Vector2.Distance(this.transform.position, PlayerTransform.transform.position) < 4f)
                {
                    EnemyModes = EnemyMode.Attack;
                    break;
                }
                break;


            case EnemyMode.Attack:
                //プレイヤーに向き直す
                this.transform.LookAt(PlayerTransform.position);
                attackWaitTime -= Time.deltaTime;
                if (Vector2.Distance(this.transform.position, PlayerTransform.position) > 5f)
                {
                    EnemyModes = EnemyMode.Move;
                    break;

                }
                //攻撃の待ち時間が0を下回ったら
                if (attackWaitTime < 0)
                {
                    attackWaitTime = 1f;
                    dragonAnimator.SetTrigger("IsAttack");

                }


                break;


        }




    }
}
