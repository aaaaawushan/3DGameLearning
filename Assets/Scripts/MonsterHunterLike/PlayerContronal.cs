using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    /// <summary>
    /// CollisionDetector�̃C���X�^���X
    /// </summary>
    public CollisionDetector collisionDetector;

    public GameCharacterParameter DragonParameter;
    public GameCharacterParameter PlayerParameter;

    private PlayerParameterPresenter playerParameterPresenter;

    private bool IsDiving = false;

    private float diveSpeed = 5f;
    /// <summary>
    /// ��э��݂̎�������
    /// </summary>
    private float diveDuration = 1f;
    private float diveTimer = 0f;

    /// <summary>
    ///����Ώۂ�Transform
    /// </summary>
    private Transform playerTransform;

    /// <summary>
    /// �v���C���[��O�ɐi�܂���͂̒l
    /// </summary>
    private float frontPower;

    /// <summary>
    /// �v���C���[���E�ɐi�ޗ͂̒l
    /// </summary>
    private float rightPower;
    /// <summary>
    /// �ړ��X�s�[�h�̌W��
    /// </summary>
    private float moveSpeed = 2f;
    /// <summary>
    /// ���߂��Transform
    /// </summary>



    [SerializeField]
    private Transform cameraTransform;//�J��������Q�l

    ///<summary>
    ///�d�͉����x
    ///</summary>
    private float gravity = -9.81f;

    ///<summary>
    ///y���ɑ΂��Ă̑��x
    ///</summary>
    private float verticalVelocity = 0f;

    ///<summary>
    ///�ڒn����p�̋����i�I�u�W�F�N�g�̒��S����v�����J�n�����j
    ///</summary>
    private float groundCheckDistance = 0.1f;
    ///<summary>
    ///�v���C���[���n�ʂɐݒu���Ă��邩�ǂ���
    ///</summary>
    private bool isGrounded = false;

    ///<summary>
    ///���݂̈ړ��X�s�[�h
    ///</summary>
    private float currentSpeed = 0f;

    private float currentHealth;
    private bool isDead;
    public bool CanTakeDamage = false;

    ///<summary>
    ///���݂̃X�s�[�h���擾����A�N�Z�X
    ///</summary>
    public float GetCurrentSpeed
    {
        get { return currentSpeed; }
    }


    /// </summary>
    // Start is called before the first frame update
    private void CheckGroundStatus()
    {
        //�v���C���[�̑������牺�Ɍ�������Ray�𔭎�
        RaycastHit hit;
        if (Physics.Raycast(
            playerTransform.position,
            Vector3.down,
            out hit,
            groundCheckDistance))
        {
            isGrounded = true;//�n�ʂɐڂ��Ă���
        }
        else
        {
            isGrounded = false;//�󒆂ɂ���
        }

    }
    void Start()

    {
        //������Tramsform��������
        playerTransform = this.transform;
        collisionDetector = GetComponent<CollisionDetector>();
        playerParameterPresenter = GetComponent<PlayerParameterPresenter>();

        currentHealth = playerParameterPresenter.CharacterParameter.MaxHealth;
    }

    private Vector3 CalculateMoveDirection()
    {
        //�J�����̑O�����ƉE�����������iy���̉�]�̂ݍl������j
        Vector3 forward = cameraTransform.forward;
        Vector3 right = cameraTransform.right;

        //Y���̉�]�ȊO�̉e�������i�n�ʂ������Ĉړ�������j
        forward.y = 0;
        right.y = 0;

        //���K���i�x�N�g���̒�����1�ɂ���j
        forward.Normalize();
        right.Normalize();

        //�J�����̑O��E���E�̕�������Ɉړ��x�N�g�����v�Z
        Vector3 moveDirction = forward * frontPower + right * rightPower;

        //frontPower�̂݁A������Ă������̂��Amagnitude�Ɉړ��X�s�[�h���|�����l���X�V
        currentSpeed = moveDirction.magnitude * moveSpeed;

        return moveDirction;
    }
    // Update is called once per frame

    void Update()
    {
        //�f�o�b�O���[�h
        if (Input.GetKeyDown(KeyCode.Alpha0))
        {
            //�v���C���[�������I�Ɏ��Ȃ���
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
        //�㉺�L�[��WS�L�[�̓��͂�l�ɕύX����i1~-1)
        frontPower = Input.GetAxis("Vertical");
        //���E�L�[��AD�L�[�̓��͂�l�ɕύX����i1~-1)
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


        //space�L�[�����������э��݊J�n
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded && !IsDiving)

        {
            IsDiving = true;
            diveTimer = diveDuration;
        }
        //��э��ݒ��̏���
        if (IsDiving)
        {
            //��э��ݎ��Ԃ͌��݂̑O��������Ɍv�Z
            Vector3 diveDirction = playerTransform.forward;
            playerTransform.position += diveDirction * diveSpeed * Time.deltaTime;
            diveTimer -= Time.deltaTime;

            if (diveTimer <= 0)
            {
                IsDiving = false;
            }

        }
        //Diving���ĂȂ����̈ړ����@
        else
        {



            //�����AfrontPower��0���傫��������

            //�㉺���E�L�[�ȂǂŒl�����͂��ꂽ�ꍇ�A�J�����̌����Ă�������ɓ���



            //�����Ɍ�����Raycast���s���A���߂�ɂ������Ă��邩���`�F�b�N
            //�ړ��������v�Z
            Vector3 moveDirection = CalculateMoveDirection();
            //�����Ɍ�����Raycast���s���A�n�ʂɐݒu���Ă��邩��check

            CheckGroundStatus();
            //�n�ʂɐݒu���Ă���ꍇ�͏d�͂�K�p
            if (isGrounded)
            {
                verticalVelocity = 0f;//�n�ʂɐݒu���Ă�Ԃ͏d�͂����Z�b�g
            }
            else
            {
                verticalVelocity += gravity * Time.deltaTime;//�n�ʂɐݒu���Ă���Ԃ͏d�͂����Z�b�g
            }
            //�ړ�������Y���̏d�͂�������
            moveDirection.y = verticalVelocity;
            Vector3 newPosition = moveDirection * moveSpeed * Time.deltaTime;
            if (!collisionDetector.GetIsCollided)
            {
                playerTransform.position += newPosition;
            }
            else//�Փ˂����ꍇ��
            {
                //�Փ˕����̃x�N�g�����擾
                Vector3 collisionDirection = collisionDetector.
                   GetCollisionDirection();

                //�Փ˕������玩�������̃x�N�g�����v�Z���A�����߂��̋�����ݒ肷��
                float pushBackStrength = 2f;//�����߂��̋��x�A����
                Vector3 pushBackDirection = collisionDirection * pushBackStrength;

                //�v���C���[�������߂�
                playerTransform.position += pushBackDirection * Time.deltaTime;



            }

            //�㉺���E�L�[�ȂǂŒl�����͂��ꂽ�ꍇ�A�J�����̌����Ă�������ɓ���


            //�ڒn���Ă��Ă��ړ����������Ă���ꍇ�A�v���C���[�̌������ړ������ɍ��킹��
            if (isGrounded && moveDirection.sqrMagnitude > 0.01f)//�������l�ŕs�v�ȉ�]��h�~
            {
                Quaternion targetRotation = Quaternion.LookRotation(moveDirection);
                playerTransform.rotation = Quaternion.Slerp(
                playerTransform.rotation, targetRotation, Time.deltaTime * 10f);

            }

        }

    }
}