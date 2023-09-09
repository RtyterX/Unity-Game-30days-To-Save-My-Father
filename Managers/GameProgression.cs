using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameProgression : MonoBehaviour
{
    // Stats
    string playerName;
    int daysPassed;
    int timePlayed;
    int deaths;
    int enemysKilled;
    int playerLevel;
    int moneyEarned;
    int moneySpended;
    int gameComplete;


    // UI
    public GameObject GameProgressionMenu;
    public Text playerNameUI;
    public Text daysPassedUI;
    public Text timePlayedUI;
    public Text deathsUI;
    public Text enemysKilledUI;
    public Text playerLevelUI;
    public Text moneyEarnedUI;
    public Text moneySpendedUI;
    public Text gameCompleteUI;


    // Always Checking Stats
    void FixedUpdate()
    {
        
    }
}
