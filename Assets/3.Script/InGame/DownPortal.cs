using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DownPortal : MonoBehaviour
{
    [SerializeField] private GameObject player;
    [SerializeField] private int ActivateStageNum;
    private ParticleSystem particlesystem;
    private bool isOn;
    private void Awake()
    {
        TryGetComponent(out particlesystem);
    }

    private void Update()
    {
        if (GameManager.Instance.monsterCount[ActivateStageNum] != 0)
            particlesystem.Stop();
        else
        {
            if (!isOn) { particlesystem.Play(); isOn = true; }
        } 
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
        {
            if(GameManager.Instance.stageNum == 3)
            {
                GameManager.Instance.stageNum = 1;
                player.transform.position = new Vector3(96.96f, 105.32f, 0);
            }
        }
    }
}
