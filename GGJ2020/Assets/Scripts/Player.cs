using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Player : MonoBehaviour
{
    [SerializeField]
    public float attackThreshold;
    [SerializeField]
    public float correctAttackTime;
    [SerializeField]
    public float wrongAttackTime;

    public bool Attacking
    {
        get; private set;
    }

    private Enemy target;

    public void Attack(int direction)
    {
        if (Attacking) return;

        Attacking = true;

        Enemy enemy = GameManager.Instance.GetEnemyTarget(transform.position, direction);
        
        if(enemy)
        {
            Vector3 distance = enemy.transform.position - transform.position;
            if(distance.magnitude < attackThreshold)
            {
                attackOnRange(enemy);
                target = enemy;
            } else
            {
                attackOutOfRange(direction);
            }
        }
        else
        {
            attackOutOfRange(direction);
        }
    }

    private void attackOnRange(Enemy enemy)
    {
        transform.DOMove(enemy.transform.position, correctAttackTime).onComplete = () =>
        {
            Attacking = false;
        };
        
    }

    private void attackOutOfRange(int direction)
    {
        transform.DOMoveX(transform.position.x + (attackThreshold * Mathf.Sign(direction)), wrongAttackTime).onComplete = () =>
        {
            Attacking = false;
        };
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (Attacking && target && collider.gameObject == target.gameObject)
        {
            GameManager.Instance.RemoveEnemy(target);
            target = null;
            Attacking = false;
        } else
        {
            GetComponent<SpriteRenderer>().DOColor(new Color(255, 255, 255, 0), 1f).SetEase(Ease.Flash, 8);
        }
    }
}
