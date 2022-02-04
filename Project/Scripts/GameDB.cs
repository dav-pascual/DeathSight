using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameDB : MonoBehaviour
{
    // Weapons related
    public List<Weapon> weapons = new List<Weapon>();
    public enum enumWeapon { PISTOL = 0, SUBMGUN = 1 };
    public int unlockSubMRound;
    public int weaponSelected;
    
    // Methods
    public int getPistolId()
    {
        return (int)enumWeapon.PISTOL;
    }

    public int getSubMGunId()
    {
        return (int)enumWeapon.SUBMGUN;
    }
}

