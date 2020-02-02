using UnityEngine;
using DG.Tweening;
using DragonBones;
using Transform = UnityEngine.Transform;

[RequireComponent(typeof(UnityArmatureComponent))]
public class Player : MonoBehaviour
{
    [SerializeField]
    private float attackThreshold;
    [SerializeField]
    private PlayerAttack playerAttack;
    [SerializeField]
    private float hitKnockback;

    public bool Attacking
    {
        get; private set;
    }

    private EnemyController target;
    UnityArmatureComponent armature;

    private void Awake()
    {
        armature = GetComponent<UnityArmatureComponent>();
        armature.animation.Play("idle");

        playerAttack.SetPlayerScript(this);
        playerAttack.gameObject.SetActive(false);
    }


    private void attackOnRange(EnemyController enemy)
    {
        float animationTime = armature.animation.Play("punsh", 1).totalTime;
        playerAttack.gameObject.SetActive(true);

        transform.DOMove(enemy.transform.position, animationTime).onComplete = () =>
        {
            Attacking = false;
            armature.animation.Play("idle");
            playerAttack.gameObject.SetActive(false);
        };
        
    }

    private void attackOutOfRange(int direction)
    {
        float animationTime = armature.animation.Play("punsh", 1).totalTime;
        transform.DOMoveX(transform.position.x + (attackThreshold * Mathf.Sign(direction)), animationTime).onComplete = () =>
        {
            Attacking = false;
            armature.animation.Play("idle");
            playerAttack.gameObject.SetActive(false);
        };
    }

    public void Attack(int direction)
    {
        if (Attacking) return;

        Attacking = true;

        transform.localScale = new Vector3(Mathf.Abs(transform.localScale.x) * Mathf.Sign(direction), transform.localScale.y, transform.localScale.z);

        EnemyController enemy = GameManager.Instance.GetEnemyTarget(transform.position, direction);

        if (enemy)
        {
            float distance = Mathf.Abs(enemy.transform.position.x - transform.position.x);
            if (distance < attackThreshold)
            {
                attackOnRange(enemy);
                target = enemy;
            }
            else
            {
                attackOutOfRange(direction);
            }
        }
        else
        {
            attackOutOfRange(direction);
            GameManager.Instance.PlayerMissAttack();
        }
    }

    public void OnPlayerAttackHit(GameObject enemy)
    {
        if(target && enemy == target.gameObject)
        {
            enemy.GetComponent<EnemyController>().Hit(1);
            target = null;
            Attacking = false;
        }
    }

    public void Hit(Transform hitterTransform)
    {
        if(hitterTransform.position.x > transform.position.x)
        {
            transform.position -= Vector3.right * hitKnockback; 
        } else
        {
            transform.position += Vector3.right * hitKnockback;
        }
    }
}
