using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RightHand : MonoBehaviour
{
    [SerializeField] GameObject laser;
    private Animator animator;
    Quaternion rotation = Quaternion.Euler(0f, 0f, 180f);
    private void Awake()
    {
        TryGetComponent(out animator);
    }
    private void ShootLaser()
    {
        EnemySoundManager.Instance.PlayBossLaser();
        Instantiate(laser, transform.position + new Vector3(-0.7f, -0.2f, 0), rotation);
    }

    private void ToIdle()
    {
        animator.SetBool("Attack", false);
    }
}
