using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimeControl : MonoBehaviour
{
    public bool isPaused;

    [Header("Time In Game")]
    [HideInInspector] public int hoursInGame;
    [HideInInspector] public int minutesInGame;
    public float days;
    public string timeInGame;
    
    [HideInInspector]public float realTime;

    [Space(10)]
    [SerializeField] private Text clock;

    void Awake()
    {
        hoursInGame = 11;
        minutesInGame = 15;
    }
    
    void FixedUpdate()
    {
        if (!isPaused)
        {
            realTime += Time.deltaTime;
            minutesInGame = (int)realTime / 2;

            // Minutes Reset
            if (minutesInGame >= 60)
            {
                hoursInGame += 1;
                realTime -= 120f;
                minutesInGame -= 60;
            }

            // Hours Reset
            if (hoursInGame >= 24)
            {
                days += 1;
                hoursInGame = 0;
            }
            
            // Time String
            timeInGame = (hoursInGame.ToString("00") 
                + ":" 
                + minutesInGame.ToString("00"));

            // Show Time on The Screen
            clock.text = timeInGame;
        }
    }

    // Add Timme
    public void TimeAdd(int hours, int minutes)
    {
        hoursInGame += hours;
        minutesInGame += minutes;
    }


    // ?????????
    public void TimeConverter(int hours, int minutes)
    {
        if(minutes >= 60)
        {
            hoursInGame += 1;
            realTime -= 120f;
            minutesInGame -= 60;
        }
        else
        {
            realTime += minutes * 2;
            hoursInGame += hours;
        }
    }
}
