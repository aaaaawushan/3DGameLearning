using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameJudge : MonoBehaviour
{
    //プレイヤーのパラメーターをつかさどっているコンポーネント
    [SerializeField]
    private PlayerParameterPresenter PlayerParameterPresenter;
    //敵のパラメーターをつかさどっているコンポーネント
    [SerializeField]
    private EnemyController EnemyController;
    //シーンをよみこみちゅうかのフラグ
    private bool isLoadingScene = false;

    private void Update()
    {
        //プレイヤーか、エネミーのどちらかが死んだ場合
        if (PlayerParameterPresenter.GetIsDead || EnemyController.GetIsDead)
        {
            //シーン読み込み中じゃなかったら
            if (!isLoadingScene)
            {

                //GameOverPresenterにアクセスして、プレイヤーが死んだかどうかのフラグを代入
                GameOverPresenter.Instance.IsPlayerDead = PlayerParameterPresenter.GetIsDead;
                //Gameoverシーンを読み込み
                SceneManager.LoadScene("GameOver");

                isLoadingScene = true;
            }
        }
    }
}
