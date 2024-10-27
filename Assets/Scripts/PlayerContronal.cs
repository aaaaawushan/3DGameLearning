using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
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
    private float groundCheckDistance = 1f;
    ///<summary>
    ///�v���C���[���n�ʂɐݒu���Ă��邩�ǂ���
    ///</summary>
    private bool isGrounded = false;

    ///<summary>
    ///���݂̈ړ��X�s�[�h
    ///</summary>
    private float currentSpeed = 0f;

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

        //currentSpeed��z���i���s�����̒l�����j
        currentSpeed = moveDirction.z;

        return moveDirction;
    }
    // Update is called once per frame
    void Update()
    {
        //�㉺�L�[��WS�L�[�̓��͂�l�ɕύX����i1~-1)
        frontPower = Input.GetAxis("Vertical");
        //���E�L�[��AD�L�[�̓��͂�l�ɕύX����i1~-1)
        rightPower = Input.GetAxis("Horizontal");

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
        //�㉺���E�L�[�ȂǂŒl�����͂��ꂽ�ꍇ�A�J�����̌����Ă�������ɓ���
        playerTransform.position += moveDirection * Time.deltaTime;

    }
}
