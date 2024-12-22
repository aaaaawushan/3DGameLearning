using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// シーンをまたいでプレイやーが死んだかどうか伝えるクラス
/// シーンをまたぐ形だが、ゲーム中に一つしかないという縛りを負う
/// </summary>
public class GameOverPresenter : MonoBehaviour
{/// <summary>
 ///staticをつける事でプログラム内のどこからでもアクセスできるようにする
 ///そのクラスを指示するものとして一般的にInstanceと呼ばれる
 ///<summary>
    public static GameOverPresenter Instance;
    /// <summary>
    ///プレイヤーが死んだかどうか
    ///<summary>
    public bool IsPlayerDead = false;
    /// <summary>
    /// ゲームを実行する時に一度だけ、Startの前に呼び出される
    ///  <summary>
    void Awake()
    {
        //gameoverpresenterであるInstanceにアクセスした時に
        //ゲーム内に生まれていないとみなされた場合は
        if (Instance == null)
        {
            //Instanceは自分を代入し
            Instance = this;
            //シーン遷移でも廃棄しない
            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            //何度もシーンが読み込まれた場合、重複したら廃棄
            Destroy(this.gameObject);
        }


    }
}

