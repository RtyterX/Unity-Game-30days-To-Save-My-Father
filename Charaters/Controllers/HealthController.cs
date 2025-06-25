using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.UI;

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

    [SerializeField] public Text damageUI;


    void Start()
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
    }

    public void GainLife(int lifeGained)  // Only for Player
    {
        life += lifeGained;
        Debug.Log("Life +" + lifeGained);
    }

    public void LostLife()  // Only for Player
    {
        life -= 1;
        Debug.Log("Player Lost 1 Life");
    }


    public void TakeDamage(double damageTaken, DamageType type, Vector2 direction)
    {
        if (currentHealth <= 0)
        {
            ResetRecoverByTime();
            CalculateResistenceByType(type, damageTaken);

            if (currentHealth < 1)
            {
                currentHealth = 0;
                IsDead();
            }
            else
            {
                if (TryGetComponent<KnockBack>(out KnockBack thisObj))
                {
                    thisObj.DoKnockBack((float)damageTaken, direction);
                }
            }
        }

    }

    public void IsDead()
    {
        TryGetComponent<BehaviourController>(out BehaviourController obj);

        if (gameObject.CompareTag("Player"))                                     // Player
        {
            LostLife();                                                                    // Lose 1 Life
            if (life <= 0)                                                                 // If Life goes below 0 
            {
                life = 0;
                isDead = true;

                obj.ChangeState(StateMachine.Dead);
                Debug.Log("Player Morreu");
            }
            else                                                                            // if life is not below 0
            {                                                                               // Restore Health and Stamina
                healthGoesMax = true;                                                       // Health Goes Max (animation)
                StaminaController staminaController = GetComponent<StaminaController>();    // Stamina Goes Max
                staminaController.currentStamina = staminaController.maxStamina;
                DropInventory();
                RespawnPlayer();
            }
        }
        else                                                                      // Not Player
        {
            isDead = true;
            Invoke("DropDead", 2);
            obj.ChangeState(StateMachine.Dead);
        }

    }

    public void RespawnPlayer()
    {
        TryGetComponent<RespawnPointer>(out RespawnPointer respawn);
        respawn.DoRespawn();
    }


    // Method Needed for Invoke to work
    public void DropDead()
    {
        if (gameObject.TryGetComponent<LootScript>(out LootScript loot))
        {
            loot.DropLoot();
        }
    }

    public void DropInventory()
    {

    }


    public void CalculateResistenceByType(DamageType type, double damageTaken)
    {
        StatsController stats = GetComponent<StatsController>();
     
        if (type == DamageType.Physic)
        {
            damageTaken = damageTaken * stats.physicResistence;
        }
        if (type == DamageType.Magic)
        {
            damageTaken = damageTaken * stats.magicResistence;
        }
        if (type == DamageType.Fire)
        {
            damageTaken = damageTaken * stats.fireResistence;
        }
        if (type == DamageType.Ice)
        {
            damageTaken = damageTaken * stats.iceResistence;
        }
        if (type == DamageType.Electric)
        {
            damageTaken = damageTaken * stats.electricResistence;
        }
        if (type == DamageType.Dark)
        {
            damageTaken = damageTaken * stats.darkResistence;
        }

        currentHealth -= damageTaken;
        damageUI.text = damageTaken.ToString();
        // Debug.Log(name + " lost " + damageTaken + " of Health");
    }
}


// Make a better Health Goes Max 




