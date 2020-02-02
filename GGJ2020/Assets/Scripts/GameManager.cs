using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;


public delegate void NumbersChanged(int value);

public class GameManager : MonoBehaviour
{
    static public GameManager Instance
    {
        get; private set;
    }

    [SerializeField]
    Player player = null;

    public NumbersChanged OnScoreChanged;
    public NumbersChanged OnComboChanged;


    List<EnemyController> enemiesInstancesLeft = new List<EnemyController>();
    List<EnemyController> enemiesInstancesRight = new List<EnemyController>();


    int _combo = 0;
    int _score = 0;

    public int Combo
    {
        get
        {
            return _combo;
        }
        set
        {
            _combo = value;
            OnComboChanged(_combo);
        }
    }

    public int Score
    {
        get
        {
            return _score;
        }
        set
        {
            _score = value;
            OnScoreChanged(_score);
        }
    }

    private void Awake()
    {
        if(Instance)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
    }

    public Player GetPlayer()
    {
        return player;
    }

    public void RegistryEnemy(EnemyController enemy)
    {
        if(enemy.transform.position.x > player.transform.position.x)
        {
            enemiesInstancesRight.Add(enemy);
        } else
        {
            enemiesInstancesLeft.Add(enemy);
        }
    }

    public void RemoveEnemy(EnemyController enemy)
    {
        if (enemy.transform.position.x > player.transform.position.x)
        {
            enemiesInstancesRight.Remove(enemy);
        }
        else
        {
            enemiesInstancesLeft.Remove(enemy);
        }

        Combo++;
        Score += enemy.Score * Combo;
    }

    public EnemyController GetEnemyTarget(Vector3 position, int direction)
    {
        List<EnemyController> enemies;
        if(direction > 0)
        {
            enemies = enemiesInstancesRight;
        } else
        {
            enemies = enemiesInstancesLeft;
        }

        enemies.Sort((enemyA, enemyB) =>
        {
            float distanceA = (enemyA.transform.position - position).magnitude;
            float distanceB = (enemyB.transform.position - position).magnitude;
            if (distanceA > distanceB) return 1;
            else if (distanceA == distanceB) return 0;
            else return -1;
        });

        if (enemies.Count > 0) return enemies[0];
        else return null;
    }

    public void PlayerHit()
    {
        Combo /= 2;
    }

    public void PlayerMissAttack()
    {
        Combo -= 3;
        Combo = Combo > 0 ? Combo : 0;
    }
}
