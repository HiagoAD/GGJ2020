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


    List<EnemyController> enemiesInstances = new List<EnemyController>();


    int _combo = 0;
    int _score = 0;
    int life = 3;

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
        enemiesInstances.Add(enemy);
    }

    public void RemoveEnemy(EnemyController enemy)
    {
        enemiesInstances.Remove(enemy);

        Combo++;
        Score += enemy.Score * Combo;
    }

    public EnemyController GetEnemyTarget(Vector3 position, int direction)
    {
        List<EnemyController> enemiesOnPlayerSide = enemiesInstances.FindAll(enemie =>
        {
            return (direction > 0 && enemie.transform.position.x > position.x) || (direction < 0 && enemie.transform.position.x < position.x);
        });
        enemiesOnPlayerSide.Sort((enemyA, enemyB) =>
        {
            float distanceA = (enemyA.transform.position.x - position.x);
            float distanceB = (enemyB.transform.position.x - position.x);

            if (distanceA > distanceB) return 1;
            else if (distanceA == distanceB) return 0;
            else return -1;
        });

        if (enemiesOnPlayerSide.Count > 0) return enemiesOnPlayerSide[0];
        else return null;
    }

    public void PlayerHit(EnemyController hitter)
    {
        Combo /= 2;
        Debug.Log("Life " + --life);

        player.Hit(hitter.transform);
    }

    public void PlayerMissAttack()
    {
        Combo -= 3;
        Combo = Combo > 0 ? Combo : 0;
    }
}
