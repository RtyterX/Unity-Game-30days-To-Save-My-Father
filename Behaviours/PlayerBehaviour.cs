using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerBehaviour : BehaviourController
{
    // Components
    [Header("Components")]
    [SerializeField] private PlayerMovement movement;
    [SerializeField] private PlayerStatus playerStatus;
    [SerializeField] private StaminaController staminaController;
    [SerializeField] private BoxCollider2D boxCollider;
    [SerializeField] private GameObject deathMenuUI;


    // Respawn
    [Header("Respawn")]
    public Vector2 respawnLocation;
    RespawnPointer respawnPoint;


    [Header("KnockBack")]
    public bool canKnockBack;
    [SerializeField] private float strengthKnockBack;
    [SerializeField] private float delayKnockBack;
    [SerializeField] private Vector2 direction;


    public void Start()
    {
        // Inicial State
        state = StateMachine.Idle;

        // Components
        movement = GetComponent<PlayerMovement>();
        playerStatus = GetComponent<PlayerStatus>();
        boxCollider = GetComponent<BoxCollider2D>();
        myAnimator = GetComponent<Animator>();
        myRigidbody = GetComponent<Rigidbody2D>();
        healthController = GetComponent<HealthController>();
        staminaController = GetComponent<StaminaController>();

        


        // Respawn
        respawnPoint = GetComponent<RespawnPointer>();
        respawnLocation = respawnPoint.respawnPosition;

    }

    void Update()
    {

        if (state == StateMachine.Interect && state == StateMachine.Stagger && state == StateMachine.Dead)
        {
            movement.isPaused = true;
        }

        // Interect
        if (state == StateMachine.Interect)
        {
            StartCoroutine(InterectCo());
        }

        StartCoroutine(CheckRespawnLocation());

    }


    // ---------------  Coroutines  --------------------


    // Coroutine KnockBack

    public IEnumerator KnockBackCo()
    {
        // Pause Player
        state = StateMachine.Stagger;
        myAnimator.SetBool("stagger", true);
        movement.isPaused = true;

        // Codigo que Realiza o KockBack
        Vector2 direction = (transform.position - gameObject.transform.position).normalized;
        myRigidbody.AddForce(direction * strengthKnockBack, ForceMode2D.Impulse);

        Debug.Log("Realizou o KnockBack");

        yield return new WaitForSeconds(delayKnockBack);

        // Return Player Movement
        myAnimator.SetBool("stagger", false);
        state = StateMachine.Idle;
        movement.isPaused = false; 

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

    public void PlayerRespawn()
    {

        // Pause Player Movement
        movement.isPaused = true;

        // Player lost Life
        healthController.life = healthController.life - 1;
        Debug.Log("Player Perdeu Vida");

        // Check if Life is Below 0
        if (healthController.life <= 0)
        {
            healthController.life = 0;

            // Death Coroutine
            StartCoroutine(DeathCo());

        }
        else  // Respawn
        {
            StartCoroutine(RespawnCo());
        }
    }


    //  ---------- Coroutines ---------- 

    public IEnumerator RespawnCo()
    {
        // Pause Player
        state = StateMachine.Stagger;
        myAnimator.SetBool("stagger", true);
        movement.isPaused = true;

        // KockBack
        Vector2 direction = (transform.position - gameObject.transform.position).normalized;
        myRigidbody.AddForce(direction * strengthKnockBack, ForceMode2D.Impulse);

        Debug.Log("Realizou o KnockBack");

        // KnockBack Wait Time
        yield return new WaitForSeconds(delayKnockBack);

        // Respawn Player
        gameObject.transform.position = new Vector2(respawnLocation.x, respawnLocation.y);
        Debug.Log("Player deu Respawn");

        // Health Goes Max
        healthController.healthGoesMax = true;

        // Respawn movement delay
        yield return new WaitForSeconds(0.2f);

        // Player can Move Again
        state = StateMachine.Idle;
        myAnimator.SetBool("stagger", false);
        movement.isPaused = false;
    }


    public IEnumerator DeathCo()
    {
        Debug.Log("Comecou a Rotina DeathCo()");

        // Pause Player
        state = StateMachine.Stagger;
        myAnimator.SetBool("stagger", true);
        movement.isPaused = true;

        // Programação Defensiva
        healthController.life = 0;
        healthController.health = 0;

        // KockBack
        Vector2 direction = (transform.position - gameObject.transform.position).normalized;
        myRigidbody.AddForce(direction * strengthKnockBack, ForceMode2D.Impulse);

        Debug.Log("Realizou o KnockBack");

        // KnockBack Wait Time
        yield return new WaitForSeconds(delayKnockBack);


        // Player goes Dead
        state = StateMachine.Dead;
        boxCollider.enabled = false;
        movement.isPaused = true;
        healthController.isDead = true;

        // Animação de Morte
        myAnimator.SetBool("dying", true);

        // Espera a Animação
        yield return new WaitForSeconds(1.1f);

        // Menu de Morte
        deathMenuUI.SetActive(true);

    }

    IEnumerator CheckRespawnLocation()
    {
        respawnLocation = respawnPoint.respawnPosition;
        yield return new WaitForSeconds(0.1f);
    }
    
}