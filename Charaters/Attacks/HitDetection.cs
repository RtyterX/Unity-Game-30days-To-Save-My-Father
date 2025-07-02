using UnityEngine;
using UnityEngine.Events;

public class HitDetection : MonoBehaviour
{
    public bool wasHit;

    [Header("Damage")]
    public double baseDamage;
    public double finalDamage; // For Tests
    public double critical;
    public DamageType damageType;
    public UnityEvent effect;

    public LayerMask myLayerMask;

    [Header("KnockBack Direction")]
    Vector2 direction;

    public void Start()
    {
        CalculateDamage();
    }

    public void OnTriggerEnter2D(Collider2D col)
    {
        //if (!wasHit)
       // {
            wasHit = true;

            if (col.gameObject.TryGetComponent<HealthController>(out HealthController otherObj))
            {
                if (myLayerMask == col.gameObject.layer)
                {
                    Debug.Log("Pegou Hit");
                    CalculateDamage();
                    direction = col.gameObject.transform.position - transform.position;                      // Set Knock Back Direction
                    otherObj.TakeDamage(finalDamage, damageType, direction);                                  // Apply Damage
                    if (effect != null)
                    {
                        effect.Invoke();                                                                     // Apply Effect, if weapon has one
                    }

                    // ------ GainXP -------
                    if (otherObj.isDead)
                    {
                        if (gameObject.CompareTag("Player"))
                        {
                            if (col.gameObject.TryGetComponent<StatsController>(out StatsController otherObjStats)
                            && gameObject.TryGetComponent<LevelController>(out LevelController levelController))
                            {
                                levelController.GainEXP(otherObjStats.exp);
                            }
                        }

                    }
                    Invoke("CanHitAgain", 0.5f);
                }
            }
           // Debug.Log(name + " do " + baseDamage);
        //}
    }

    public void CanHitAgain()
    {
        wasHit = false;
    }

    public void CalculateCritical(bool criticalOrNot)
    {
        StatsController enemyStats = gameObject.GetComponentInParent<StatsController>();

        // Chance de Dano Critico
        double luckBoost = enemyStats.luck / 1.5f;
        double finalCritical = critical + luckBoost;
        double randomNumber = Random.Range((float)finalCritical, 100);

            Debug.Log("Chance de Dano Critico: "
             + finalCritical
             + "Lucky Boost:"
             + luckBoost
             + "Número escolhido: "
             + randomNumber);

        if (randomNumber >= 99) 
        {
            criticalOrNot = true;
        }
        else
        {
            criticalOrNot = false;
        }

    }

    // Make Return
    public void CalculateDamage()
    {
        StatsController stats = gameObject.GetComponentInParent<StatsController>();

        bool criticalOrNot = true;      // True for Tests
        CalculateCritical(criticalOrNot);

        // Apply Damage 
        if (criticalOrNot)
        {
            baseDamage += baseDamage * 0.30f;
         //   Debug.Log("Critical = " + baseDamage);
        }

        finalDamage = 1f + (((float)stats.strength - 1) / 1.5f);

       // Debug.Log(name + " do " + baseDamage);

    }
}

