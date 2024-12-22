using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "GameCharacterParameter", menuName = "Game/Character Parameter")]
public class GameCharacterParameter : ScriptableObject

{
    ///<summary>
    ///Å‘åHP
    ///</summary>
    public int MaxHealth = 100;
    ///<summary>
    ///Å‘åƒXƒ^ƒ~ƒi
    /// </summary>
    public int MaxStamina = 100;

    ///<summary>
    ///UŒ‚—Í
    ///</summary>
    public int AttackPower = 20;

    ///<summary>
    ///–h‰q—Í
    /// </summary>
    public int DefensePower = 10;

   
}
