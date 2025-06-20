using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DebugTestScript : MonoBehaviour
{
    public UnityEngine.GameObject otherGameObj;

    private void OnCollisionEnter2D(Collision2D col)
    {
        otherGameObj = col.gameObject;

        if (otherGameObj.TryGetComponent<HealthController>(out HealthController testObj))
        {
            DamageType dmgtype = DamageType.Physic;

            testObj.TakeDamage(10, 1, dmgtype);

            Debug.Log("Obj has Collied with Test Script!");
        }
        
    }
}
