using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyController : MonoBehaviour
{
    [Header ("Status")]
    [SerializeField] int hpMax = 3;
    [SerializeField] float moveSpeed = 1;
    [SerializeField] float distanceToFollowPlayer = 3;

    private int hp;
    private Transform player;
    
    private bool alive;
    private SpawnController spawner;    
    private int ID; //Verify what type this enemy is

    public bool Alive {get => alive;}

    public void Init(SpawnController spawner, int ID, Transform player)
    {
        this.spawner = spawner;
        this.ID = ID;
        this.player = player;
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
        if(distanceToFollowPlayer >= Vector2.Distance(transform.position, player.position))
            transform.position = Vector3.MoveTowards(transform.position, player.position, moveSpeed*Time.deltaTime);
        else
        {
            var newVec = new Vector3(player.position.x, transform.position.y, player.position.z);
            transform.position = Vector3.MoveTowards(transform.position, newVec, moveSpeed*Time.deltaTime);
        }
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
        GameManager.Instance.RemoveEnemy(this);
        alive = false;
        transform.position = new Vector2 (1000, 1000);
    }
}
