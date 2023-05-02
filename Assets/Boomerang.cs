using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boomerang : MonoBehaviour,IWeapon
{
    public int WeaponNumber = 1;
    private Animator animator;
    private Rigidbody2D boomerang_rb;

    [SerializeField] private float speed;
    [SerializeField] private float attackTime;
    [SerializeField] InGameCursor inGameCursor;

    private Vector3 directionVector;
    private Vector3 attackPos;
    private Vector3 initialPos;
    private bool isAttack;
    private void Awake()
    {
        TryGetComponent(out animator);
        TryGetComponent(out boomerang_rb);
        initialPos = transform.position;
        directionVector = Vector3.zero;
    }

    private void Update()
    {
        if(!isAttack)
            transform.localPosition = Vector3.zero;
    }
    public IEnumerator Attack_co()
    {
        isAttack = true;
        directionVector = (inGameCursor.position - transform.position).normalized;
        attackPos = transform.position;
        animator.SetBool("Attack", true);
        yield return null;
        boomerang_rb.velocity = directionVector * speed;
        yield return new WaitForSeconds(attackTime);
        boomerang_rb.velocity = (attackPos - transform.position).normalized * speed;
        yield return new WaitForSeconds(attackTime + 0.5f);
        boomerang_rb.velocity = Vector2.zero;
        transform.localPosition = Vector3.zero;
        animator.SetBool("Attack", false);
        isAttack = false;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision != null && collision.CompareTag("Enemy"))
            collision.gameObject.GetComponent<IEnemy>().TakeDamage(10);
    }

    public int GetWeaponNum()
    {
        return (WeaponNumber);
    }

    public int WeaponDamage()
    {
        throw new System.NotImplementedException();
    }


}
