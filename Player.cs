using UnityEngine;
using UnityEngine.SceneManagement;
using System;
using System.Collections;

public enum PlayerState
{
    Idle,
    Walk,
    Running,
    Attack,
    Stagger,
    Interect,
}

public class Player : MonoBehaviour
{
      //State
    public PlayerState state;
    public bool paused;

     //Components
    Rigidbody2D myRigidbody;
    private Animator myAnimator;

     // Movement
    public float speed = 3f;
    public float maxSpeed = 0.5f;
    public float run = 3f * 1.5f;
     
     //Level
    public int level;
    public int maxLevel;

     // Inputs
    float inputHorizontal;
    float inputVertical;
    float inputRunning;
    float inputAttacking;


    // Start is called before the first frame update
    void Start()
    {
        state = PlayerState.Idle;
        paused = false;

        myAnimator = GetComponent<Animator>();
        myRigidbody = GetComponent<Rigidbody2D>();

    }

    // Update is called once per frame
    void Update()
    {

         //Movement Inputs
        inputHorizontal = Input.GetAxisRaw("Horizontal");
        inputVertical = Input.GetAxisRaw("Vertical");
        inputRunning = Input.GetAxisRaw("Jump");
        inputAttacking = Input.GetAxisRaw("Attack");


    }

    void FixedUpdate()
    {
        if (!paused)
        {

             // Movement
            if (inputHorizontal != 0 || inputVertical != 0 || state != PlayerState.Attack || state != PlayerState.Interect || state != PlayerState.Stagger)
            {
                state = PlayerState.Walk;
                myAnimator.SetBool("walking", true);

                if (inputHorizontal != 0 && inputVertical != 0)
                {
                    inputHorizontal *= maxSpeed;
                    inputVertical *= maxSpeed;
                }

                 // Movement - Running
                if (inputRunning != 0)
                {
                    state = PlayerState.Running;
                    myAnimator.SetBool("running", true);
                    myRigidbody.velocity = new Vector2(inputHorizontal * run, inputVertical * run);
                    myAnimator.SetBool("running", false);
                }
                else
                {
                    myRigidbody.velocity = new Vector2(inputHorizontal * speed, inputVertical * speed);
                }

                myAnimator.SetBool("walking", false);

            }
            else
            {
                // Idle - Not Running
                myRigidbody.velocity = new Vector2(0f, 0f);
                state = PlayerState.Idle;
            }


             //Interect
            if (state == PlayerState.Interect)
            {
                StartCoroutine(InterectCo());

            }

            IEnumerator InterectCo()
            {
                state = PlayerState.Interect;
                myAnimator.SetBool("interect", true);
                myRigidbody.velocity = Vector2.zero;
                yield return new WaitForSeconds(2f);
                myAnimator.SetBool("interect", false);
                state = PlayerState.Idle;
                myRigidbody.velocity = Vector2.zero;
            }

             // Stagger
            if (state == PlayerState.Stagger)
            {
                StartCoroutine(StaggerCo());
            }

            IEnumerator StaggerCo()
            {
                state = PlayerState.Stagger;
                myAnimator.SetBool("stagger", true);
                myRigidbody.velocity = Vector2.zero;
                yield return new WaitForSeconds(.3f);
                myAnimator.SetBool("stagger", false);
                state = PlayerState.Idle;
                myRigidbody.velocity = Vector2.zero;
            }

             //  Attack
            if (inputAttacking != 0 && state != PlayerState.Stagger && state != PlayerState.Interect)
            {
                StartCoroutine(AttackCo());
                myRigidbody.velocity = new Vector2(0f, 0f);
            }
            else
            {
                
            }

            IEnumerator AttackCo()
            {
                myRigidbody.velocity = new Vector2(0f, 0f);
                myAnimator.SetBool("attacking", true);
                state = PlayerState.Attack;
                yield return new WaitForSeconds(.3f);
                myAnimator.SetBool("attacking", false);
            }



            // // Rotina de Ataque
            // if (Input.GetButtonDown("attack") && currentState != PlayerState.attack && currentState != PlayerState.stagger)
            //  {
            //        StartCoroutine(AttackCo());
            //  }
            //  else if (currentState == PlayerState.walk || currentState == PlayerState.idle)
            //   {
            //        UpdateAnimationAndMove();
            //   }

        }
        else
        {
             // Not Move
            myRigidbody.velocity = new Vector2(0f, 0f);
        }

    }
}