using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class HealthController : MonoBehaviour
{
    public Player player;
    public Enemy enemy;

    public bool isDead;
    public bool healthGoesMax;

    [SerializeField] public double health;
    [SerializeField] public double maxHealth;

    [SerializeField] public int life;


    public void TakeDamage(double damage)
    {
        health -= damage;
        Debug.Log("Perdeu " + damage + " de Dano");

        if (isDead == false)
        {
            if (health <= 0)
            {
                Debug.Log("Vida Chegou a 0");
                health = 0;

                if (!player)
                {
                    StartCoroutine(enemy.DeathCo());
                }
                else
                {
                    StartCoroutine(player.RespawnCo());
                }
            }
            else
            {
                return;
            }

            if (healthGoesMax)
            {
                StartCoroutine(MaxHealthCo());
            }
            else
            {
                health -= damage;
            }

        }


        IEnumerator MaxHealthCo()
        {
            health = maxHealth;
            healthGoesMax = false;
            yield return new WaitForSeconds(0f);
        }

    }
}
