using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnmatorController : MonoBehaviour
{
    /// <summary>
    /// プレイヤーのAnimator
    /// </summary>
    private Animator animator;
    /// <summary>
    /// CurrentSpeedを所得するためにPlayControllerを参考する
    /// </summary>
    private PlayerController playerController;
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
        //アニメーターに速度を渡す（たとえば、走るアニメを速度で調整）
        animator.SetFloat("Speed", playerController.GetCurrentSpeed);


        if (Input.GetMouseButtonDown(0))
        {
            animator.SetTrigger("IsAttack");
        }
    }
}






