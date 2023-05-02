using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossBullet : MonoBehaviour
{
    private Animator animator;
    private Vector3 directionVector;
    [SerializeField] private float bulletSpeed;

    private Vector3 leftVector = new Vector3(-125, 180, 0);
    private Vector3 rightVector = new Vector3(-90, 220, 0);

    private bool isHit;
    private void Awake()
    {
        TryGetComponent(out animator);
    }

    private void OnEnable()
    {
        directionVector = transform.localPosition.normalized;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
        {
            EnemySoundManager.Instance.PlayBulletHit();
            collision.gameObject.GetComponent<PlayerStatus>().TakeDamage(10);
            animator.SetTrigger("Hit");
            isHit = true;
        }
    }

    private void Update()
    {
        if(!isHit)
            transform.position += directionVector * bulletSpeed * Time.deltaTime;
        if (transform.position.x <= leftVector.x || transform.position.y <= leftVector.y || transform.position.x >= rightVector.x || transform.position.y >= rightVector.y)
            DestroyBullet();
    }

    private void DestroyBullet()
    {
        Destroy(gameObject);
    }
}
