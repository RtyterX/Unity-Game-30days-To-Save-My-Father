<<<<<<< HEAD
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
}
=======
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
}
>>>>>>> a8399fb (Inicio Do Jogo: Movimentacao/Correr/Dano/Respawn)
