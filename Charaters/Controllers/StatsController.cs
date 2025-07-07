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
    public double luck;                  // Loot / Critical Damage / Dark Resistance


    // -- Resistances --
    [Header("Resistances")]              // Against certain types of damage
    public float physicResistence;      // Resistance:  Limiter = 0,5f
    public float magicResistence;       // Arcane:      Limiter = 0,6f
    public float fireResistence;        // Arcane:      Limiter = 0,4f
    public float iceResistence;         // Arcane:      Limiter = 0,4f
    public float electricResistence;    // Arcane:      Limiter = 0,4f
    public float darkResistence;        // Luck         Limiter = 0,7f

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
        healthController.maxHealth = vitality * 5.7f;
        staminaController.maxStamina = (float)endurance * 3f;
        // magicController.maxMagic = arcane * 4f;

        // --- Resistances ---
        physicResistence = (float)resistance * 0.5f;
        magicResistence = (float)arcane * 0.6f;
        fireResistence = (float)arcane * 0.4f;
        iceResistence = (float)arcane * 0.4f;
        electricResistence = (float)arcane * 0.4f;
        darkResistence = (float)luck * 0.7f;

    }

}
