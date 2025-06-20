using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatsController : MonoBehaviour
{
    public double level;

    // Attributes
    [Header("Attributes")]
    public double vitality;            // Health
    public double endurance;           // Stamina
    public double strength;            // Damage
    public double resistance;          // Defense
    public double arcane;              // Magic
    public double luck;                // Luck

    // Equips
    [Header("Equipments")]             // Stats you get from Equipment
    public double damage;              
    public double magicDmg;
    public double criticalDmg;
    public double armorResistence;
    public double bloodDamage;
    public double defense;

    // -- Resistences --
    [Header("Resistences")]            // Resistence against certain
    public double physicResistence;    // types of damage
    public double magicResistence;
    public double electricResistence;
    public double fireResistence;
    public double iceResistence;
    public double darkResistence;

    [Header("EXP")]
    public int exp;                    // EXP Given to Player when the enemy dies

    // Components
    [HideInInspector] PlayerBehaviour player;
    [HideInInspector] HealthController healthController;
    [HideInInspector] StaminaController staminaController;

    // Start is called before the first frame update
    void Awake()
    {
        // Get Components
        player = GetComponent<PlayerBehaviour>();
        healthController = GetComponent<HealthController>();
        staminaController = GetComponent<StaminaController>();

        UpdateStats();
    }

    // ---------- Methods ---------- 

    public void UpdateStats()
    {
        healthController.maxHealth = vitality * 35;
        staminaController.maxStamina = (float)endurance * 50;
        // resistance * 20;
        // arcane * Damage / 3;

    }

}
