using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCameraController : MonoBehaviour
{
    /// <summary>
    /// �V���A���C�Y���\�ƂȂ�AUnity Editor���璼�ڎQ�Ƃ��邱�Ƃ��\�ƂȂ�
    /// </summary>
    [SerializeField]
    private Transform playerTransform;//�v���C���[��Transform��ݒ�
    private Vector3 offset = new Vector3(0, 4, -8);//�J�����̏����I�t�Z�b�g

    private float rotationSpeed = 2.0f;//��]�̃X�r�[�g
    private float minYAngle = -30f;//�J������Y���̉����p�x
    private float maxYangle = 60f;//�J������Y���̏���p�x

    private float currentX = 0f;//�J������X����]
    private float currentY = 0f;//�J������Y����]

    // Update is called once per frame
    void Update()
    {
        //�}�E�X/�E�X�e�B�b�N���͂ŃJ�����̉�]�𐧌�
        currentX += Input.GetAxis("Mouse X") * rotationSpeed;
        currentY += Input.GetAxis("Mouse Y") * rotationSpeed;
        //Y���̊p�x�𐧌�
        currentY = Mathf.Clamp(currentY, minYAngle, maxYangle);

    }
    /// <summary>
    /// Update�̎��Ɏ��s����郁�\�b�h
    /// </summary>
    private void LateUpdate()
    {
        //��]�������ɃJ�����̈ʒu�����v�Z
        Quaternion rotation = Quaternion.Euler(currentY, currentX, 0);

        //�v���C���[�ʒu+�I�t�Z�b�g+��]��K�p���ăJ�����̈ʒu������
        Vector3 targetPosition = playerTransform.position + rotation * offset;

        //�J�����̈ʒu�Ɖ�]��K�p
        transform.position = targetPosition;
        //�v���C���[�̓���𒍎�
        transform.LookAt(playerTransform.position + Vector3.up * 1.5f);
    }
}

       