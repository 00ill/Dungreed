using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossDead : MonoBehaviour
{

    private int playerLayer;
    private int deadLayer;


    private void Awake()
    {
        playerLayer = LayerMask.NameToLayer("Player");
        deadLayer = LayerMask.NameToLayer("Dead");
        Physics2D.IgnoreLayerCollision(playerLayer, deadLayer, true);
        Physics2D.IgnoreLayerCollision(deadLayer, deadLayer, true);
    }
}
