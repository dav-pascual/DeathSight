using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Zombie : MonoBehaviour
{
    public float stoppingDistance;
    UnityEngine.AI.NavMeshAgent agent;
    GameObject target;
    public float health;
    Animator animZombie;
    public float deadTime;
    public GameObject m_RightFist;
    GameObject spawn;
    public bool dead = false;

    // Start is called before the first frame update
    void Start()
    {
        animZombie = GetComponent<Animator>();
        agent = GetComponent<UnityEngine.AI.NavMeshAgent>();
        target = GameObject.FindGameObjectWithTag("Player");
        spawn = GameObject.FindGameObjectWithTag("Spawn");

        // Poner skin al azar
        GameObject skins = transform.Find("Skin").gameObject;
        int randomSkin = Random.Range(0, skins.transform.childCount), i=0;
        foreach (Transform skin in skins.transform) 
        {
            if (i == randomSkin)
                skin.gameObject.SetActive(true);
            else
                skin.gameObject.SetActive(false);
            i++;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (health <= 0 && dead == false)
        {
            dead = true;
            Die();
        }
        else if (health <= 0 && dead == true)
            return;
        else
        {
            if (target.GetComponent<PlayerController>().health > 0) // Si el jugador esta vivo ir hacia el
            {
                float dist = Vector3.Distance(transform.position, target.transform.position);
                if (dist < stoppingDistance)
                {
                    transform.LookAt(target.transform);  // Zombie mirando hacia jugador cuando ataca
                    StopEnemy();
                    GetComponent<Animator>().SetTrigger("Attack");
                }
                else
                {
                    GoToTarget();
                }
            }
            else
            {
                animZombie.SetBool("IdleBool", true);
                StopEnemy();
            }
        }
    }

    private void GoToTarget(){
        agent.isStopped = false;
        agent.SetDestination(target.transform.position);
    }

    public void activateFist()
    {
        m_RightFist.GetComponent<Collider>().enabled = true;
    }

    public void deactivateFist()
    {
        m_RightFist.GetComponent<Collider>().enabled = false;
    }

    private void StopEnemy(){
        agent.isStopped = true;
    }

    public void TakeDamage(float bulletDamage)
    {
        health -= bulletDamage;
    }

    public void Die()
    {
        deactivateFist();
        agent.isStopped = true;
        animZombie.SetTrigger("ZombieDeath");    // Animacion muerte zombie
        target.GetComponent<PlayerController>().KilledZombies();
        spawn.GetComponent<Spawner>().enemiesKilled++;
        spawn.GetComponent<Spawner>().enemiesLive--;
        Destroy(this.gameObject, deadTime);
        (gameObject.GetComponent(typeof(Collider)) as Collider).isTrigger = true;  // El cadaver se puede atravesar
    }

}
