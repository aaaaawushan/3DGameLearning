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

    ///<summary>
    ///重力加速度
    ///</summary>
    private float gravity = -9.81f;

    ///<summary>
    ///y軸に対しての速度
    ///</summary>
    private float verticalVelocity = 0f;

    ///<summary>
    ///接地判定用の距離（オブジェクトの中心から計測が開始される）
    ///</summary>
    private float groundCheckDistance = 1f;
    ///<summary>
    ///プレイヤーが地面に設置しているかどうか
    ///</summary>
    private bool isGrounded = false;

    ///<summary>
    ///現在の移動スピード
    ///</summary>
    private float currentSpeed = 0f;

    ///<summary>
    ///現在のスピードを取得するアクセス
    ///</summary>
    public float GetCurrentSpeed
    {
        get { return currentSpeed; }
    }


    /// </summary>
    // Start is called before the first frame update
    private void CheckGroundStatus()
    {
        //プレイヤーの足元から下に向かってRayを発射
        RaycastHit hit;
        if (Physics.Raycast(
            playerTransform.position,
            Vector3.down,
            out hit,
            groundCheckDistance))
        {
            isGrounded = true;//地面に接している
        }
        else
        {
            isGrounded = false;//空中にいる
        }

    }
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

        //currentSpeedにz軸（奥行方向の値を代入）
        currentSpeed = moveDirction.z;

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

        //上下左右キーなどで値が入力された場合、カメラの向いている方向に動く
       


        //足元に向けてRaycastを行い、じめんにせっしているかをチェック
        //移動方向を計算
        Vector3 moveDirection = CalculateMoveDirection();
        //足元に向けてRaycastを行い、地面に設置しているかをcheck

        CheckGroundStatus();
        //地面に設置している場合は重力を適用
        if (isGrounded)
        {
            verticalVelocity = 0f;//地面に設置してる間は重力をリセット
        }
        else
        {
            verticalVelocity += gravity * Time.deltaTime;//地面に設置している間は重力をリセット
        }
        //移動方向にY軸の重力を加える
        moveDirection.y = verticalVelocity;
        //上下左右キーなどで値が入力された場合、カメラの向いている方向に動く
        playerTransform.position += moveDirection * Time.deltaTime;

    }
}
