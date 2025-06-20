using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum Months
{
    January,
    February,
    March,
    April,
    May,
    June,
    July,
    August,
    September,
    October,
    November,
    Dezember,
}

public class TimeControl : MonoBehaviour
{
    public bool isPaused;

    [Header("Time In Game")]
    [HideInInspector] public int hoursInGame;
    [HideInInspector] public int minutesInGame;
    public int passedDays;
    public string timeInGame;

    [Header("Date In Game")]
    public int day;
    public Months month;
    public string date;

    //[HideInInspector]
    public float realTime;

    [Space(10)]

    [Header("HUD")]
    [SerializeField] private Text clockHUD;
    [SerializeField] private Text dayHUD;

    public UnityEngine.GameObject player;


    void Awake()
    {
        hoursInGame = 23;
        minutesInGame = 57;
        day = 29;
        month = Months.June;
    }
    
    void FixedUpdate()
    {
        if (!isPaused)
        {
            realTime += Time.deltaTime;
            minutesInGame = (int)realTime / 2;
            
            if (day >= 30)
            {
                isPaused = true;
                player.TryGetComponent<DeathController>(out DeathController death);
                StartCoroutine(death.OpenDeathMenuCo(TypeOfDeath.Deadline));
            }

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
                hoursInGame = 0;
                day++;
                passedDays += 1;


                // Months Change
                #region
                if (month == Months.January)
                {
                    if (day >= 32)
                    {
                        day = 1;
                        month = Months.February;
                    }
                }

                if (month == Months.February)
                {
                    if (day >= 29)
                    {
                        day = 1;
                        month = Months.March;
                    }
                }

                if (month == Months.March)
                {
                    if (day >= 32)
                    {
                        day = 1;
                        month = Months.April;
                    }
                }

                if (month == Months.April)
                {
                    if (day >= 31)
                    {
                        day = 1;
                        month = Months.May;
                    }
                }

                if (month == Months.May)
                {
                    if (day >= 32)
                    {
                        day = 1;
                        month = Months.June;
                    }
                }

                if (month == Months.June)
                {
                    if (day >= 31)
                    {
                        day = 1;
                        month = Months.July;
                    }
                }

                if (month == Months.July)
                {
                    if (day >= 32)
                    {
                        day = 1;
                        month = Months.August;
                    }
                }

                if (month == Months.August)
                {
                    if (day >= 32)
                    {
                        day = 1;
                        month = Months.September;
                    }
                }

                if (month == Months.September)
                {
                    if (day >= 31)
                    {
                        day = 1;
                        month = Months.October;
                    }
                }

                if (month == Months.October)
                {
                    if (day >= 32)
                    {
                        day = 1;
                        month = Months.November;
                    }
                }

                if (month == Months.November)
                {
                    if (day >= 31)
                    {
                        day = 1;
                        month = Months.Dezember;
                    }
                }

                if (month == Months.Dezember)
                {
                    if (day >= 32)
                    {
                        day = 1;
                        month = Months.January;
                    }
                }
                #endregion
            }

            // Time String
            timeInGame = (hoursInGame.ToString("00") 
                + ":" 
                + minutesInGame.ToString("00"));

            // Date String
            date = (day.ToString("00")
                + " de "
                + month.ToString(""));


            // Show Time on The Screen
            clockHUD.text = timeInGame;

            // Show Days on The Screen
            dayHUD.text = day.ToString("00");

        }
    }

    // Add Timme
    public void TimeAdd(int hours, int minutes)
    {
        hoursInGame += hours;
        minutesInGame += minutes;
    }


    // ----------- Time Converter ----------- 
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

    // ----------- Days Converter ----------- 
    public void DaysConverter(int addDays, out string newDate)
    {

        int newDays = day + addDays;
        Months actualMonth = month;

        // Months Change
        #region
        if (actualMonth == Months.January)
        {
            if (newDays >= 32)
            {
                newDays = 1;
                actualMonth = Months.February;
            }
        }

        if (actualMonth == Months.February)
        {
            if (newDays >= 29)
            {
                newDays = 1;
                actualMonth = Months.March;
            }
        }

        if (actualMonth == Months.March)
        {
            if (newDays >= 32)
            {
                newDays = 1;
                actualMonth = Months.April;
            }
        }

        if (actualMonth == Months.April)
        {
            if (newDays >= 31)
            {
                newDays = 1;
                actualMonth = Months.May;
            }
        }

        if (actualMonth == Months.May)
        {
            if (newDays >= 32)
            {
                newDays = 1;
                actualMonth = Months.June;
            }
        }

        if (actualMonth == Months.June)
        {
            if (newDays >= 31)
            {
                newDays = 1;
                actualMonth = Months.July;
            }
        }

        if (actualMonth == Months.July)
        {
            if (newDays >= 32)
            {
                newDays = 1;
                actualMonth = Months.August;
            }
        }

        if (actualMonth == Months.August)
        {
            if (newDays >= 32)
            {
                newDays = 1;
                actualMonth = Months.September;
            }
        }

        if (actualMonth == Months.September)
        {
            if (newDays >= 31)
            {
                newDays = 1;
                actualMonth = Months.October;
            }
        }

        if (actualMonth == Months.October)
        {
            if (newDays >= 32)
            {
                newDays = 1;
                actualMonth = Months.November;
            }
        }

        if (actualMonth == Months.November)
        {
            if (newDays >= 31)
            {
                newDays = 1;
                actualMonth = Months.Dezember;
            }
        }

        if (actualMonth == Months.Dezember)
        {
            if (newDays >= 32)
            {
                newDays = 1;
                actualMonth = Months.January;
            }
        }
        #endregion

        newDate = (newDays.ToString("00")
                                     + " de "
                                     + actualMonth.ToString(""));

    }

}
