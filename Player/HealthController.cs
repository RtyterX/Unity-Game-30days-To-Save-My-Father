using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthController : MonoBehaviour
{
    // Components
    [Header("Components")]
    [SerializeField] private PlayerBehaviour player;
    [SerializeField] private PlayerStatus statusPlayer;

    // Health
    [Header("Health")]
    public double health;
    public double maxHealth;
    public bool healthGoesMax;

    // Life
    [Header("Life")]
    public int life;
    public bool isDead;

    // Health Bar - Life 
    [SerializeField] private Slider healthBar;
    [SerializeField] private Text lifeUI;


    void Awake()
    {
        healthGoesMax = true;
    }

    void Update()
    {
        // Check Max Health Value
        if (statusPlayer)
        {
            // Checa se o valor de Vida Maxima alterou
        }

        // Check if Health Goes Max
        if (healthGoesMax)
        {
            StartCoroutine(MaxHealthCo());
        }

        // Update UI
        if (healthBar)
        {
            UpdateHealthBar();
        }
        if (lifeUI)
        {
            UpdateLifeUI();
        }

    }


    // --------- Methods --------- 

    public void TakeDamage(double damage, double criticalValue, DamageType type)
    {

        // ---------- Damage By Type ---------------


        // ******* NEED TO CHANGE LATER ******* 


        // Physic Damage
        if (type == DamageType.Physic)
        {
            if (gameObject.TryGetComponent<StatsController>(out StatsController enemyStatsComp))
            {

                // Resistencia
               // damage -= enemyStatsComp.physicResistence;


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
                else
                {
                    // Applied Damage
                    health -= damage;

                    Debug.Log("Dano: " + damage);
                }

                // Check if Health is Below 0
                CheckIsDead();

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

        // ----------------------------------------

    }

    public void CheckIsDead()
    {
        if (!isDead)
        {
            if (health <= 0)
            {
                Debug.Log("Vida Chegou a 0");
                health = 0;

                // Check if Game Object is player
                if (player) 
                {
                    player.PlayerRespawn();
                }
                else
                {
                    // ******* NEED TO CHANGE LATER ******* 
                    Destroy(gameObject);
                }

            }
            else
            {
                if (player.canKnockBack)
                {
                    StartCoroutine(player.KnockBackCo());
                }
            }
        }
    }

    // Health Bar
    public void UpdateHealthBar()
    {
        // healthBar.value = (int)health / (int)maxHealth;
    }

    // Life Counter UI
    public void UpdateLifeUI()
    {
        lifeUI.text = life.ToString("00");
    }


    // --------- Coroutine --------- 
    IEnumerator MaxHealthCo()
    {
        health = maxHealth;
        healthGoesMax = false;
        yield return new WaitForSeconds(0f);
    }
}
