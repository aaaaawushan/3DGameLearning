using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionDetector : MonoBehaviour
{
    /// <summary>
    /// �����Collider
    /// </summary>
    public Collider[] OtherColliders;


    /// <summary>
    /// �������Ă鑊���Collider(�����Ή��j
    /// </summary>

    private List<Collider> collidedWith = new List<Collider>();
    /// <summary>
    /// ������Collider
    /// </summary>
    private Collider myCollider;

    /// <summary>
    /// �����蔻��̃t���O
    /// </summary>


    // Start is called before the first frame update
    /// <summary>
    /// �������Ă��邩�̃t���O
    /// </summary>
    private bool isCollided = false;
    /// <summary>
    /// �������Ă邩�̃t���O���O������������߂̃A�N�Z�X
    /// </summary>
    public bool GetIsCollided
    {
        get { return isCollided; }
    }

    /// <summary>
    /// �������Ă���Collider�̃��X�g������
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
        //�����w�肳��Ă��Ȃ��ꍇ
        if (myCollider == null || OtherColliders == null || OtherColliders.Length == 0)
        {
            isCollided = false;
            collidedWith.Clear();
            return;
        }

        //�������Ă��邩���͂�Ă�
        isCollided = false;
        collidedWith.Clear();

        //for�����g���āAOtherColliders�Ŏw�肵���񐔕�
        //�������J��Ԃ�
        for (int i = 0; i < OtherColliders.Length; i++)
        {
            if (OtherColliders[i] != null && myCollider.bounds.Intersects(OtherColliders[i].bounds))
            {
                isCollided = true;
                collidedWith.Add(OtherColliders[i]);
                Debug.Log($"�Փ˒�:{OtherColliders[i].name}");
            }
        }

    }


    /// <summary>
    /// �O������isCollided�̒l���擾�ł���A�N�Z�T
    /// </summary>
    public bool GetCollided()
    {
        return isCollided;
    }
    ///<summary>
    ///�Փ˂̕�������������
    /// </summary>
    /// �Փˑ��肩�玩���ւ̕����x�N�g��</returns>
    public Vector3 GetCollisionDirection()
    {
        if (isCollided && collidedWith.Count > 0)
        {
            //�ŏ��ɏՓ˂����������ɕ������v�Z
            Vector3 direction = (transform.position - collidedWith[0].transform.position).normalized;
            direction.y = 0;
            return direction;
        }
        return Vector3.zero;//�Փ˂��Ă��Ȃ��ꏊ�͕����Ȃ�
    }
}



