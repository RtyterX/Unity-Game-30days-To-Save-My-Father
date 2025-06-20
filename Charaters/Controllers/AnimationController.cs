using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationController : MonoBehaviour
{
    // Arrays
    public float moveX;                                       // Necessary for Animation Array
    public float moveY;                                       // Necessary for Animation Array
    [HideInInspector] float positionX;
    [HideInInspector] float positionY;


    [Header("Components")]
    [HideInInspector] public Rigidbody2D myRigidbody;
    [HideInInspector] public Animator myAnimator;

    public void Start()
    {
        // Get Compenents
        myRigidbody = GetComponent<Rigidbody2D>();
        myAnimator = GetComponent<Animator>();
    }

    public void Update()
    {
        float oldPositionX = positionX;
        float oldPositionY = positionY;

        if (myRigidbody.position.y > oldPositionY && myRigidbody.position.x != positionX)
        {
            moveX = 0;
            moveY = 1;
        }
        if (myRigidbody.position.y < oldPositionY && myRigidbody.position.x != positionX)
        {
            moveX = 0;
            moveY = -1;
        }
        if (myRigidbody.position.x > oldPositionX)
        {
            moveX = 1;
            moveY = 0;
        }
        if (myRigidbody.position.x < oldPositionX)
        {
            moveX = -1;
            moveY = 0;
        }
        if (myRigidbody.position.y > oldPositionY)
        {
            moveY = 1;
            moveX = 0;
        }
        if (myRigidbody.position.y < oldPositionY)
        {
            moveY = -1;
            moveX = 0;
        }
    }

    public void FixedUpdate()
    {

        // Vector2 moveArrays = myRigidbody.velocity;            // Get Player velocity
        // moveX = moveArrays.x;                                 // Get arrays from player look angle X
        // moveY = moveArrays.y;                                 // Get arrays from player look angle Y
        myAnimator.SetFloat("moveX", moveX);                  // Set animation values for Y Axis
        myAnimator.SetFloat("moveY", moveY);                  // Set animation values for X Axis

        positionX = myRigidbody.position.x;
        positionY = myRigidbody.position.y;
    }

}

