using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BGMManager : MonoBehaviour
{
    private AudioSource audioSource;
    [SerializeField] private AudioClip town;
    [SerializeField] private AudioClip field;
    [SerializeField] private AudioClip boss;

    private int count;
    private void Awake()
    {
        TryGetComponent(out audioSource);
        audioSource.clip = town;
        audioSource.Play();
    }

    private void Update()
    {
        if(GameManager.Instance.stageNum == 1)
        {
            if(count == 0) 
            {
                audioSource.Stop();
                audioSource.clip = field;
                audioSource.Play();
                count++;
            }
        }

        if(GameManager.Instance.isBossSummon)
        {
            if(count == 1)
            {
                audioSource.Stop();
                audioSource.clip = boss;
                audioSource.Play();
                count++;
            }
        }
    }




}
