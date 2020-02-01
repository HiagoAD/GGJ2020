using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    static public GameManager Instance
    {
        get; private set;
    }

    [SerializeField]
    Player player = null;


    List<Enemy> enemiesInstancesLeft = new List<Enemy>();
    List<Enemy> enemiesInstancesRight = new List<Enemy>();

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

    public void RegistryEnemy(Enemy enemy)
    {
        if(enemy.transform.position.x > player.transform.position.x)
        {
            enemiesInstancesRight.Add(enemy);
        } else
        {
            enemiesInstancesLeft.Add(enemy);
        }
    }

    public void RemoveEnemy(Enemy enemy)
    {
        if (enemy.transform.position.x > player.transform.position.x)
        {
            enemiesInstancesRight.Remove(enemy);
        }
        else
        {
            enemiesInstancesLeft.Remove(enemy);
        }

        Destroy(enemy.gameObject);
    }

    public Enemy GetEnemyTarget(Vector3 position, int direction)
    {
        List<Enemy> enemies;
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
}
