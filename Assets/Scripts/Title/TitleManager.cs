using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class TitleManager : MonoBehaviour
{
    //�X�^�[�g�p�̃{�^��
    [SerializeField]
    private Button clickToStartButton;
    private void Start()
    {
        //�{�^���������ꂽ���̏�����ݒ肷��
        clickToStartButton.onClick.AddListener(GotoMainScene);

    }
    //scence���폜���ꂽ��A�Q�[�����I�������Ƃ��ɌĂ΂�郁�]�b�g
    private void OnDestroy()
    {
        //�{�^���������ꂽ���̏��������ׂč폜
        clickToStartButton.onClick.RemoveAllListeners();
    }
    //MonsterHunterLikeScence�ɔ�я����̃��]�b�g
    private void GotoMainScene()
    {
        SceneManager.LoadScene("MonsterHunterLike");
    }



}
