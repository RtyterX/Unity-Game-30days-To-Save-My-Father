using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementController : MonoBehaviour
{
    public bool isPaused;

    // Components
    [Header("Components")]
    public Rigidbody2D myRigidbody;
    public Animator myAnimator;
	public BehaviourController behaviour;


    // Position
	[Header("Position")]
    public float PositionX;
    public float PositionY;
    public float oldPositionX;
    public float oldPositionY;
	

    public void Start()
    {
        isPaused = false;

        // Get Components
        myAnimator = GetComponent<Animator>();
        myRigidbody = GetComponent<Rigidbody2D>();
       // behaviour = GetComponent<BehaviourController>();

        // Start with correct position
        PositionX = gameObject.transform.position.x;
        PositionY = gameObject.transform.position.y;
        oldPositionX = PositionX;
        oldPositionY = PositionY;

        // Animation - Start Looking Down (Animator)
        myAnimator.SetFloat("moveX", 0);
        myAnimator.SetFloat("moveY", -1);

    }

    // ------- Detecting Object Moviment ------- 
	
	// FixedUpdate get the object postion in real time
	// While Update gets a older position because of its 
	// slower rate.
	// Next I compare the 2 variables. If the actual position 
	// is not equal the older position, then the object is moving
	
	// If the objetc Position its equals the "oldPosition"
	// then its not moving
	
	
    void Update()
    {
		// Get Player Older Position
        oldPositionX = PositionX;
        oldPositionY = PositionY;
    }


    void FixedUpdate()
    {
		// Get Actual Player Position
        PositionX = gameObject.transform.position.x;
        PositionY = gameObject.transform.position.y;


	    // ----------- Tired -----------
		//if (behaviour.state == StateMachine.Tired)
       // {
			// Change Animator to Tired
		//	myAnimator.SetBool("tired", true);				
		//}
      //  else
      //  {
			// Change Animator to NOT Tired
		//	myAnimator.SetBool("tired", false);
	//	}

         
        // --------- If NOT Paused --------- 
		#region
        if (!isPaused)
        {
			
			 // --------- Running ---------
           // if (behaviour.state == StateMachine.Running)
           // {
                // Change Animation to Running
           //     myAnimator.SetBool("running", true);
			//}
           // else
           // {
				// Maintain Animation to Not Running
           //     myAnimator.SetBool("running", false);
			//}
			
            // ----------------------  In Movement ----------------------
			
			// Compare oldPosition and Actual Position
            if (oldPositionX != PositionX || oldPositionY != PositionY )
			 // && behaviour.state != StateMachine.Stagger)
            {
				// Change Animation to Walking
				myAnimator.SetBool("walking", true);
			
				// --- Walking Animation ---
                #region
				
                // 45º Moving Down
                if (oldPositionY > PositionY && oldPositionX != PositionX)
                {
                    myAnimator.SetFloat("moveX", 0);
                    myAnimator.SetFloat("moveY", -1);
                }
                // 45º Moving Up
                else if (oldPositionY < PositionY && oldPositionX != PositionX)
                {
                    myAnimator.SetFloat("moveX", 0);
                    myAnimator.SetFloat("moveY", 1);
                }		
			    // Down
				if (oldPositionY > PositionY && oldPositionX == PositionX)
			    {
					myAnimator.SetFloat("moveX", 0);
                    myAnimator.SetFloat("moveY", -1);
			    }
			    // Up
			    if (oldPositionY < PositionY && oldPositionX == PositionX)
			    {
					myAnimator.SetFloat("moveX", 0);
                    myAnimator.SetFloat("moveY", 1);
			    }
			    // Right
				if (oldPositionX < PositionX && oldPositionY == PositionY)
				{
					myAnimator.SetFloat("moveX", 1);
                    myAnimator.SetFloat("moveY", 0);
				}
				// Left
				if (oldPositionX > PositionX && oldPositionY == PositionY)
				{
					myAnimator.SetFloat("moveX", -1);
                    myAnimator.SetFloat("moveY", 0);						
                }
				#endregion
				
            }
		 
            // ------ Object NOT Moving ------
            else
            {       
		        // Reset Animator to NOT Walking	
				myAnimator.SetBool("walking", false);
            }
        }
		#endregion

        // ------------- Movement Paused -------------

        // If Paused - CANT Move
        else
        {
			// CANT move
            myRigidbody.velocity = new Vector2(0f, 0f);
            myRigidbody.mass = 5000;
			
			// Reset Animator
            myAnimator.SetFloat("moveX", 0);
            myAnimator.SetFloat("moveY", 0);
            myAnimator.SetBool("running", false);
            myAnimator.SetBool("walking", false);

        }
						
    }

}
