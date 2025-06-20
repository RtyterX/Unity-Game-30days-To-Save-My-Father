using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuInventory : MonoBehaviour
{

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.I))
        {
            Debug.Log("Test");
            if (gameObject.activeSelf == true)
            {
                gameObject.SetActive(false);
                Time.timeScale = 1f;
            }

        }

    }

    // ------ Buttons ------



}

