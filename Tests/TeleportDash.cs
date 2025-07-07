using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Teleport : MonoBehaviour
{
    public PlayerMovement2D playerMovement;

    // Start is called before the first frame update
    void Start()
    {
        playerMovement = gameObject.GetComponent<PlayerMovement2D>();
    }

    // Update is called once per frame
    void Update()
    {
          //Pressiona o Botão x
        if (Input.GetKeyDown(KeyCode.X))
        {
            StartCoroutine(TeleportOurPlayer());
        }

    }

    IEnumerator TeleportOurPlayer()
    {
        playerMovement.isPaused = true;
        yield return new WaitForSeconds(0.1f);
        gameObject.transform.position = new Vector2(5f, 0f);
        yield return new WaitForSeconds(0.1f);
        playerMovement.isPaused = false;
    }
}
