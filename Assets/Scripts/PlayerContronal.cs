using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    /// <summary>
    ///操作対象のTransform
    /// </summary>
    private Transform playerTransform;

    /// <summary>
    /// プレイヤーを前に進ませる力の値
    /// </summary>
    private float frontPower;

    /// <summary>
    /// プレイヤーを右に進む力の値
    /// </summary>
    private float rightPower;
    /// <summary>
    /// 移動スピードの係数
    /// </summary>
    private float moveSpeed = 2f;
    /// <summary>
    /// かめらのTransform
    /// </summary>
    [SerializeField]
    private Transform cameraTransform;//カメラから参考

    /// </summary>
    // Start is called before the first frame update
    void Start()
    {
        //自分のTramsformを代入する
        playerTransform = this.transform;
    }
    private Vector3 CalculateMoveDirection()
    {
        //カメラの前方向と右方向を所得（y軸の回転のみ考慮する）
        Vector3 forward = cameraTransform.forward;
        Vector3 right = cameraTransform.right;

        //Y軸の回転以外の影響無視（地面をそって移動させる）
        forward.y = 0;
        right.y = 0;

        //正規化（ベクトルの長さを1にする）
        forward.Normalize();
        right.Normalize();

        //カメラの前後・左右の方向を基準に移動ベクトルを計算
        Vector3 moveDirction = forward * frontPower + right * rightPower;

        return moveDirction;
    }
    // Update is called once per frame
    void Update()
    {
        //上下キーやWSキーの入力を値に変更する（1~-1)
        frontPower = Input.GetAxis("Vertical");
        //左右キーやADキーの入力を値に変更する（1~-1)
        rightPower = Input.GetAxis("Horizontal");

        //もし、frontPowerが0より大きかったら
        if (frontPower > 0)
        {
            //z軸の方向に一秒に一メートル進む値を加算する
            playerTransform.position += CalculateMoveDirection() * moveSpeed * Time.deltaTime;
        }
        if (frontPower < 0)
        {
            //z軸の方向に一秒にマイナス一メートル進む値を加算する
            playerTransform.position += CalculateMoveDirection() * moveSpeed * Time.deltaTime;
        }
        if (rightPower > 0)
        {
            //x軸の方向に一秒に一メートル進む値を加算する
            playerTransform.position += CalculateMoveDirection() * moveSpeed * Time.deltaTime;
        }
        if (rightPower < 0)
        {
            //x軸の方向に一秒にマイナス一メートル進む値を加算する
            playerTransform.position += CalculateMoveDirection() * moveSpeed * Time.deltaTime;
        }


    }
}
