using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    PlayerUI playerUI;
    GameObject spawner;
    private CharacterController controller;
    private Vector3 playerVelocity;
    public float playerSpeed;
    public float killedZombies;
    public float zombieDamage;
    public float health;
    private float noSpeed = 0.0f;
    private float gravityValue = -9.81f;
    public static bool isDead;
    Animator animPlayer;
    public GameObject gameController;
    private GameDB gameDB;
    public GameObject canvas;
    private GameOverMenu gameOverMenu;
    public GameObject camara;
    private AudioSource audioClip;

    private void Start()
    {
        playerUI = GetComponent<PlayerUI>();
        if ( PlayerPrefs.GetString("hudState") == "OFF")
        {
            GameObject[] gameObjectArray = GameObject.FindGameObjectsWithTag("UIStats");
            foreach (GameObject go in gameObjectArray)
            {
                go.SetActive(false);
            }
        }

        killedZombies = 0;
        isDead = false;
        controller = GetComponent<CharacterController>();
        animPlayer = GetComponent<Animator>();
        spawner = GameObject.FindGameObjectWithTag("Spawn");
        gameDB = gameController.GetComponent<GameDB>();
        gameOverMenu = canvas.GetComponent<GameOverMenu>();
        audioClip = GetComponent<AudioSource>();
        SetStats();
        SelectSkin();
    }

    void Update()
    {
        if (health <= 0)
        {
            Die();
            isDead = true;
        }
        else
        {
            // Player movement
            Vector3 move = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
            controller.Move(move * Time.deltaTime * playerSpeed);

            if (move != Vector3.zero)
            {
                gameObject.transform.forward = move;
                animPlayer.SetFloat("Speed_f", playerSpeed);
            }
            else
            {
                animPlayer.SetFloat("Speed_f", noSpeed);
            }

            playerVelocity.y += gravityValue * Time.deltaTime;
            controller.Move(playerVelocity * Time.deltaTime);

        }
        SetStats();
    }

    public void SelectSkin()
    {
        GameObject skins = transform.Find("Skin").gameObject;
        int selectedSkin = PlayerPrefs.GetInt("selectedSkin"), i = 0;
        foreach (Transform skin in skins.transform)
        {
            if (i == selectedSkin)
                skin.gameObject.SetActive(true);
            else
                skin.gameObject.SetActive(false);
            i++;
        }
    }

    public void TakeDamage(float zombieDamage)
    {
        health -= zombieDamage;
        SetStats();
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "ZombieFist"){
            TakeDamage(zombieDamage);
            camara.GetComponent<TopDownCamera>().ShakeScreen();
            if (health <= 0){
                Die();
            }
        }
    }

    public void KilledZombies()
    {
        killedZombies ++;
        SetStats();
    }

    public void Die()
    { 
        audioClip.PlayOneShot(audioClip.clip);
        animPlayer.SetTrigger("CharacterDeathTrigger");    // Animacion muerte
        animPlayer.SetInteger("WeaponType_int", 0);
        playerUI.DisableUI();
        gameOverMenu.GameOver(killedZombies);
        GameObject.FindGameObjectWithTag("MainCamera").GetComponent<AudioSource>().Stop();
    }

    void SetStats()
    {
        if (health > 0){
            playerUI.healthAmount.text = health.ToString();
        }
        else{
            playerUI.healthAmount.text = "0";
        }
        playerUI.killedAmount.text = killedZombies.ToString();
        playerUI.roundNumber.text = spawner.GetComponent<Spawner>().roundNumber.ToString();
        if (gameDB.weaponSelected == gameDB.getPistolId())
        {
            playerUI.currentWeapon.text = "PISTOL";
        }
        else if (gameDB.weaponSelected == gameDB.getSubMGunId())
        {
            playerUI.currentWeapon.text = "SUBMGUN";
        }
        playerUI.ammoAmount.text = gameDB.weapons[gameDB.weaponSelected].currentAmmo.ToString();
    } 
   
}
