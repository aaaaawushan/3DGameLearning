using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttackController : MonoBehaviour
{
    /// <summary>
    /// UŒ‚‘ÎÛ‚ÌEnemyController
    /// </summary>
    public EnemyController DragonController;

    public void Hit()
    {
        DragonController.CanTakeDamage = true;
    }
}
