
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueController : MonoBehaviour
{
    public bool troca1;

    public InGameEventManager eventManager;
    public DialogueScript dialogueCode;

    // public int number;
    public string[] newDialog;

    void Start()
    {
        eventManager = GameObject.Find("InGameEventManager").GetComponent<InGameEventManager>();
        dialogueCode = gameObject.GetComponent<DialogueScript>();
    }

    void Update()
    {
        if (eventManager.trocaDeDialogo == true && !troca1)
        {
          //  number = dialogueCode.dialog.Length - 1;
          //  System.Array.Resize(ref dialogueCode.dialog, dialogueCode.dialog.Length - number);
            
            dialogueCode.dialog = newDialog;

          //  dialogueCode.dialog[0] = "teste 2";
            troca1 = true;
        }
        
    }
}
