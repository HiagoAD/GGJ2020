using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    [Header ("Status")]
    [SerializeField] float moveSpeed = 1;
    private bool alive;
    private SpawnController spawner;    
    private int ID; //Verify what type this enemy is

    public bool Alive {get => alive;}

    public void Init(SpawnController spawner, int ID)
    {
        this.spawner = spawner;
        this.ID = ID;
        alive = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
            Kill();

        if(alive)
        {
            transform.position = Vector3.MoveTowards(transform.position, new Vector3(20,0,0), moveSpeed*Time.deltaTime);
        }
    }

    public void Restart()
    {
        transform.position = spawner.transform.position;
        alive = true;
    }

    private void Kill()
    {
        alive = false;
        transform.position = new Vector2 (1000, 1000);
    }
}
