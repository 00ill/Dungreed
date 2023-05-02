using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MonsterHpSlider : MonoBehaviour
{
    private void Awake()
    {
        transform.Find("Fill Area").Find("Fill").GetComponent<Image>().enabled = false;
    }
    
    private void Update()
    {
        if(transform.GetComponent<Slider>().value < 1)
            transform.Find("Fill Area").Find("Fill").GetComponent<Image>().enabled = true;
        if (transform.GetComponent<Slider>().value <= 0)
        {
            Destroy(gameObject);
        }
    }
}