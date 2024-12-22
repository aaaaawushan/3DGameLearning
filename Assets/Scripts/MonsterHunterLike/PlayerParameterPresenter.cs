using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerParameterPresenter : MonoBehaviour
{
    ///<summary>
    ///AssertフォルダからPlayer用のScriptableObjectを参考してクラス内で使用する
    /// </summary>
    public GameCharacterParameter CharacterParameter;

    ///<summary>
    ///CanvasからHPBarControllerを参考してクラス内で使用する
    /// </summary>
    public HPBarController HPBarController;
    ///<summary>
    ///現在のHP
    /// </summary>
    private float currentHealth;
    ///<summary>
    ///最大HP
    /// </summary>
    private float maxHealth;

    /// <summary>
    /// CanvasからStaminaBarControllerを参考にしてクラス内で使用する
    /// </summary>
    public StaminaBarController StaminaBarController;
    /// <summary>
    /// 現在のStamina
    /// </summary>
    private float currentStamina;
    /// <summary>
    /// Staminaの最大値
    /// </summary>
    private float maxStamina;
    private float StaminaConsumeSpeed = 10f;
    private float StaminaRecoverySpeed = 10f;
    private bool isTired = false;
    private bool isDead;
    //死んだ判定を外部から所得アクセス
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
        //DriveRolling中でスタミナ消費まだ行われてない場合
        if (PlayerAnimatorController.GetIsDiveRolling && !hasConsumeStaminaForDive)
        {
            ConsumeStaminaForDive();
            hasConsumeStaminaForDive = true;

        }
        //DiveRollingが終了後フラグをリセット
        if (!PlayerAnimatorController.GetIsDiveRolling)
        {
            hasConsumeStaminaForDive = false;
        }


        //「左シフト」キーが押されたら、スタミナを減らす
        if (Input.GetKey(KeyCode.LeftShift) && !isTired)
        {
            currentStamina -= Time.deltaTime * StaminaConsumeSpeed;
            StaminaBarController.StaminaBar.fillAmount = currentStamina / maxStamina;
            if (currentStamina < 0)
            {
                isTired = true;
            }
        }

        else //「左シフト」キーが押されてない場合、スタミナを増やす
        {
            //現在のスタミナが100より少なかったら回復する
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
        //「K」キーが押されたら、ダメージを受ける
        if (Input.GetKeyDown(KeyCode.K))
        {
            int damage = 10;
            currentHealth -= damage;
            HPBarController.HPBarImage.fillAmount = currentHealth / maxHealth;
        }
    }



    ///<summary>
    ///スタミナを消費する処理
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

