using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// �V�[�����܂����Ńv���C��[�����񂾂��ǂ����`����N���X
/// �V�[�����܂����`�����A�Q�[�����Ɉ�����Ȃ��Ƃ�������𕉂�
/// </summary>
public class GameOverPresenter : MonoBehaviour
{/// <summary>
 ///static�����鎖�Ńv���O�������̂ǂ�����ł��A�N�Z�X�ł���悤�ɂ���
 ///���̃N���X���w��������̂Ƃ��Ĉ�ʓI��Instance�ƌĂ΂��
 ///<summary>
    public static GameOverPresenter Instance;
    /// <summary>
    ///�v���C���[�����񂾂��ǂ���
    ///<summary>
    public bool IsPlayerDead = false;
    /// <summary>
    /// �Q�[�������s���鎞�Ɉ�x�����AStart�̑O�ɌĂяo�����
    ///  <summary>
    void Awake()
    {
        //gameoverpresenter�ł���Instance�ɃA�N�Z�X��������
        //�Q�[�����ɐ��܂�Ă��Ȃ��Ƃ݂Ȃ��ꂽ�ꍇ��
        if (Instance == null)
        {
            //Instance�͎���������
            Instance = this;
            //�V�[���J�ڂł��p�����Ȃ�
            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            //���x���V�[�����ǂݍ��܂ꂽ�ꍇ�A�d��������p��
            Destroy(this.gameObject);
        }


    }
}

