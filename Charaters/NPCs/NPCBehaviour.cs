using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum NPCMode
{
    Idle,
    FollowPath,
    RandomWalk,
    Attack,
    Paused,
}

public class NPCBehaviour : MonoBehaviour
{
    public bool canMove;

    // Components
    [Header("Components")]
    public Rigidbody2D myRigidbody;
    public Animator myAnimator;
    public StaminaController stamina;
    public TimeControl clock;
    public HealthController health;


    // State Machine
    [Header("NPC State")]
    public NPCMode mode;


    // Movement
    [Header("Movement")]
    public float speed;
    public float attackSpeed;
    public bool isStopped;
    public bool routineStop;
    public Vector2 newPosition;


    // Path
    [Header("Path")]
    public NPCPath[] path;
    private int currentPath;
    private int currentWaypoint;
    private int pathMaxNumber;
    public bool noCurrentPath;

    // Delay Between Paths
    public float delayBetweenMovement;
    public float delayBetweenRandomPaths;
    public bool canGoToNextWaypoint;


    // Attack
    [Header("Attack")]
    public UnityEngine.GameObject attack;
    public float attackRadius;
    public float followRadius;
    public UnityEngine.GameObject target;
    public float outOfAttackModeRadius;


    // Timer
    [Header("Timer")]
    public float timer;
    public float delayTimer;
    public bool activeTimer;


    // Random Path
    [Header("Random Path")]
    public bool goesX;
    public float randomValue;
    public float randomWalkMin;
    public float randomWalkMax;
    public bool needToGetRandomValue;
    public bool startRandomWalk;
    public float randomWalkRadius;

    public UnityEngine.GameObject randomRadiusObj;


    // Health

    public bool regenHealth;
    public int regenHealthSpeed;



    void Start()
    {
        // Components
        myRigidbody = GetComponent<Rigidbody2D>();
        myAnimator = GetComponent<Animator>();
        health = GetComponent<HealthController>();
        stamina = GetComponent<StaminaController>();

        // Start Values
        currentWaypoint = 0;
        pathMaxNumber = path.Length - 1;
        noCurrentPath = true;
        mode = NPCMode.RandomWalk;
        needToGetRandomValue = true;
    }


    public void Update()
    {
        // Check Health
        if (regenHealth)
        {
            HealthRegeneration();
        }
        else
        {
            HealthCheck();
        }

        // Check Clock Time
        TimeCheck();
    }


    public void FixedUpdate()
    {

        // -------- TIMER --------

        if (activeTimer)
        {
            if (timer >= delayTimer)
            {
                mode = NPCMode.Idle;
                isStopped = false;
                routineStop = false;
            }

            timer += Time.deltaTime;
        }

        // -------- MOVEMENT --------

        if (!isStopped)
        {
            // Attack Mode
            if (mode == NPCMode.Attack)
            {
                CheckRangeAttackMode();
                FollowTarget();
            }

            // Follow Path Mode
            else if (mode == NPCMode.FollowPath)
            {
                FollowPath(currentPath);
            }

            // Random Walk Mode
            else if (mode == NPCMode.RandomWalk)
            {
                RandomWalk();
            }

            else // Idle Mode
            {
                mode = NPCMode.Idle;
            }

        }
        else // Paused Mode
        {
            mode = NPCMode.Paused;
        }
    }


    // --------------- Methods --------------- 

    public void TimeCheck()
    {
        // Check if routine is playing
        if (!routineStop)
        {
            // Check Schedule Loop
            for (int i = 0; i <= pathMaxNumber; ++i)
            {

                // Check Schedule for every path created in Inspector
                if (clock.timeInGame == path[i].schedule)
                {
                    noCurrentPath = false;
                    Debug.Log("Follow path" + i);
                    currentPath = i;
                    currentWaypoint = 0;
                }

            }

        }

    }


    // Stop Movement Methods
    public void StopNPC()
    {
        isStopped = true;
    }

    public void CanMoveAgain()
    {
        isStopped = false;
    }


    // ------- CHECK ATTACK MODE -------

    public void CheckRangeAttackMode()
    {
        if (Vector3.Distance(target.transform.position, transform.position) >= outOfAttackModeRadius)
        {
            regenHealth = true;
            mode = NPCMode.Idle;
        }
    }


