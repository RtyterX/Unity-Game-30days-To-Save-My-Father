using UnityEngine;

public class DebugTestScript : MonoBehaviour
{
    public GameObject otherGameObj;
    public Vector2 direction;

    private void OnCollisionEnter2D(Collision2D col)
    {
        otherGameObj = col.gameObject;
        direction = transform.position - otherGameObj.transform.position;

        if (otherGameObj.TryGetComponent<HealthController>(out HealthController testObj))
        {
            DamageType dmgtype = DamageType.Physic;

            testObj.TakeDamage(10, dmgtype, direction);

            Debug.Log("Obj has Collied with Test Script!");
        }
        
    }
}
