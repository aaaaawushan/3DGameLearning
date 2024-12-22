using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyController : MonoBehaviour
{
    ///<summary>
    ///�����蔻��p�R���|�[�l���g
    /// </summary>
    private CollisionDetector dragonCollisionDetector;

    ///<summary>
    ///�h���S���̃p�����[�^�[
    /// </summary>
    public GameCharacterParameter DragonParameter;

    ///<summary>
    ///�v���C���[�̃p�����[�^�[
    /// </summary>
    public GameCharacterParameter PlayerParameter;

    ///<summary>
    ///����̗̑�
    /// </summary>
    private float currentHealth;

    ///<summary>
    ///���񂾔���
    /// </summary>
    private bool isDead;
    //���S������O����̃Q�b�g���邽�߂̃A�N�Z�X
    public bool GetIsDead
    {
        get { return isDead; }
    }
    /// <summary>
    /// �_���[�W����
    /// </summary>
    public bool CanTakeDamage = false;
    /// <summary>
    /// �h���S����animator
    /// </summary>
    private Animator dragonAnimator;
    private float groundCheckDistance = 0.1f;

    private float gravity = -9.81f;
    private float verticalVelocity = 0f;
    public bool isGrounded = false;
    private NavMeshAgent navMeshAgent;
    //eye�ɔz�u����Ă���CollisionDetector
    public CollisionDetector EyecollisionDetector;
    //enemey�̊������


    public enum EnemyMode
    {
        Invalid = -1,
        Normal,
        Attack,
        Find,
        Move,

    }
    /// <summary>
    /// ENEMY�̊�����Ԃ�������
    /// </summary>
    public EnemyMode EnemyModes = EnemyMode.Normal;
    /// <summary>
    /// �v���C���[�̈ʒu
    /// </summary>
    public Transform PlayerTransform;
    /// <summary>
    /// �U���p�̑҂�����
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
        //�����蔻�肪�������Ƃ��ACanTakeDamage��true�������ꍇ
        if (dragonCollisionDetector.GetIsCollided && CanTakeDamage)
        {
            //float��0.0001�������ꍇ�A������ʂ��Ă���Ȃ�
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
        //�n�ʂɐݒu���Ă��Ȃ��ꍇ�͏d�͓K�p
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
            //��������
            this.transform.position = pos;
        }
        if (navMeshAgent != null)
        {
            //�ڒn�n�_�̒������s��
            navMeshAgent.baseOffset += verticalVelocity * Time.deltaTime;

        }
        switch (EnemyModes)
        {
            case EnemyMode.Normal:
                //player�Ƃ̋�����1m�ȓ���������
                //if (Vector2.Distance(this.transform.position, PlayerTransform.position) < 1f)
                // {
                //    EnemyModes = EnemyMode.Find;
                // }
                //���E��RPG�@character������
                if (EyecollisionDetector.GetIsCollided)
                {
                    EnemyModes = EnemyMode.Find;
                }
                break;
            case EnemyMode.Find:
                //Find���ɉ������������Ƃ����
                //�Ȃ����movemods�Ɉړ�
                EnemyModes = EnemyMode.Move;
                break;
            case EnemyMode.Move:


                //player�̈ʒu���ݒ肳��Ă��āAnavMesh�������
                if (PlayerTransform != null && navMeshAgent != null)
                {
                    //�^�[�Q�b�g�Ɍ������Ĉړ�
                    navMeshAgent.SetDestination(PlayerTransform.position);
                    //speed�Ɍ��݂̉����x��ǉ�
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
                //�v���C���[�Ɍ�������
                this.transform.LookAt(PlayerTransform.position);
                attackWaitTime -= Time.deltaTime;
                if (Vector2.Distance(this.transform.position, PlayerTransform.position) > 5f)
                {
                    EnemyModes = EnemyMode.Move;
                    break;

                }
                //�U���̑҂����Ԃ�0�����������
                if (attackWaitTime < 0)
                {
                    attackWaitTime = 1f;
                    dragonAnimator.SetTrigger("IsAttack");

                }


                break;


        }




    }
}
