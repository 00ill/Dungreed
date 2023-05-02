using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySoundManager : MonoBehaviour
{
    private static EnemySoundManager _instance = null;
    public static EnemySoundManager Instance
    {
        get
        {
            if (_instance == null)
            {
                return null;
            }
            return _instance;
        }
    }
    private AudioSource audioSource;

    [SerializeField] AudioClip Die;
    [SerializeField] AudioClip bossEncounter;
    [SerializeField] AudioClip bossLaser;
    [SerializeField] AudioClip bossBullet;
    [SerializeField] AudioClip bossSummonSword;
    [SerializeField] AudioClip bulletHit;
    [SerializeField] AudioClip batFire;
    [SerializeField] AudioClip monsterHit;

    private void Awake()
    {
        if (null == _instance)
        {
            _instance = this;
        }
        else
        {
            Destroy(this.gameObject);
        }

        audioSource = GetComponent<AudioSource>();
    }

    public void PlayDie()
    {
        if (audioSource != null)
        {
            if (!audioSource.isPlaying)
                audioSource.PlayOneShot(Die);
        }
    }

    public void PlayBossEncounter()
    {
        audioSource.PlayOneShot(bossEncounter);
    }  
    public void PlayBossLaser()
    {
        audioSource.PlayOneShot(bossLaser);
    }
    public void PlayBossBullet()
    {
        audioSource.PlayOneShot(bossBullet);
    }

    public void PlayBulletHit()
    {
        audioSource.PlayOneShot(bulletHit);
    }
    public void PlayBatFire()
    {
        audioSource.PlayOneShot(batFire);
    }
    public void PlayMonsterHit()
    {
        audioSource.PlayOneShot(monsterHit);
    }
    public void PlayBossSummonSword()
    {
        audioSource.PlayOneShot(bossSummonSword);
    }

}