using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimatorController : MonoBehaviour
{
    /// <summary>
    /// プレイヤーのAnimator
    /// </summary>
    private Animator animator;

    private bool isDiveRolling = false;
    /// <summary>
    /// CurrentSpeedを所得するためにPlayControllerを参考する
    /// </summary>
    private PlayerController playerController;

    ///<summary>
    ///DiveRolling中かを外部からGetするアクセス
    ///</summary>
    public bool IsDead;
    private bool isDeadAnimationEnd = false;
    public bool GetIsDiveRolling
    {
        get
        {
            return isDiveRolling;
        }

    }
    public PlayerParameterPresenter PlayerParameterPresenter;
    /// </summary>
    // Start is called before the first frame update
    void Start()
    {
        //Animatorコンポーネントを所得
        animator = GetComponent<Animator>();
        //PlayControllerを所得
        playerController = GetComponent<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {
        if (PlayerParameterPresenter.GetIsDead && !isDeadAnimationEnd)
        {
            animator.SetTrigger("IsDead");
            isDeadAnimationEnd = true;
        }
        //アニメーターに速度を渡す（たとえば、走るアニメを速度で調整）
        animator.SetFloat("Speed", playerController.GetCurrentSpeed);


        if (Input.GetMouseButtonDown(0))
        {
            animator.SetTrigger("IsAttack");
        }


        //DiveRolling中のフラグ管理
        isDiveRolling =
            animator.GetCurrentAnimatorStateInfo(0).IsName("DiveRolling") && !animator.IsInTransition(0);




        //状態遷移中かDiveRolling中はここからの処理はしない
        if (animator.IsInTransition(0) || animator.GetCurrentAnimatorStateInfo(0).IsName("DiveRolling"))
        {
            return;
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            animator.SetTrigger("IsDiveRolling");

        }
    }
}






