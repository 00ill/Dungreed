using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossDoor : MonoBehaviour
{
    private bool isClose;
    private Transform player;
    private bool isOpen;
    private Animator animator;

    [SerializeField] private GameObject button;
    private void Awake()
    {
        TryGetComponent(out animator);
        player = GameObject.FindGameObjectsWithTag("Player")[0].transform;
        button.SetActive(false);
    }


    private void Update()
    {
        if (GameManager.Instance.monsterCount[7] == 0)
        {
            animator.SetBool("Open", true);
            isOpen = true;
        }
        if (isClose && isOpen)
        {
            button.SetActive(true);
            if (Input.GetKeyDown(KeyCode.F)) { player.position = new Vector3(-96.39f, 146.03f, 0); GameManager.Instance.stageNum = 8; }
        }
        else { button.SetActive(false); }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        isClose = true;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        isClose = false;
    }
}
