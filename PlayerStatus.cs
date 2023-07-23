using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStatus : MonoBehaviour
{

      // Components
    HealthController healthController;
    Player player;


      // Atributos
    public double vitality;
    public double endurance;
    public double strength;
    public double resistance;
    public double arcane;

    // Outros Status
    public double stamina;
    public double equipLoad;
    public double defense;
    public double attackDamage;
    public double magicDamage;
    public double criticalDmg;

    // Equips
    public double weaponDmg;
    public double weaponMagicDmg;
    public double weaponCriticalDmg;
    public double armorResistence;



    void Start()
    {
        player = GetComponent<Player>();
        healthController = GetComponent<HealthController>();


        healthController.maxHealth = vitality * 1.3;
    }

      // Update para que ele verifique constante mente o valor, e nao só quando ele for alterado
    void Update()
    {
        healthController.maxHealth    =  vitality   *  1.3;
        
        stamina      =  endurance  *  1.5;
        defense      =  resistance *  armorResistence;
        attackDamage =  strength   *  weaponDmg;
        magicDamage  =  arcane     *  weaponMagicDmg;
        criticalDmg  =  strength   *  weaponCriticalDmg;

    }
}
