using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Player : MonoBehaviour
{
    [SerializeField] public float attackThreshold;
    [SerializeField] public float correctAttackTime;
    [SerializeField] public float wrongAttackTime;

    private Vector2 startPosition;

    public bool Attacking
    {
        get; private set;
    }

    private EnemyController target;

    private void Awake()
    {
        startPosition = transform.position;
    }

    private void Start()
    {
        GameManager.Instance.OnRestartGame += Restart;
        GameManager.Instance.OnGameOver += GameOver;
    }

    public void Attack(int direction)
    {
        if (Attacking) return;

        Attacking = true;

        EnemyController enemy = GameManager.Instance.GetEnemyTarget(transform.position, direction);
        
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
            GameManager.Instance.PlayerMissAttack();
        }
    }

    private void attackOnRange(EnemyController enemy)
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
            collider.gameObject.GetComponent<EnemyController>().Hit(1);
            target = null;
            Attacking = false;
        } else
        {
            GetComponent<SpriteRenderer>().DOColor(new Color(255, 255, 255, 0), 1f).SetEase(Ease.Flash, 8)
            .OnComplete(()=>GetComponent<SpriteRenderer>().color += new Color(0, 0, 0, 1));
            GameManager.Instance.PlayerHit();
        }
    }

    private void Restart()
    {
        transform.position = startPosition;
    }

    private void GameOver()
    {
        //call death animation
    }


}
