using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;



public class GameOverManager : MonoBehaviour
{
    //�^�C�g���ɖ߂��p�̃{�^��
    [SerializeField]
    private Button clickToTitleButton;
    //���ʂ�\������e�L�X�g
    [SerializeField]
    private TextMeshProUGUI resultText;
    private void Start()
    {
        //�Q�[���̌��ʂɂ���āA�\�����e��ύX
        if (GameOverPresenter.Instance.IsPlayerDead)
        {
            resultText.text = "You Lose";

        }
        else
        {
            resultText.text = "You Win!";
        }
        // resultText.text = string.Empty;
        //�{�^���������ꂽ���̏�����ݒ�
        clickToTitleButton.onClick.AddListener(GotoTitleScene);
    }

    //scene���폜���ꂽ��A�Q�[�����I�������Ƃ��ɌĂ΂�郁�]�b�g
    private void OnDestroy()
    {
        //�{�^���������ꂽ���̏��������ׂč폜
        clickToTitleButton.onClick.RemoveAllListeners();
    }
    private void GotoTitleScene()
    {
        SceneManager.LoadScene("Title");
    }

}


