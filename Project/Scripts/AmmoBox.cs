using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmmoBox : MonoBehaviour
{
    public GameObject gameController;
    private GameDB gameDB;
    public int ammoPistolPickUp, ammoSubMGunPickUp;
    private AudioSource audioClip;

    // Start is called before the first frame update
    void Start()
    {
        gameDB = gameController.GetComponent<GameDB>();
        audioClip = GetComponent<AudioSource>();
    }

    void OnTriggerEnter(Collider other)  // Player picks up ammo
    {
        if (other.tag == "Player")  
        {
            if (gameDB.getPistolId() == gameDB.weaponSelected)
            {
                gameDB.weapons[gameDB.getPistolId()].currentAmmo += ammoPistolPickUp;
            }
            else if (gameDB.getSubMGunId() == gameDB.weaponSelected)
            {
                gameDB.weapons[gameDB.getSubMGunId()].currentAmmo += ammoSubMGunPickUp;
            }

            AudioSource.PlayClipAtPoint(audioClip.clip, GameObject.FindGameObjectWithTag("MainCamera").transform.position, 1f);
            this.gameObject.SetActive(false);
        }
    }
}
