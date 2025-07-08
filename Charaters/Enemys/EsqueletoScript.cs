using UnityEngine;

public class EsqueletoScript : EnemyState
{
    [Header("Attack")]
    public float attackDelay;
    public GameObject attackObj;

    [Header("RandomMovement")]
    public Vector2 randomNextMove;
    public float maxRandom;

    public override void Start()
    {
        base.Start();
        GetRandomMove();
    }

    public override void Update()
    {
        base.Update();

        // Movement
        if (canMove)
        {
            if (Vector3.Distance(transform.position, target.transform.position) <= battleRadius)
            {
                if (Vector3.Distance(transform.position, target.transform.position) > attackRadius)
                {
                    ChaseMovement(baseSpeed);
                }
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

    public override void ChangeState(StateMachine enemyState)
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

    public void ReturnMovement()
    {
        ChangeState(StateMachine.Idle);
        attackObj.SetActive(false);
        GetRandomMove();
    }

    // Important to create delays between attack (block Attack Spam)
    public void ReturnAttack()
    {
        canAttack = true;
    }

    public void RandomMovement()
    {
        Vector2 actualPosition = new Vector2(gameObject.transform.position.x, gameObject.transform.position.y);

        if (actualPosition == randomNextMove)             //  If character reachs random position
        {
            canMove = false;                                        //  Pause Movement
            Invoke("ReturnMovement", 1.5f);                         //  Return Movement After 1,5s
        }
        else                                             // If character has not reached random position
        {
            if (state != StateMachine.Walking)
            {
                ChangeState(StateMachine.Walking);                  //  Set State to Walking
            }

            // Move to Random Position
            Vector3 random = Vector3.MoveTowards(transform.position, randomNextMove, baseSpeed * Time.deltaTime);
            myRigidbody.MovePosition(random);
        }
    }

    public void GetRandomMove()
    {
        // New Positions
        float newX = transform.position.x;
        float newY = transform.position.y;

        // Random Direction
        int test = Random.Range(1, 100);
        if (test >= 0 && test < 25)                  // Left
        {
            float value = Random.Range(0.20f, 1);
            Debug.Log("value = " + value);
            newX += value;
            Debug.Log("Positive NewX: " + newX + " - e o value foi: " + value);
        }
        else if (test >= 25 && test < 50)            // Right
        {
            newX -= Random.Range(0.20f, 1);
            Debug.Log("Negative NewX: " + newX + " - e o teste foi: " + test);
        }
        else if (test >= 50 && test < 75)            // Up  
        {
            newY += Random.Range(0.20f, 1);
            Debug.Log("Positive NewY: " + newX + " - e o teste foi: " + test);
        }
        else if (test >= 75 && test < 100)           // Down 
        {
            newY -= Random.Range(0.20f, 1);
            Debug.Log("Negative NewY: " + newX + " - e o teste foi: " + test);
        }

        randomNextMove = new Vector2(newX, newY);    // Set new Random Move

        CheckNextMove();
    }

    public void CheckNextMove()
    {
        // Positive Max:
        float limitX = startPosition.x + maxRandom;
        float limitY = startPosition.y + maxRandom;

        // Negative Max:
        float nLimitX = startPosition.x - maxRandom;
        float nLimitY = startPosition.y - maxRandom;

        if (randomNextMove.x > limitX && randomNextMove.y > limitY
        && randomNextMove.x < nLimitX && randomNextMove.y < nLimitY)      //  If reach limit of random values
        {
            GetRandomMove();                                              //  Try Again  
        }
    }

}
