using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionDetector : MonoBehaviour
{
    /// <summary>
    /// 相手のCollider
    /// </summary>
    public Collider[] OtherColliders;


    /// <summary>
    /// 当たってる相手のCollider(複数対応）
    /// </summary>

    private List<Collider> collidedWith = new List<Collider>();
    /// <summary>
    /// 自分のCollider
    /// </summary>
    private Collider myCollider;

    /// <summary>
    /// 当たり判定のフラグ
    /// </summary>


    // Start is called before the first frame update
    /// <summary>
    /// 当たっているかのフラグ
    /// </summary>
    private bool isCollided = false;
    /// <summary>
    /// 当たってるかのフラグを外部からつかうためのアクセス
    /// </summary>
    public bool GetIsCollided
    {
        get { return isCollided; }
    }

    /// <summary>
    /// 当たっているColliderのリストを所得
    /// </summary>
    public List<Collider> CollidedWith
    {

        get { return collidedWith; }
    }
    private void Start()
    {
        myCollider = GetComponentInChildren<Collider>();
    }
    void Update()
    {
        //何も指定されていない場合
        if (myCollider == null || OtherColliders == null || OtherColliders.Length == 0)
        {
            isCollided = false;
            collidedWith.Clear();
            return;
        }

        //当たっているかをはんてい
        isCollided = false;
        collidedWith.Clear();

        //for分を使って、OtherCollidersで指定した回数分
        //処理を繰り返す
        for (int i = 0; i < OtherColliders.Length; i++)
        {
            if (OtherColliders[i] != null && myCollider.bounds.Intersects(OtherColliders[i].bounds))
            {
                isCollided = true;
                collidedWith.Add(OtherColliders[i]);
                Debug.Log($"衝突中:{OtherColliders[i].name}");
            }
        }

    }


    /// <summary>
    /// 外部からisCollidedの値を取得できるアクセサ
    /// </summary>
    public bool GetCollided()
    {
        return isCollided;
    }
    ///<summary>
    ///衝突の方向を所得する
    /// </summary>
    /// 衝突相手から自分への方向ベクトル</returns>
    public Vector3 GetCollisionDirection()
    {
        if (isCollided && collidedWith.Count > 0)
        {
            //最初に衝突した相手を基準に方向を計算
            Vector3 direction = (transform.position - collidedWith[0].transform.position).normalized;
            direction.y = 0;
            return direction;
        }
        return Vector3.zero;//衝突していない場所は方向なし
    }
}



