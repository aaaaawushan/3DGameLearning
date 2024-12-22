using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimatorController : MonoBehaviour
{
    /// <summary>
    /// �v���C���[��Animator
    /// </summary>
    private Animator animator;

    private bool isDiveRolling = false;
    /// <summary>
    /// CurrentSpeed���������邽�߂�PlayController���Q�l����
    /// </summary>
    private PlayerController playerController;

    ///<summary>
    ///DiveRolling�������O������Get����A�N�Z�X
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
        //Animator�R���|�[�l���g������
        animator = GetComponent<Animator>();
        //PlayController������
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
        //�A�j���[�^�[�ɑ��x��n���i���Ƃ��΁A����A�j���𑬓x�Œ����j
        animator.SetFloat("Speed", playerController.GetCurrentSpeed);


        if (Input.GetMouseButtonDown(0))
        {
            animator.SetTrigger("IsAttack");
        }


        //DiveRolling���̃t���O�Ǘ�
        isDiveRolling =
            animator.GetCurrentAnimatorStateInfo(0).IsName("DiveRolling") && !animator.IsInTransition(0);




        //��ԑJ�ڒ���DiveRolling���͂�������̏����͂��Ȃ�
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






