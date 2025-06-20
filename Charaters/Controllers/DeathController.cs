using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;

public enum TypeOfDeath 
{ 
    NotDead,
    HealthBelow0,
    Deadline,

    // Effects
    Fire,
    Poison,
    Sleep,
    Fear,

    // Scenario
    Hole,
    Drownning,
}

public class DeathController : MonoBehaviour
{
    public UnityEngine.GameObject player;

    [Header("UI")]
    [SerializeField] public UnityEngine.GameObject deathMenuUI;
    [SerializeField] private Text deathTextUI;

    [Space(2)]

    public TypeOfDeath type = TypeOfDeath.NotDead;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // ---------------- Methods ----------------
    public IEnumerator OpenDeathMenuCo(TypeOfDeath Type)
    {
        player.TryGetComponent<PlayerMovement2D>(out PlayerMovement2D playerMovement);
        player.TryGetComponent<PlayerBehaviour>(out PlayerBehaviour playerBehaviour);

        // Check if Death Menu is Active
        if (!deathMenuUI.activeSelf)
        {
            // Death when Health goes Below 0 but no Effects applyed
            if (Type == TypeOfDeath.HealthBelow0)
            {
                Type = TypeOfDeath.HealthBelow0;
                playerMovement.isPaused = true;
                playerBehaviour.state = StateMachine.Dead;
                // Make Animation
                deathTextUI.text = "Você Morreu";
                yield return new WaitForSeconds(1f);
                deathMenuUI.SetActive(true);
            }

            // Death by Time Deadline
            if (Type == TypeOfDeath.Deadline)
            {
                Type = TypeOfDeath.Deadline;
                playerMovement.isPaused = true;
                playerBehaviour.state = StateMachine.Dead;
                // Make Animation
                deathTextUI.text = "Seu Tempo acabou...";
                yield return new WaitForSeconds(1f);
                deathMenuUI.SetActive(true);
            }


        }

    }

}

