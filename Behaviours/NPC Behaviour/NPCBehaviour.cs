using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCBehaviour : MonoBehaviour
{
    // Components
    [Header("Components")]
    public Rigidbody2D myRigidbody;
    public Animator myAnimator;
    public HealthController health;
    public StaminaController stamina;
    public TimeControl clock;

    // Movement
    [Header("Movement")]
    public float speed;
    public float attackSpeed;
    public bool isStopped;
    public bool routineStop;

    // Path
    [Header("Path")]
    public NPCPath[] path;
    public int currentPath;
    public int currentWaypoint;
    private int pathMaxNumber;
    private bool noCurrentPath;

    // Delay Between Paths
    public float delayBeforeNextWaypoint;
    public bool canGoToNextWaypoint;


    // Attack
    [Header("Attack")]
    public bool attackMode;
    public GameObject attack;
    public float attackRadius;
    public GameObject target;


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
    }


    void Update()
    {   
        // Check: if NPC get hurt, enter Attack Mode
        if (health.health != health.maxHealth)
        {
            routineStop = true;
            attackMode = true;
        }

        TimeCheck();
    }


    // Movement
    public void FixedUpdate()
    {
        if (!routineStop)
        {
            if (!isStopped)
            {
                if (!attackMode)
                {
                    // Check if Can follow a Path
                    if (!noCurrentPath)
                    {
                        FollowPath(currentPath);
                    }
                }
                else
                {
                    FollowTarget();
                }

            }
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


    // Follow Methods
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
                    noCurrentPath = true;
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

            // Move to Waypoint
            Vector3 moveToWaypoint = Vector3.MoveTowards(transform.position,
                path[followPath].waypoint[currentWaypoint].transform.position, 
                speed * Time.deltaTime);

            myRigidbody.MovePosition(moveToWaypoint);

            Debug.Log("NPC indo para o Waypoint" + currentWaypoint);

        }
    }


    public void FollowTarget()
    {
        if (Vector3.Distance(target.transform.position, transform.position) >= attackRadius)
        {
            Vector3 moveToTarget = Vector3.MoveTowards(transform.position,
            target.transform.position, attackSpeed * Time.deltaTime);

            myRigidbody.MovePosition(moveToTarget);
        }
        else
        {
            isStopped = true;
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


    public void NPCHealthRegen()
    {
        health.health = health.maxHealth;
    }


    // -------------- Coroutines ---------------

    public IEnumerator DelayBetweenWaypoints()
    {
        yield return new WaitForSeconds(delayBeforeNextWaypoint);

        currentWaypoint++;
        canGoToNextWaypoint = false;
    }

}
