using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;

public class KnockBack : MonoBehaviour
{
    [SerializeField] private Rigidbody2D myRigidbody2D;
    [SerializeField] public float strength;
    [SerializeField] public float delay;


    [Header("KnockBack")]
    public bool canKnockBack;
    [SerializeField] private float strengthKnockBack;
    [SerializeField] private float delayKnockBack;
    private Vector2 direction;


    // Antigo
    public void StartKnockBack(UnityEngine.GameObject sender)
    {
        StopAllCoroutines();

         // Codigo que Realiza o KockBack
        Vector2 direction = (transform.position - sender.transform.position).normalized;
        myRigidbody2D.AddForce(direction * strength, ForceMode2D.Impulse);

        Debug.Log("Realizou o KnockBack");
        StartCoroutine(ResetKnockBackCo());

    }

    private IEnumerator ResetKnockBackCo()
    {
        // Tempo de Espera para Voltar a fazer suas Rotinas
        yield return new WaitForSeconds(delay);
        myRigidbody2D.velocity = Vector3.zero;
       //  this.gameObject.Enemy.StopCoroutines; - Para Rotinas
    }



    // novo?
    public IEnumerator KnockBackCo()
    {
        StopAllCoroutines();

        Debug.Log("Realizou o KnockBack");

        //state = StateMachine.Stagger;
        //canMove = false;

        // Codigo que Realiza o KockBack
        Vector2 direction = (transform.position - gameObject.transform.position).normalized;
        //myRigidbody.AddForce(direction * strengthKnockBack, ForceMode2D.Impulse);

        yield return new WaitForSeconds(delayKnockBack);

       // state = StateMachine.Idle;
       // canMove = true;
    }


}

