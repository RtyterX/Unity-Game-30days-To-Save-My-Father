using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : Stats
{
    // Components
    public InputController inputs;
    public PlayerStatus playerStatus;

    public Vector2 respawnLocation;

    RespawnPointer respawnPoint;
    BoxCollider2D boxCollider;
    public GameObject deathMenuUI;

    // Movement
    public float speed;
    private float maxSpeed = 0.5f;
    public float run;


    public GameObject testeAttack;

    // Start is called before the first frame update
    public void Start()
    {
        // State Inicial
        state = StateMachine.Idle;
        isPaused = false;
     

        // Components
        inputs = GetComponent<InputController>();
        playerStatus = GetComponent<PlayerStatus>();
        respawnPoint = GetComponent<RespawnPointer>();
        boxCollider =  GetComponent<BoxCollider2D>();

        myAnimator = GetComponent<Animator>();
        myRigidbody = GetComponent<Rigidbody2D>();
        healthController = GetComponent<HealthController>();

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
            // ---------------  Movimentação  --------------------

                // Movement - Down/Up/Right/Left
            if (inputs.inputHorizontal != 0 || inputs.inputVertical != 0 && state != StateMachine.Attack && state != StateMachine.Interect && state != StateMachine.Stagger)
            {
                inputs = GetComponent<InputController>();

                // Movement - 45º
                if (inputs.inputHorizontal != 0 && inputs.inputVertical != 0)
                {
                    inputs.inputHorizontal *= maxSpeed;
                    inputs.inputVertical *= maxSpeed;
                }

                // Movement - Running
                if (inputs.inputRunning != 0)
                {
                    state = StateMachine.Running;
                    myAnimator.SetBool("running", true);
                    myAnimator.SetBool("walking", false);

                    float moveX = inputs.inputHorizontal * run;
                    float moveY = inputs.inputVertical * run;
                      // Foi necessario criar variaveis as variavbeis moveX e moveY
                      // para utilizar nos Floats do Animator


                       // Realiza o Movimento usando o .velocity
                    myRigidbody.velocity = new Vector2(moveX, moveY);

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

                // Caso Contrario - Walking (Not Running)
                else
                {
                    state = StateMachine.Walk;
                    myAnimator.SetBool("walking", true);
                    myAnimator.SetBool("running", false);

                    float moveX = inputs.inputHorizontal * speed;
                    float moveY = inputs.inputVertical * speed;
                    // Foi necessario criar variaveis as variavbeis moveX e moveY
                    // para utilizar nos Floats do Animator


                    // Realiza o Movimento usando o .velocity
                    myRigidbody.velocity = new Vector2(moveX, moveY);

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

               // Caso Contrario - NotWalking
            else
            {
                state = StateMachine.Idle;
                myAnimator.SetBool("running", false);
                myAnimator.SetBool("walking", false);
                myRigidbody.velocity = new Vector2(0f, 0f);
            }

            // ---------------  Ações  --------------------

               //  Attack
            if (inputs.inputAttacking != 0 && state != StateMachine.Stagger && state != StateMachine.Interect)
            {
                StartCoroutine(AttackCo());
            }

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
             }
            
        }

    }

    // ---------------  Coroutines  --------------------

       // Coroutine Attack

    IEnumerator AttackCo()
    {

        // Entra no estado de Ataque (attacking)
        state = StateMachine.Attack;
        myAnimator.SetBool("attacking", true);

        // Player não se Move
        myRigidbody.velocity = new Vector2(0f, 0f);

        testeAttack.gameObject.SetActive(true);

        // Espera o Tempo Necessario para Realizar a Animação
        yield return new WaitForSeconds(.5f);

        // Volta para o estado normal (Idle/Not Attacking)
        myAnimator.SetBool("attacking", false);

        testeAttack.gameObject.SetActive(false);

        // Depois que a rotina terminar o jogador volta poder andar normalmente
    }


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