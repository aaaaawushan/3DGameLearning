using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    /// <summary>
    /// CollisionDetectorのインスタンス
    /// </summary>
    public CollisionDetector collisionDetector;

    public GameCharacterParameter DragonParameter;
    public GameCharacterParameter PlayerParameter;

    private PlayerParameterPresenter playerParameterPresenter;

    private bool IsDiving = false;

    private float diveSpeed = 5f;
    /// <summary>
    /// 飛び込みの持続時間
    /// </summary>
    private float diveDuration = 1f;
    private float diveTimer = 0f;

    /// <summary>
    ///操作対象のTransform
    /// </summary>
    private Transform playerTransform;

    /// <summary>
    /// プレイヤーを前に進ませる力の値
    /// </summary>
    private float frontPower;

    /// <summary>
    /// プレイヤーを右に進む力の値
    /// </summary>
    private float rightPower;
    /// <summary>
    /// 移動スピードの係数
    /// </summary>
    private float moveSpeed = 2f;
    /// <summary>
    /// かめらのTransform
    /// </summary>



    [SerializeField]
    private Transform cameraTransform;//カメラから参考

    ///<summary>
    ///重力加速度
    ///</summary>
    private float gravity = -9.81f;

    ///<summary>
    ///y軸に対しての速度
    ///</summary>
    private float verticalVelocity = 0f;

    ///<summary>
    ///接地判定用の距離（オブジェクトの中心から計測が開始される）
    ///</summary>
    private float groundCheckDistance = 0.1f;
    ///<summary>
    ///プレイヤーが地面に設置しているかどうか
    ///</summary>
    private bool isGrounded = false;

    ///<summary>
    ///現在の移動スピード
    ///</summary>
    private float currentSpeed = 0f;

    private float currentHealth;
    private bool isDead;
    public bool CanTakeDamage = false;

    ///<summary>
    ///現在のスピードを取得するアクセス
    ///</summary>
    public float GetCurrentSpeed
    {
        get { return currentSpeed; }
    }


    /// </summary>
    // Start is called before the first frame update
    private void CheckGroundStatus()
    {
        //プレイヤーの足元から下に向かってRayを発射
        RaycastHit hit;
        if (Physics.Raycast(
            playerTransform.position,
            Vector3.down,
            out hit,
            groundCheckDistance))
        {
            isGrounded = true;//地面に接している
        }
        else
        {
            isGrounded = false;//空中にいる
        }

    }
    void Start()

    {
        //自分のTramsformを代入する
        playerTransform = this.transform;
        collisionDetector = GetComponent<CollisionDetector>();
        playerParameterPresenter = GetComponent<PlayerParameterPresenter>();

        currentHealth = playerParameterPresenter.CharacterParameter.MaxHealth;
    }

    private Vector3 CalculateMoveDirection()
    {
        //カメラの前方向と右方向を所得（y軸の回転のみ考慮する）
        Vector3 forward = cameraTransform.forward;
        Vector3 right = cameraTransform.right;

        //Y軸の回転以外の影響無視（地面をそって移動させる）
        forward.y = 0;
        right.y = 0;

        //正規化（ベクトルの長さを1にする）
        forward.Normalize();
        right.Normalize();

        //カメラの前後・左右の方向を基準に移動ベクトルを計算
        Vector3 moveDirction = forward * frontPower + right * rightPower;

        //frontPowerのみ、代入していたものを、magnitudeに移動スピードを掛けた値を更新
        currentSpeed = moveDirction.magnitude * moveSpeed;

        return moveDirction;
    }
    // Update is called once per frame

    void Update()
    {
        //デバッグモード
        if (Input.GetKeyDown(KeyCode.Alpha0))
        {
            //プレイヤーを強制的に死なせる
            playerParameterPresenter.GetDamage(1000);
        }
        if (playerParameterPresenter.GetIsDead)
        {
            return;

        }

        if (collisionDetector.GetIsCollided && CanTakeDamage)

        {
            playerParameterPresenter.GetDamage(DragonParameter.AttackPower);
            CanTakeDamage = false;


        }
        //上下キーやWSキーの入力を値に変更する（1~-1)
        frontPower = Input.GetAxis("Vertical");
        //左右キーやADキーの入力を値に変更する（1~-1)
        rightPower = Input.GetAxis("Horizontal");

        if (playerParameterPresenter.GetIsTired)
        {
            currentSpeed = 0f;
            return;
        }

        if (Input.GetKey(KeyCode.LeftShift))
        {
            moveSpeed = 4f;
        }
        else
        {
            moveSpeed = 2f;
        }


        //spaceキーが押したら飛び込み開始
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded && !IsDiving)

        {
            IsDiving = true;
            diveTimer = diveDuration;
        }
        //飛び込み中の処理
        if (IsDiving)
        {
            //飛び込み時間は現在の前方向を基準に計算
            Vector3 diveDirction = playerTransform.forward;
            playerTransform.position += diveDirction * diveSpeed * Time.deltaTime;
            diveTimer -= Time.deltaTime;

            if (diveTimer <= 0)
            {
                IsDiving = false;
            }

        }
        //Divingしてない時の移動方法
        else
        {



            //もし、frontPowerが0より大きかったら

            //上下左右キーなどで値が入力された場合、カメラの向いている方向に動く



            //足元に向けてRaycastを行い、じめんにせっしているかをチェック
            //移動方向を計算
            Vector3 moveDirection = CalculateMoveDirection();
            //足元に向けてRaycastを行い、地面に設置しているかをcheck

            CheckGroundStatus();
            //地面に設置している場合は重力を適用
            if (isGrounded)
            {
                verticalVelocity = 0f;//地面に設置してる間は重力をリセット
            }
            else
            {
                verticalVelocity += gravity * Time.deltaTime;//地面に設置している間は重力をリセット
            }
            //移動方向にY軸の重力を加える
            moveDirection.y = verticalVelocity;
            Vector3 newPosition = moveDirection * moveSpeed * Time.deltaTime;
            if (!collisionDetector.GetIsCollided)
            {
                playerTransform.position += newPosition;
            }
            else//衝突した場合は
            {
                //衝突方向のベクトルを取得
                Vector3 collisionDirection = collisionDetector.
                   GetCollisionDirection();

                //衝突方向から自分方向のベクトルを計算し、押し戻しの強さを設定する
                float pushBackStrength = 2f;//押し戻しの強度、調整
                Vector3 pushBackDirection = collisionDirection * pushBackStrength;

                //プレイヤーを押し戻し
                playerTransform.position += pushBackDirection * Time.deltaTime;



            }

            //上下左右キーなどで値が入力された場合、カメラの向いている方向に動く


            //接地していてかつ移動が発生している場合、プレイヤーの向きを移動方向に合わせる
            if (isGrounded && moveDirection.sqrMagnitude > 0.01f)//しきい値で不要な回転を防止
            {
                Quaternion targetRotation = Quaternion.LookRotation(moveDirection);
                playerTransform.rotation = Quaternion.Slerp(
                playerTransform.rotation, targetRotation, Time.deltaTime * 10f);

            }

        }

    }
}