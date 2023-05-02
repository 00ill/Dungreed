using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;

public class RangeBallBullet : MonoBehaviour
{
    private Animator animator;
    private Rigidbody2D Bullet_rb;
    private Transform player;
    private Vector2 directionVector;
    private readonly float shootSpeed = 5f;

    private void Awake()
    {
        TryGetComponent(out animator);
        TryGetComponent(out Bullet_rb);
        player = GameObject.FindGameObjectsWithTag("Player")[0].transform;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.CompareTag("Bullet") && !collision.CompareTag("Enemy"))
        {
            EnemySoundManager.Instance.PlayBulletHit();
            animator.SetTrigger("Hit");
            Bullet_rb.velocity = Vector2.zero;
            if (collision.CompareTag("Player"))
            {
                collision.gameObject.GetComponent<PlayerStatus>().TakeDamage(6);
            }
        }
    }

    public IEnumerator Shoot()
    {
        yield return null;
        directionVector = (player.position - transform.parent.position).normalized;
        Bullet_rb.velocity += directionVector * shootSpeed;
    }

    public IEnumerator Shoot(Vector2 directionVector_)
    {
        yield return null;
        Bullet_rb.velocity += directionVector_ * shootSpeed;
    }
    private void DestroyBullet()
    {
        Destroy(gameObject);
    }
}
