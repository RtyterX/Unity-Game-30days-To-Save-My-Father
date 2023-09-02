using System;
using System.Collections;
using UnityEngine;

public class InventaryScript
{

    public static bool InventoryOn = false;

     // Inputs
    float inputHorizontal;
    float inputVertical;
    float inputConfirmation;
    float inputCancelation;
    float inputInspect;

    void Update()
    {
        //Movement Inputs
        inputHorizontal = Input.GetAxisRaw("Horizontal");
        inputVertical = Input.GetAxisRaw("Vertical");
     //   inputConfirmation = Input.GetKeyDown("Confirmation");
     //   inputCancelation = Input.GetKeyDown("Cancelation");
      //  inputInspect = Input.GetKeyDown("Inspect");

    }

    void FixedUpdate()
    {

        if (InventoryOn == true)
        {

        }
    }

}
