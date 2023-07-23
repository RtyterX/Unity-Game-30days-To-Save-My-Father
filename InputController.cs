using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputController : MonoBehaviour
{

    // Inputs
    public float inputHorizontal;
    public float inputVertical;
    public float inputRunning;
    public float inputAttacking;
    public float inputInterect;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        inputHorizontal = Input.GetAxisRaw("Horizontal");
        inputVertical = Input.GetAxisRaw("Vertical");
        inputRunning = Input.GetAxisRaw("Jump");
        inputAttacking = Input.GetAxisRaw("Attack");
        inputInterect = Input.GetAxisRaw("Interect");
    }
}
