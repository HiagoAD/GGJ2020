using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;


public delegate void NumbersChanged(int value);
public delegate void GameState();

public class GameManager : MonoBehaviour
{
    static public GameManager Instance
    {
        get; private set;
    }

    [SerializeField] Player player = null;
    [SerializeField] readonly int playerHpMax = 3;

    public int maxHp { get => playerHpMax; }

    private int playerHp;

    public NumbersChanged OnScoreChanged;
    public NumbersChanged OnComboChanged;
    public NumbersChanged PlayerHitted;

    public GameState OnGameOver;
    public GameState OnRestartGame;


    List<EnemyController> enemiesInstances = new List<EnemyController>();


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
            OnComboChanged?.Invoke(_combo);
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
            OnScoreChanged?.Invoke(_score);
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

        playerHp = playerHpMax;
        OnRestartGame += Restart;
    }

    public Player GetPlayer()
    {
        return player;
    }

    public void RegistryEnemy(EnemyController enemy)
    {
        enemiesInstances.Add(enemy);
    }

    public void RemoveEnemy(EnemyController enemy, bool inGame = true)
    {
        enemiesInstances.Remove(enemy);

        if(inGame)
        {
            Combo++;
            Score += enemy.Score * Combo;
        }
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

        player.Hit(hitter.transform);
        playerHp--;
        PlayerHitted?.Invoke(playerHp);

        if (playerHp == 0)
            OnGameOver?.Invoke();
    }

    public void PlayerMissAttack()
    {
        Combo -= 3;
        Combo = Combo > 0 ? Combo : 0;
        Debug.Log(Combo);
    }

    private void Restart()
    {
        playerHp = playerHpMax;
        _combo = 0;
        _score = 0;
    }
}
