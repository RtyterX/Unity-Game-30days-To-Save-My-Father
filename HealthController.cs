using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthController : MonoBehaviour
{
    public PlayerBehaviour player;
    public EnemyBehaviour enemy;

    public bool isDead;
    public bool healthGoesMax;

    [SerializeField] public double health;
    [SerializeField] public double maxHealth;

    [SerializeField] public int life;

    [SerializeField] public Slider healthBar;


    public void TakeDamage(double damage, double criticalValue, DamageType type)
    {
        // Physic Damage
        if (type == DamageType.Physic)
        {
            if (gameObject.TryGetComponent<StatsController>(out StatsController enemyStatsComp))
            {

                // Resistencia
                damage -= enemyStatsComp.physicResistence;


                // Chance de Dano Critico
                double criticalChance = enemyStatsComp.luck / 50;
                double randomNumber = Random.Range(((float)criticalChance), 7f);


                Debug.Log("Chance de Dano Critico: "
                            + criticalChance
                            + "Número escolhido: "
                            + randomNumber);

                // Dano Critico: Valor
                if (criticalChance == randomNumber)
                {
                    double criticalHit = criticalValue / 10;
                    criticalHit = criticalHit / 6;
                    damage *= criticalHit;

                    Debug.Log("Dano Critico: " + damage);
                }

                // Dano Final
                health -= damage;

                Debug.Log("Dano: " + damage);
            }
        }


        // Magic Damage
        if (type == DamageType.Magic)
        {

            if (gameObject.TryGetComponent<StatsController>(out StatsController enemyStatsComp))
            {
                // Resistencia
                damage -= enemyStatsComp.magicResistence;


                // Chance de Dano Critico

                double criticalChance = enemyStatsComp.luck / 60;
                double randomNumber = Random.Range(((float)criticalChance), 7);

                Debug.Log("Chance de Dano Critico: "
                             + criticalChance
                             + "Número escolhido: "
                             + randomNumber);

                // Dano Critico: Valor
                if (criticalChance == randomNumber)
                {
                    double criticalHit = criticalValue / 10;
                    criticalHit = criticalHit / 6;
                    damage *= criticalHit;

                    Debug.Log("Dano Critico: " + damage);
                }

                // Dano Final
                health -= damage;

                Debug.Log("Dano: " + damage);
            }

        }


        // Eletric Damage
        if (type == DamageType.Electric)
        {
            if (gameObject.TryGetComponent<StatsController>(out StatsController enemyStatsComp))
            {

                // Resistencia
                damage -= enemyStatsComp.electricResistence;



                // Chance de Dano Critico
                double criticalChance = enemyStatsComp.luck / 50;
                double randomNumber = Random.Range(((float)criticalChance), 7);

                Debug.Log("Chance de Dano Critico: "
                            + criticalChance
                            + "Número escolhido: "
                            + randomNumber);

                // Dano Critico: Valor
                if (criticalChance == randomNumber)
                {
                    double criticalHit = criticalValue / 10;
                    criticalHit = criticalHit / 6;
                    damage *= criticalHit;

                    Debug.Log("Dano Critico: " + damage);
                }

                // Dano Final
                health -= damage;

                Debug.Log("Dano: " + damage);
            }
        }


        // Fire Damage
        if (type == DamageType.Fire)
        {

            if (gameObject.TryGetComponent<StatsController>(out StatsController enemyStatsComp))
            {
                // Resistencia
                damage -= enemyStatsComp.fireResistence;


                // Chance de Dano Critico
                double criticalChance = enemyStatsComp.luck / 50;
                double randomNumber = Random.Range(((float)criticalChance), 7);

                Debug.Log("Chance de Dano Critico: "
                            + criticalChance
                            + "Número escolhido: "
                            + randomNumber);

                // Dano Critico: Valor
                if (criticalChance == randomNumber)
                {
                    double criticalHit = criticalValue / 10;
                    criticalHit = criticalHit / 6;
                    damage *= criticalHit;

                    Debug.Log("Dano Critico: " + damage);
                }

                // Dano Final
                health -= damage;

                Debug.Log("Dano: " + damage);
            }
        
        }


        // Ice Damage
        if (type == DamageType.Ice)
        {
            if (gameObject.TryGetComponent<StatsController>(out StatsController enemyStatsComp))
            {
                // Resistencia
                damage -= enemyStatsComp.iceResistence;


                // Chance de Dano Critico
                double criticalChance = enemyStatsComp.luck / 50;
                double randomNumber = Random.Range(((float)criticalChance), 7);

                Debug.Log("Chance de Dano Critico: "
                            + criticalChance
                            + "Número escolhido: "
                            + randomNumber);

                // Dano Critico: Valor
                if (criticalChance == randomNumber)
                {
                    double criticalHit = criticalValue / 10;
                    criticalHit = criticalHit / 6;
                    damage *= criticalHit;

                    Debug.Log("Dano Critico: " + damage);
                }

                // Dano Final
                health -= damage;

                Debug.Log("Dano: " + damage);
            }
        }


        // Dark Damage
        if (type == DamageType.Dark)
        {
            if (gameObject.TryGetComponent<StatsController>(out StatsController enemyStatsComp))
            {

                // Resistencia
                damage -= enemyStatsComp.darkResistence;

                // Chance de Dano Critico
                double criticalChance = enemyStatsComp.luck / 50;
                double randomNumber = Random.Range(((float)criticalChance), 7);

                Debug.Log("Chance de Dano Critico: "
                          + criticalChance
                          + "Número escolhido: "
                          + randomNumber);

                // Dano Critico: Valor
                if (criticalChance == randomNumber)
                {
                    double criticalHit = criticalValue / 10;
                    criticalHit = criticalHit / 6;
                    damage *= criticalHit;

                    Debug.Log("Dano Critico: " + damage);
                }

                // Dano Final
                health -= damage;

                Debug.Log("Dano: " + damage);
            }
        }


        // Blood Damage
        if (type == DamageType.Blood)
        {
            if (gameObject.TryGetComponent<StatsController>(out StatsController enemyStatsComp))
            {
                // Resistencia
                damage -= enemyStatsComp.physicResistence;

                // Chance de Dano Critico
                double bloodChance = enemyStatsComp.bloodDamage / 10;
                double randomNumber = Random.Range(((float)bloodChance), 15);

                Debug.Log("Chance de Dano Critico: " + bloodChance
                                                     + "Número escolhido: "
                                                     + randomNumber);

                // Dano Critico: Valor
                if (bloodChance == randomNumber)
                {
                    double criticalHit = criticalValue / 10;
                    criticalHit = criticalHit / 8;
                    damage *= criticalHit;

                    Debug.Log("Dano de Sangue: " + damage);
                }

                // Dano Final
                health -= damage;
                UpdateHealthBar();

                Debug.Log("Dano: " + damage);
            }
        }

        CheckIsDead();

    }

    public void CheckIsDead()
    {
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
  
        }
    }

    public void UpdateHealthBar()
    {
        healthBar.value = (int)health / (int)maxHealth;
    }

    IEnumerator MaxHealthCo()
    {
        health = maxHealth;
        healthGoesMax = false;
        yield return new WaitForSeconds(0f);
    }
}
