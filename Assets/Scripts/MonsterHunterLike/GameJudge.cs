using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameJudge : MonoBehaviour
{
    //�v���C���[�̃p�����[�^�[�������ǂ��Ă���R���|�[�l���g
    [SerializeField]
    private PlayerParameterPresenter PlayerParameterPresenter;
    //�G�̃p�����[�^�[�������ǂ��Ă���R���|�[�l���g
    [SerializeField]
    private EnemyController EnemyController;
    //�V�[������݂��݂��イ���̃t���O
    private bool isLoadingScene = false;

    private void Update()
    {
        //�v���C���[���A�G�l�~�[�̂ǂ��炩�����񂾏ꍇ
        if (PlayerParameterPresenter.GetIsDead || EnemyController.GetIsDead)
        {
            //�V�[���ǂݍ��ݒ�����Ȃ�������
            if (!isLoadingScene)
            {

                //GameOverPresenter�ɃA�N�Z�X���āA�v���C���[�����񂾂��ǂ����̃t���O����
                GameOverPresenter.Instance.IsPlayerDead = PlayerParameterPresenter.GetIsDead;
                //Gameover�V�[����ǂݍ���
                SceneManager.LoadScene("GameOver");

                isLoadingScene = true;
            }
        }
    }
}
