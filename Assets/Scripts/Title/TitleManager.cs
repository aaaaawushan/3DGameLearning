using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class TitleManager : MonoBehaviour
{
    //スタート用のボタン
    [SerializeField]
    private Button clickToStartButton;
    private void Start()
    {
        //ボタンが押された時の処理を設定する
        clickToStartButton.onClick.AddListener(GotoMainScene);

    }
    //scenceが削除されたり、ゲームが終了したときに呼ばれるメゾット
    private void OnDestroy()
    {
        //ボタンが押された時の処理をすべて削除
        clickToStartButton.onClick.RemoveAllListeners();
    }
    //MonsterHunterLikeScenceに飛び処理のメゾット
    private void GotoMainScene()
    {
        SceneManager.LoadScene("MonsterHunterLike");
    }



}
