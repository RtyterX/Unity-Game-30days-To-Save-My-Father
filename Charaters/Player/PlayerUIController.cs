using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerUIController : MonoBehaviour
{
    // UI
    [SerializeField] private Text lifeUI;
    [SerializeField] private Slider healthBar;
    [SerializeField] private Slider staminaBar;

    // Components
    [HideInInspector] private GameObject player;
    [HideInInspector] private HealthController health;
    [HideInInspector] private StaminaController stamina;

    void Start()
    {
        // Get Components
        player = GameObject.FindWithTag("Player");
        health = player.GetComponent<HealthController>();
        stamina = player.GetComponent<StaminaController>();
    }

    void Update()
    {
        // --- Update UI ---

        if (lifeUI)
        {
            lifeUI.text = health.life.ToString();
        }

        if (healthBar)
        {
            healthBar.value = (float)health.currentHealth / (float)health.maxHealth;
        }

        if (staminaBar)
        {
            staminaBar.value = stamina.currentStamina / stamina.maxStamina;
        }

    }

}


// Não ficar checando o tempo todo. Se o valor estiver ok, ficar atualizando todo frame  


