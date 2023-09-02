using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class KnockBack : MonoBehaviour
{
    [SerializeField] private Rigidbody2D myRigidbody2D;

    [SerializeField] public float strength;
    [SerializeField] public float delay;

    public void StartKnockBack(GameObject sender)
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


}

