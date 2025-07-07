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
        if (currentHealth >= 1)                                                  // Stop applying damage if Obj is Dead (health below 0)
        {
            ResetRecoverByTime();                                                // Stop if Obj is recovering Health by time
            ApplyDamage(type, damageTaken);                                      // Apply Damage

            if (currentHealth < 1)                                               // Check if Obj is Dead
            {
                currentHealth = 0;                                               // Ensure health is 0
                Invoke("IsDead", 1.5f);                                          // Start Death Method with Delay
            }
            else
            {
                BehaviourController obj = GetComponent<BehaviourController>();
                obj.state = StateMachine.Stagger;                                // Change Obj State to Stagger

                if (TryGetComponent<KnockBack>(out KnockBack thisObj))
                {
                    thisObj.DoKnockBack((float)damageTaken, direction);          // Do KnockBack only if Obj has the script
                }
            }
        }
    }


    public void ApplyDamage(DamageType type, double damageTaken)
    {
        StatsController stats = GetComponent<StatsController>();

        if (type == DamageType.Physic)            // Physic
        {
            damageTaken = damageTaken * (1f - (((float)stats.resistance - 1) * 0.005f));
        }
        if (type == DamageType.Magic)             // Magic
        {
            damageTaken = damageTaken * (1f - (((float)stats.arcane - 1) * 0.006f));
        }
        if (type == DamageType.Fire)              // Fire
        {
            damageTaken = damageTaken * (1f - (((float)stats.arcane - 1) * 0.004f));
        }
        if (type == DamageType.Ice)              // Ice
        {
            damageTaken = damageTaken * (1f - (((float)stats.arcane - 1) * 0.004f));
        }
        if (type == DamageType.Electric)         // Electric
        {
            damageTaken = damageTaken * (1f - (((float)stats.arcane - 1) * 0.004f));
        }
        if (type == DamageType.Dark)             // Dark
        {
            damageTaken = damageTaken * (1f - (((float)stats.luck - 1) * 0.007f));
        }

        currentHealth -= damageTaken;            // Apply Damage
        damageUI.text = damageTaken.ToString();  // Teste with damage showing on screen

        // Debug.Log(name + " lost " + damageTaken + " of Health");
    }


    public void IsDead()
    {
        TryGetComponent<BehaviourController>(out BehaviourController obj);

        if (gameObject.CompareTag("Player"))                               // Player
        {
            LostLife();                                                                     // Lose 1 Life
            if (life <= 0)                                                                  // If Life goes below 0...
            {
                life = 0;                                                                   // Ensure life is 0
                isDead = true;
                obj.ChangeState(StateMachine.Dead);                                         // Change Player State to Dead
                Debug.Log("Player Morreu");
            }
            else                                                                            // if life is not below 0
            {
                DropInventory();                                                            // Drop Inventory
                RespawnPlayer();                                                            // Respawn Player
            }
        }
        else                                                               // Not Player
        {
            isDead = true;
            obj.ChangeState(StateMachine.Dead);                                             // Change obj State to Dead
            Invoke("DropDead", 2);                                                          // Drop Loot
        }

    }

    public void RespawnPlayer()
    {
        healthGoesMax = true;                                                               // Health Goes Max (animation)
        StaminaController staminaController = GetComponent<StaminaController>();
        staminaController.currentStamina = staminaController.maxStamina;                    // Stamina Goes Max
        TryGetComponent<RespawnPointer>(out RespawnPointer respawn);
        respawn.DoRespawn();                                                                // Respawn Player
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
        // Make it Later
    }

}


// Make a better Health Goes Max 









