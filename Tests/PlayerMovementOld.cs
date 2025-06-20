using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovementOld : MonoBehaviour
{
    public bool isPaused;

    // Components
    [Header("Components")]
    public PlayerBehaviour playerBehaviour;
    public InputController inputs;
    public Rigidbody2D myRigidbody;
    public Animator myAnimator;
    public StaminaController staminaController;

    // Movement
    [Header("Movement")]
    public float speed;
    private float maxSpeed = 0.5f;
    public float run;
    public float tiredSpeed;


    public void Start()
    {
        isPaused = false;

        // Components
        playerBehaviour = GetComponent<PlayerBehaviour>();
        inputs = GetComponent<InputController>();
        myAnimator = GetComponent<Animator>();
        myRigidbody = GetComponent<Rigidbody2D>();
        staminaController = GetComponent<StaminaController>();

        // Animação - Inicia Olhando para Baixo (Animator)
        myAnimator.SetFloat("moveX", 0);
        myAnimator.SetFloat("moveY", -1);


    }

    // Update is called once per frame
    void Update()
    {
        inputs = GetComponent<InputController>();
    }

    void FixedUpdate()
    {
        // Caso não esteja Pausado
        if (!isPaused)
        {
            // ----------------------  Player Movement  ----------------------

            // Movement - Down/Up/Right/Left
            if (inputs.Horizontal != 0 || inputs.Vertical != 0 && playerBehaviour.state != StateMachine.Attacking && playerBehaviour.state != StateMachine.Interect && playerBehaviour.state != StateMachine.Stagger)
            {
                // Get Component
                inputs = GetComponent<InputController>();

                if (!staminaController.isTired)
                {
                    // Movement - 45º
                    if (inputs.Horizontal != 0 && inputs.Vertical != 0)
                    {
                        inputs.Horizontal *= maxSpeed;
                        inputs.Vertical *= maxSpeed;
                    }

                    // ------ Running ------
                    if (inputs.Run != 0)
                    {
                        // Change State to Running
                        playerBehaviour.state = StateMachine.Running;
                        myAnimator.SetBool("running", true);
                        myAnimator.SetBool("walking", false);

                        // Movement
                        float moveX = inputs.Horizontal * run;
                        float moveY = inputs.Vertical * run;
                        myRigidbody.velocity = new Vector2(moveX, moveY);
                        //  * Foi necessario criar variaveis moveX e moveY
                        //    para utilizar nos Floats do Animator

                        // Stamina Lost
                        staminaController.UseStamina(1);

                        // --- Running Animation ---

                        // Animação em 45º Baixo/Direita
                        if (inputs.Horizontal < 0 && inputs.Vertical < 0)
                        {
                            myAnimator.SetFloat("moveX", 0);
                            myAnimator.SetFloat("moveY", -1);
                        }
                        // Animação em 45º Baixo/Esquerda
                        else if (inputs.Horizontal > 0 && inputs.Vertical < 0)
                        {
                            myAnimator.SetFloat("moveX", 0);
                            myAnimator.SetFloat("moveY", -1);
                        }
                        // Animação Normal (Animator)
                        else
                        {
                            myAnimator.SetFloat("moveX", moveX);
                            myAnimator.SetFloat("moveY", moveY);
                        }
                    }

                    // ------ NOT Running ------
                    else
                    {
                        // Change State to Walking
                        playerBehaviour.state = StateMachine.Walking;
                        myAnimator.SetBool("walking", true);
                        myAnimator.SetBool("running", false);

                        // Movement
                        float moveX = inputs.Horizontal * speed;
                        float moveY = inputs.Vertical * speed;
                        myRigidbody.velocity = new Vector2(moveX, moveY);
                        //  * Foi necessario criar variaveis moveX e moveY
                        //    para utilizar nos Floats do Animator

                        // Animação em 45º Baixo/Direita
                        if (inputs.Horizontal < 0 && inputs.Vertical < 0)
                        {
                            myAnimator.SetFloat("moveX", 0);
                            myAnimator.SetFloat("moveY", -1);
                        }
                        // Animação em 45º Baixo/Esquerda
                        else if (inputs.Horizontal > 0 && inputs.Vertical < 0)
                        {
                            myAnimator.SetFloat("moveX", 0);
                            myAnimator.SetFloat("moveY", -1);
                        }
                        // Animação Normal (Animator)
                        else
                        {
                            myAnimator.SetFloat("moveX", moveX);
                            myAnimator.SetFloat("moveY", moveY);
                        }
                    }

                }
                // --- IF is Tired ---
                else
                {
                    // Change State to Walking
                    playerBehaviour.state = StateMachine.Walking;
                    myAnimator.SetBool("walking", true);
                    myAnimator.SetBool("running", false);

                    // Movement
                    float moveX = inputs.Horizontal * tiredSpeed;
                    float moveY = inputs.Vertical * tiredSpeed;
                    myRigidbody.velocity = new Vector2(moveX, moveY);
                    //  * Foi necessario criar variaveis moveX e moveY
                    //    para utilizar nos Floats do Animator

                    // Animação em 45º Baixo/Direita
                    if (inputs.Horizontal < 0 && inputs.Vertical < 0)
                    {
                        myAnimator.SetFloat("moveX", 0);
                        myAnimator.SetFloat("moveY", -1);
                    }
                    // Animação em 45º Baixo/Esquerda
                    else if (inputs.Horizontal > 0 && inputs.Vertical < 0)
                    {
                        myAnimator.SetFloat("moveX", 0);
                        myAnimator.SetFloat("moveY", -1);
                    }
                    // Animação Normal (Animator)
                    else
                    {
                        myAnimator.SetFloat("moveX", moveX);
                        myAnimator.SetFloat("moveY", moveY);
                    }
                }

            }
            else // Caso Contrario - NotWalking
            {
                playerBehaviour.state = StateMachine.Idle;
                myAnimator.SetBool("running", false);
                myAnimator.SetBool("walking", false);
                myRigidbody.velocity = new Vector2(0f, 0f);
            }
        }


        // ----------------------  END Player Movement  ----------------------


        // Caso Esteja Pausado - Not Move
        else
        {
            myRigidbody.velocity = new Vector2(0f, 0f);
            myRigidbody.mass = 5000;
            myAnimator.SetFloat("moveX", 0);
            myAnimator.SetFloat("moveY", 0);
            myAnimator.SetBool("running", false);
            myAnimator.SetBool("walking", false);

        }
    }
}
