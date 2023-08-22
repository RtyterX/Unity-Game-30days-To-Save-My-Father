using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    // Teste
    [Space(10)]
    public GameObject testeAttack;

    // Components
    [Header("Components")]
    public PlayerBehaviour player;
    public StaminaController staminaController;
    public InputController inputs;

    // Attack
    [Header("Attack")]
    public float attackTime;
    public bool canAttack;


    // Timer
    [Header("Timer")]
    [SerializeField] private bool timerActive;
    [SerializeField] private float timer;
    [SerializeField] private float rechargeTime;


    void Start()
    {
        testeAttack.gameObject.SetActive(false);
        canAttack = true;
        rechargeTime = 1.5f;
        timerActive = false;
        timer = 0;
        
    }

    void Update()
    {
        // Timer Check
        if (timer >= rechargeTime)
        {
            timer = 0;
            timerActive = false;
            canAttack = true;
        }

        if (canAttack)
        {
            //  Attack
            if (inputs.inputAttacking != 0 && player.state != StateMachine.Stagger && player.state != StateMachine.Interect)
            {
                if (!staminaController.isTired)
                {
                    StartCoroutine(AttackCo());
                }
            }

        }
    }


    // ------ Timer ------

    // Must be FixedUpdate to get real time in game

    // If timer is started, Count Time
    public void FixedUpdate()
    {
        if (timerActive)
        {
            timer += Time.deltaTime;
        }
    }


    IEnumerator AttackCo()
    {
        Debug.Log("comecou a Attack Co");
        // Entra no estado de Ataque (attacking)
        player.state = StateMachine.Attack;
        player.myAnimator.SetBool("attacking", true);

        // Player não se Move
        player.myRigidbody.velocity = new Vector2(0f, 0f);

        testeAttack.gameObject.SetActive(true);

        // Espera o Tempo Necessario para Realizar a Animação
        yield return new WaitForSeconds(attackTime);

        // Volta para o estado normal (Idle/Not Attacking)
        player.myAnimator.SetBool("attacking", false);

        testeAttack.gameObject.SetActive(false);

        // Depois que a rotina terminar o jogador volta poder andar normalmente

        // Stamina Lost
        staminaController.UseStamina(20);

        // Start Recharge Time
        timerActive = true;
    }
}
