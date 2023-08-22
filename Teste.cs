using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Teste : MonoBehaviour
{
    public bool teste;
    public TimeControl timeManager;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (teste)
        {
            timeManager.TimeConverter(4, 35);
            teste = false;

        }
    }
}
