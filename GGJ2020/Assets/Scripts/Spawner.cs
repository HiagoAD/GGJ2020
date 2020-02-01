using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField]
    Enemy enemyPrefab = null;
    [SerializeField]
    float spawnEverySeconds = 1;

    float lastSpawned = 0;

    private void Update()
    {
        lastSpawned += Time.deltaTime;
        if(lastSpawned > spawnEverySeconds)
        {
            float xStartPoint = Random.value > 0.5 ? Screen.width : 0;
            Vector3 spawnPoint = Camera.main.ScreenToWorldPoint(new Vector3(xStartPoint, Screen.height/2, 0));
            Enemy enemyInstance = Instantiate(enemyPrefab);
            enemyInstance.transform.position = spawnPoint;
            lastSpawned = 0;
        }
    }
}
