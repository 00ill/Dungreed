using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stele : MonoBehaviour
{
    [SerializeField] private int ActivateStageNum;
    private SpriteRenderer spriteRenderer;
    private BoxCollider2D boxCollider;
    private Animator animator;

    private int count;
    private void Awake()
    {
        TryGetComponent(out spriteRenderer);
        TryGetComponent(out boxCollider);
        TryGetComponent(out animator);
        boxCollider.enabled = false;
        spriteRenderer.enabled = false;
    }

    private void Update()
    {
        if(ActivateStageNum == GameManager.Instance.stageNum)
        { 
            boxCollider.enabled = true;
            spriteRenderer.enabled =true;
            if(count == 0) { animator.SetTrigger("Close"); count++; }
        }

        if (GameManager.Instance.monsterCount[ActivateStageNum] == 0)
        {
            animator.SetBool("Idle", false);
        }
    }
#pragma warning disable IDE0051
    public void ToIdle()
    {
        animator.SetBool("Idle", true);
    }

    public void DestroyStele()
    {
        Destroy(gameObject);
    }
}