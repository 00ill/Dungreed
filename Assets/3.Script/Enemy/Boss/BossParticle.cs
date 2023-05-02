using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossParticle : MonoBehaviour
{
    private ObjectPool objectPool;

    private void Start()
    {
        objectPool = GameObject.FindGameObjectWithTag("ParticlePool").GetComponent<ObjectPool>();
    }
    public void DestroyParticle()
    {
        objectPool.ReturnObject(gameObject);
    }
}