using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMoveControll : MonoBehaviour
{
    //Tips
    //�u;�v�͓��{��̕��͂ɂ�����u�B�v�ƂȂ�܂��B
    //�u.�v�͓��{��̕��͂ɂ�����u�́v�Ȃǂ̐ڑ����ƂȂ�܂��B
    //�u�o�p�v�͓��{��̕��͂ɂ������i���ƂȂ�܂��B
    //�if�j�̓t�@���N�V�����Ƃ����Ӗ��ƂȂ�܂��B
    //�uHogehoge�i�j�v��Hogehoge�Ƃ����������s���Ƃ����Ӗ��ƂȂ�܂��B
    //�u���v�͑���ł��B�u�����v�͐��w�I�ɃC�R�[���Ɠ����Ӗ��ƂȂ�܂��B
    //�u+,-,/,*�v���Z�A���Z�A��Z�A�q���Z�͂��̂܂܂̈Ӗ��ł��B�u%�v�]����g�����Ƃ�����܂��B
    //�ϐ��͐��w�ł����Ƃ���́u���v�Ƃ��ł��B���g��ύX�ł���l�B
    //�v���O���~���O�̕ϐ��̓v���O���~���O�ň������ׂĂ̌^�������B
    //�^��ϐ��Ƃ��Đ錾����B

    /// <summary>
    /// �J�����̈ʒu�A��]�A�g�k���i�[���ꂽTransfrom�^�̕ϐ�
    /// ��/summary>
    private Transform cameraTransform;

    // Start is called before the first frame update
    void Start()
    {
        //�錾�����ϐ��ɂ��̃R���|�[�l���g���ǉ����ꂽGameObject��Transform��������
        cameraTransform=this.transform;
    }

    // Update is called once per frame
    void Update()
    {
        //�J�����̈ʒu���ɁA�E�����̃x�N�g���Ƀt���[�����ԂɊ|�����l�����Z��������
        //deltaTime=CPU����������t���[���̂��̃t���[���܂ł̎���
        //�t���[���Ƃ͈�b�Ԃɉ���`�悪�X�V�����l�B
        //60fps�̃Q�[���̏ꍇ�͈�b�Ԃ�60��A�����A�`�悪�X�V�����B
        //frame per second�̗��B
        //60fps��Time.deltaTime�͌������Ԃ�0.0166�b�Ƃ����l
        //30fps��Time.deltaTime�͌������Ԃ�0.0333�b�Ƃ����l
        cameraTransform.position+=Vector3.right*Time.deltaTime;
        
    }
}
