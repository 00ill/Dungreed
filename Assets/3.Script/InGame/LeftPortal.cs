using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeftPortal : MonoBehaviour
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
            if (GameManager.Instance.stageNum == 1)
            {
                GameManager.Instance.stageNum = 6;
                player.transform.position = new Vector3(40.95f, 95.44f, 0);
            }
            else if (GameManager.Instance.stageNum == 2)
            {
                GameManager.Instance.stageNum = 1;
                player.transform.position = new Vector3(114.47f, 96.85f, 0);
            }
            else if (GameManager.Instance.stageNum == 4)
            {
                GameManager.Instance.stageNum = 3;
                player.transform.position = new Vector3(107.12f, 131.37f, 0);
            }
            else if (GameManager.Instance.stageNum == 5)
            {
                GameManager.Instance.stageNum = 4;
                player.transform.position = new Vector3(188.48f, 129.21f, 0);
            }
            else if (GameManager.Instance.stageNum == 6)
            {
                GameManager.Instance.stageNum = 7;
                player.transform.position = new Vector3(-19.39f, 101.34f, 0);
            }
        }
    }
}
