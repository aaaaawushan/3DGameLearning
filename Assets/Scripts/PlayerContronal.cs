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

    /// </summary>
    // Start is called before the first frame update
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
        if (frontPower > 0)
        {
            //z���̕����Ɉ�b�Ɉꃁ�[�g���i�ޒl�����Z����
            playerTransform.position += CalculateMoveDirection() * moveSpeed * Time.deltaTime;
        }
        if (frontPower < 0)
        {
            //z���̕����Ɉ�b�Ƀ}�C�i�X�ꃁ�[�g���i�ޒl�����Z����
            playerTransform.position += CalculateMoveDirection() * moveSpeed * Time.deltaTime;
        }
        if (rightPower > 0)
        {
            //x���̕����Ɉ�b�Ɉꃁ�[�g���i�ޒl�����Z����
            playerTransform.position += CalculateMoveDirection() * moveSpeed * Time.deltaTime;
        }
        if (rightPower < 0)
        {
            //x���̕����Ɉ�b�Ƀ}�C�i�X�ꃁ�[�g���i�ޒl�����Z����
            playerTransform.position += CalculateMoveDirection() * moveSpeed * Time.deltaTime;
        }


    }
}
