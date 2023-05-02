using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeWeapon : MonoBehaviour
{
    [SerializeField] Sprite shortSword;
    [SerializeField] Sprite drumStick;
    private SpriteRenderer spriteRenderer;
    private bool isChanged = false;

    private void Awake()
    {
        TryGetComponent(out spriteRenderer);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            if (isChanged)
            {
                spriteRenderer.sprite = shortSword;
                isChanged = false;
            }
            else
            {
                spriteRenderer.sprite = drumStick;
                isChanged = true;
            }
        }
    }


}
