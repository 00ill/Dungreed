using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BigsellAttackColl : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
        {
            collision.gameObject.GetComponent<PlayerStatus>().TakeDamage(20);
        }
    }
}
