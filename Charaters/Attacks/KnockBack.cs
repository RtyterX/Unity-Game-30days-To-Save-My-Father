using UnityEngine;

public class KnockBack : MonoBehaviour
{
    [SerializeField] private float resistence;
    [SerializeField] private float duration;
    BehaviourController behaviour;
    Vector2 kbDirection;

    public void Start()
    {
        behaviour = GetComponent<BehaviourController>();
    }

    public void DoKnockBack(float strength, Vector2 forceDirection)
    {
        StatsController stats = GetComponent<StatsController>();
        float temp = (float)stats.resistance / 75;
        strength = (400 * temp) - resistence;
        if (strength > 500)   { strength = 500;  }
        if (strength < 80)    { strength = 80;   }
        if (duration == 0)    { duration = 0.5f; }

        kbDirection = forceDirection.normalized;
        behaviour.myRigidbody.AddForce(kbDirection * strength, ForceMode2D.Impulse);                           // Do the Knock Back Effect
        Invoke("StaggerDelay", duration);                                                                         // KnockBack Duration
    }
    
    public void StaggerDelay()
    {
        behaviour.ChangeState(StateMachine.Stagger);

        StatsController stats = GetComponent<StatsController>();
        float temp = (float)stats.resistance % 25;
        float staggerDelay = 2 - temp;
        if (staggerDelay < 0.5f) { staggerDelay = 0.5f; }

        Invoke("ResetKnockBack", staggerDelay);
    }
    private void ResetKnockBack()
    {
        behaviour.ChangeState(StateMachine.Idle);
    }

}

