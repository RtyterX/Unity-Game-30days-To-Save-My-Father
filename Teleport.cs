using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Teleport : MonoBehaviour
{
    public Player playerControl;

    // Start is called before the first frame update
    void Start()
    {
        playerControl = gameObject.GetComponent<Player>();
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
        playerControl.paused = true;
        yield return new WaitForSeconds(0.1f);
        gameObject.transform.position = new Vector2(5f, 0f);
        yield return new WaitForSeconds(0.1f);
        playerControl.paused = false;
    }
}
