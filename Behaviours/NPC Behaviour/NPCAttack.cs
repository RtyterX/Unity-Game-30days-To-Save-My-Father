using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCAttack : MonoBehaviour
{
    // Components
    [Header("Components")]
    public NPCBehaviour behaviour;
    public HealthController health;
    public GameObject hit;

    // Timer
    [Header("Timer")]
    public bool activeTimer;
    public float timer;
    public float delay;

    [Space(10)]
    public bool canAttack;


    void Start()
    {
        // Get Components
        health = GetComponent<HealthController>();
        behaviour = GetComponent<NPCBehaviour>();

        // Start Values
        hit.SetActive(false);
        activeTimer = true;
        canAttack = false;
    }

    
    void Update()
    {
        // Check: if NPC get hurt, enter Attack Mode
        if (health.health != health.maxHealth)
        {
            behaviour.mode = NPCMode.Attack;
            canAttack = true;
        }

        if (canAttack)
        {
            if (behaviour.attackRadius <  0)
            {
                Invoke("Attack", 1.2f);
                activeTimer = true;
                canAttack = false;
            }
        }

    }


    // Timer
    public void FixedUpdate()
    {
        if (activeTimer)
        {
            if (timer >= delay)
            {
                activeTimer = false;
                canAttack = true;
            }

            timer += Time.deltaTime;
        }
    }


    // ---------- Methods ----------

    public void Attack()
    {
        hit.SetActive(true);
    }
   
    public void OnCollisionEnter2D(Collider2D other)
    {
        behaviour.target = other.gameObject;
    }

}
