using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponController : MonoBehaviour
{
    //[SerializeField] private int weaponSelected = 0;
    private Vector3 vIni;
    public GameObject gameController;
    private GameDB gameDB;
    public GameObject outOfAmmoText;

    public GameObject spawners;
    private Spawner spawner;
    public bool isUnlocked;
    private bool isNotified;
    public float msgTime;
    public GameObject unlockedText;

    void Awake()
    {
        vIni = transform.rotation.eulerAngles;  // Rotacion inicial
    }

    void LateUpdate()
    {
        // Mantenemos rotacion del eje Z de weapon (mantener arma derecha)
        Vector3 vAux = transform.rotation.eulerAngles;
        transform.rotation = Quaternion.Euler(vAux.x, vAux.y, vIni.z);
    }

    void Start()
    {
        spawner = spawners.GetComponent<Spawner>();
        isNotified = false;
        isUnlocked = false;
        unlockedText.SetActive(false);

        gameDB = gameController.GetComponent<GameDB>();
        SwapWeapon();
    }

    void Update()
    {
        int previousWeaponSelected = gameDB.weaponSelected;
        
        if (spawner.roundNumber >= gameDB.unlockSubMRound)  // Is submgun unlocked?
        {
            if (!isNotified)
            {
                unlockedText.SetActive(true);
                Invoke("hideUnlockedMsg", msgTime);
                isNotified = true;
            }
            isUnlocked = true;
        }

        if ( !PauseMenu.GameIsPaused && !PlayerController.isDead )
        {
            if (Input.GetKeyDown(KeyCode.Period) || Input.GetKeyDown(KeyCode.X))  // Se presiona la tecla para next weapon
            {
                if (gameDB.weaponSelected >= transform.childCount - 1)
                    gameDB.weaponSelected = 0;
                else
                    gameDB.weaponSelected++;
            }
            if (Input.GetKeyDown(KeyCode.Comma) || Input.GetKeyDown(KeyCode.Z))  // Se presiona la tecla para previous weapon
            {
                if (gameDB.weaponSelected <= 0)
                    gameDB.weaponSelected = transform.childCount - 1;
                else
                    gameDB.weaponSelected--;
            }
        }

        if (previousWeaponSelected != gameDB.weaponSelected)
        {
            if (gameDB.weaponSelected == gameDB.getSubMGunId() && !isUnlocked)
            {
                gameDB.weaponSelected = previousWeaponSelected;
            }
            SwapWeapon();
        }
    }

    void hideUnlockedMsg()
    {
        unlockedText.SetActive(false);
    }

    void SwapWeapon()
    {
        int i = 0;
        foreach (Transform weapon in transform)  // Weapons tienen un indice asignado automaticamente segun esten ordenados en jerarquia
        {
            if (i == gameDB.weaponSelected)
                weapon.gameObject.SetActive(true);
            else
                weapon.gameObject.SetActive(false);
            i++;
        }
        outOfAmmoText.SetActive(false);
    }
}
