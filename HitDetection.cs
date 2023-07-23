using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class HitDetection : MonoBehaviour
{
    public bool canKnockBack;

    [SerializeField] private Rigidbody2D myRigidbody;
    [SerializeField] private Stats objStatus;

    [SerializeField] public float strength;
    [SerializeField] public float delay;

    public GameObject thisGameObj;
    public GameObject otherGameObj;

    Vector2 direction;

    public void OnCollisionEnter2D(Collision2D col)
    {
        otherGameObj = col.gameObject;
        thisGameObj = gameObject;

        myRigidbody = thisGameObj.GetComponent<Rigidbody2D>();

        if (otherGameObj.tag == "Inimigo")
        {

            Debug.Log("Encostou no Inimigo");

            if (otherGameObj.TryGetComponent<StatsController>(out StatsController enemyStatsComp) &&
                thisGameObj.TryGetComponent<HealthController>(out HealthController playerHealthComp))
            {

                // Aplica Dano no Player
                playerHealthComp.TakeDamage(enemyStatsComp.damage);
                Debug.Log("Player Tomou Dano");
                
                if(canKnockBack)
                {
                    StartCoroutine(KnockBackCo());
                }

                if (playerHealthComp.life == 0)
                {
                    otherGameObj.TryGetComponent<Enemy>(out Enemy enemyChaseComp);

                    enemyChaseComp.stopChase = true;
                }

            }

        }

        if (col.gameObject.tag == "PlayerHit")
        {

            Debug.Log("Encostou no Player");

            if (otherGameObj.TryGetComponent<StatsController>(out StatsController playerStatsComp) &&
                thisGameObj.TryGetComponent<HealthController>(out HealthController enemyHealthComp))
            {
                // Aplica Dano ao Inimigo
                enemyHealthComp.TakeDamage(playerStatsComp.damage);
                Debug.Log("Player Tomou Dano");

                if (canKnockBack)
                {
                    StartCoroutine(KnockBackCo());
                }
                
            }

        }

    }


    public IEnumerator KnockBackCo()
    {
        if (thisGameObj.tag == "Player")
        {
            Debug.Log("Realizou o KnockBack");

            objStatus.state = StateMachine.Stagger;
            objStatus.isPaused = true;

            // Codigo que Realiza o KockBack
            Vector2 direction = (transform.position - otherGameObj.transform.position).normalized;
            myRigidbody.AddForce(direction * strength, ForceMode2D.Impulse);

            yield return new WaitForSeconds(delay);

            objStatus.state = StateMachine.Idle;
            objStatus.isPaused = false;
            
        }
        
        if (thisGameObj.tag == "Enemy")
        {
            Debug.Log("Realizou o KnockBack");

            objStatus.state = StateMachine.Stagger;
            objStatus.isPaused = true;

            // Codigo que Realiza o KockBack
            Vector2 direction = (transform.position - otherGameObj.transform.position).normalized;
            myRigidbody.AddForce(direction * strength, ForceMode2D.Impulse);

            yield return new WaitForSeconds(delay);

            objStatus.state = StateMachine.Idle;
            objStatus.isPaused = false;

        }
        
        
    }

}

