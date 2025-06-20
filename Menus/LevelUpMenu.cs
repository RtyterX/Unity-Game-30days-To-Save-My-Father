using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelUpMenu : MonoBehaviour
{
    public double increasePoints;
    [HideInInspector] public double maxPoints = 5;

    [Header("Stats")]
    public double newVitality;
    public double newEndurance;
    public double newStrength;
    public double newResistence;
    public double newArcane;
    public double newLuck;

    // Components
    [HideInInspector] UnityEngine.GameObject player;
    [HideInInspector] StatsController statsController;
    [HideInInspector] LevelController levelController;

    [Header("UI")]
    public Text increasePointsTxt;
    public Text newVitalityTxt;
    public Text newEnduranceTxt;
    public Text newStrengthTxt;
    public Text newResistenceTxt;
    public Text newArcaneTxt;
    public Text newLuckTxt;


    // Start is called before the first frame update
    void Start()
    {
        increasePoints = increasePoints + maxPoints;

        // Get Components
        player = UnityEngine.GameObject.FindWithTag("Player");
        statsController = player.GetComponent<StatsController>();
        levelController = player.GetComponent<LevelController>();

        // Get Stats Values
        newVitality = statsController.vitality;
        newEndurance = statsController.endurance;
        newStrength = statsController.strength;
        newResistence = statsController.resistance;
        newArcane = statsController.arcane;
        newLuck = statsController.luck;

        UpdateLevelUpMenuUI();
    }


    // ----------  BUTTONS ----------  

    // ---- Increase ----
    #region
    public void IncreaseVitality()
    {
        increasePoints = increasePoints - 1;

        if (increasePoints < 0)
        {
            increasePoints = 0;
        }
        else
        {
            newVitality = newVitality++;
            UpdateLevelUpMenuUI();
        }
    }

    public void IncreaseEndurance()
    {
        increasePoints = increasePoints - 1;

        if (increasePoints < 0)
        {
            increasePoints = 0;
        }
        else
        {
            newEndurance = newEndurance++;
            UpdateLevelUpMenuUI();
        }
    }

    public void IncreaseStrength()
    {
        increasePoints = increasePoints - 1;

        if (increasePoints < 0)
        {
            increasePoints = 0;
        }
        else
        {
            newStrength = newStrength++;
            UpdateLevelUpMenuUI();
        }
    }

    public void IncreaseResistence()
    {
        increasePoints = increasePoints - 1;

        if (increasePoints < 0)
        {
            increasePoints = 0;
        }
        else
        {
            newResistence = newResistence++;
            UpdateLevelUpMenuUI();
        }
    }

    public void IncreaseArcane()
    {
        increasePoints = increasePoints - 1;

        if (increasePoints < 0)
        {
            increasePoints = 0;
        }
        else
        {
            newArcane = newArcane++;
            UpdateLevelUpMenuUI();
        }
    }

    public void IncreaseLuck()
    {
        increasePoints = increasePoints - 1;

        if (increasePoints < 0)
        {
            increasePoints = 0;
        }
        else
        {
            newLuck = newLuck++;
            UpdateLevelUpMenuUI();
        }
    }
    #endregion

    // ---- Decrease ----
    #region
    public void DecreaseVitality()
    {
        increasePoints = increasePoints + 1;

        if (increasePoints > maxPoints)
        {
            increasePoints = maxPoints;
        }
        else
        {
            newVitality = newVitality--;
            UpdateLevelUpMenuUI();
        }
    }

    public void DecreaseEndurance()
    {
        increasePoints = increasePoints + 1;

        if (increasePoints > maxPoints)
        {
            increasePoints = maxPoints;
        }
        else
        {
            newEndurance = newEndurance--;
            UpdateLevelUpMenuUI();
        }
    }

    public void DecreaseStrength()
    {
        increasePoints = increasePoints + 1;

        if (increasePoints > maxPoints)
        {
            increasePoints = maxPoints;
        }
        else
        {
            newStrength = newStrength--;
            UpdateLevelUpMenuUI();
        }
    }

    public void DecreaseResistence()
    {
        increasePoints = increasePoints + 1;

        if (increasePoints > maxPoints)
        {
            increasePoints = maxPoints;
        }
        else
        {
            newResistence = newResistence--;
            UpdateLevelUpMenuUI();
        }
    }

    public void DecreaseArcane()
    {
        increasePoints = increasePoints + 1;

        if (increasePoints > maxPoints)
        {
            increasePoints = maxPoints;
        }
        else
        {
            newArcane = newArcane--;
            UpdateLevelUpMenuUI();
        }
    }

    public void DecreaseLuck()
    {
        increasePoints = increasePoints + 1;

        if (increasePoints > maxPoints)
        {
            increasePoints = maxPoints;
        }
        else
        {
            newLuck = newLuck--;
            UpdateLevelUpMenuUI();
        }
    }
    #endregion

    // ---- Confirm / Cancel ----

    public void ConfirmLevelUp()
    {
        statsController.vitality = newVitality;  
        statsController.endurance = newEndurance;
        statsController.strength = newStrength;
        statsController.resistance = newResistence;
        statsController.arcane = newArcane;
        statsController.luck = newLuck;

        levelController.LevelUp();

        gameObject.SetActive(false);
    }

    public void UpdateLevelUpMenuUI()
    {
        increasePointsTxt.text = increasePoints.ToString();
        newVitalityTxt.text = newVitality.ToString();
        newEnduranceTxt.text = newEndurance.ToString();
        newStrengthTxt.text = newStrength.ToString(); ;
        newResistenceTxt.text = newResistence.ToString();
        newArcaneTxt.text = newArcane.ToString();
        newLuckTxt.text = newLuck.ToString();
    }

}
