using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarregadorDeObjeto : MonoBehaviour
{
    public Transform target;
    public Player ourPlayer;
    public InputController inputs;

    public bool segurandoItem;

    [SerializeField] float newPositionX;
    [SerializeField] float newPositionY;

    public float teste;

    void Start()
    {
        segurandoItem = false; 
        ourPlayer = gameObject.GetComponent<Player>();
        inputs = GetComponent<InputController>();



        // target =
    }


    // void OnTriggerEnter2D(Collider2D col)
    //{
       // inputs = GetComponent<InputController>();
      //  newPositionX = ourPlayer.transform.position.x;
      //  newPositionY = ourPlayer.transform.position.y + 001;

     //   Debug.Log("Trigger 2D ativou");
    //    if (col.gameObject.tag == "ObjetoDestrutivel" || col.gameObject.tag == "ObjetoCarregavel")
      //  {
        //    Debug.Log("Trigger 2D ativou parte 2");
        //    if (inputs.inputInterect == 0)
        //    {
        //       teste = inputs.inputInterect;
        //
       //     }
       //     else
        //    {
        //        Debug.Log("Pressionou o Q");

         //       if (!segurandoItem)
         //       {
              //      StartCoroutine(CarregaObjCo());
        //        }
       //         else
         //       {
         //           StartCoroutine(JogaObjCo());
          //      }
//
         //   }
      //  }
        



    IEnumerator CarregaObjCo()
    {
        Debug.Log("Comecou o CarregaObjCo");
        segurandoItem = true;



        target.transform.position = new Vector2(newPositionX, newPositionY);
        yield return new WaitForSeconds(0.1f);
        
    }

    IEnumerator JogaObjCo()
    {
        Debug.Log("Comecou o JogaObjCo");

          // Olhando para Baixo
       // if (ourPlayer.myAnimator.SetFloat("moveX", -1) && ourPlayer.myAnimator.Float("moveY", 0))
        //{
//            target.velocity = new Vector2(10f, 0f);
   //         target.BoxCollider2D.SetActive(true);
              yield return new WaitForSeconds(1);
        //    segurandoItem = false;
          //  target.velocity = new Vector2(0f, 0f);
        

    }
}
