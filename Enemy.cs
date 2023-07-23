using UnityEngine;
using UnityEngine.SceneManagement;
using System;
using System.Collections;

public class Enemy : Stats
{
    public GameObject target;
    public Transform targetPlayer;

    public bool canRespawn;
    public bool stopChase;

    public float speed = 0.8f;

    public float chaseRadius;
    public float attackRadius;
    public float spawnRadius;

    Vector2 checkingDistance;

    public float respawnDelay;


    // Start is called before the first frame update
    void Start()
    {

        state = StateMachine.Idle;

        myAnimator = GetComponent<Animator>();
        myRigidbody = GetComponent<Rigidbody2D>();
        healthController = GetComponent<HealthController>();

        targetPlayer = GameObject.FindGameObjectWithTag("Player").transform;

    }

    // Update is called once per frame
    void Update()
    {

        if (!targetPlayer)
        {
            GetTarget();
        }

        checkingDistance = new Vector2(targetPlayer.position.x, targetPlayer.position.y);
    }

    public void FixedUpdate()
    {

        CheckDistance();

    }


    void GetTarget()
    {

        // if (targetPlayer != null)
        // {
        //    GameObject.transform.position = targetPlayer.position;
        // }
    }


    void CheckDistance()
    {
        if (!isPaused)
        {


            // Chase Target
            if (Vector3.Distance(targetPlayer.position, transform.position) <= chaseRadius && Vector3.Distance(targetPlayer.position, transform.position) > attackRadius)
            {
                Debug.Log("Entrou no Raio de Chase");

                if (!stopChase)
                {
                    ChaseMovement();
                }

            }
            else if (Vector3.Distance(targetPlayer.position, transform.position) > chaseRadius)
            {
                myAnimator.SetBool("wakeUp", false);
            }
        }
        else
        {
             // Fica Parado
            myRigidbody.velocity = Vector2.zero;
        }

      //  void movement()
       // {
        //    myRigidbody.velocity = new Vector2(targetPlayer.position.x * speed, targetPlayer.position.y * speed);
      //  }

    }

    public void ChaseMovement()
    {

        Debug.Log("Comecou Chase Movement");

        if (state == StateMachine.Idle || state == StateMachine.Walk && state != StateMachine.Stagger)
            {
                Vector3 temp = Vector3.MoveTowards(transform.position, targetPlayer.position, speed * Time.deltaTime);
                myRigidbody.MovePosition(temp);
                state = StateMachine.Walk;
                myAnimator.SetBool("wakeUp", true);
            }

            else if (Vector3.Distance(targetPlayer.position, transform.position) <= attackRadius)
            {
                Vector3 temp = Vector3.MoveTowards(transform.position, targetPlayer.position, speed * Time.deltaTime);
                AttackCo();
            }
        

        

    }

    public IEnumerator AttackCo()
    {
        // Entra no estado de Ataque (attacking)
        state = StateMachine.Attack;
        myAnimator.SetBool("attacking", true);

        // Player não se Move
        myRigidbody.velocity = new Vector2(0f, 0f);

        // Espera o Tempo Necessario para Realizar a Animação
        yield return new WaitForSeconds(.3f);

        // Volta para o estado normal (Idle/Not Attacking)
        myAnimator.SetBool("attacking", false);

        // Player não se Move
        myRigidbody.velocity = new Vector2(0f, 0f);

        // Depois que a rotina terminar o jogador volta poder andar normalmente
    }

    public IEnumerator DeathCo() 
    {
        Debug.Log("Inimigo Morreu... F");

        isPaused = true;

        myAnimator.SetBool("Death", true);

        yield return new WaitForSeconds(5f);

        if (canRespawn)
        {
            StartCoroutine(RespawnEnemyCo());
        }

    }

    public IEnumerator RespawnEnemyCo()
    {
        yield return new WaitForSeconds(respawnDelay);
    }
}
