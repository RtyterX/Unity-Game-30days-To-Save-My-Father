using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputController : MonoBehaviour
{
    [Header("Basics")]
    public float Horizontal;   // Right and Left Arrow
    public float Vertical;     // Up and Down Arrow
    public float Run;          // Shift
    public float Attack;       // Mouse Left
    public float Attack2;      // Mouse Right

    [Header("Menus")]
    public float Pause;        // Esc
    public float Status;       // T
    public float Map;          // M
    public float Inventory;    // Tab - I
    public float PassTime;     // P

    [Header("Others")]
    public float Interect;     // Space
    public float Cure;         // Q

    [Header("N° Shortcuts")]
    public float number1;            // 1
    public float number2;            // 2
    public float number3;            // 3
    public float number4;            // 4
    public float number5;            // 5


    void FixedUpdate()
    {
        Horizontal = Input.GetAxisRaw("Horizontal");
        Vertical = Input.GetAxisRaw("Vertical");
        Run = Input.GetAxisRaw("Running");
        Attack = Input.GetAxisRaw("Attack");
        Attack2 = Input.GetAxisRaw("Attack2");
        Pause = Input.GetAxisRaw("Pause");
        Status = Input.GetAxisRaw("Status");
        Map = Input.GetAxisRaw("Map");
        Inventory = Input.GetAxisRaw("Inventary");
        PassTime = Input.GetAxisRaw("PassTime");
        Interect = Input.GetAxisRaw("Interect");
        Cure = Input.GetAxisRaw("Cure");
        number1 = Input.GetAxisRaw("1");
        number2 = Input.GetAxisRaw("2");
        number3 = Input.GetAxisRaw("3");
        number4 = Input.GetAxisRaw("4");
        number5 = Input.GetAxisRaw("5");

    }

}
