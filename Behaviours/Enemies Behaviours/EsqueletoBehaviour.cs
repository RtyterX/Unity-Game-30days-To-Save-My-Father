using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum EnemyMode
{
    NotStarted,
    Idle,
    RandomWalk,
    Attack
}

public class EsqueletoBehaviour : MonoBehaviour
{
    // Started
    public bool started;
    public float startDistance;
    public bool isPaused;


    // Component
    [Header("Components")]
    public GameObject player;
    public HealthController health;
    public Animator myAnimator;
    public Rigidbody2D myRigidbody;
    public GameObject attackHit;


    // Mode
    [Header("Mode")]
    public EnemyMode mode;


    // Movement
    [Header("Moviment")]
    public bool doRandomMoviment;
    public float speedChase;
    public float normalSpeed;
    public GameObject spawnPoint;

    // Random Movement
    [Header("Random Movement")]
    public Vector2 newPosition;
    public bool needToGetRandomValue;
    public float delayBetweenRandomPaths;
    public float randomWalkRadius;
    public float randomWalkMax;
    public float randomWalkMin;



    // Attack
    [Header("Attack")]
    public float chaseRadius;
    public float attackRadius;
    public GameObject target;
    public float delayAttack;
    public bool regenHealth;


    public void Start()
    {
        // Component
        player = GameObject.FindGameObjectWithTag("Player");
        health = GetComponent<HealthController>();
        myAnimator = GetComponent<Animator>();
        myRigidbody = GetComponent<Rigidbody2D>();

        // Started Value
        started = false;
    }


    // Mode Controller
    public void Update()
    {
        if (regenHealth)
        {
            HealthRegeneration();
        }

        if (started)
        {
            if (Vector3.Distance(target.transform.position, transform.position) <= chaseRadius)
            {
                // Start Attack Mode
                mode = EnemyMode.Attack;
            }
            else if (Vector3.Distance(target.transform.position, transform.position) >= chaseRadius)
            {
                regenHealth = true;

                if (doRandomMoviment)
                {
                    StartCoroutine(StartRandomMovimentCo());
                }

            }

        }
        else
        {
            mode = EnemyMode.NotStarted;
        }
    }


    // Movement
    public void FixedUpdate()
    {
        if (!isPaused)
        {
            if (!started)
            {
                // Start Enemy Behaviour
                CheckStart();
                mode = EnemyMode.Idle;
            }
            else
            {
                EndStarted();

                if (mode == EnemyMode.Attack)
                {
                    ChaseMovement();
                }
                else if (mode == EnemyMode.RandomWalk)
                {
                    RandomMovement();
                }
                else
                {
                    mode = EnemyMode.Idle;
                }

            }
        }
    }


    // ------- CHECK START -------

    public void CheckStart()
    {
        if (Vector3.Distance(player.transform.position, transform.position) <= startDistance)
        {
            started = true;

            StartCoroutine(StartBehaviour());
        }
    }


    // ------- END STARTED -------

    public void EndStarted()
    {
        if (Vector3.Distance(player.transform.position, transform.position) >= startDistance)
        {
            EndedBehaviour();
            
        }
    }


    // -------- MOVEMENT --------

    public void ChaseMovement()
    {
        // Chase The Target
        Vector3 moveToTarget = Vector3.MoveTowards(transform.position,
                target.transform.position,
                speedChase * Time.deltaTime);

        myRigidbody.MovePosition(moveToTarget);

        // If in Attack Radius, do Attack
        if (Vector3.Distance(target.transform.position, transform.position) <= attackRadius)
        {
            StartCoroutine(AttackCo());
        }
    }

    public void RandomMovement()
    {
            Debug.Log("Entered Random Walk");

        if (needToGetRandomValue)
        {
            // Get a random position
            RandomValueForRandomPath();
            needToGetRandomValue = false;
        }

        // Get Actual Position
        Vector2 actualPosition = new Vector2(transform.position.x, transform.position.y);

        if (actualPosition == newPosition)
        {
            Debug.Log("Delay Between Random Path");

            StartCoroutine(DelayBetweenRandomPath());
            needToGetRandomValue = true;
        }
        else
        {
            Debug.Log("Moving to Random Point");

            if (Vector3.Distance(spawnPoint.transform.position, transform.position) >= randomWalkRadius)
            {
                newPosition = new Vector2(transform.position.x, transform.position.y);
            }
            else
            {
                // -- Random Movement --
                Vector3 moveToRandomPoint = Vector3.MoveTowards(transform.position,
                    newPosition, normalSpeed * Time.deltaTime);

                myRigidbody.MovePosition(moveToRandomPoint);

            }
        }
        
    }

