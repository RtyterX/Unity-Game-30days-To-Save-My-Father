using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Teste : MonoBehaviour
{
    public GameObject testeT;

    public Vector2 testePosition;

    // Start is called before the first frame update
    void Start()
    {
        testeT = this.gameObject;

        testePosition = new Vector2(testeT.transform.position.x, testeT.transform.position.y);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
