using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GiantBat : MonoBehaviour, IEnemy
{
    [SerializeField] private int ActivateStageNum;
    [SerializeField] private GameObject bullet;

    private Animator animator;
    private SpriteRenderer spriteRenderer;
    private Transform player;

    private bool isAttackCoroutineRunning;

    private bool isSummon;

    private float Hp;
    private float MaxHp = 46;

    private Vector2 directionVector;
    private Vector2 directionVector2;
    private Vector3 rotated;
    private readonly int rotateAngle = 15;
    private Vector2 bulletToPlayer;

    private void Awake()
    {
        TryGetComponent(out animator);
        TryGetComponent(out spriteRenderer);
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
                if (player.position.x < transform.position.x) { spriteRenderer.flipX = true; }
                else { spriteRenderer.flipX = false; }

                if (!isAttackCoroutineRunning)
                    StartCoroutine(MakeBullet_co());

                if (Hp <= 0)
                {
                    StopCoroutine(MakeBullet_co());
                    StartCoroutine(Die_co());
                }
            }
        }
    }

    private IEnumerator MakeBullet_co()
    {
        isAttackCoroutineRunning = true;
        yield return new WaitForSeconds(1f);
        while (true)
        {
            for (int i = 0; i < 9; i++)
            {
                Instantiate(bullet, transform.position, Quaternion.identity, gameObject.transform);
            }
            yield return new WaitForSeconds(1.7f);
            animator.SetBool("Attack", true);
            yield return new WaitForSeconds(0.3f);
            int childCount = 0;

            bulletToPlayer = (player.transform.position - transform.position).normalized;
            rotated.x = Mathf.Cos(rotateAngle * Mathf.Deg2Rad) * bulletToPlayer.x - Mathf.Sin(rotateAngle * Mathf.Deg2Rad) * bulletToPlayer.y;
            rotated.y = Mathf.Sin(rotateAngle * Mathf.Deg2Rad) * bulletToPlayer.x + Mathf.Cos(rotateAngle * Mathf.Deg2Rad) * bulletToPlayer.y;
            directionVector = rotated;

            rotated.x = Mathf.Cos((-rotateAngle) * Mathf.Deg2Rad) * bulletToPlayer.x - Mathf.Sin(-rotateAngle) * bulletToPlayer.y;
            rotated.y = Mathf.Sin(( -rotateAngle) * Mathf.Deg2Rad) * bulletToPlayer.x + Mathf.Cos((-rotateAngle) * Mathf.Deg2Rad) * bulletToPlayer.y;
            directionVector2 = rotated;


            yield return null;
            foreach (Transform child in this.transform)
            {
                EnemySoundManager.Instance.PlayBatFire();
                if (!child.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).IsName("RangeBallBulletHit"))
                {
                    if(childCount % 3 == 1)
                    {
                        StartCoroutine(child.GetComponent<RangeBallBullet>().Shoot(directionVector));
                    }
                    else if (childCount % 3 == 2)
                    {
                        StartCoroutine(child.GetComponent<RangeBallBullet>().Shoot(bulletToPlayer.normalized));
                        yield return new WaitForSeconds(0.3f);

                    }
                    else if (childCount % 3 == 0)
                    {
                        StartCoroutine(child.GetComponent<RangeBallBullet>().Shoot(directionVector2));
                    }
                    childCount++;
                }
            }
            yield return new WaitForSeconds(1.5f);
        }
    }
    private IEnumerator hitAnim()
    {
        spriteRenderer.color = Color.red;
        yield return new WaitForSeconds(0.2f);
        spriteRenderer.color = new Color(255, 255, 255, 255);
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
    private void ToBeforeAttack()
    {
        animator.SetBool("Attack", false);
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
