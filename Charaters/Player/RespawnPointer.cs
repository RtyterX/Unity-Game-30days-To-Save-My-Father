using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RespawnPointer : MonoBehaviour
{
    public Vector2 respawnPosition;

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag == "Respawn")
        {
            respawnPosition = new Vector2(col.gameObject.transform.position.x, col.gameObject.transform.position.y);
            Debug.Log("Respawn Mudou");
        }
    }

    public void DoRespawn()
    {
       gameObject.transform.position = respawnPosition;
        Invoke("PausePlayerForASec", 0.4f);
    }

    private void PausePlayerForASec()
    {
        PlayerMovement2D player = GetComponent<PlayerMovement2D>();
        player.isPaused = true;
    }
}