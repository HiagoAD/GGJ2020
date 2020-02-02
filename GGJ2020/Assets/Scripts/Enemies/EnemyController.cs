using UnityEngine;
using UnityEngine.UI;
using DragonBones;

[RequireComponent(typeof(UnityArmatureComponent))]
public class EnemyController : MonoBehaviour
{
    [Header ("Status")]
    [SerializeField] int hpMax = 3;
    [SerializeField] float moveSpeed = 1;
    [SerializeField] float distanceToFollowPlayer = 3;
    [SerializeField] EnemyAttack enemyAttack;

    public int Score { get { return hpMax * 10; } }

    private int hp;
    private Player player;  

    
    private bool playerDead = false;
    private int ID; //Verify what type this enemy is
    private UnityArmatureComponent armature;

    public bool Alive { get; private set; }
    public bool Usable { get; private set; }

    public void Init(int ID, Player player)
    {
        this.ID = ID;
        this.player = player;

        GameManager.Instance.OnGameOver += GameOver;
        GameManager.Instance.OnRestartGame += Restart;

        Reset();
    }

    private void Awake()
    {
        armature = GetComponent<UnityArmatureComponent>();
        armature.animation.Play("idle");

        enemyAttack.SetEnemyScript(this);
        enemyAttack.gameObject.SetActive(false);
    }

    void Update()
    {
        if(Alive)
        {
            Behaviour();
        }
    }

    private void Behaviour()
    {
        if (playerDead)
            return;

        if (player.transform.position.x > transform.position.x)
        {
            transform.localScale = new Vector3(-Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);
        } else if (player.transform.position.x < transform.position.x) // That's nescessary for keep the sate when the positions are the same
        {
            transform.localScale = new Vector3(Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);
        }

        if(distanceToFollowPlayer >= Vector2.Distance(transform.position, player.transform.position))
            transform.position = Vector3.MoveTowards(transform.position, player.transform.position, moveSpeed*Time.deltaTime);
        else
        {
            var newVec = new Vector3(player.transform.position.x, transform.position.y, player.transform.position.z);
            transform.position = Vector3.MoveTowards(transform.position, newVec, moveSpeed * Time.deltaTime);
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

    public void OnEnemyAttackHit()
    {
        SoundManager.Instance.PlayEnemyHit();
        GameManager.Instance.PlayerHit(this);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(Alive)
        {
            if (collision.CompareTag("Player"))
            {
                armature.animation.Play("attack", 1);
                enemyAttack.gameObject.SetActive(true);
                armature.AddDBEventListener(EventObject.COMPLETE, (str, eventObj) =>
                {
                    armature.animation.Play("idle");
                    enemyAttack.gameObject.SetActive(false);
                });
            }

        }
    }

    private void Reset()
    {
        Alive = true;
        hp = hpMax;
    }

    private void Kill(bool inGame = true)
    {
        GameManager.Instance.RemoveEnemy(this, inGame);
        Alive = false;
        if (inGame)
        {
            Usable = false;
            armature.animation.Play("death", 1);
            armature.AddDBEventListener(EventObject.COMPLETE, (str, obj) =>
            {
                transform.position = new Vector2 (1000, 1000);
                Usable = true;
            });
        } else
        {
            transform.position = new Vector2(1000, 1000);
        }
    }

    private void GameOver()
    {
        playerDead = true;
        //call animation idle
    }

    private void Restart()
    {
        playerDead = false;
        Kill(false);
    }
}
