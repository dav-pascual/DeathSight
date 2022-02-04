using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SubMachineGun : MonoBehaviour
{
    public float nextFire = 0f;
    public GameObject bulletSpawnPoint;
    private Transform bulletSpawn;
    private Vector3 vIni;
    Animator animPlayer;

    public GameObject gameController;
    private GameDB gameDB;
    private Weapon subMGun;
    private GameObject bullet;
    private AudioSource audioClip;
    public GameObject outOfAmmoText;

    void Start()
    {
        animPlayer = transform.root.GetComponent<Animator>();
        gameDB = gameController.GetComponent<GameDB>();
        subMGun = gameDB.weapons[gameDB.getSubMGunId()];
        bullet = subMGun.bulletObject;
        subMGun.currentAmmo = subMGun.initAmmo;
        outOfAmmoText.SetActive(false);
        audioClip = GetComponent<AudioSource>();
    }

    void Update()
    {
        if ( !PauseMenu.GameIsPaused && !PlayerController.isDead )
        {
            if (Input.GetButton("Shoot") && Time.time > nextFire)
            {
                nextFire = Time.time + subMGun.fireRate;
                // animPlayer.SetTrigger("ShootTrigger");    // Animacion disparo
                if (subMGun.currentAmmo > 0)
                {
                    Shoot();
                }
            }
        }
            
        if (subMGun.currentAmmo > 0)
        {
            outOfAmmoText.SetActive(false);
        }
        else
        {
            outOfAmmoText.SetActive(true);
        }
    }

    public void Shoot()
    {
        audioClip.PlayOneShot(audioClip.clip);
        bulletSpawn = Instantiate(bullet.transform, bulletSpawnPoint.transform.position, transform.rotation);
        bulletSpawn.rotation = bulletSpawnPoint.transform.rotation;
        subMGun.currentAmmo--;
    }
}
