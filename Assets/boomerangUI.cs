using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class boomerangUI : MonoBehaviour, IWeapon
{
    public int WeaponNumber = 1;

    public int GetWeaponNum()
    {
        return (WeaponNumber);
    }

    public int WeaponDamage()
    {
        throw new System.NotImplementedException();
    }
}