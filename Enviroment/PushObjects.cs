using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PushObjects : MonoBehaviour
{
    private float forceMagnitude;

    public void OnControllerColliderHit(ControllerColliderHit hit)
    {
     //   Rigidbody2D myRigidbody = hit.colliderattachedRigidbody;

      //  if (myRigidbody != null)
       // {
       //     Vector3 forceDirection = hit.gameObject.transform.position - transform.position;
        //    forceMagnitude.z = 0;
       //     forceMagnitude.Normalized();

       //     myRigidbody.AddForceAtPosition(forceDirection * forceMagnitude, transform.position, ForceMode.Impulse);
       // }
    }
}
