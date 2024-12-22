using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttackController : MonoBehaviour
{ /// <summary>
  /// çUåÇëŒè€ÇÃEnemyController
  /// </summary>
    public PlayerController PlayerController;

    public void Hit()
    {
        PlayerController.CanTakeDamage = true;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
