using UnityEngine;

public class EnemyState : BehaviourController
{
    [Header("Battle Mode")]
    public bool battleOn;
    public GameObject target;
    public float battleDistance;

    [Space(2)]
    public float baseSpeed;

    [Header("Respawn")]
    public GameObject startPosition;


    public override void Start()
    {
        base.Start();
        state = StateMachine.Paused;
        target = GameObject.FindGameObjectWithTag("Player");
    }


    public virtual void Update()
    {
        if (state == StateMachine.Dead)
        {
            if (Vector3.Distance(target.transform.position, transform.position) >= battleDistance + 15)
            {
                Invoke("SpawnBack", 5);
            }
        }
        else
        {
            if (!battleOn)
            {
                if (Vector3.Distance(target.transform.position, transform.position) <= battleDistance)
                {
                    EnterBattleMode();
                }
            }
            if (Vector3.Distance(target.transform.position, startPosition.transform.position) >= battleDistance + 3)
            {
                MoveBackToSpawn();
            }
        }
   }


    public void EnterBattleMode()
    {
        battleOn = true;
    }


    public void MoveBackToSpawn()
    {
        battleOn = false;
        Vector3 BackToSpawn = Vector3.MoveTowards(transform.position, startPosition.transform.position, baseSpeed * Time.deltaTime);
        myRigidbody.MovePosition(BackToSpawn);
    }


    public void SpawnBack()
    {
        // Needed to be duplicates in case player reenter the Spawn Area before Invoke 
        if (Vector3.Distance(target.transform.position, transform.position) >= battleDistance + 15) 
        {
            gameObject.transform.position = new Vector2(startPosition.transform.position.x, startPosition.transform.position.y);
            state = StateMachine.Idle;
        }
    }


    public void ChaseMoviment(float speed)
    {
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
