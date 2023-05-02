using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;
using UnityEngine.UI;
using Quaternion = UnityEngine.Quaternion;
using Vector3 = UnityEngine.Vector3;

public class Boss : MonoBehaviour, IEnemy
{
    private Animator animator;
    private SpriteRenderer spriteRenderer;
    private Transform player;

    [SerializeField] private GameObject leftHand;
    [SerializeField] private GameObject rightHand;
    [SerializeField] private float handMoveSpeed;
    [SerializeField] private float offset;
    private Vector3 defaultLeftHandPosition = new Vector3(-114.5f, 196.5f, 0);
    private Vector3 defaultRightHandPosition = new Vector3(-102.6f, 196.5f, 0);

    [SerializeField] private float patternCooldown; //패턴 사이의 간격
    [SerializeField] private GameObject bossBullet;

    private UnityEngine.Vector3[] bulletVector = new UnityEngine.Vector3[4];
    private int rotateAngle = 6;

    [SerializeField] private GameObject bossSword;

    private int patternNum;
    private bool isPatternCoroutineRunning;
    private int bulletRotation;

    private float Hp;
    private float MaxHp;

    [SerializeField] private GameObject dead;

    [SerializeField] private GameObject lifeBar;
    [SerializeField] private GameObject HpBar;
    private void Awake()
    {
        TryGetComponent(out animator);
        TryGetComponent(out spriteRenderer);
        player = GameObject.FindGameObjectsWithTag("Player")[0].transform;
        spriteRenderer.enabled = false;
        foreach (Transform child in this.transform)
        {
            child.gameObject.SetActive(false);
        }
        MaxHp = 500;
        Hp = MaxHp;
        lifeBar.SetActive(false);
    }

    private void Update()
    {
        if (GameManager.Instance.isBossSummon)
        {
            spriteRenderer.enabled = true;
            lifeBar.SetActive(true);
            foreach (Transform child in this.transform)
            {
                child.gameObject.SetActive(true);
            }
            HpBar.GetComponent<Slider>().value = Hp / MaxHp;
        }

        if (Hp <= 0)
        {
            Instantiate(dead, transform.position, Quaternion.identity);
            lifeBar.SetActive(false);
            foreach (Transform child in this.transform)
            {
                Destroy(child);
            }
            Destroy(gameObject);

        }

        if (!isPatternCoroutineRunning && GameManager.Instance.isBossSummon)
        {
            patternNum = Random.Range(0, 4);
            switch (patternNum)
            {
                case 0:
                    isPatternCoroutineRunning = true;
                    animator.SetBool("Attack", true);
                    break;
                case 1:
                    StartCoroutine(Laser_co());
                    break;
                case 2:
                    StartCoroutine(LeftLaser_co());
                    break;
                case 3:
                    StartCoroutine(SummonSword_co());
                    break;
            }
        }
    }

    private IEnumerator ShootBullet_co()
    {
        bulletRotation = Random.Range(0, 2);
        if (bulletRotation == 0) { rotateAngle = -rotateAngle; }

        bulletVector[0] = Vector3.down;
        bulletVector[1] = Vector3.up;
        bulletVector[2] = Vector3.left;
        bulletVector[3] = Vector3.right;
        for (int j = 0; j < 50; j++)
        {
            for (int i = 0; i < 4; i++)
            {
                EnemySoundManager.Instance.PlayBossBullet();
                Instantiate(bossBullet, transform.Find("ShootStart").transform.position + bulletVector[i], Quaternion.identity, transform.Find("ShootStart").transform);
            }
            for (int i = 0; i < 4; i++)
            {
                bulletVector[i] = UnityEngine.Quaternion.Euler(0, 0, rotateAngle) * bulletVector[i];
            }
            yield return new WaitForSeconds(0.08f);
        }
        animator.SetBool("Attack", false);
        yield return new WaitForSeconds(3f);
        isPatternCoroutineRunning = false;
    }

