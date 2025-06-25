using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    [Header("Attack Values")]
    public float attackTime;
    public bool canAttack;
    [Space(10)]
    public UnityEngine.GameObject attack;

    [Header("Timer")]
    [SerializeField] private bool timerActive;
    [SerializeField] private float timer;
    [SerializeField] private float rechargeTime;

    [Header("Components")]
    private PlayerEquipment equipController;
    private PlayerBehaviour player;
    private StaminaController stamina;
    private InputController inputs;


    void Start()
    {
        // Start Values
        canAttack = true;
        rechargeTime = 0.5f;
        timerActive = false;
        timer = 0;

        // Components
        player = transform.GetComponent<PlayerBehaviour>();
        equipController = GetComponent<PlayerEquipment>();
        stamina = GetComponent<StaminaController>();
        inputs = FindAnyObjectByType<InputController>();
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
            // If Player Press Both Attack Buttons
            if (inputs.Attack != 0 && inputs.Attack2 != 0)
            {
                player.myRigidbody.velocity = new Vector2(0f, 0f);
            }
            // Press Attack Button 1
            else if (inputs.Attack != 0 && player.state != StateMachine.Stagger && player.state != StateMachine.Interect)
            {
                attack = equipController.slot1;

                if (!stamina.isTired)
                {
                    StartCoroutine(AttackCo());
                }
            }
            // Press Attack Button 2
            else if (inputs.Attack2 != 0 && player.state != StateMachine.Stagger && player.state != StateMachine.Interect)
            {
                attack = equipController.slot2;

                if (!stamina.isTired)
                {
                    StartCoroutine(AttackCo());
                }
            }

        }
    }


    // ------ Timer ------

    // Must be FixedUpdate to get real time in game
    public void FixedUpdate()
    {
        if (timerActive)   // If timer is started, Count Time
        { 
            timer += Time.deltaTime;   
        }
   }


    IEnumerator AttackCo()
    {
        canAttack = false;

        Debug.Log("comecou a Attack Co");

        // Entra no estado de Ataque (attacking)
        player.ChangeState(StateMachine.Attacking);
        player.myAnimator.SetBool("attacking", true);

        // Player não se Move
        player.myRigidbody.velocity = new Vector2(0f, 0f);

        attack.gameObject.SetActive(true);

        // Espera o Tempo Necessario para Realizar a Animação
        yield return new WaitForSeconds(attackTime);

        // Volta para o estado normal (Idle/Not Attacking)
        player.myAnimator.SetBool("attacking", false);
        player.ChangeState(StateMachine.Idle);


        attack.gameObject.SetActive(false);

        // Stamina Lost
        stamina.UseStamina(20);

        // Start Recharge Time
        timerActive = true;
    }
}
