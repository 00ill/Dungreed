using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossSword : MonoBehaviour
{
    private Transform player;
    private SpriteRenderer spriteRenderer;
    private Rigidbody2D sword_rb;
    private Animator animator;

    private Vector3 directionVector;
    private float angle;

    private bool isShootCoroutineRunning;
    [SerializeField] private float shootSpeed;

    private Vector3 leftVector = new Vector3(-117, 192, 0);
    private Vector3 rightVector = new Vector3(-100, 208, 0);

    private void Awake()
    {
        TryGetComponent(out spriteRenderer);
        TryGetComponent(out sword_rb);
        TryGetComponent(out animator);
        player = GameObject.FindGameObjectsWithTag("Player")[0].transform;
    }
    private void OnEnable()
    {
        StartCoroutine(Shoot_co());
    }
    private void Update()
    {
        if (!isShootCoroutineRunning)
        {
            directionVector = (player.position - transform.position).normalized;
            angle = Mathf.Atan2(directionVector.y, directionVector.x) * Mathf.Rad2Deg - 90;
            transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        }

        if (transform.position.x <= leftVector.x || transform.position.y <= leftVector.y || transform.position.x >= rightVector.x || transform.position.y >= rightVector.y)
            StartCoroutine(HitWall_co());
    }

    private IEnumerator Shoot_co()
    {
        yield return new WaitForSeconds(1.5f);
        animator.SetBool("Shoot", true);
        isShootCoroutineRunning = true;
        sword_rb.velocity = directionVector * shootSpeed;
        yield return new WaitForSeconds(1.5f);
        isShootCoroutineRunning = false;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            collision.gameObject.GetComponent<PlayerStatus>().TakeDamage(6);
        }
    }

    private IEnumerator HitWall_co()
    {
        yield return null;
        sword_rb.velocity = Vector2.zero;
        transform.Find("FX").gameObject.GetComponent<Animator>().SetBool("Hit", true);
        animator.SetBool("Shoot", false);
        StartCoroutine(DestroySword_co());
    }

    private IEnumerator DestroySword_co()
    {
        yield return new WaitForSeconds(1f);
        Destroy(gameObject);    
    }
}