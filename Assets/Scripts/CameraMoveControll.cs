using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMoveControll : MonoBehaviour
{
    //Tips
    //「;」は日本語の文章における「。」となります。
    //「.」は日本語の文章における「の」などの接続詞となります。
    //「｛｝」は日本語の文章における一段落となります。
    //（f）はファンクションという意味となります。
    //「Hogehoge（）」はHogehogeという処理を行うという意味となります。
    //「＝」は代入です。「＝＝」は数学的にイコールと同じ意味となります。
    //「+,-,/,*」加算、減算、乗算、賭け算はそのままの意味です。「%」余剰を使うこともあります。
    //変数は数学でいうところの「ｘ」とかです。中身を変更できる値。
    //プログラミングの変数はプログラミングで扱うすべての型を扱う。
    //型を変数として宣言する。

    /// <summary>
    /// カメラの位置、回転、拡縮が格納されたTransfrom型の変数
    /// ＜/summary>
    private Transform cameraTransform;

    // Start is called before the first frame update
    void Start()
    {
        //宣言した変数にこのコンポーネントが追加されたGameObjectのTransformを代入する
        cameraTransform=this.transform;
    }

    // Update is called once per frame
    void Update()
    {
        //カメラの位置情報に、右方向のベクトルにフレーム時間に掛けた値を加算し続ける
        //deltaTime=CPUが処理するフレームのつぎのフレームまでの時間
        //フレームとは一秒間に何回描画が更新される値。
        //60fpsのゲームの場合は一秒間に60回、処理、描画が更新される。
        //frame per secondの略。
        //60fpsのTime.deltaTimeは現実時間で0.0166秒という値
        //30fpsのTime.deltaTimeは現実時間で0.0333秒という値
        cameraTransform.position+=Vector3.right*Time.deltaTime;
        
    }
}
