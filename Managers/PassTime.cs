using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class PassTime : MonoBehaviour
{
    // Components
    [Header("Components")]
    public TimeControl clock;
    public PlayerMovement player;

    // Pass Time
    [Header("Pass Time")]
    public int timeToAdd = 1;
    public int goalHours;
    public int showNumber;
    public int tempHours;
    public string finalDate;

    // Delays
    [Header("Delays")]
    public float delay;
    public float howFastItPass;

    // Bools
    [Header("Bools")]
    public bool canCloseUI;
    public bool canPassTime;
    [SerializeField] private bool ActiveUI;
    [SerializeField] private bool activePassTime;

    // UI Elements
    [Header("UI Elements")]
    public GameObject passTimeUI;
    public Text addTimeUI;
    public Text ClockTimeUI;
    public Text DateUI;
    public GameObject blackScreen;

    // Buttons
    [Header("Buttons")]
    [SerializeField] private GameObject startPassTimeButton;
    [SerializeField] private GameObject addTimeButton;
    [SerializeField] private GameObject decreaseTimeButton;
    [SerializeField] private GameObject cancelButton;


    void Start()
    {
        canPassTime = true;
        ActiveUI = true;
        howFastItPass = 100;
    }

    // UI Active KeyDown
    void Update()
    {
        // If Player Press T...
        if (Input.GetKeyDown(KeyCode.T))
        {

            if (canPassTime)
            {
                // Turn UI On
                if (ActiveUI)
                {
                    // Deactive UI
                    passTimeUI.SetActive(true);

                    // Stop Player and Time
                    player.isPaused = true;
                    Time.timeScale = 0f;

                    // Bools
                    canCloseUI = true;
                    ActiveUI = false;

                    UpdateAddTimeUI();
                }
                else // Turn UI Off
                {
                    if (canCloseUI)
                    {
                        // Active UI
                        passTimeUI.SetActive(false);

                        // Time and Player goes back to normal
                        player.isPaused = false;
                        Time.timeScale = 1f;

                        // Bools
                        ActiveUI = true;

                        // Reset Stats
                        goalHours = 0;
                        timeToAdd = 1;
                    }
                }
            }
        }
    }


    void FixedUpdate()
    {
        ClockTimeUI.text = clock.timeInGame;
        DateUI.text = clock.date;

        // If Pass Time is Active...
        if (activePassTime)
        {
            ChangeAddTimeUI();

            // If time has passed...
            if (clock.hoursInGame == goalHours && clock.date == finalDate)
            {
                canCloseUI = true;
                activePassTime = false;

                // Time Pause
                Time.timeScale = 0f;

                // Reset Stats
                goalHours = 0;
                timeToAdd = 1;

                // Active/Deactive Buttons
                startPassTimeButton.SetActive(true);
                addTimeButton.SetActive(true);
                decreaseTimeButton.SetActive(true);
                cancelButton.SetActive(false);

                // Background goes back to Normal
                blackScreen.SetActive(false);

                UpdateAddTimeUI();
            }
        }

    }


    // ---------- Methods -----------

    // --- *Button* ---
    // Start Pass Time 
    public void StartPassTime()
    {
        goalHours = clock.hoursInGame + timeToAdd;

        int daysToGo = 0;

        if (goalHours > 24)
        {
            goalHours -= 24;

            daysToGo = 1;

            Debug.Log("Final Date = " + finalDate);
        }

        clock.DaysConverter(daysToGo, out finalDate);

        tempHours = clock.hoursInGame;
        showNumber = timeToAdd;

        canCloseUI = false;
        activePassTime = true;

        // Active/Deactive Buttons
        startPassTimeButton.SetActive(false);
        addTimeButton.SetActive(false);
        decreaseTimeButton.SetActive(false);
        cancelButton.SetActive(true);

        // Background goes Black
        blackScreen.SetActive(true);

        // Pass Time
        Time.timeScale = howFastItPass;

    }

    // --- *Button* ---
    // Add Time To Pass
    public void AddPassTime()
    {
        if (timeToAdd <= 23)
        {
            timeToAdd++;
        }
        else
        {
            timeToAdd = 24;
        }

        UpdateAddTimeUI();
    }

    // --- *Button* ---
    // Decrease Pass Time
    public void DecreasePassTime()
    {
        // Pass Time cant be lesser than 1
        if (timeToAdd > 1)
        {
            timeToAdd--;
        }

        UpdateAddTimeUI();
    }

    // --- *Button* ---
    // Cancel Pass Time
    public void CancelPassTime()
    {
        canCloseUI = true;
        activePassTime = false;

        // Time Goes Back to Normal
        Time.timeScale = 1f;
        
        // Reset Stats
        goalHours = 0;
        timeToAdd = 1;

        // Active/Deactive Buttons
        startPassTimeButton.SetActive(true);
        addTimeButton.SetActive(true);
        decreaseTimeButton.SetActive(true);
        cancelButton.SetActive(false);

        // Background goes back to Normal
        blackScreen.SetActive(false);

        // Close UI
        passTimeUI.SetActive(false);

        // Pause Player
        player.isPaused = false;

        UpdateAddTimeUI();

    }


    // ------------ UI --------------

    // Show Add Time UI
    public void UpdateAddTimeUI()
    { 
        addTimeUI.text = timeToAdd.ToString("00");
    }

    // Update Add Time UI
    public void ChangeAddTimeUI()
    {
        if (showNumber == 1)
        {
            if (clock.minutesInGame >= 40)
            {
                showNumber--;
            }
        }

        if (clock.hoursInGame != tempHours)
        {
            showNumber--;
            tempHours = clock.hoursInGame;
        }

        addTimeUI.text = showNumber.ToString("00");
    }

}