    // ------- CHECK HEALH STATUS -------

    public void HealthCheck()
    {
        if (!regenHealth)
        {
            if (health.currentHealth != health.maxHealth)
            {
                mode = NPCMode.Attack;
            }
        }
    }

    public void HealthRegeneration()
    {
        if (health.currentHealth == health.maxHealth)
        {
            regenHealth = false;
        }

        health.currentHealth += regenHealthSpeed;
    }



// -------- MOVEMENT METHODS ---------

    public void FollowPath(int followPath)
    {
        if (!isStopped)
        {
            int waypointLength = path[followPath].waypoint.Length - 1;

            // If NPC position = Waypoint position, move to the next one
            if (transform.position == path[followPath].waypoint[currentWaypoint].transform.position)
            {

                // if NPC reached the final waypoint, he stops
                if (currentWaypoint >= waypointLength)
                {
                    Debug.Log("Terminou path" + followPath);

                    if(mode == NPCMode.FollowPath)
                    {
                        if (path[followPath].doRandomPath)
                        {
                            randomRadiusObj = path[followPath].waypoint[currentPath];
                            StartCoroutine(DelayBetweenRandomPath());
                        }

                        noCurrentPath = true;                  
                    }

                }
                else // Current Waypoint +1
                {
                    if (!canGoToNextWaypoint)
                    {
                        StartCoroutine(DelayBetweenWaypoints());
                        canGoToNextWaypoint = true;
                    }

                }
            }
            else
            {
                // Move to Waypoint
                Vector3 moveToWaypoint = Vector3.MoveTowards(transform.position,
                    path[followPath].waypoint[currentWaypoint].transform.position,
                    speed * Time.deltaTime);

                myRigidbody.MovePosition(moveToWaypoint);

                mode = NPCMode.FollowPath;

                MovementAnimation();

                needToGetRandomValue = true;

                Debug.Log("NPC indo para o Waypoint" + currentWaypoint);

            }

        }
    }


    public void RandomWalk()
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

            if (Vector3.Distance(randomRadiusObj.transform.position, transform.position) >= randomWalkRadius)
            {
                newPosition = new Vector2(transform.position.x, transform.position.y);
            }
            else
            {
                // -- Random Movement --
                Vector3 moveToRandomPoint = Vector3.MoveTowards(transform.position,
                    newPosition, speed * Time.deltaTime);

                myRigidbody.MovePosition(moveToRandomPoint);

                MovementAnimation();
            }

        }

    }

    public void FollowTarget()
    {
        if (Vector3.Distance(target.transform.position, transform.position) >= attackRadius
            && Vector3.Distance(target.transform.position, transform.position) <= followRadius)
        {
            Vector3 moveToTarget = Vector3.MoveTowards(transform.position,
            target.transform.position, attackSpeed * Time.deltaTime);

            myRigidbody.MovePosition(moveToTarget);
        }

        // if Target gets out of Range
        if (Vector3.Distance(target.transform.position, transform.position) >= followRadius)
        {
            // Active Recover Position Timer
            if (!activeTimer)
            {
                isStopped = true;
                activeTimer = true;
            }
        }

    }


    // ---- Other Methods ----

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


    // Stop NPC when collied with Player
    private void OnCollisionStay2D(Collision2D other)
    {
        if (other.collider.tag == "Player")
        {
            StopNPC();
        }
    }

    private void OnCollisionExit2D(Collision2D other)
    {
        if (other.collider.tag == "Player")
        {
            Invoke("CanMoveAgain", 0.5f);
        }
    }






    void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(randomRadiusObj.transform.position, randomWalkRadius);
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


    // -------------- Coroutines ---------------

    public IEnumerator DelayBetweenWaypoints()
    {
        yield return new WaitForSeconds(delayBetweenMovement);

        currentWaypoint++;
        canGoToNextWaypoint = false;
    }

    public IEnumerator DelayBetweenRandomPath()
    {
        Debug.Log("Start Delay Rotine");

        mode = NPCMode.Idle;

        yield return new WaitForSeconds(delayBetweenRandomPaths);

        mode = NPCMode.RandomWalk;
        needToGetRandomValue = true;
    }

}
