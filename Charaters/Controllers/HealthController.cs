using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;

public class HealthController : MonoBehaviour
{
    public double currentHealth;
    public double maxHealth;
    public bool healthGoesMax;                                   // Used for Spawn Animation only

    [Header("Recover")]
    public bool recovering;                                     // Start Recover Health
    public float recoverTime;
    public float recoverValue;
    public float rechargeRate;

    [Header("Life")]
    public int life;
    public bool isDead;                                        // Used to start Death Menu or to tell Enemy is Dead

    // Components
    [HideInInspector] private StatsController status;


    void Awake()
    {
        status = GetComponent<StatsController>();
        // maxHealth = status.vitality * 35;
        recovering = false;
        recoverTime = 0;

        if (gameObject.CompareTag("Player"))
        {
            currentHealth = 1;
            healthGoesMax = true;
        }
        else
        {
            currentHealth = maxHealth;
        }
    }

    void Update()
    {
        // Just for Spawn Health Bar Animation if Player
        if (healthGoesMax)
        {
            if (currentHealth < maxHealth)
            {
                currentHealth += rechargeRate;
            }
            else
            {
                currentHealth = maxHealth;
                healthGoesMax = false;
            }

        }

    }

    private void FixedUpdate()
    {
        // Recovering By Time
        if (recovering)
        {
            if (recoverValue > recoverTime)
            {
                ResetRecoverByTime();
            }
            else
            {
                if (currentHealth < maxHealth)
                {
                    currentHealth += rechargeRate;
                }
                else
                {
                    currentHealth = maxHealth;
                    ResetRecoverByTime();
                }

                recoverValue += Time.deltaTime;

            }

        }
    }


    // --------- Methods ---------

    public void RecoverHealth(float recoveredHealth)
    {
        currentHealth += recoveredHealth;

        if (currentHealth > maxHealth)                                       // Make sure doesnt go upper than Max Healh
        {
            currentHealth = maxHealth;
        }

        Debug.Log("RecoverHealth!");
    }

    public void RecoverByTime(float RecoverTime, float RechargeRateBoost)
    {
        if (RechargeRateBoost > rechargeRate)                                // Check if has recharge Rate Boost
        {
            rechargeRate = rechargeRate * RechargeRateBoost;
        }

        recoverTime = RecoverTime;
        recovering = true;

        Debug.Log("RecoverByTime!");
    }

    public void ResetRecoverByTime()
    {
        recovering = false;
        recoverTime = 0;
        recoverValue = 0;
        rechargeRate = 0.02f;

        Debug.Log("ResetRecoverByTime!");
    }

    public void GainLife(int lifeGained)
    {
        life += lifeGained;
        Debug.Log("GainLife! = " + lifeGained);
    }

    public void TakeDamage(double damageTaken, double criticalDmgMultiplier, DamageType type)
    {
        ResetRecoverByTime();

        // ---------- Damage By Type ---------------

        // Physic Damage
        if (type == DamageType.Physic)
        {
            #region

            if (gameObject.TryGetComponent<StatsController>(out StatsController enemyStatsComp))  // Necessary ?
            {
                // Chance de Dano Critico
                double criticalChance = enemyStatsComp.luck / 100;
                double randomNumber = Random.Range((float)criticalChance, 7f);

                Debug.Log("Chance de Dano Critico: "
                 + criticalChance
                 + "Número escolhido: "
                 + randomNumber);


                // Applie Damage 
                if (criticalChance == randomNumber)
                {
                    // Critical Hit
                    double criticalDamage = damageTaken * criticalDmgMultiplier;

                    // Resistence HERE 
                    // Ex: 
                    criticalDamage = criticalDamage * status.physicResistence;

                    currentHealth = currentHealth - criticalDamage;

                    Debug.Log("Dano Critico: " + criticalDamage);
                }
                else
                {
                    // Resistence HERE 
                    // Ex: 
                    // damageTaken = damageTaken * status.physicResistence;

                    // Normal Hit
                    currentHealth -= damageTaken;

                    Debug.Log("Dano: " + damageTaken);
                }

            }
            #endregion
        }

        // ------- Teste Needed --------
        #region

        // Magic Damage
        if (type == DamageType.Magic)
        {
            if (gameObject.TryGetComponent<StatsController>(out StatsController enemyStatsComp))
            {
                // Resistencia
                damageTaken -= enemyStatsComp.magicResistence;
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
                    double criticalHit = criticalDmgMultiplier / 10;
                    criticalHit = criticalHit / 6;
                    damageTaken *= criticalHit;
                    Debug.Log("Dano Critico: " + damageTaken);
                }
                // Dano Final
                currentHealth -= damageTaken;
                Debug.Log("Dano: " + damageTaken);
            }
        }

