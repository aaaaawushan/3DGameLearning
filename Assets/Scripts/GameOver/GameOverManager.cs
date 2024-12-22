using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;



public class GameOverManager : MonoBehaviour
{
    //タイトルに戻す用のボタン
    [SerializeField]
    private Button clickToTitleButton;
    //結果を表示するテキスト
    [SerializeField]
    private TextMeshProUGUI resultText;
    private void Start()
    {
        //ゲームの結果によって、表示内容を変更
        if (GameOverPresenter.Instance.IsPlayerDead)
        {
            resultText.text = "You Lose";

        }
        else
        {
            resultText.text = "You Win!";
        }
        // resultText.text = string.Empty;
        //ボタンが押された時の処理を設定
        clickToTitleButton.onClick.AddListener(GotoTitleScene);
    }

    //sceneが削除されたり、ゲームが終了したときに呼ばれるメゾット
    private void OnDestroy()
    {
        //ボタンが押された時の処理をすべて削除
        clickToTitleButton.onClick.RemoveAllListeners();
    }
    private void GotoTitleScene()
    {
        SceneManager.LoadScene("Title");
    }

}


