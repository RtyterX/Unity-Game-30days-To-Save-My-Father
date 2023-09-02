using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackStatus : MonoBehaviour
{
    // Components
    [Header("Components")]
    [SerializeField] private HitDetection hitDetection;
    [SerializeField] private GameObject thisGameObject;
    [SerializeField] private StatsController objStatus;

    // Damage
    [Header("Damage")]
    public DamageType damageType;
    public double damage;


    void Start()
    {
      
    }

    void Update()
    {
        if (objStatus)
        {
            damage = objStatus.damage;
        }
    }
}
