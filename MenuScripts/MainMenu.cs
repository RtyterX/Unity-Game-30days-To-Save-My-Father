using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    // Start is called before the first frame update


    // Update is called once per frame
    void Update()
    {

    }

    public void LoadGame()
    {
        Debug.Log("Carregando o Jogo...");
        SceneManager.LoadScene("BTM");
    }

    public void QuitGame()
    {
        Debug.Log("Saindo do jogo...");
        Application.Quit();
    }

}