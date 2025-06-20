using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeHealth : MonoBehaviour
{
    public UnityEngine.GameObject target;

    public void IncreasceHealth(float amount)
    {
        if (target != null)
        {
            target.TryGetComponent<HealthController>(out HealthController targetHealth);
            targetHealth.RecoverHealth(amount);
        }
        if (target == null)
        {
            target = UnityEngine.GameObject.FindWithTag("Player");

            target.TryGetComponent<HealthController>(out HealthController targetHealth);
            targetHealth.RecoverHealth(amount);
        }

    }

    public void IncreaseHealthByTime(float time, float amount)
    {
        if (target != null)
        {
            target.TryGetComponent<HealthController>(out HealthController targetHealth);
            targetHealth.RecoverByTime(time, amount);
        }
        if (target == null)
        {
            target = UnityEngine.GameObject.FindWithTag("Player");

            target.TryGetComponent<HealthController>(out HealthController targetHealth);
            targetHealth.RecoverByTime(time, amount);
        }

    }

}
