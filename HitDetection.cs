using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitDetection : MonoBehaviour
{
    [Header("GameObjects")]
    public GameObject thisGameObj;
    public GameObject otherGameObj;

    [Space(10)]
    [Header("Components")]
    [SerializeField] private Rigidbody2D myRigidbody;
    [SerializeField] private BehaviourController objBehaviour;
    [SerializeField] private AttackController attack;

    [Header("KnockBack")]
    public bool canKnockBack;
    [SerializeField] private float strengthKnockBack;
    [SerializeField] private float delayKnockBack;
    private Vector2 direction;


    private void OnCollisionEnter2D(Collision2D col)
    {
        myRigidbody = thisGameObj.GetComponent<Rigidbody2D>();

        Debug.Log("Encostou no Inimigo");

        // GameObjects Check
        otherGameObj = col.gameObject;
        thisGameObj = gameObject;

        if (otherGameObj.tag == "Inimigo")
        {

            if (otherGameObj.TryGetComponent<StatsController>(out StatsController enemyStatsComp) &&
                thisGameObj.TryGetComponent<HealthController>(out HealthController playerHealthComp))
            {

                // Aplica Dano no Player
                playerHealthComp.TakeDamage(attack.damage, 1, attack.damageType);
                Debug.Log("Player Tomou Dano");
                
                if(canKnockBack)
                {
                    StartCoroutine(KnockBackCo());
                }

                if (playerHealthComp.life == 0)
                {
                    otherGameObj.TryGetComponent<EnemyBehaviour>(out EnemyBehaviour enemyChaseComp);

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
                enemyHealthComp.TakeDamage(attack.damage, 1, attack.damageType);
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

            objBehaviour.state = StateMachine.Stagger;
            objBehaviour.isPaused = true;

            // Codigo que Realiza o KockBack
            Vector2 direction = (transform.position - otherGameObj.transform.position).normalized;
            myRigidbody.AddForce(direction * strengthKnockBack, ForceMode2D.Impulse);

            yield return new WaitForSeconds(delayKnockBack);

            objBehaviour.state = StateMachine.Idle;
            objBehaviour.isPaused = false;
            
        }
        
        if (thisGameObj.tag == "Enemy")
        {
            Debug.Log("Realizou o KnockBack");

            objBehaviour.state = StateMachine.Stagger;
            objBehaviour.isPaused = true;

            // Codigo que Realiza o KockBack
            Vector2 direction = (transform.position - otherGameObj.transform.position).normalized;
            myRigidbody.AddForce(direction * strengthKnockBack, ForceMode2D.Impulse);

            yield return new WaitForSeconds(delayKnockBack);

            objBehaviour.state = StateMachine.Idle;
            objBehaviour.isPaused = false;

        }
        
        
    }

}

