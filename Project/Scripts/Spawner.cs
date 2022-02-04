using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public GameObject[] spawners;
    public GameObject enemy;
    public int roundNumber = 0;
    public int enemiesLive = 0;
    public int enemiesKilled = 0;
    public int enemiesOnScreen = 10;
    public int enemySpawnAmount = 2;
    private bool beingHandled = false;
    private int flag = 0;
    private float upgradeZombieHealth = 1.12f;
    private float upgradeWeaponDamage = 1.03f;
    private int delayBeforeNewRound = 3;

    public GameObject gameController;
    private GameDB gameDB;
    private Weapon pistol;
    private Weapon submachine;

    private int pistolId = 0;
    private int submachineId = 1;
    
    // Start is called before the first frame update
    void Start()
    {
        roundNumber = 0;
        gameController = GameObject.FindGameObjectWithTag("GameController");
        gameDB = gameController.GetComponent<GameDB>();
        pistol = gameDB.weapons[pistolId];
        submachine = gameDB.weapons[submachineId];
        spawners = new GameObject[10];

        for(int i = 0; i < spawners.Length; i++)
        {
            spawners[i] = transform.GetChild(i).gameObject;
        }
    }

    private void SpawnEnemy()
    {
        int spawnerID = Random.Range(0, spawners.Length);
        Instantiate(enemy, spawners[spawnerID].transform.position, spawners[spawnerID].transform.rotation);     
    }

    // Update is called once per frame
    void Update()
    {       
        if( !beingHandled )
        {
            if(enemiesLive >= enemiesOnScreen)
            {
                return;
            }
            else if((enemySpawnAmount <= enemiesOnScreen) && (flag == 0)){
                for(int i = 0; i < enemySpawnAmount; i++){
                    NextZombie();
                }
                flag = 1;
            }
            else if((enemiesLive < enemiesOnScreen)  && (flag == 2) && ((enemySpawnAmount - enemiesKilled) >= enemiesOnScreen))
            {
                NextZombie();
            }        

            if(enemiesKilled >= enemySpawnAmount)
            {
                StartCoroutine(NextRound());
            }
        }
    }   

    private void StartRound()
    {
        roundNumber = 1;
        enemySpawnAmount = 4;
        enemiesKilled = 0;

        for(int i = 0; i < enemySpawnAmount; i++)
        {
            SpawnEnemy();
        }
    }

    public void NextZombie()
    {
        if((flag == 0) || (flag == 2))
        {
            SpawnEnemy();
            enemiesLive++;
        }

    }

    IEnumerator NextRound()
    {
        enemy.GetComponent<Zombie>().health = enemy.GetComponent<Zombie>().health * upgradeZombieHealth;
        pistol.bulletDamage = pistol.bulletDamage *  upgradeWeaponDamage;
        submachine.bulletDamage = submachine.bulletDamage * upgradeWeaponDamage;
        roundNumber++;
        enemySpawnAmount += 2;
        enemiesKilled = 0;
        enemiesLive = 0;
        if(enemySpawnAmount <= enemiesOnScreen)
        {
            flag = 0;
        }
        else{
            flag = 2;
        }
        beingHandled = true;
        yield return new WaitForSeconds(delayBeforeNewRound);
        beingHandled = false;

    }      
}
