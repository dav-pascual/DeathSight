using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Weapon 
{
    // Related to weapon object
    public string name;
    public int id;
    public GameObject weaponObject;
    public float fireRate;  // Cadencia
    public int initAmmo;  // Municion inicial del arma
    public int currentAmmo;  // Municion actual del arma

    // Related to bullet object
    public GameObject bulletObject;
    public int bulletSpeed;
    public float bulletLife; // Segundos max antes de que la bala se autodestruya
    public float bulletDamage;  // Daño que causa la bala al zombie
}
