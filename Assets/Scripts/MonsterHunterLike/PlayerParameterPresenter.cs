using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerParameterPresenter : MonoBehaviour
{
    ///<summary>
    ///Assert�t�H���_����Player�p��ScriptableObject���Q�l���ăN���X���Ŏg�p����
    /// </summary>
    public GameCharacterParameter CharacterParameter;

    ///<summary>
    ///Canvas����HPBarController���Q�l���ăN���X���Ŏg�p����
    /// </summary>
    public HPBarController HPBarController;
    ///<summary>
    ///���݂�HP
    /// </summary>
    private float currentHealth;
    ///<summary>
    ///�ő�HP
    /// </summary>
    private float maxHealth;

    /// <summary>
    /// Canvas����StaminaBarController���Q�l�ɂ��ăN���X���Ŏg�p����
    /// </summary>
    public StaminaBarController StaminaBarController;
    /// <summary>
    /// ���݂�Stamina
    /// </summary>
    private float currentStamina;
    /// <summary>
    /// Stamina�̍ő�l
    /// </summary>
    private float maxStamina;
    private float StaminaConsumeSpeed = 10f;
    private float StaminaRecoverySpeed = 10f;
    private bool isTired = false;
    private bool isDead;
    //���񂾔�����O�����珊���A�N�Z�X
    public bool GetIsDead
    {
        get
        {
            return isDead;
        }
    }
    public bool GetIsTired
    {
        get
        {
            return isTired;

        }

    }
    private bool hasConsumeStaminaForDive = false;
    private PlayerAnimatorController PlayerAnimatorController;

    private void Start()
    {
        currentHealth = CharacterParameter.MaxHealth;
        maxHealth = CharacterParameter.MaxHealth;
        HPBarController.HPBarImage.fillAmount = currentHealth / maxHealth;

        currentStamina = CharacterParameter.MaxStamina;
        maxStamina = CharacterParameter.MaxStamina;
        StaminaBarController.StaminaBar.fillAmount = currentStamina / maxStamina;

        PlayerAnimatorController = GetComponent<PlayerAnimatorController>();
    }
    private void Update()
    {
        //DriveRolling���ŃX�^�~�i����܂��s���ĂȂ��ꍇ
        if (PlayerAnimatorController.GetIsDiveRolling && !hasConsumeStaminaForDive)
        {
            ConsumeStaminaForDive();
            hasConsumeStaminaForDive = true;

        }
        //DiveRolling���I����t���O�����Z�b�g
        if (!PlayerAnimatorController.GetIsDiveRolling)
        {
            hasConsumeStaminaForDive = false;
        }


        //�u���V�t�g�v�L�[�������ꂽ��A�X�^�~�i�����炷
        if (Input.GetKey(KeyCode.LeftShift) && !isTired)
        {
            currentStamina -= Time.deltaTime * StaminaConsumeSpeed;
            StaminaBarController.StaminaBar.fillAmount = currentStamina / maxStamina;
            if (currentStamina < 0)
            {
                isTired = true;
            }
        }

        else //�u���V�t�g�v�L�[��������ĂȂ��ꍇ�A�X�^�~�i�𑝂₷
        {
            //���݂̃X�^�~�i��100��菭�Ȃ�������񕜂���
            if (currentStamina < 100)
            {
                currentStamina += Time.deltaTime * StaminaRecoverySpeed;
                StaminaBarController.StaminaBar.fillAmount = currentStamina / maxStamina;


            }
            else
            {
                if (isTired)
                {
                    isTired = false;
                }


            }



        }


        if (currentHealth < 0)
        {
            currentHealth = 0;
            return;
        }
        //�uK�v�L�[�������ꂽ��A�_���[�W���󂯂�
        if (Input.GetKeyDown(KeyCode.K))
        {
            int damage = 10;
            currentHealth -= damage;
            HPBarController.HPBarImage.fillAmount = currentHealth / maxHealth;
        }
    }



    ///<summary>
    ///�X�^�~�i������鏈��
    /// </summary>
    private void ConsumeStaminaForDive()
    {
        if (currentStamina >= 20f)
        {
            currentStamina -= 20f;
            StaminaBarController.StaminaBar.fillAmount = currentStamina / maxStamina;

        }
        else
        {
            isTired = true;
        }

    }
    public void GetDamage(float damage)
    {
        if (currentHealth >= damage)
        {
            currentHealth -= damage;
            HPBarController.HPBarImage.fillAmount = currentHealth / maxHealth;

        }
        else
        {
            currentHealth = 0;
            isDead = true;

        }

    }



}

