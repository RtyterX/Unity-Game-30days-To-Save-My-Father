using UnityEngine;
using UnityEngine.SceneManagement;
using System;
using System.Collections;

public enum EnemyState
{
    Idle,
    Walk,
    Attacking,
}

public class Inimigo : MonoBehaviour
{

    public EnemyState state;

    public Transform targetPlayer;
    Rigidbody2D myRigidbody;
    public Animator myAnimator;

    public float health;
    public int level;

    public float speed = 2.5f;
    public float maxSpeed = 0.5f;

    // Start is called before the first frame update
    void Start()
    {
        state = EnemyState.Idle;

        myAnimator = GetComponent<Animator>();
        myRigidbody = GetComponent<Rigidbody2D>();
        
    }

    // Update is called once per frame
    void Update()
    {

        if (!targetPlayer)
        {
            GetTarget();
        }

        IEnumerator GetTarget()
        {
            if (GameObject.FindGameObjectWithTag("Player"))
            {
                targetPlayer = GameObject.FindGameObjectWithTag("Player").transform;
                yield return new WaitForSeconds(3f);
            }
        }



    }

    public void FixedUpdate()
    {
        
        


    }
}
