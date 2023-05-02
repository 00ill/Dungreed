using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HpBar : MonoBehaviour
{
    [SerializeField] private GameObject hpBar;
    private GameObject canvas;
    private RectTransform hpRect;
    private float barOffset;
    private float Hp;
    private float MaxHp;

    private void Awake()
    {
        canvas = GameObject.FindGameObjectWithTag("Canvas");
        barOffset = GetComponent<BoxCollider2D>().size.y * 5;
        hpRect = Instantiate(hpBar, canvas.transform).GetComponent<RectTransform>();
        MaxHp = transform.GetComponent<IEnemy>().getMaxHp();
    }

    private void LateUpdate()
    {
        if (hpRect != null)
        {
            hpRect.position = Camera.main.WorldToScreenPoint(new Vector3(transform.position.x, transform.position.y - barOffset, 0));
            Hp = transform.GetComponent<IEnemy>().getHp();
            hpRect.transform.GetComponent<Slider>().value = Hp / MaxHp;
        }
    }
}