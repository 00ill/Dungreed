using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(BoxCollider2D))]
public class RedGiantBat : MonoBehaviour, IEnemy
{
    [SerializeField] private int ActivateStageNum;
    [SerializeField] private GameObject bullet;
    [SerializeField] private float bulletDist;

    private Animator animator;
    private SpriteRenderer spriteRenderer;
    private Transform player;

    private bool isAttackCoroutineRunning = false;

    private float Hp;
    private float MaxHp = 44;
    private bool isSummon = false;
    public void TakeDamage(int damage)
    {
        EnemySoundManager.Instance.PlayMonsterHit();
        Hp -= damage;
        StartCoroutine(hitAnim());
    }

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
            if(!isSummon) { animator.SetBool("Idle", false); isSummon = true; }
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

    private IEnumerator Attack_co()
    {
        yield return null;
    }

    private IEnumerator MakeBullet_co()
    {
        isAttackCoroutineRunning = true;
        yield return new WaitForSeconds(1f);
        while (true)
        {
            for (int i = 0; i < 360; i += 18)
            {
                Instantiate(bullet, transform.position + new Vector3(bulletDist * Mathf.Cos(i * Mathf.Deg2Rad), bulletDist * Mathf.Sin(i * Mathf.Deg2Rad), 0)
                    , Quaternion.identity, gameObject.transform);
                yield return new WaitForSeconds(0.04f);
            }
            yield return new WaitForSeconds(1.7f);
            animator.SetBool("Attack", true);
            yield return new WaitForSeconds(0.3f);

            foreach (Transform child in this.transform)
            {
                if (!child.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).IsName("RangeBallBulletHit"))
                {
                    EnemySoundManager.Instance.PlayBatFire();
                    StartCoroutine(child.GetComponent<RangeBallBullet>().Shoot());
                }
            }
            yield return new WaitForSeconds(2.5f);
        }
    }
    private IEnumerator hitAnim()
    {
        spriteRenderer.color = Color.red;
        yield return new WaitForSeconds(0.2f);
        spriteRenderer.color = new Color(255, 255, 255, 255);
    }


    private IEnumerator Die_co()
    {
        EnemySoundManager.Instance.PlayDie();
        animator.SetBool("Die", true);
        yield return new WaitForSeconds(0.2f);
        SubtractMonsterCount();
        Destroy(gameObject);
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

    public float getHp()
    {
        return Hp;
    }

    public float getMaxHp()
    {
        return MaxHp;
    }
}