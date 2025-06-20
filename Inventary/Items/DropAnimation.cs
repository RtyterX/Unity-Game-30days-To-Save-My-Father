using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Windows;
using static UnityEngine.GraphicsBuffer;

public class DropAnimation : MonoBehaviour
{
    public bool changeVector;
    public float speed = 2f;

    public Vector2 endPosition;
    public Vector2 jumpPosition;
    public Rigidbody2D myRigidbody;

    public bool testButton;

    void Awake()
    {
        myRigidbody = GetComponent<Rigidbody2D>();  
        endPosition = new Vector2(gameObject.transform.position.x, gameObject.transform.position.y - 0.04f);
        jumpPosition = new Vector2(gameObject.transform.position.x, gameObject.transform.position.y + 0.50f);
    }

    void Update()
    {
        if (!changeVector)
        {
            JumpAnimation();          
        }
        else
        {
            ReturnToStart();
        }

        // Restart Animation
        if (testButton)
        {
            gameObject.transform.position = new Vector2(gameObject.transform.position.x, gameObject.transform.position.y + 0.04f);
            changeVector = false;
            speed = 2f;
            testButton = false;
        }
            
    }



    public void JumpAnimation()
    {
        Vector2 currentPosition = gameObject.transform.position;
        if (currentPosition != jumpPosition)
        {
            Vector3 temp = Vector3.MoveTowards(transform.position, jumpPosition, speed * Time.deltaTime);
            myRigidbody.MovePosition(temp);
            speed += 0.2f;
        }
        else
        {
            changeVector = true;
            speed = 10;
        }

    }

    public void ReturnToStart()
    {
        Vector2 currentPosition = gameObject.transform.position;
        if (currentPosition != endPosition)
        {
            Vector3 temp = Vector3.MoveTowards(transform.position, endPosition, speed * Time.deltaTime);
            myRigidbody.MovePosition(temp);
            speed += 0.3f;
        }
        else
        {
            speed = 0;
        }

    }


}
