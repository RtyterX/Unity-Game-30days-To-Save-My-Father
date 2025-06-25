using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using static UnityEditor.Progress;

public class PlayerMovement2D : MonoBehaviour
{
    public bool isPaused;

    [Header("Values")]
    [SerializeField] public float currentSpeed;
    [SerializeField] public float targetSpeed;
    [SerializeField] public Vector2 targetVelocity;

    [Space(10)]

    // Sub-Values
    public float walk;
    public float run;
    public float tired;
    public float staminaCost;    // For Runnig

    // Smooth Moviment Values
    [Header("Acceleration")]
    public float acceleration = 5f;      // Acceleration rate  
    public float deceleration = 10f;     // Deceleration rate

    [Header("Components")]
    [HideInInspector] public UnityEngine.GameObject inputs;
    [HideInInspector] public PlayerBehaviour behaviour;
    [HideInInspector] public Rigidbody2D myRigidbody;
    [HideInInspector] public StaminaController staminaController;

    public void Start()
    {
        // Get Compenents
        behaviour = GetComponent<PlayerBehaviour>();
        myRigidbody = GetComponent<Rigidbody2D>();
        staminaController = GetComponent<StaminaController>();
    }

    public void FixedUpdate()
    {
        // Movement is Not Paused
        if (!isPaused)
        {
            inputs.TryGetComponent<InputController>(out InputController input);

            // If Player goes Down/Up/Right/Left
            if (behaviour.state != StateMachine.Attacking)
            {
                if (input.Horizontal != 0 || input.Vertical != 0)
                {
                    // ---- Change Target Speed Values ---- 

                    #region
                    // Player is Tired
                    if (behaviour.state == StateMachine.Tired)
                    {
                        targetSpeed = tired;
                    }
                    else
                    {
                        // Player is Running
                        if (input.Run != 0)
                        {
                            behaviour.state = StateMachine.Running;
                            staminaController.UseStamina(staminaCost);
                            targetSpeed = run;
                        }
                        else // Player is Walking
                        {
                            behaviour.state = StateMachine.Walking;
                            targetSpeed = walk;
                        }
                    }
                    #endregion

                    // ----  Block 45° Walk ----
                    #region

                    if (input.Horizontal != 0 && input.Vertical == 0)   // Horizontal Movement
                    {
                       myRigidbody.velocity = new Vector2(currentSpeed * input.Horizontal, 0);
                       targetVelocity = new Vector2(targetSpeed * input.Horizontal, 0);
                    }
                    if (input.Vertical != 0 && input.Horizontal == 0)   // Vertical Movement
                    {
                        myRigidbody.velocity = new Vector2(0, currentSpeed * input.Vertical);
                        targetVelocity = new Vector2(0, targetSpeed * input.Vertical);
                    }

                    #endregion

                    // --------- Movement with Acceleration ---------
                    #region

                    // Check if current player velocity isnt greater than Max Velocity
                    if (myRigidbody.velocity.magnitude >= targetSpeed)
                    {
                        myRigidbody.velocity = targetVelocity;
                    }
                    else
                    {
                        // Creates a accelerationVector and apply acceleration as times passes
                        Vector2 accelerationVector = targetVelocity.normalized * (acceleration * Time.fixedDeltaTime);
                      
                        // Update player velocity
                        myRigidbody.velocity += accelerationVector;

                        // ---- Calculete Current Speed ----

                        Vector2 currentVelocity = myRigidbody.velocity;

                        // See if Vector X ou Y is the Greater Value
                        if (currentVelocity.x != 0d)
                        {
                            currentSpeed = currentVelocity.x * input.Horizontal;
                        }
                        else 
                        {
                            currentSpeed = currentVelocity.y * input.Vertical;
                        }

                    }
                    #endregion
                }

                // Player is Idle
                if (input.Horizontal == 0 && input.Vertical == 0
                    && behaviour.state != StateMachine.Attacking)
                {
                    behaviour.state = StateMachine.Idle;
                    currentSpeed = 0;
                    myRigidbody.velocity = Vector2.zero;

                    // --------- Decelerate Moviment ---------

                    // Decelerate until player stops 
                    Vector2 decelerationVector = -myRigidbody.velocity.normalized * (deceleration * Time.fixedDeltaTime);
                    myRigidbody.velocity += decelerationVector;

                    // Ensure velocity doesn't go below zero  
                    if (Vector2.Dot(myRigidbody.velocity, decelerationVector) < 0f)
                    {
                        myRigidbody.velocity = Vector2.zero;
                    }
                }
            }

        }
        else // If Movement is Paused
        {
            myRigidbody.velocity = Vector2.zero;     // Stop any Movement
            myRigidbody.mass = 5000;                 // Necessary?
            currentSpeed = 0;
        }
    }
}



// ----- Testes /  Melhorias -----

// Trocar de horizontal pra Vertial ao trocar de Input
// Remover curvatura enquanto a velocidade maxima não é alcançada (criar currentSpeed)
// Quando Atacar, não perder a velocidade
// Checar necessidade de alterar a massa quando pausado



