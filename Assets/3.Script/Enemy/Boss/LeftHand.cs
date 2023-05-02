using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeftHand : MonoBehaviour
{
    [SerializeField] GameObject laser;
    private Animator animator;

    private void Awake()
    {
        TryGetComponent(out animator);
    }
    private void ShootLaser()
    {
        EnemySoundManager.Instance.PlayBossLaser();
        Instantiate(laser, transform.position + new Vector3(0.7f, -0.2f, 0), Quaternion.identity);
    }

    private void ToIdle()
    {
        animator.SetBool("Attack", false);
    }
}