    public void RandomValueForRandomPath()
    {
        Debug.Log("Get Random Value");

        // -- Random Value --
        if (needToGetRandomValue)
        {
            // Toss a Random Value
            float randomValue = Random.Range(randomWalkMin, randomWalkMax);

            // The moviment is only in one direction (X or Y)
            bool goesX = Random.value > 0.6f;

            bool goesNegative = Random.value > 0.5f;

            // Verify if the Random Value goes X or Y
            if (goesX)
            {
                if (!goesNegative)
                {
                    newPosition = new Vector2(transform.position.x + randomValue, transform.position.y);
                }
                else
                {
                    newPosition = new Vector2(transform.position.x - randomValue, transform.position.y);
                }
            }
            else
            {
                if (!goesNegative)
                {
                    newPosition = new Vector2(transform.position.x, transform.position.y + randomValue);
                }
                else
                {
                    newPosition = new Vector2(transform.position.x, transform.position.y - randomValue);
                }
            }

            // Disable Get Random Value
            needToGetRandomValue = false;
        }

    }


    // ---- Animation ----

    public void MovementAnimation()
    {
        // Max and Min difference values in X axis
        float maxDiffX = transform.position.x + 1;
        float minDiffX = transform.position.x - 1;

        // *********** DELETE LATER ************
        Debug.Log("Max Diferrence Value X: =  " + maxDiffX + "  - Min value:  " + minDiffX);

        // Max and Min difference values in Y axis
        float maxDiffY = transform.position.y + 1;
        float minDiffY = transform.position.y - 1;

        // *********** DELETE LATER ************
        Debug.Log("Max Diferrence Value Y: =  " + maxDiffY + "  - Min value:  " + minDiffY);


        // We need the Max and Min values for more precise animations. Sometimes the Object moving in only one axis
        // will get values between 0, and 1, even with the axis locked.
        // For Exemplo: The Object moving toward the waypoint will get values like 0,634673467 on the locked axis.

        // With maxDiff and minDiff I can decide the animation values for 45º moviment, if he is moving more then 1 ou less then -1 on X axis.

        #region 
        // Move 45º Up
        if (transform.position.y < newPosition.y
               || transform.position.x >= maxDiffX && transform.position.x <= minDiffX)
        {
            myAnimator.SetFloat("moveX", 0);
            myAnimator.SetFloat("moveY", 1);
        }

        // Move 45º Down
        if (transform.position.y > newPosition.y
               || transform.position.x >= maxDiffX && transform.position.x <= minDiffX)
        {
            myAnimator.SetFloat("moveX", 0);
            myAnimator.SetFloat("moveY", -1);
        }

        // Move Left
        if (transform.position.x < newPosition.x
               || transform.position.y <= maxDiffY && transform.position.y <= minDiffY)
        {
            myAnimator.SetFloat("moveX", 1);
            myAnimator.SetFloat("moveY", 0);
        }

        // Move Right
        if (transform.position.x > newPosition.x
               || transform.position.y <= maxDiffY && transform.position.y <= minDiffY)
        {
            myAnimator.SetFloat("moveX", -1);
            myAnimator.SetFloat("moveY", 0);
        }

        // Move Up
        if (transform.position.y < newPosition.y
               || transform.position.x <= maxDiffX && transform.position.x <= minDiffX)
        {
            myAnimator.SetFloat("moveX", 0);
            myAnimator.SetFloat("moveY", 1);
        }

        // Move Down
        if (transform.position.y > newPosition.y
               || transform.position.x <= maxDiffX && transform.position.x <= minDiffX)
        {
            myAnimator.SetFloat("moveX", 0);
            myAnimator.SetFloat("moveY", -1);
        }
        #endregion

    }


    // ------- CHECK ATTACK MODE -------

    public void HealthRegeneration()
    {
        if (health.health == health.maxHealth)
        {
            regenHealth = false;
        }

        health.health += 3;
    }


    // ------- COROUTINES -------

    public IEnumerator StartBehaviour()
    {
        isPaused = true;
        myAnimator.SetBool("Started", true);

        yield return new WaitForSeconds(1f);

        isPaused = false;
        myAnimator.SetBool("Started", false);

    }

    public IEnumerator EndedBehaviour()
    {
        yield return new WaitForSeconds(10f);

        isPaused = true;
        myAnimator.SetBool("Ended", true);
        mode = EnemyMode.Idle;

        yield return new WaitForSeconds(3f);

        started = false;

        isPaused = false;
        myAnimator.SetBool("Ended", false);
        mode = EnemyMode.NotStarted;

    }

    public IEnumerator AttackCo()
    {
        Debug.Log("Attach Co");

        isPaused = true;
        attackHit.SetActive(true);

        yield return new WaitForSeconds(delayAttack);

        isPaused = false;
        attackHit.SetActive(false);

    }

    public IEnumerator StartRandomMovimentCo()
    {
        Debug.Log("Random Moviment Co");

        isPaused = true;
        mode = EnemyMode.Idle;

        yield return new WaitForSeconds(1f);

        isPaused = false;
        mode = EnemyMode.RandomWalk;

    }

    public IEnumerator DelayBetweenRandomPath()
    {
        Debug.Log("Start Delay Rotine");

        mode = EnemyMode.Idle;

        yield return new WaitForSeconds(delayBetweenRandomPaths);

        mode = EnemyMode.RandomWalk;
        needToGetRandomValue = true;
    }

}
