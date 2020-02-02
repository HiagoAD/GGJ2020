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

    public int Score { get { return hpMax * 10; } }

    private int hp;
    private Transform player;
    
    private bool alive;
    private bool playerDead = false;
    private SpawnController spawner;    
    private int ID; //Verify what type this enemy is

    public bool Alive {get => alive;}

    public void Init(SpawnController spawner, int ID, Transform player)
    {
        this.spawner = spawner;
        this.ID = ID;
        this.player = player;

        GameManager.Instance.OnGameOver += GameOver;
        GameManager.Instance.OnRestartGame += Restart;

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
        if (playerDead)
            return;

        if(distanceToFollowPlayer >= Vector2.Distance(transform.position, player.position))
            transform.position = Vector3.MoveTowards(transform.position, player.position, moveSpeed*Time.deltaTime);
        else
        {
            var newVec = new Vector3(player.position.x, transform.position.y, player.position.z);
            transform.position = Vector3.MoveTowards(transform.position, newVec, moveSpeed*Time.deltaTime);
        }
    }

    public void Restart(Vector3 position)
    {
        transform.position = position;
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

    private void GameOver()
    {
        playerDead = true;
        //call animation idle
    }

    private void Restart()
    {
        Kill();
    }
}
