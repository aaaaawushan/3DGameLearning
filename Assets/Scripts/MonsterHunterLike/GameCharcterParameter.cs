using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "GameCharacterParameter", menuName = "Game/Character Parameter")]
public class GameCharacterParameter : ScriptableObject

{
    ///<summary>
    ///�ő�HP
    ///</summary>
    public int MaxHealth = 100;
    ///<summary>
    ///�ő�X�^�~�i
    /// </summary>
    public int MaxStamina = 100;

    ///<summary>
    ///�U����
    ///</summary>
    public int AttackPower = 20;

    ///<summary>
    ///�h�q��
    /// </summary>
    public int DefensePower = 10;

   
}
