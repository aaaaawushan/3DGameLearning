using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCameraController : MonoBehaviour
{
    /// <summary>
    /// シリアライズが可能となり、Unity Editorから直接参照することが可能となる
    /// </summary>
    [SerializeField]
    private Transform playerTransform;//プレイヤーのTransformを設定
    private Vector3 offset = new Vector3(0, 4, -8);//カメラの初期オフセット

    private float rotationSpeed = 2.0f;//回転のスビート
    private float minYAngle = -30f;//カメラのY軸の下限角度
    private float maxYangle = 60f;//カメラのY軸の上限角度

    private float currentX = 0f;//カメラのX軸回転
    private float currentY = 0f;//カメラのY軸回転

    // Update is called once per frame
    void Update()
    {
        //マウス/右スティック入力でカメラの回転を制御
        currentX += Input.GetAxis("Mouse X") * rotationSpeed;
        currentY += Input.GetAxis("Mouse Y") * rotationSpeed;
        //Y軸の角度を制限
        currentY = Mathf.Clamp(currentY, minYAngle, maxYangle);

    }
    /// <summary>
    /// Updateの次に実行されるメソッド
    /// </summary>
    private void LateUpdate()
    {
        //回転情報を元にカメラの位置情報を計算
        Quaternion rotation = Quaternion.Euler(currentY, currentX, 0);

        //プレイヤー位置+オフセット+回転を適用してカメラの位置を決定
        Vector3 targetPosition = playerTransform.position + rotation * offset;

        //カメラの位置と回転を適用
        transform.position = targetPosition;
        //プレイヤーの頭上を注視
        transform.LookAt(playerTransform.position + Vector3.up * 1.5f);
    }
}

       