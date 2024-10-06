using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyPatrolPoints : MonoBehaviour
{
    public EnemyHealthController enemy;

    private void Update()
    {
        if (enemy.curHealth <= 0)
        {
            Destroy(gameObject);
        }
    }
}
