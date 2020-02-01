using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyController : MonoBehaviour
{
    [Header ("Status")]
    [SerializeField] int hpMax = 3;
    [SerializeField] float moveSpeed = 1;

    private int hp;

    private Image spawnLenght;
    private bool alive;
    private SpawnController spawner;    
    private int ID; //Verify what type this enemy is

    public bool Alive {get => alive;}

    public void Init(SpawnController spawner, int ID)
    {
        this.spawner = spawner;
        this.ID = ID;
        Reset();
    }
    
    void Update()
    {
        if(alive)
        {
            Behaviour();
        }
    }

    private void Behaviour()
    {
        transform.position = Vector3.MoveTowards(transform.position, new Vector3(20,0,0), moveSpeed*Time.deltaTime);
    }

    public void Restart(float offset)
    {
        transform.position = spawner.transform.position + Vector3.up*offset;
        Reset();
    }

    public void Hit(int damage)
    {
        hp -= damage;

        if(hp <= 0)
            Kill();
    }

    private void Reset()
    {
        alive = true;
        hp = hpMax;
    }

    private void Kill()
    {
        alive = false;
        transform.position = new Vector2 (1000, 1000);
    }
}
