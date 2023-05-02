using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageData : MonoBehaviour
{
    public Vector2[] stageLeft = new Vector2[20];
    public Vector2[] stageRight = new Vector2[20];

    private void Awake()
    {
        stageLeft[0] = new Vector2(-60.59f, 0f);
        stageRight[0] = new Vector2(10.51f, 6.6f);

        stageLeft[1] = new Vector2(92.9f, 100f);
        stageRight[1] = new Vector2(106.9f, 102.7f);

        stageLeft[2] = new Vector2(182f, 97.1f);
        stageRight[2] = new Vector2(200.92f, 102.88f);

        stageLeft[3] = new Vector2(92.85f, 132.1f);
        stageRight[3] = new Vector2(100.13f, 138.2f);

        stageLeft[4] = new Vector2(171.97f, 131.1f);
        stageRight[4] = new Vector2(181f, 132.1f);

        stageLeft[5] = new Vector2(243.7f, 131.6f);
        stageRight[5] = new Vector2(256.98f, 137.6f);

        stageLeft[6] = new Vector2(19.12f, 96.8f);
        stageRight[6] = new Vector2(33.99f, 103.5f);

        stageLeft[7] = new Vector2(-36f, 95.2f);
        stageRight[7] = new Vector2(-27.2f, 104.6f);

        stageLeft[8] = new Vector2(-97.6f, 147.6f);
        stageRight[8] = new Vector2(-93f, 147.6f);

        stageLeft[9] = new Vector2(-110.09f, 194.3f);
        stageRight[9] = new Vector2(-106.87f, 205.12f);
    }
}