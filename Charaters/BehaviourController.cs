using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum StateMachine
{
    Paused,
    Idle,
    Walking,
    Running,
    Attacking,
    Tired,
    Stagger,
    Interect,
    Chase,
    Dead,
}

public abstract class BehaviourController : MonoBehaviour
{
    [Header("State")]
    public StateMachine state;

    [Header("Components")]
    [HideInInspector] public Rigidbody2D myRigidbody;
    [HideInInspector] public Animator myAnimator;
    [HideInInspector] public HealthController health;
    [HideInInspector] public StaminaController stamina;

    public virtual void Start()
    {
        myAnimator = GetComponent<Animator>();
        myRigidbody = GetComponent<Rigidbody2D>();
        health = GetComponent<HealthController>();
        stamina = GetComponent<StaminaController>();
    }

}
