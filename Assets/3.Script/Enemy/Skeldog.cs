using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skeldog : MonoBehaviour, IEnemy
{
    [SerializeField] private int ActivateStageNum;

    private Rigidbody2D skelDog_rb;
    private SpriteRenderer spriteRenderer;
    private Animator animator;
    private BoxCollider2D boxCollider;
    private Transform player;

    private bool isAttackCoroutineRunning;
    [SerializeField] private float moveSpeed;
    [SerializeField] private float jumpForce;
    [SerializeField] private float jumpTime;
    [SerializeField] private float jumpDelay;
    [SerializeField] private float attakcDist;
    private Vector2 jumpVector;

    private float Hp;
    private float MaxHp = 20f;

    private int playerLayer;
    private int myLayer;

    private bool isDead;
    public void TakeDamage(int damage)
    {
        EnemySoundManager.Instance.PlayMonsterHit();
        Hp -= damage;
        StartCoroutine(hitAnim());
    }

    private void Awake()
    {
        TryGetComponent(out skelDog_rb);
        TryGetComponent(out spriteRenderer);
        TryGetComponent(out animator);
        TryGetComponent(out boxCollider);
        player = GameObject.FindGameObjectWithTag("Player").transform;
        Hp = MaxHp;

        playerLayer = LayerMask.NameToLayer("Player");
        myLayer = LayerMask.NameToLayer("GroundEnemy");
        Physics2D.IgnoreLayerCollision(playerLayer, myLayer, true);
    }
    private void Start()
    {
        AddmonsterCount();
    }

    private void Update()
    {
        if (ActivateStageNum == GameManager.Instance.stageNum)
        {
            if (player.position.x < transform.position.x) { spriteRenderer.flipX = true; }
            else { spriteRenderer.flipX = false; }

            if (Vector2.Distance(player.position, transform.position) < attakcDist)
            {
                if (!isAttackCoroutineRunning && !isDead)
                {
                    StartCoroutine(Attack_co());
                }
            }
            else
            {
                if (!isAttackCoroutineRunning && !isDead)
                {
                    animator.SetBool("Run", true);
                    if (transform.position.x < player.position.x)
                    {
                        skelDog_rb.velocity = new Vector2(moveSpeed, skelDog_rb.velocity.y);
                    }
                    else
                    {
                        skelDog_rb.velocity = new Vector2(-moveSpeed, skelDog_rb.velocity.y);
                    }
                }
            }

            if (Hp <= 0)
            {
                if (!isDead)
                    StartCoroutine(Die_co());
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (isAttackCoroutineRunning)
        {
            if (collision.CompareTag("Player"))
            {
                collision.gameObject.GetComponent<PlayerStatus>().TakeDamage(3);
            }
        }
    }

    private IEnumerator Attack_co()
    {
        animator.SetBool("Run", false);
        isAttackCoroutineRunning = true;
        yield return new WaitForSeconds(jumpDelay);
        if (transform.position.x < player.position.x)
        {
            jumpVector = new Vector2(1f, 1f);
        }
        else
        {
            jumpVector = new Vector2(-1f, 1f);
        }
        Physics2D.IgnoreLayerCollision(playerLayer, myLayer, false);
        skelDog_rb.velocity = (jumpVector * jumpForce);
        boxCollider.isTrigger = true;
        yield return new WaitForSeconds(jumpTime);
        Physics2D.IgnoreLayerCollision(playerLayer, myLayer, true);
        boxCollider.isTrigger = false;
        skelDog_rb.velocity = Vector2.zero;
        isAttackCoroutineRunning = false;
    }

    private IEnumerator Die_co()
    {
        isDead = true;
        EnemySoundManager.Instance.PlayDie();
        animator.SetBool("Die", true);
        spriteRenderer.color = Color.gray;
        Physics2D.IgnoreLayerCollision(playerLayer, myLayer, true);
        boxCollider.isTrigger = false;
        skelDog_rb.velocity = Vector2.zero;
        SubtractMonsterCount();
        yield return null;
    }
    private IEnumerator hitAnim()
    {
        spriteRenderer.color = Color.red;
        yield return new WaitForSeconds(0.2f);
        spriteRenderer.color = new Color(255, 255, 255, 255);
    }
    public float getHp()
    {
        return Hp;
    }
    public float getMaxHp()
    {
        return MaxHp;
    }
#pragma warning disable IDE0051
    private void ToIdle()
    {
        animator.SetBool("Idle", true);
    }
    public void AddmonsterCount()
    {
        GameManager.Instance.monsterCount[ActivateStageNum]++;
    }
    public void SubtractMonsterCount()
    {
        GameManager.Instance.monsterCount[ActivateStageNum]--;
    }
}