using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackController : MonoBehaviour
{
    [Space(10)]
    public bool canAttack;

    [Header("Damage")]
    public DamageType damageType;
    public float damage;


    void Start()
    {
        canAttack = true; 
    }


    void Update()
    {
        
    }
}
