using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class Vespa : EnemyState
{
    [Header("Attack")]
    public GameObject attackObj;
    public float attackDelay;
    float attackRadiusF;
    float rushAttackSpeed;
    float finalSpeed;

    [Header("Random Movement")]
    Vector2 randomNextMove;
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
            if (Vector3.Distance(target.transform.position, transform.position) <= battleRadius)
            {
                ChaseMovement(finalSpeed);
            }
            else
            {
                RandomMovement();
            }
        }
        // Attack
        if (canAttack)
        {
            if (Vector3.Distance(target.transform.position, transform.position) <= attackRadiusF)
            {
                StartAttack();
            }
        }
    }

    public override void ChangeState(StateMachine enemyState)
    {
        base.ChangeState(enemyState);
        switch (state)
        {
            case StateMachine.Idle:
                canMove = true;
                // Alter Animation
                break;
            case StateMachine.Attacking:
                canMove = false;
                canAttack = false;
                break;
            case StateMachine.Stagger:
                canMove = false;
                canAttack = false;
                // Alter Animation
                break;
            case StateMachine.Dead:
                canMove = false;
                canAttack = false;
                break;
        }
    }

    public void StartAttack()
    {
        int num = Random.Range(0, 99);
        if (num >= 50)                               // Normal Attack
        {
            attackObj.SetActive(true);
            // Alter Animation
            Invoke("ReturnAttack", attackDelay);
        }
        else                                        // Rush Attack
        {
            state = StateMachine.Attacking;
            finalSpeed = rushAttackSpeed;
            attackRadiusF = 0.2f;
            // Change Animation
            Invoke("ReturnAttack", 1.5f);
        }
    }

    public void ReturnAttack()
    {
        attackObj.SetActive(false);
        finalSpeed = baseSpeed;
        attackRadiusF = attackRadius;
        ReturnMovement();
        Invoke("CanAttackAgain", attackDelay);
    }

    public void ReturnMovement()
    {
        ChangeState(StateMachine.Idle);
    }

    public void CanAttackAgain()
    {
        canAttack = true;
    }


    // ------ RANDOM MOVEMENT ------

    public void RandomMovement()
    {
        // Limite Random Move Range
        float limitX = startPosition.x + maxRandom;    // Positive Max:
        float limitY = startPosition.y + maxRandom;
        float nLimitX = startPosition.x - maxRandom;   // Negative Max:
        float nLimitY = startPosition.y - maxRandom;

        // Verify if next movement is out of Limits
        if (randomNextMove.x > limitX && randomNextMove.y > limitY        //  If its out of limit..
        && randomNextMove.x < nLimitX && randomNextMove.y < nLimitY)
        {                                                                 
            GetRandomMove();                                              //  Try Again	          
        }
        if ((Vector2)gameObject.transform.position == randomNextMove)     //  If character reachs next move... 
        {
            GetRandomMove();                                              //  Get new random movement
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

        int num = Random.Range(0, 100);
        if (num >= 0 && num <= 25)                   // Left
        {
            newX =+ Random.Range(0, maxRandom);
        }
        if (num >= 25 && num <= 50)                  // Right
        {
            newX =- Random.Range(0, maxRandom);
        }
        if (num >= 50 && num <= 75)                  // Up
        {
            newY =+ Random.Range(0, maxRandom);
        }
        if (num >= 75 && num <= 100)                 // Down
        {
            newY =- Random.Range(0, maxRandom);
        }
        randomNextMove = new Vector2(newX, newY);
    }
}
