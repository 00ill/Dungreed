using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateParticle : MonoBehaviour
{
    private ObjectPool objectPool;

    private float timer;
    private float cooldown = 0.5f;

    private float randX;
    private float randY;
    private void Awake()
    {
        TryGetComponent(out objectPool);
    }

    private void Update()
    {
        randX = Random.Range(-110f, -107.7f);
        randY = Random.Range(197.68f, 200f);

        if (timer >= cooldown)
        {
            objectPool.GetObject().transform.position = new Vector3(randX, randY, 0);
            timer = 0;
        }
        else
            timer += Time.deltaTime;
    }
}
