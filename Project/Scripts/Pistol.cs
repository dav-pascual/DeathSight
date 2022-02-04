using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Pistol : MonoBehaviour
{
    public float nextFire = 50f;
    public GameObject bulletSpawnPoint;
    private Transform bulletSpawn;
    Animator animPlayer;

    public GameObject gameController;
    private GameDB gameDB;
    private Weapon pistol;
    private GameObject bullet;
    private AudioSource audioClip;
    public GameObject outOfAmmoText;

    void Start()
    {
        animPlayer = transform.root.GetComponent<Animator>();
        gameDB = gameController.GetComponent<GameDB>();
        pistol = gameDB.weapons[gameDB.getPistolId()];
        bullet = pistol.bulletObject;
        pistol.currentAmmo = pistol.initAmmo;
        outOfAmmoText.SetActive(false);
        audioClip = GetComponent<AudioSource>();
    }

    void Update()
    {
        if ( !PauseMenu.GameIsPaused && !PlayerController.isDead )
        {
            if (Input.GetButtonDown("Shoot") && Time.time > nextFire)
            {
                nextFire = Time.time + pistol.fireRate;
                animPlayer.SetTrigger("ShootTrigger");    // Animacion disparo
                if (pistol.currentAmmo > 0)
                {
                    Shoot();
                }
            }
        }
            
        if (pistol.currentAmmo > 0)
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
        pistol.currentAmmo--;
    }
}
