using System.Collections;
using UnityEngine;

public class Swing : MonoBehaviour
{
    private ObjectPool objectPool;
    
    private void Start()
    {
        objectPool = GameObject.FindGameObjectWithTag("SwingPool").GetComponent<ObjectPool>();

    }

    private void OnEnable()
    {
        StartCoroutine("DisableObject_co");
    }
        
    private IEnumerator DisableObject_co()
    {
        yield return new WaitForSeconds(0.2f);
        objectPool.ReturnObject(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision != null && collision.CompareTag("Enemy"))
            collision.gameObject.GetComponent<IEnemy>().TakeDamage(8);
    }
}
