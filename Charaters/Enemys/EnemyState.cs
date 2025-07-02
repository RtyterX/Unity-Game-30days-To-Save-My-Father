using UnityEngine;

public class EnemyState : BehaviourController
{
    [Header("Behaviours")]
    public bool canMove;
    public bool canAttack;
    public GameObject target;
    public float battleRadius;
    public float attackRadius;

    [Space(2)]
    public float baseSpeed;

    [Header("Respawn")]
    public bool canRespawn;
    public Vector2 startPosition;


    public override void Start()
    {
        base.Start();
        state = StateMachine.Paused;
        target = GameObject.FindGameObjectWithTag("Player");
        startPosition = gameObject.transform.position;
    }


    public virtual void Update()
    {
        if (state == StateMachine.Dead)
        {
            if (Vector3.Distance(target.transform.position, transform.position) >= battleRadius + 7)
            {
                Invoke("SpawnBack", 5);
            }
        }

    }


    public void MoveBackToSpawn()
    {
        if (state != StateMachine.Walking)
        {
            ChangeState(StateMachine.Walking);
        }

        HealthRegeneration();

        Vector3 BackToSpawn = Vector3.MoveTowards(transform.position, startPosition, baseSpeed * Time.deltaTime);
        myRigidbody.MovePosition(BackToSpawn);
    }


    public void SpawnBack()
    {
        // Needed to be duplicates in case player reenter the Spawn Area before Invoke 
        if (Vector3.Distance(target.transform.position, transform.position) >= battleRadius + 15)
        {
            if (canRespawn)
            {
                gameObject.transform.position = new Vector2(startPosition.x, startPosition.y);
                health.currentHealth = health.maxHealth;
                ChangeState(StateMachine.Idle);
            }
            else
            {
                Destroy(gameObject);
            }
        }
        else
        {
            // Stay Dead
        }


    }

    public void ChaseMoviment(float speed)
    {
        if (state != StateMachine.Walking)
        {
            ChangeState(StateMachine.Walking);
        }

        Vector3 ChaseTarget = Vector3.MoveTowards(transform.position, target.transform.position, speed * Time.deltaTime);
        myRigidbody.MovePosition(ChaseTarget);
    }

    public void HealthRegeneration()
    {
        // If Health reach Max Health...
        if (health.currentHealth == health.maxHealth)
        {
            health.currentHealth = health.maxHealth;
        }

        health.currentHealth += 3;   // Regen Health
    }
}
