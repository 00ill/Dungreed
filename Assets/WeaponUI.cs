using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WeaponUI : MonoBehaviour
{
    [SerializeField] private Sprite shortSword;
    [SerializeField] private Sprite boomerang;

    private Image image;
    private RectTransform rectTransform;
    private void Awake()
    {
        image = GetComponent<Image>();
        rectTransform = GetComponent<RectTransform>();
    }
    private void Update()
    {
        if(GameManager.Instance.equipWeapon == 0)
        {
            rectTransform.localScale = new Vector3(0.7f, 0.7f, 1);
            image.sprite = shortSword;  
        }
        else
        {
            rectTransform.localScale = new Vector3(1f, 1.5f, 1);
            image.sprite = boomerang;
        }
    }
}
