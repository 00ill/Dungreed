using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShortSword : MonoBehaviour,IWeapon
{
    public int WeaponNumber = 0;

    public int GetWeaponNum()
    {
        return (WeaponNumber);
    }

    public int WeaponDamage()
    {
        throw new System.NotImplementedException();
    }
}
