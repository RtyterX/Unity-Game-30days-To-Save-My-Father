using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    // Components
    [Header("Components")]
    public PlayerBehaviour player;
    public StaminaController stamina;
    public InputController inputs;

    // Attack
    [Header("Attack")]
    public float attackTime;
    public bool canAttack;
    [Space(10)]
    public GameObject attack;

    // Timer
    [Header("Timer")]
    [SerializeField] private bool timerActive;
    [SerializeField] private float timer;
    [SerializeField] private float rechargeTime;


    void Start()
    {
        attack.gameObject.SetActive(false);
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
                if (!stamina.isTired)
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

        attack.gameObject.SetActive(true);

        // Espera o Tempo Necessario para Realizar a Animação
        yield return new WaitForSeconds(attackTime);

        // Volta para o estado normal (Idle/Not Attacking)
        player.myAnimator.SetBool("attacking", false);
        player.state = StateMachine.Idle;

        attack.gameObject.SetActive(false);

        // Stamina Lost
        stamina.UseStamina(20);

        // Start Recharge Time
        timerActive = true;
    }
}
