using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Aranha : EnemyBehaviour
{
    public bool started;

    public float startDistance;


    public void Start()
    {
        started = false;
    }

    public void Update()
    {
        if(!started)
        {
            CheckStart();
            
        }
        else
        {
            if (!target)
            {
                RandomMovement();
            }
            else
            {
                ChaseMovement();
            }
        }

    }

    public void CheckStart()
    {
        if (Vector3.Distance(target.position, transform.position) <= startDistance)
        {
            Debug.Log("CheckStart");
            started = true;

            StartCoroutine(StartAranhaCo());
        }
    }

    public void RandomMovement()
    {

    }

    public void ChaseMovement()
    {


        Debug.Log("Chase Movement Aranha");



            CheckDistance();

    }

    public IEnumerator StartAranhaCo()
    {

        isPaused = true;
        myAnimator.SetBool("Started", true);

        yield return new WaitForSeconds(1f);

        isPaused = false;
        myAnimator.SetBool("Started", false);


    }

}
