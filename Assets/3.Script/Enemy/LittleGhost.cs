using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization;
using UnityEngine;
using UnityEngine.SubsystemsImplementation;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(BoxCollider2D))]

public class LittleGhost : MonoBehaviour, IEnemy
{
    [SerializeField] private int ActivateStageNum;
    [SerializeField] private float moveSpeed;
    [SerializeField] private float attackMoveSpeed;
    [SerializeField] private float attackTime;
    [SerializeField] private float attackCooldown;

    private Transform player;
    private Animator animator;
    private Vector3 directionVector;
    private Rigidbody2D littleGhost_rb;
    private SpriteRenderer littleGhost_sr;

    private bool isAttackCoroutineRunning;
    private float attackTimer;

    private int Hp;
    private readonly int MaxHp = 6;
    private readonly int damage = 4;

    private bool isSummon = false;
    private int count;

    private void Awake()
    {
        TryGetComponent(out animator);
        TryGetComponent(out littleGhost_rb);
        TryGetComponent(out littleGhost_sr);
        player = GameObject.FindGameObjectsWithTag("Player")[0].transform;
        Hp = MaxHp;
    }

    private void Start()
    {
        AddmonsterCount();
    }
    private void Update()
    {
        if (ActivateStageNum == GameManager.Instance.stageNum)
        {
            if (!isSummon) { animator.SetBool("Idle", false); isSummon = true; }
            if (!animator.GetCurrentAnimatorStateInfo(0).IsName("Summon"))
            {
                if (littleGhost_rb.velocity.x < 0) { littleGhost_sr.flipX = true; }
                else { littleGhost_sr.flipX = false; }

                if (attackTimer > attackCooldown)
                {
                    StartCoroutine(Attack_co());
                    attackTimer = 0;
                }
                else if (!isAttackCoroutineRunning)
                {
                    attackTimer += Time.deltaTime;
                }

                if (!isAttackCoroutineRunning)
                {
                    if (count != 0)
                    {
                        directionVector = (player.position - transform.position).normalized;
                        littleGhost_rb.velocity = directionVector * moveSpeed;
                    }
                    count++;
                }
                if (Hp <= 0)
                {
                    StartCoroutine(Die_co());
                }
            }
        }
    }

    private IEnumerator Attack_co()
    {
        isAttackCoroutineRunning = true;
        animator.SetBool("Attack", true);
        directionVector = (player.position - transform.position).normalized;
        littleGhost_rb.velocity = directionVector * attackMoveSpeed;
        yield return new WaitForSeconds(attackTime);
        animator.SetBool("Attack", false);
        isAttackCoroutineRunning = false;
    }

    public void TakeDamage(int damage)
    {
        EnemySoundManager.Instance.PlayMonsterHit();
        Hp -= damage;
        StartCoroutine(hitAnim());
    }

    private IEnumerator Die_co()
    {
        EnemySoundManager.Instance.PlayDie();
        animator.SetBool("Die", true);
        yield return new WaitForSeconds(0.2f);
        SubtractMonsterCount();
        Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (isAttackCoroutineRunning)
        {
            if (collision.CompareTag("Player"))
            {
                collision.gameObject.GetComponent<PlayerStatus>().TakeDamage(damage);
            }
        }
    }
    private IEnumerator hitAnim()
    {
        littleGhost_sr.color = Color.red;
        yield return new WaitForSeconds(0.2f);
        littleGhost_sr.color = new Color(255, 255, 255, 255);
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