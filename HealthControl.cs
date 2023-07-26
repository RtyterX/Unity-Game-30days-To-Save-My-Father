using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class HealthControl : MonoBehaviour
{
     // Player life
    public int life = 2;
    public float health = 1;
    public float maxHealth = 1;

      // Components
    Animator myAnimation;
    Player ourPlayer;
    RespawnPointer respawnPoint;

      //Respawn
    public Vector2 respawnLocation;

    // Start is called before the first frame update
    void Start()
    {
        ourPlayer = gameObject.GetComponent<Player>();
        myAnimation = gameObject.GetComponent<Animator>();
        respawnPoint = gameObject.GetComponent<RespawnPointer>();

        respawnLocation = respawnPoint.respawnPosition;
    }

    // Update is called once per frame
    void Update()
    {

        if (health <= 0)
        {
            Debug.Log("Comecou a Rotina DeathCo()");
            StartCoroutine(DeathCo());
        }

        if (life <= 0)
        {
            // Destroy(this.gameObject);
            Debug.Log("Carregando o Menu...");
            //  SceneManager.LoadScene("Menu");
        }

    }

    void FixedUpdate()
    {
        StartCoroutine(CheckRespawnLocation());
    }

    IEnumerator DeathCo()
    {
        life = life - 1;
        health = maxHealth;
        Debug.Log("Player Perdeu Vida");
        ourPlayer.paused = true;
        myAnimation.SetBool("dying", true);
        yield return new WaitForSeconds(1f);
        myAnimation.SetBool("dying", false);
        gameObject.transform.position = new Vector2(respawnLocation.x, respawnLocation.y);
        yield return new WaitForSeconds(0.1f);
        ourPlayer.paused = false;
        Debug.Log("Player deu Respawn");

    }

    IEnumerator CheckRespawnLocation()
    {
        respawnLocation = respawnPoint.respawnPosition;
        yield return new WaitForSeconds(0.1f);
    }

}