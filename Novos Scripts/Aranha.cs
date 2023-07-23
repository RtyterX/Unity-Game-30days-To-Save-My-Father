using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Aranha : Enemy
{
    public bool started = false;

    public void Start()
    {

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

    }

    public void RandomMovement()
    {

    }

    public void ChaseMovement()
    {
        Debug.Log("Chase Movement Aranha");
    }

    public IEnumerator StartAranhaCo()
    {

        isPaused = true;
        myAnimator.SetBool("Started", true);

        //Invoke

        yield return new WaitForSeconds(3f);

        isPaused = false;
        myAnimator.SetBool("Started", false);


    }

    public IEnumerator AttackAranhaCo()
    {
        isPaused = true;
        myAnimator.SetBool("Attack", true);

        //Invoke

        yield return new WaitForSeconds(3f);

        isPaused = false;
        myAnimator.SetBool("Attack", false);
    }

}
