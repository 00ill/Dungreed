using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bigskel : MonoBehaviour, IEnemy
{
    [SerializeField] private int ActivateStageNum;

    private Rigidbody2D skel_rb;
    private SpriteRenderer spriteRenderer;
    private Animator animator;
    private Transform player;

    [SerializeField] private float moveSpeed;
    [SerializeField] private float attackDelay;
    [SerializeField] private GameObject attackCollider;
    [SerializeField] private float attakcDist;

    private bool isAttackCoroutineRunning;

    private float Hp;
    private readonly float MaxHp = 50f;

    private int playerLayer;
    private int myLayer;
    private void Awake()
    {
        TryGetComponent(out skel_rb);
        TryGetComponent(out spriteRenderer);
        TryGetComponent(out animator);
        player = GameObject.FindGameObjectsWithTag("Player")[0].transform;
        Hp = MaxHp;
        playerLayer = LayerMask.NameToLayer("Player");
        myLayer = LayerMask.NameToLayer("GroundEnemy");
        Physics2D.IgnoreLayerCollision(myLayer, playerLayer, true);
    }
    private void Start()
    {
        AddmonsterCount();
    }

    private IEnumerator hitAnim()
    {
        spriteRenderer.color = Color.red;
        yield return new WaitForSeconds(0.2f);
        spriteRenderer.color = new Color(255, 255, 255, 255);
    }


    private void Update()
    {
        if (ActivateStageNum == GameManager.Instance.stageNum)
        {
            if (player.position.x < transform.position.x) { spriteRenderer.flipX = true; }
            else { spriteRenderer.flipX = false; }

            if (Vector2.Distance(player.position, transform.position) < attakcDist)
            {
                if (!isAttackCoroutineRunning)
                    StartCoroutine(Attack_co());
            }
            else
            {
                if (!isAttackCoroutineRunning)
                {
                    animator.SetBool("Attack", false);
                    if (transform.position.x < player.position.x)
                    {
                        skel_rb.velocity = new Vector2(moveSpeed, skel_rb.velocity.y);
                    }
                    else
                    {
                        skel_rb.velocity = new Vector2(-moveSpeed, skel_rb.velocity.y);
                    }
                }
            }
            if (Hp <= 0)
            {
                StartCoroutine(Die_co());
            }
        }
    }

    public void TakeDamage(int damage)
    {
        EnemySoundManager.Instance.PlayMonsterHit();
        Hp -= damage;
        StartCoroutine(hitAnim());
    }
    private IEnumerator Attack_co()
    {
        isAttackCoroutineRunning = true;
        skel_rb.velocity = Vector2.zero;
        animator.SetBool("Attack", true);
        yield return new WaitForSeconds(attackDelay);
        Time.timeScale = 1f;
    }

    private IEnumerator Die_co()
    {
        EnemySoundManager.Instance.PlayDie();
        animator.SetBool("Die", true);
        yield return new WaitForSeconds(0.2f);
        SubtractMonsterCount();
        Destroy(gameObject);
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
    private void Stop()
    {
        Time.timeScale = 0f;
    }

    private void MakeColl()
    {
        Instantiate(attackCollider, transform.position, Quaternion.identity, transform);
    }

    private void DeleteColl()
    {
        Destroy(transform.GetChild(0).gameObject);
    }
    private void QuitAttack()
    {
        isAttackCoroutineRunning = false;
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
