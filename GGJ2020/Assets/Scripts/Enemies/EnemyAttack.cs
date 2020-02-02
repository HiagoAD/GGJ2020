using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
    EnemyController enemy;
    public void SetEnemyScript(EnemyController enemy)
    {
        this.enemy = enemy;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.CompareTag("Player"))
            enemy.OnEnemyAttackHit();
    }
}
