using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatsController : MonoBehaviour
{
    // -- Attributes --
    [Header("Attributes")]
    public double vitality;              // Health
    public double endurance;             // Stamina
    public double strength;              // Physical Damage
    public double resistance;            // Physical Resistance
    public double arcane;                // Magic / Magic Damage / Magic Resitance
    public double luck;                  // Loot Luck / Critical Damage


    // -- Resistances --
    [Header("Resistances")]              // Against certain types of damage
    public float physicResistence;      // Resistance:  Limiter = 0,5f
    public float magicResistence;       // Arcane:      Limiter = 0,6f
    public float fireResistence;        // Arcane:      Limiter = 0,4f
    public float iceResistence;         // Arcane:      Limiter = 0,4f
    public float electricResistence;    // Arcane:      Limiter = 0,4f
    public float darkResistence;

    [Header("Boosts")]
    // public List<double> Boosts = new List<double>();     // Make in list???  


    [Header("EXP")]
    public int exp;                      // EXP Given to Player when the enemy dies

    // -- Components --
    [HideInInspector] HealthController healthController;
    [HideInInspector] StaminaController staminaController;
    // [HideInInspector] MagicController magicController;


    void Awake()
    {
        // Get Components
        healthController = GetComponent<HealthController>();
        staminaController = GetComponent<StaminaController>();
        // magicController = GetComponent<MagicController>();

        UpdateStats();
    }

    // ---------- Methods ---------- 

    public void UpdateStats()
    {
        // Check Boosts


        // --- Attributes ---
        healthController.maxHealth = vitality * 35;
        staminaController.maxStamina = (float)endurance * 50;
        // magicController.maxMagic = arcane * 10;


        // --- Resistances ---
        physicResistence = 1f - (((float)resistance - 1) * 0.005f);
        magicResistence = 1f - (((float)arcane - 1) * 0.006f);
        fireResistence = 1f - (((float)arcane - 1) * 0.004f);
        iceResistence = 1f - (((float)arcane - 1) * 0.004f);
        electricResistence = 1f - (((float)arcane - 1) * 0.004f);
    }

}
