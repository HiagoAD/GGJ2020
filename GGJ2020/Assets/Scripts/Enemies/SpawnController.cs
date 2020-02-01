using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnController : MonoBehaviour
{
    [System.Serializable]
    struct Enemy
    {
        public EnemyController main;
        [HideInInspector] public List<GameObject> buffer;
    }

    [SerializeField] float spawnDelay = 1;

    [SerializeField] Enemy[] enemies = null;

    private float spawnCount;

    private float lenght;

    private Transform player;

    private void Awake()
    {
        lenght = transform.localScale.y/2;

        player = GameManager.Instance.GetPlayer().transform;

        if(!player)
            Debug.LogError("MISSING PLAYER!!!");

        //initialize all the list of enemies
        for (int i = 0; i < enemies.Length; i++)
        {
            enemies[i].buffer = new List<GameObject>();
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(spawnCount <= Time.time)
        {
            Spawn();
            spawnCount = Time.time + spawnDelay;
        }

        if (Input.GetKeyDown(KeyCode.Space))
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
        float offsetPostion = Random.Range(-lenght, lenght);
        
        foreach (var enemy in enemies[id].buffer)
        {
            var controller = enemy.GetComponent<EnemyController>();
            if(!controller.Alive)
            {
                controller.Restart(offsetPostion);
                return enemy;
            }
        }

        EnemyController spawned = Instantiate(enemies[id].main, transform.position + Vector3.up*offsetPostion, Quaternion.identity);

        spawned.GetComponent<EnemyController>().Init(this, id, player);
        GameManager.Instance.RegistryEnemy(spawned);

        return spawned.gameObject;
    }


}
