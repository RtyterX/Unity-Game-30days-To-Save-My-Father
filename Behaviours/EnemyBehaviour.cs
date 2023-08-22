using UnityEngine;
using UnityEngine.SceneManagement;
using System;
using System.Collections;

public class EnemyBehaviour : BehaviourController
{
    // public GameObject target;
    public Transform target;

    public bool canRespawn;
    public bool stopChase;

    public float speed = 0.8f;

    public float chaseRadius;
    public float stopRadius;
    public float attackRadius;
    public float spawnRadius;

    Vector2 checkingDistance;

    public float respawnDelay;


    // Start is called before the first frame update
    public override void Start()
    {

        state = StateMachine.Idle;

        myAnimator = GetComponent<Animator>();
        myRigidbody = GetComponent<Rigidbody2D>();
        healthController = GetComponent<HealthController>();

        target = GameObject.FindGameObjectWithTag("Player").transform;

    }

    // Update is called once per frame
    void Update()
    {

        if (!target)
        {
            GetTarget();
        }

        checkingDistance = new Vector2(target.position.x, target.position.y);
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


    public void CheckDistance()
    {
        if (!isPaused)
        {


            // Chase Target com Attack
            // if (Vector3.Distance(target.position, transform.position) <= chaseRadius && Vector3.Distance(target.position, transform.position) > attackRadius)
            if (Vector3.Distance(target.position, transform.position) <= chaseRadius)
            {
                Debug.Log("Entrou no Raio de Chase");

                if (!stopChase)
                {
                    ChaseMovement();
                }

            }
            else if (Vector3.Distance(target.position, transform.position) > chaseRadius)
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
        //    myRigidbody.velocity = new Vector2(target.position.x * speed, target.position.y * speed);
      //  }

    }

    public void ChaseMovement()
    {

        Debug.Log("Comecou Chase Movement");

        if (state == StateMachine.Idle || state == StateMachine.Walk && state != StateMachine.Stagger)
            {
                Vector3 temp = Vector3.MoveTowards(transform.position, target.position, speed * Time.deltaTime);
                myRigidbody.MovePosition(temp);
                state = StateMachine.Walk;
                myAnimator.SetBool("wakeUp", true);
            }

            else if (Vector3.Distance(target.position, transform.position) <= attackRadius)
            {
                Vector3 temp = Vector3.MoveTowards(transform.position, target.position, speed * Time.deltaTime);
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