        // Eletric Damage
        if (type == DamageType.Electric)
        {
            if (gameObject.TryGetComponent<StatsController>(out StatsController enemyStatsComp))
            {
                // Resistencia
                damageTaken -= enemyStatsComp.electricResistence;
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
                    double criticalHit = criticalDmgMultiplier / 10;
                    criticalHit = criticalHit / 6;
                    damageTaken *= criticalHit;
                    Debug.Log("Dano Critico: " + damageTaken);
                }
                // Dano Final
                currentHealth -= damageTaken;
                Debug.Log("Dano: " + damageTaken);
            }
        }

        // Fire Damage
        if (type == DamageType.Fire)
        {
            if (gameObject.TryGetComponent<StatsController>(out StatsController enemyStatsComp))
            {
                // Resistencia
                damageTaken -= enemyStatsComp.fireResistence;
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
                    double criticalHit = criticalDmgMultiplier / 10;
                    criticalHit = criticalHit / 6;
                    damageTaken *= criticalHit;
                    Debug.Log("Dano Critico: " + damageTaken);
                }
                // Dano Final
                currentHealth -= damageTaken;
                Debug.Log("Dano: " + damageTaken);
            }

        }

        // Ice Damage
        if (type == DamageType.Ice)
        {
            if (gameObject.TryGetComponent<StatsController>(out StatsController enemyStatsComp))
            {
                // Resistencia
                damageTaken -= enemyStatsComp.iceResistence;
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
                    double criticalHit = criticalDmgMultiplier / 10;
                    criticalHit = criticalHit / 6;
                    damageTaken *= criticalHit;
                    Debug.Log("Dano Critico: " + damageTaken);
                }
                // Dano Final
                currentHealth -= damageTaken;
                Debug.Log("Dano: " + damageTaken);
            }
        }

        // Dark Damage
        if (type == DamageType.Dark)
        {
            if (gameObject.TryGetComponent<StatsController>(out StatsController enemyStatsComp))
            {
                // Resistencia
                damageTaken -= enemyStatsComp.darkResistence;
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
                    double criticalHit = criticalDmgMultiplier / 10;
                    criticalHit = criticalHit / 6;
                    damageTaken *= criticalHit;
                    Debug.Log("Dano Critico: " + damageTaken);
                }
                // Dano Final
                currentHealth -= damageTaken;
                Debug.Log("Dano: " + damageTaken);
            }
        }

        // Blood Damage
        if (type == DamageType.Blood)
        {
            if (gameObject.TryGetComponent<StatsController>(out StatsController enemyStatsComp))
            {
                // Resistencia
                damageTaken -= enemyStatsComp.physicResistence;
                // Chance de Dano Critico
                double bloodChance = enemyStatsComp.bloodDamage / 10;
                double randomNumber = Random.Range(((float)bloodChance), 15);
                Debug.Log("Chance de Dano Critico: " + bloodChance
                + "Número escolhido: "
                + randomNumber);
                // Dano Critico: Valor
                if (bloodChance == randomNumber)
                {
                    double criticalHit = criticalDmgMultiplier / 10;
                    criticalHit = criticalHit / 8;
                    damageTaken *= criticalHit;
                    Debug.Log("Dano de Sangue: " + damageTaken);
                }
                // Dano Final
                currentHealth -= damageTaken;
                Debug.Log("Dano: " + damageTaken);
            }
        }

        #endregion


        // ----------------------------------------

        Debug.Log("TakeDamage!");

        if (currentHealth < 1)
        {
            currentHealth = 0;

            if (!isDead)
            {
                IsDead();   
                Debug.Log(gameObject.name + " - Vida Chegou a 0");
            }
            
        }

    }

    public void IsDead()
    {
        if (gameObject.CompareTag("Player"))                                          // If GameObject is Player
        {
            life = life - 1;                                                          // Lose 1 Life

            if (life <= 0)                                                            // If Life goes 0 
            {
                life = 0;
                isDead = true;
                Debug.Log("Player Morreu");
            }
            else                                                                      // Else Restore Health and Stamina
            {
                healthGoesMax = true;                                                 // Health Goes Max
                StaminaController staminaController;
                staminaController = GetComponent<StaminaController>();                // Stamina Goes Max
                staminaController.currentStamina = staminaController.maxStamina;
                // Respawn Player
            }
        }
        else                                                                          // If GameObject is Not Player
        {
            Debug.Log(name + "is Dead");
            isDead = true;
            Invoke("DropDead", 2);
            // NEEDED CHANGE FOR RESPAWN AGAIN
        }

    }

    // Method Needed for Invoke to work
    public void DropDead()
    {
        gameObject.TryGetComponent<LootScript>(out LootScript loot);
        loot.DropLoot();
    }

}

// Make Player Respawn when Life is Lost
// Make Enemy Respawn by Distance and time 
// Corret Other types of Damage
// Make a better Health Goes Max 




