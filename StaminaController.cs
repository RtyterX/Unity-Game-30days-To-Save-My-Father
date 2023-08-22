using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StaminaController : MonoBehaviour
{
    public bool isPaused;

    // Components
    [Header("Components")]
    [SerializeField] private Slider staminaBar;
    [SerializeField] private PlayerBehaviour player;

    // Stamina
    [Header("Stamina")]
    public float currentStamina;
    [SerializeField] private float maxStamina;
    public bool isTired;

    // Recovery
    [Space(20)]
    [SerializeField] private bool startRecover;
    [SerializeField] private int recoveryTime;
    [SerializeField] private float chargeSpeed;
    public float tiredDelay;

    // Timer
    [Header("Timer")]
    [SerializeField] private bool timerActive;
    [SerializeField] private float timer;
    [SerializeField] private float startRecoverDelay;


    void Start()
    {
        // Components
        player = GetComponent<PlayerBehaviour>();
        staminaBar = GetComponent<Slider>();

        // Start Values
        currentStamina = maxStamina;
        startRecover = false;
        timerActive = false;
        isTired = false;
    }


    void Update()
    {
       // UpdateStaminaBar();

        if (!isPaused)
        {
            // Check if Stamina is below 0
            if (currentStamina < 0)
            {
                // Player is Tired
                isTired = true;

                StopAllCoroutines();
                StartCoroutine(PlayerIsTiredCo());

                // Stop Stamina Check
                currentStamina = 0;
            }
            else
            {
                if (!isTired)
                {
                    // Start Recovery
                    if (timer >= startRecoverDelay)
                    {
                        // Start Recovery
                        startRecover = true;

                        // Reset Timer
                        timerActive = false;
                        timer = 0;
                    }

                    // Recovery Loop
                    if (startRecover)
                    {
                        if (currentStamina < maxStamina)
                        {
                            StartCoroutine(RecoverStaminaCo());
                        }
                        else  // Stop Recovery
                        {
                            StopAllCoroutines();
                            startRecover = false;
                            currentStamina = maxStamina;
                        }

                    }
                }   
            }

        }
    }


    // ------ Timer ------

    // Must be FixedUpdate to get real time in game

    // If timer is started, Count Time
    public void FixedUpdate()
    {
        if (timerActive)
        {
            timer += Time.deltaTime;
        }
    }


    // ------ Methods ------

    public void UseStamina(float usedStamina)
    {
        // Recovery Off
        StopAllCoroutines();
        startRecover = false;

        // Stamina Value
        currentStamina -= usedStamina;

        // Acrive Timer
        timerActive = true;
        timer = 0;
    }

    public void AddStamina(float addStamina)
    {
        // Recovery off
        StopAllCoroutines();
        startRecover = false;

        // Stamina Value
        currentStamina += addStamina;

        // Deactive Timer
        timerActive = false;
        timer = 0;
    }


    // ------ Coroutines ------

    // Recover Stamina Coroutine
    IEnumerator RecoverStaminaCo()
    {
        currentStamina += chargeSpeed;
        yield return null;
    }

    IEnumerator PlayerIsTiredCo()
    {
        // Start Timer
        timer = 0;
        timerActive = true;

        // Wait for Tired Status to Stop
        yield return new WaitForSeconds(tiredDelay);

        // Start Recover
        isTired = false;
        startRecover = true;
    }


    // ------ Stamania Bar UI ------

    // Stamina Bar
    public void UpdateStaminaBar()
    {
        staminaBar.value = (int)currentStamina / (int)maxStamina;
    }

}