using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;

public class LevelController : MonoBehaviour
{
    public int currentLevel;

    [Header("EXP")]
    public int currentEXP;
    public int nextLevelEXP;

    [Header("Level Up! Animation")]
    public bool canLevelUp;
    public UnityEngine.GameObject levelUpAnimation;
    public UnityEngine.GameObject levelUpIcon;

    public void GainEXP(int expEarned)
    {
        currentEXP += expEarned;

        if (currentEXP > nextLevelEXP)
        {
            currentEXP = nextLevelEXP;                                         // 
            canLevelUp = true;
            StartCoroutine(LevelUpUICo());

            Debug.Log("Ganhou " + expEarned + " de XP");
        }
    }

    public void LoseEXP(int expLost)
    {
        currentEXP -= expLost;

        if (currentEXP < 0)
        {
            currentEXP = 0;
            Debug.Log("Perdeu XP... ");
        }
    }

    public void LevelUp()
    {
        currentLevel++;                                                    // Current Level + 1
        currentEXP = 0;
        nextLevelEXP = currentLevel * 15;                                  // Calculate Next Level EX
        canLevelUp = false;

        StatsController stats = GetComponent<StatsController>();           // Get Component Stats
        stats.level = currentLevel;                                        // Transfer Level to Stats

        levelUpIcon.SetActive(false);

        Debug.Log("Passou de Level!");
    }


    IEnumerator LevelUpUICo()
    {
        yield return new WaitForSeconds(10);
        levelUpAnimation.SetActive(true);                                 // Start Level Up! Animation
        yield return new WaitForSeconds(5);
        levelUpIcon.SetActive(true);
        yield return new WaitForSeconds(20);
        levelUpAnimation.SetActive(false);
    }
}
