using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnController : MonoBehaviour
{
    [System.Serializable]
    struct Enemy
    {
        public GameObject main;
        [HideInInspector] public List<GameObject> buffer;
    }
    [SerializeField] Enemy[] enemies;
    
    private void Awake()
    {
        //initialize all the list of enemies
        for (int i = 0; i < enemies.Length; i++)
        {
            enemies[i].buffer = new List<GameObject>();
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.A))
            Spawn();
    }

    private void Spawn()
    {
        int enemyType = Random.Range(0, enemies.Length);

        GameObject spawned = TryGetEnemy(enemyType);
        enemies[enemyType].buffer.Add(spawned);
    }

    private GameObject TryGetEnemy(int id)
    {
        foreach (var enemy in enemies[id].buffer)
        {
            var controller = enemy.GetComponent<EnemyController>();
            if(!controller.Alive)
            {
                controller.Restart();
                return enemy;
            }
        }

        GameObject spawned = Instantiate(enemies[id].main, transform.position, Quaternion.identity);

        spawned.GetComponent<EnemyController>().Init(this, id);

        return spawned;
    }


}
