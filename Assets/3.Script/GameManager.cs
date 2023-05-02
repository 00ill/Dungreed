using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class GameManager : MonoBehaviour
{
    private static GameManager _instance = null;

    public int stageNum = 0;
    public bool isBossSummon;
    public int[] monsterCount;

    enum WeaponNum
    {
        ShortSword,
        Boomerang
    }

    public int equipWeapon;
    public int equipWeapon2;


    private void Awake()
    {
        if (_instance == null)
            _instance = this;
        monsterCount = new int[10] { 0, 0, 0, 0, 0, 0, 0, 0, 0, 1};
        Physics2D.IgnoreLayerCollision(12, 13, true);
        equipWeapon = (int)WeaponNum.ShortSword;
    }

    public static GameManager Instance
    {
        get 
        {
            if( _instance == null )
                return null;
            return _instance;
        }
    }
}
