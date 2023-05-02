using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SummonBoss : MonoBehaviour
{
    private int count;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
        {
            if(count == 0)
            {
                GameManager.Instance.isBossSummon = true;
                EnemySoundManager.Instance.PlayBossEncounter();
                count++;
            }
        }
    }
}
