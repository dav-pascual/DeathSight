using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    private float distance = 0f;
    private GameObject triggeringEnemy, enemyChild;

    private GameObject gameController;
    private GameDB gameDB;
    private Weapon weapon;

    void Start()
    {
        gameController = GameObject.FindGameObjectWithTag("GameController");
        gameDB = gameController.GetComponent<GameDB>();
        weapon = gameDB.weapons[gameDB.weaponSelected];
    }

    void Update()
    {
        transform.Translate(Vector3.forward * Time.deltaTime * weapon.bulletSpeed);
        distance += 1 * Time.deltaTime;
        if (distance >= weapon.bulletLife)
        {
            Destroy(this.gameObject);
        }
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Zombie")  // Si la bala colisiona con zombie, le resta vida
        {
            triggeringEnemy = other.gameObject;
            if (triggeringEnemy.GetComponent<Zombie>().dead == false)
            {
                Destroy(this.gameObject);
                triggeringEnemy.GetComponent<Zombie>().TakeDamage(weapon.bulletDamage);
            }
        }
    }
}