    private IEnumerator Laser_co()
    {
        isPatternCoroutineRunning = true;
        while (!((leftHand.transform.position.y < player.position.y + offset) && (leftHand.transform.position.y > player.position.y - offset)))
        {
            leftHand.transform.position += new Vector3(0, (player.position.y - leftHand.transform.position.y), 0).normalized * handMoveSpeed;
            yield return new WaitForSeconds(0.005f);
        }
        leftHand.GetComponent<Animator>().SetBool("Attack", true);
        yield return new WaitForSeconds(1f);
        while (!((rightHand.transform.position.y < player.position.y + offset) && (rightHand.transform.position.y > player.position.y - offset)))
        {
            rightHand.transform.position += new Vector3(0, (player.position.y - rightHand.transform.position.y), 0).normalized * handMoveSpeed;
            yield return new WaitForSeconds(0.005f);
        }
        rightHand.GetComponent<Animator>().SetBool("Attack", true);
        yield return new WaitForSeconds(1f);
        while (!((leftHand.transform.position.y < player.position.y + offset) && (leftHand.transform.position.y > player.position.y - offset)))
        {
            leftHand.transform.position += new Vector3(0, (player.position.y - leftHand.transform.position.y), 0).normalized * handMoveSpeed;
            yield return new WaitForSeconds(0.005f);
        }
        leftHand.GetComponent<Animator>().SetBool("Attack", true);


        yield return new WaitForSeconds(1f);
        while (!((rightHand.transform.position.y < defaultRightHandPosition.y + offset) && (rightHand.transform.position.y > defaultRightHandPosition.y - offset)))
        {
            rightHand.transform.position += new Vector3(0, defaultRightHandPosition.y - rightHand.transform.position.y, 0).normalized * handMoveSpeed;
            yield return new WaitForSeconds(0.005f);
        }
        while (!((leftHand.transform.position.y < defaultLeftHandPosition.y + offset) && (leftHand.transform.position.y > defaultLeftHandPosition.y - offset)))
        {
            leftHand.transform.position += new Vector3(0, defaultLeftHandPosition.y - leftHand.transform.position.y, 0).normalized * handMoveSpeed;
            yield return new WaitForSeconds(0.005f);
        }
        yield return new WaitForSeconds(3f);
        isPatternCoroutineRunning = false;
    }

    private IEnumerator LeftLaser_co()
    {
        isPatternCoroutineRunning = true;
        while (!((leftHand.transform.position.y < player.position.y + offset) && (leftHand.transform.position.y > player.position.y - offset)))
        {
            leftHand.transform.position += new Vector3(0, (player.position.y - leftHand.transform.position.y), 0).normalized * handMoveSpeed;
            yield return new WaitForSeconds(0.005f);
        }
        leftHand.GetComponent<Animator>().SetBool("Attack", true);
        yield return new WaitForSeconds(2f);
        isPatternCoroutineRunning = false;
    }

    private IEnumerator SummonSword_co()
    {
        isPatternCoroutineRunning = true;
        for (int i = 0; i < 6; i++)
        {
            EnemySoundManager.Instance.PlayBossSummonSword();
            Instantiate(bossSword, new Vector3(-113f + i * 1.8f, 203, 0), Quaternion.identity);
            yield return new WaitForSeconds(0.3f);
        }
        yield return new WaitForSeconds(4);
        isPatternCoroutineRunning = false;
    }

    private IEnumerator Hit_co()
    {
        spriteRenderer.color = Color.red;
        yield return new WaitForSeconds(0.2f);
        spriteRenderer.color = new Color (255,255,255,255);
    }

    public void TakeDamage(int damage)
    {
        StartCoroutine(Hit_co());
        Hp -= damage;
    }

    public float getHp()
    {
        return Hp;
    }

    public float getMaxHp()
    {
        return MaxHp;
    }
    public void AddmonsterCount()
    {
        throw new System.NotImplementedException();
    }

    public void SubtractMonsterCount()
    {
        throw new System.NotImplementedException();
    }
}