using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RightPortal : MonoBehaviour
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
            if(GameManager.Instance.stageNum == 1)
            {
                GameManager.Instance.stageNum = 2;
                player.transform.position = new Vector3(174.28f, 97.84f, 0);
            }
            else if (GameManager.Instance.stageNum == 3)
            {
                GameManager.Instance.stageNum = 4;
                player.transform.position = new Vector3(167f, 128.99f, 0);
            }
            else if (GameManager.Instance.stageNum == 4)
            {
                GameManager.Instance.stageNum = 5;
                player.transform.position = new Vector3(236.8f, 137.7f, 0);
            }
            else if (GameManager.Instance.stageNum == 6)
            {
                GameManager.Instance.stageNum = 1;
                player.transform.position = new Vector3(85.53f, 97.41f, 0);
            }
            else if (GameManager.Instance.stageNum == 7)
            {
                GameManager.Instance.stageNum = 6;
                player.transform.position = new Vector3(11.38f, 95.86f, 0);
            }
            else if (GameManager.Instance.stageNum == 8)
            {
                GameManager.Instance.stageNum = 9;
                player.transform.position = new Vector3(-116.78f, 193.47f, 0);
            }
        }
    }
}
