using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnController : MonoBehaviour
{
    [System.Serializable]
    struct Enemy
    {
        public EnemyController main;
        [HideInInspector] public List<EnemyController> buffer;
    }

    [SerializeField] float spawnDelay = 1;

    [SerializeField] Enemy[] enemies = null;

    private bool gameRunning;

    private float spawnCount;

    private float lenght;

    private Transform player;

    private void Awake()
    {
        lenght = transform.localScale.y/2;

        gameRunning = true;

        //initialize all the list of enemies
        for (int i = 0; i < enemies.Length; i++)
        {
            enemies[i].buffer = new List<EnemyController>();
        }
    }

    private void Start()
    {
        player = GameManager.Instance.GetPlayer().transform;
        GameManager.Instance.OnRestartGame += Restart;
        GameManager.Instance.OnGameOver += GameOver;
    }

    // Update is called once per frame
    void Update()
    {
        if (gameRunning)
        {
            if (spawnCount <= Time.time)
            {
                Spawn();
                spawnCount = Time.time + spawnDelay;
            }
        }

#if UNITY_EDITOR
        if (Input.GetKeyDown(KeyCode.Space))
            Spawn();
#endif
    }

    private void Spawn()
    {
        int enemyType = Random.Range(0, enemies.Length);

        EnemyController spawned = TryGetEnemy(enemyType);
        enemies[enemyType].buffer.Add(spawned);
    }

    private EnemyController TryGetEnemy(int id)
    {
        float yPosition = Random.Range(-lenght, lenght) + transform.position.y;
        float xPosition = Camera.main.ScreenToWorldPoint(new Vector3(Random.value > 0.5 ? Screen.width : 0, 0, 0)).x;
        
        foreach (EnemyController controller in enemies[id].buffer)
        {
            if(!controller.Alive)
            {
                controller.Restart(new Vector3(xPosition, yPosition, yPosition));
                GameManager.Instance.RegistryEnemy(controller);
                return controller;
            }
        }

        EnemyController spawned = Instantiate(enemies[id].main, new Vector3(xPosition, yPosition, yPosition), Quaternion.identity);

        spawned.Init(this, id, player);
        GameManager.Instance.RegistryEnemy(spawned);

        return spawned;
    }

    private void GameOver()
    {
        gameRunning = false;
    }

    private void Restart()
    {
        gameRunning = true;
    }
}
