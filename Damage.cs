using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Damage : MonoBehaviour
{
    public HealthControl healthPlayer;
    public float damage = 1;

    void Start()
    {
        healthPlayer = gameObject.GetComponent<HealthControl>();
        
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.tag == "Inimigo" || col.gameObject.tag == "AtaqueInimigo")
        {
            // mudar pra diminuir a vida e nao destruir
            Debug.Log("Colisão com o Player");
            healthPlayer.health = healthPlayer.health - damage;
        }

    }
}
