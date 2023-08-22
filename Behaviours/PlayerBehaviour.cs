using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerBehaviour : BehaviourController
{
    // Components
    [Header("Components")]
    public InputController inputs;
    public PlayerStatus playerStatus;
    public StaminaController staminaController;
    BoxCollider2D boxCollider;
    public GameObject deathMenuUI;

    // Respawn
    [Header("Respawn")]
    public Vector2 respawnLocation;
    RespawnPointer respawnPoint;

    // Movement
    [Header("Movement")]
    public float speed;
    private float maxSpeed = 0.5f;
    public float run;
    public float tiredSpeed;



    public void Start()
    {
        // Inicial State
        state = StateMachine.Idle;
        isPaused = false;
     
        // Components
        inputs = GetComponent<InputController>();
        playerStatus = GetComponent<PlayerStatus>();
        boxCollider =  GetComponent<BoxCollider2D>();
        myAnimator = GetComponent<Animator>();
        myRigidbody = GetComponent<Rigidbody2D>();
        healthController = GetComponent<HealthController>();
        staminaController = GetComponent<StaminaController>();

        // Respawn
        respawnPoint = GetComponent<RespawnPointer>();
        respawnLocation = respawnPoint.respawnPosition;


        // Animação - Inicia Olhando para Baixo (Animator)
        myAnimator.SetFloat("moveX", 0);
        myAnimator.SetFloat("moveY", -1);

    }

    // Update is called once per frame
    void Update()
    {
        inputs = GetComponent<InputController>();
        StartCoroutine(CheckRespawnLocation());
    }

    void FixedUpdate()
    {
           // Caso não esteja Pausado
        if (!isPaused)
        {
            // ----------------------  Player Movement  ----------------------

            // Movement - Down/Up/Right/Left
            if (inputs.inputHorizontal != 0 || inputs.inputVertical != 0 && state != StateMachine.Attack && state != StateMachine.Interect && state != StateMachine.Stagger)
            {
                // Get Component
                inputs = GetComponent<InputController>();

                if (!staminaController.isTired)
                {
                    // Movement - 45º
                    if (inputs.inputHorizontal != 0 && inputs.inputVertical != 0)
                    {
                        inputs.inputHorizontal *= maxSpeed;
                        inputs.inputVertical *= maxSpeed;
                    }

                    // ------ Running ------
                    if (inputs.inputRunning != 0)
                    {
                        // Change State to Running
                        state = StateMachine.Running;
                        myAnimator.SetBool("running", true);
                        myAnimator.SetBool("walking", false);

                        // Movement
                        float moveX = inputs.inputHorizontal * run;
                        float moveY = inputs.inputVertical * run;
                        myRigidbody.velocity = new Vector2(moveX, moveY);
                        //  * Foi necessario criar variaveis moveX e moveY
                        //    para utilizar nos Floats do Animator

                        // Stamina Lost
                        staminaController.UseStamina(1);

                        // --- Running Animation ---

                        // Animação em 45º Baixo/Direita
                        if (inputs.inputHorizontal < 0 && inputs.inputVertical < 0)
                        {
                            myAnimator.SetFloat("moveX", 0);
                            myAnimator.SetFloat("moveY", -1);
                        }
                        // Animação em 45º Baixo/Esquerda
                        else if (inputs.inputHorizontal > 0 && inputs.inputVertical < 0)
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
                        state = StateMachine.Walk;
                        myAnimator.SetBool("walking", true);
                        myAnimator.SetBool("running", false);

                        // Movement
                        float moveX = inputs.inputHorizontal * speed;
                        float moveY = inputs.inputVertical * speed;
                        myRigidbody.velocity = new Vector2(moveX, moveY);
                        //  * Foi necessario criar variaveis moveX e moveY
                        //    para utilizar nos Floats do Animator

                        // Animação em 45º Baixo/Direita
                        if (inputs.inputHorizontal < 0 && inputs.inputVertical < 0)
                        {
                            myAnimator.SetFloat("moveX", 0);
                            myAnimator.SetFloat("moveY", -1);
                        }
                        // Animação em 45º Baixo/Esquerda
                        else if (inputs.inputHorizontal > 0 && inputs.inputVertical < 0)
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
                    state = StateMachine.Walk;
                    myAnimator.SetBool("walking", true);
                    myAnimator.SetBool("running", false);

                    // Movement
                    float moveX = inputs.inputHorizontal * tiredSpeed;
                    float moveY = inputs.inputVertical * tiredSpeed;
                    myRigidbody.velocity = new Vector2(moveX, moveY);
                    //  * Foi necessario criar variaveis moveX e moveY
                    //    para utilizar nos Floats do Animator

                    // Animação em 45º Baixo/Direita
                    if (inputs.inputHorizontal < 0 && inputs.inputVertical < 0)
                    {
                        myAnimator.SetFloat("moveX", 0);
                        myAnimator.SetFloat("moveY", -1);
                    }
                    // Animação em 45º Baixo/Esquerda
                    else if (inputs.inputHorizontal > 0 && inputs.inputVertical < 0)
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
                StopAllCoroutines();
                state = StateMachine.Idle;
                myAnimator.SetBool("running", false);
                myAnimator.SetBool("walking", false);
                myRigidbody.velocity = new Vector2(0f, 0f);
            }


            // ----------------------  END Player Movement  ----------------------

            // ---------------  Ações  --------------------



               // Interect
            if (state == StateMachine.Interect)
            {
                StartCoroutine(InterectCo());
            }

               // Stagger
            if (state == StateMachine.Stagger)
            {
                StartCoroutine(StaggerCo());
            }

        }

        // Caso Esteja Pausado - Not Move
        else
        {
             if(state == StateMachine.Idle)
             {
                myRigidbody.velocity = new Vector2(0f, 0f);
                StopAllCoroutines();
             }
            
        }

    }


    // ---------------  Coroutines  --------------------

       // Coroutine Stagger

    IEnumerator StaggerCo()
    {
        // Player não se Move
        myRigidbody.velocity = Vector2.zero;

        // Entra no estado de Stagger 
        state = StateMachine.Stagger;
        myAnimator.SetBool("stagger", true);

        // Espera o Tempo Necessario para Realizar a Animação
        yield return new WaitForSeconds(.3f);

        // Volta para o estado normal (Idle/Can Walk our Attack)
        myAnimator.SetBool("stagger", false);
        
    }

        // CoroutineInterect

    IEnumerator InterectCo()
    {
        // Para o Tempo dentro do Jogo



        // Player não se Move
        myRigidbody.velocity = new Vector2(0f, 0f);

        // Entra no estado de Interação (Interect)
        state = StateMachine.Interect;
        myAnimator.SetBool("interect", true);

        // Espera o Tempo Necessario para Realizar a Animação
        yield return new WaitForSeconds(2f);

        // Volta para o estado normal (Idle/Can Walk our Attack)
        myAnimator.SetBool("interect", false);

        // testar se consegue andar ou se nao muda nada
        myRigidbody.velocity = Vector2.zero;

    }

    public IEnumerator RespawnCo()
    {
        isPaused = true;

        Debug.Log("Comecou a Rotina Respawn()");
        healthController.life = healthController.life - 1;
        Debug.Log("Player Perdeu Vida");

        // Animação
        if (healthController.life <= 0)
        {
            healthController.life = 0;

            // Animação de Stagger
            myAnimator.SetBool("stagger", true);
            state = StateMachine.Stagger;
            yield return new WaitForSeconds(0.5f);

            StartCoroutine(DeathCo());

        }
        else
        {
            Debug.Log("Deu Respawn");

            healthController.healthGoesMax = true;
            Debug.Log("Player Vida Maxima Voltou");

            myAnimator.SetBool("stagger", true);
            state = StateMachine.Stagger;
            yield return new WaitForSeconds(1f);

            myAnimator.SetBool("stagger", false);

            gameObject.transform.position = new Vector2(respawnLocation.x, respawnLocation.y);
            Debug.Log("Player deu Respawn");

            //yield return new WaitForSeconds(0.1f);

            isPaused = false;

        }

    }

    public IEnumerator DeathCo()
    {
        Debug.Log("Comecou a Rotina DeathCo()");

        // Programação Defensiva
        healthController.life = 0;
        healthController.health = 0;
       
        // Components
        state = StateMachine.Dead;
        boxCollider.enabled = false;
        isPaused = true;

        yield return new WaitForSeconds(0.5f);

        // Animação de Morte
        myAnimator.SetBool("dying", true);

        // Espera a Animação
        yield return new WaitForSeconds(1.1f);

        // Menu de Morte
        deathMenuUI.SetActive(true);
        Debug.Log("Abre o Menu de Morte");

    }


    IEnumerator CheckRespawnLocation()
    {
        respawnLocation = respawnPoint.respawnPosition;
        yield return new WaitForSeconds(0.1f);
    }

}