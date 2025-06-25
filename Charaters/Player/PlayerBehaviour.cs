using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerBehaviour : BehaviourController
{
    [Header("Components")]
    [HideInInspector] private PlayerMovement2D movement;

    [Header("Death")]
    [HideInInspector] private BoxCollider2D boxCollider;
    [HideInInspector] private GameObject deathMenuUI;


    public override void Start()
    {
        base.Start();
        state = StateMachine.Idle;                      // Inicial State

        // Components
        movement = GetComponent<PlayerMovement2D>();
        boxCollider = GetComponent<BoxCollider2D>();
    }


    public override void ChangeState(StateMachine playerState)
    {
        base.ChangeState(playerState);
        switch (state)
        {
            case StateMachine.Idle:
                movement.isPaused = false;
                break;
            case StateMachine.Interect:
                movement.isPaused = true;
                break;
            case StateMachine.Attacking:
                movement.isPaused = true;
                break;
            case StateMachine.Stagger:
                movement.isPaused = true;
                break;
            case StateMachine.Dead:
                movement.isPaused = true;
                break;
        }
    }


    // --- Animations --- 

    IEnumerator InterectAnimationCo()
    {
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


    private IEnumerator DeathAnimationCo()
    {
        boxCollider.enabled = false;
        myAnimator.SetBool("dying", true);       // Animação de Morte
        yield return new WaitForSeconds(1.1f);   // Espera a Animação
        deathMenuUI.SetActive(true);             // Menu de Morte

    }

}

