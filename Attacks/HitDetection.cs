using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitDetection : MonoBehaviour
{
    [Header("GameObjects")]
    public GameObject thisGameObj;
    public GameObject otherGameObj;

    [Space(10)]
    [Header("Components")]
    [SerializeField] private Rigidbody2D myRigidbody;
    [SerializeField] private BehaviourController objBehaviour;

    [Header("Hit Detection")]
    public bool wasHit;


    private void OnCollisionEnter2D(Collision2D col)
    {
        wasHit = true;

        myRigidbody = thisGameObj.GetComponent<Rigidbody2D>();

        Debug.Log("Encostou no Inimigo");

        // GameObjects Check
        otherGameObj = col.gameObject;
        thisGameObj = gameObject;

        if (otherGameObj.TryGetComponent<HealthController>(out HealthController otherObjHealth) &&
            thisGameObj.TryGetComponent<AttackStatus>(out AttackStatus attack))
        {
            // Aplica Dano no Player
            otherObjHealth.TakeDamage(attack.damage, 1, attack.damageType);
        }

    }
}

