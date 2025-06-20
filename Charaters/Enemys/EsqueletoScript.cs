using UnityEngine;

public class EsqueletoScript : EnemyState
{
    [Header("Attack")]
    public bool canAttackAgain;
    public float attackRadius;
    public float attackDelay;
    public GameObject attackObj;

    [Header("RandomMoviment")]
    public Vector2 randomNextMove;
    public float maxRandom;

    public override void Start()
    {
        base.Start();
        randomNextMove = new Vector2(startPosition.transform.position.x, startPosition.transform.position.y);
    }

    public override void Update()
    {
        base.Update();
        if (battleOn)
        {
            if (Vector3.Distance(target.transform.position, transform.position) <= attackRadius)
            {
                if (canAttackAgain)
                {
                    Invoke("StartAttack", attackDelay);
                }
            }
            else
            {
                ChaseMoviment(baseSpeed);
            }
        }
        else
        {
            RandomMoviment();
            HealthRegeneration();
        }
    }

    public void StartAttack()
    {
        canAttackAgain = false;
        attackObj.SetActive(true);
        Invoke("AttackDelay", attackDelay);
    }

    public void AttackDelay()
    {
        canAttackAgain = true;
        attackObj.SetActive(false);
    }

    public void RandomMoviment()
    {
        // Positive Max:
        float limitX = startPosition.transform.position.x + maxRandom;
        float limitY = startPosition.transform.position.y + maxRandom;

        // Negative Max:
        float nLimitX = startPosition.transform.position.x - maxRandom;
        float nLimitY = startPosition.transform.position.y - maxRandom;

        Vector2 actualPosition = new Vector2(gameObject.transform.position.x, gameObject.transform.position.y);

        if (randomNextMove.x > limitX && randomNextMove.y > limitY 
        && randomNextMove.x < nLimitX && randomNextMove.y < nLimitY)      //  If reach limit of random values
        {
            GetRandomMove();                                              //  Try Again  
        }
        if (actualPosition == randomNextMove)                             //  If character reachs random position
        {
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
