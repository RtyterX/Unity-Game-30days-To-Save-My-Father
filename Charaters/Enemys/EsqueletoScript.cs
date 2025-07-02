using UnityEngine;

public class EsqueletoScript : EnemyState
{
    [Header("Attack")]
    public float attackDelay;
    public GameObject attackObj;

    [Header("RandomMoviment")]
    public Vector2 randomNextMove;
    public float maxRandom;

    public override void Start()
    {
        base.Start();
        randomNextMove = new Vector2(startPosition.x, startPosition.y);
    }

    public override void Update()
    {
        base.Update();

        // Movement
        if (canMove)
        {
            if (Vector3.Distance(transform.position, target.transform.position) <= battleRadius && Vector3.Distance(transform.position, target.transform.position) >= attackRadius)
            {
                ChaseMoviment(baseSpeed);
            }
            else
            {
                RandomMovement();
            }
        }

        // Attack
        if (canAttack)
        {
            if (Vector3.Distance(transform.position, target.transform.position) <= attackRadius)
            {
                StartAttack();
            }
        }
    }

    public void ChangeState(StateMachine enemyState)
    {
        base.ChangeState(enemyState);
        state = enemyState;
        switch (state)
        {
            case StateMachine.Idle:
                canMove = true;
                Invoke("ReturnAttack", attackDelay);
                break;
            case StateMachine.Interect:
                canMove = false;
                canAttack = false;
                break;
            case StateMachine.Attacking:
                canMove = false;
                canAttack = false;
                break;
            case StateMachine.Stagger:
                canMove = false;
                canAttack = false;
                break;
            case StateMachine.Dead:
                canMove = false;
                canAttack = false;
                break;
        }
    }

    public void StartAttack()
    {
        ChangeState(StateMachine.Attacking);
        // Attack Animation
        attackObj.SetActive(true);
        Invoke("ReturnMovement", 1f);
    }

    public void PauseMovement()
    {
        canMove = true;
        Invoke("ReturnMovement", 1f);
    }

    public void ReturnMovement()
    {
        ChangeState(StateMachine.Idle);
        attackObj.SetActive(false);
    }

    public void ReturnAttack()
    {
        canAttack = true;
    }

    public void RandomMovement()
    {
        if (state != StateMachine.Walking)
        {
            ChangeState(StateMachine.Walking);
        }

        // Positive Max:
        float limitX = startPosition.x + maxRandom;
        float limitY = startPosition.y + maxRandom;

        // Negative Max:
        float nLimitX = startPosition.x - maxRandom;
        float nLimitY = startPosition.y - maxRandom;

        Vector2 actualPosition = new Vector2(gameObject.transform.position.x, gameObject.transform.position.y);

        if (randomNextMove.x > limitX && randomNextMove.y > limitY 
        && randomNextMove.x < nLimitX && randomNextMove.y < nLimitY)      //  If reach limit of random values
        {
            GetRandomMove();                                              //  Try Again  
        }
        if (actualPosition == randomNextMove)                             //  If character reachs random position
        {
            Invoke("PauseMovement", 1f);
            GetRandomMove();
        }

        // Moves To Random Position
        Vector3 random = Vector3.MoveTowards(transform.position, randomNextMove, baseSpeed * Time.deltaTime);
        myRigidbody.MovePosition(random);

    }


    public void GetRandomMove()
    {
        // New Positions
        float newX = transform.position.x;
        float newY = transform.position.y;

        int test = Random.Range(0, 100);
        if (test >= 0 && test <= 25)           // Left
        {
            newX =+ 1;
        }
        if (test >= 25 && test <= 50)          // Right
        {
            newX =- 1;
        }
        if (test >= 50 && test <= 75)          // Up  
        {
            newY =+ 1;
        }
        if (test >= 75 && test <= 100)         // Down 
        {
            newY =- 1;
        }

        randomNextMove = new Vector2(newX, newY);
    }

}
