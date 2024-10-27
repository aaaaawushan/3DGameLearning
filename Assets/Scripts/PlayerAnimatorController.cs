using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnmatorController : MonoBehaviour
{
    /// <summary>
    /// �v���C���[��Animator
    /// </summary>
    private Animator animator;
    /// <summary>
    /// CurrentSpeed���������邽�߂�PlayController���Q�l����
    /// </summary>
    private PlayerController playerController;
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
        //�A�j���[�^�[�ɑ��x��n���i���Ƃ��΁A����A�j���𑬓x�Œ����j
        animator.SetFloat("Speed", playerController.GetCurrentSpeed);


        if (Input.GetMouseButtonDown(0))
        {
            animator.SetTrigger("IsAttack");
        }
    }
}






