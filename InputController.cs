using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputController : MonoBehaviour
{
    // Inputs
    [Header("Inputs")]
    public float inputHorizontal;
    public float inputVertical;
    public float inputRunning;
    public float inputAttacking;
    public float inputInterect;
   // public float inputInventary;
    

    void Update()
    {
        inputHorizontal = Input.GetAxisRaw("Horizontal");
        inputVertical = Input.GetAxisRaw("Vertical");
        inputRunning = Input.GetAxisRaw("Running");
        inputAttacking = Input.GetAxisRaw("Attack");
        inputInterect = Input.GetAxisRaw("Interect");
        // inputInventary = Input.GetAxisRaw("Inventary");
    }
}
