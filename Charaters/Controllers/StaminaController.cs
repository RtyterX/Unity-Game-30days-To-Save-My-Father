using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class StaminaController : MonoBehaviour
{
    [SerializeField] public float currentStamina;
    [SerializeField] public float maxStamina;

    [Header("Recharge")]
    [SerializeField] private bool startRecover;
    [SerializeField] private float rechargeRate;
    [SerializeField] private float rechargeTime;
    [SerializeField] private bool canRecharge;               // Utilize for Counter Effects only to Stop Recharge Stamina. Ex: Ice

    [Space(8)]

    [SerializeField] public bool isTired;
    [SerializeField] public float tiredDelay;

    [Header("Timer")]
    [HideInInspector] private bool timerActive;
    [HideInInspector] private float timer;

    [HideInInspector] private BehaviourController obj;

    void Start()
    {
        // Start Values
        currentStamina = maxStamina;
        startRecover = false;
        timerActive = false;
        canRecharge = true;
        isTired = false;

        obj = GetComponent<BehaviourController>();
    }

    void Update()
    {
        // Check if Timer has passed the time
        if (timer > rechargeTime)
        {
            timerActive = false;                       // Stop Timer
            timer = 0;                                 // Reset Timer value
            startRecover = true;                       // Then start recovering stamina

        }

        // Recovery Loop
        if (startRecover)
        {
            if (currentStamina < maxStamina)          // Check if Stamina is lower than Max
            {
                if (canRecharge)
                {
                    currentStamina += rechargeRate;   // Recover Stamina by Frame
                }
            }
            else
            {
                startRecover = false;                 // Stop Recovery
                currentStamina = maxStamina;          // Maintain Stamina not above Max
            }

        }

    }

    // ---------------------- TIMER ----------------------
    // Must be FixedUpdate to get real time in game
    public void FixedUpdate()
    {
        if (timerActive)                              // If timer is Started
        {
            timer += Time.deltaTime;                  // Count Time
        }
    }


    // **Can be used for enemys to Hit Players Stamina
    public void UseStamina(float usedStamina)
    {
        if (currentStamina >= 0)
        {
            startRecover = false;                // Recover off
            currentStamina -= usedStamina;       // Stamina Value
            timerActive = true;                  // Start Timer
            timer = 0;                           // Reset Timer value
        }
        else
        {
            IsTired();
        }

    }


    public void AddStamina(float addStamina)
    {
        startRecover = false;                // Recover off
        currentStamina += addStamina;        // Stamina Value        
        if (currentStamina > maxStamina)     // Check if Stamina is higher than Max
        {
            currentStamina = maxStamina;
        }
        else                                 // If its not...
        {
            timer = 0;                       // Reset Recover Timer
        }

    }


    public void ChangeStaminaRecover(float newRechargeRate, float newRechargeTime, float duration)
    {
        float oldRechargeRate = rechargeRate;
        float oldRechargeTime = rechargeTime;

        rechargeRate = newRechargeRate;
        rechargeTime = newRechargeTime;

        Invoke("ReturnRecoverToNormal(oldRechargeRate, oldRechargeTime, duration)", duration);
    }


    public void ReturnRecoverToNormal(float oldRechargeRate, float oldRechargeTime, float duration)
    {
        //yield return new WaitForSeconds(duration);                  // Wait duration Time before 
        rechargeRate = oldRechargeRate;                             // change back to old values
        rechargeTime = oldRechargeTime;
    }


    public void IsTired()
    {
        isTired = true;
        obj.ChangeState(StateMachine.Tired);       // Change State
        timerActive = false;                          // Stop Timer
        Invoke("RecoverTired", tiredDelay);           // Wait for Tired Status to Stop    
    }

    public void RecoverTired()
    {
        isTired = false;
        obj.ChangeState(StateMachine.Idle);       // Change State
        timer = 0;                                    // Reset Timer Value
        timerActive = true;                           // Start Timer
    }
}

// ------- FUTURE IMPROVEMENTS -------  
// ReturnRecoverToNormalCo is better being a Coroutine or being utilized with Invoke ?
// UseStamina Check if Character can Use or Not (if player has above 0 Stamina)