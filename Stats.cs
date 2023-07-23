using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum StateMachine
{
    Idle,
    Walk,
    Running,
    Attack,
    Stagger,
    Interect,
    Chase,
    Dead,
}

public abstract class Stats : MonoBehaviour
{
    // State
    public StateMachine state;
    public bool isPaused;
    
    //Components
    public Rigidbody2D myRigidbody;
    public Animator myAnimator;
    public HealthController healthController;

    public virtual void Start()
    {

        myAnimator = GetComponent<Animator>();
        myRigidbody = GetComponent<Rigidbody2D>();
        healthController = GetComponent<HealthController>();
    }

}
