using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollowPlayer : MonoBehaviour
{
    public Transform target;
    public float Smoothing;
    public Vector2 maxPosition;
    public Vector2 minPosition;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void LateUpdate()
    {
        if (transform.position != target.position)
        {
            Vector3 TargetPosition = new Vector3(target.position.x,
                                                 target.position.y,
                                                 transform.position.z);
            TargetPosition.x = Mathf.Clamp(TargetPosition.x,
                                           minPosition.x,
                                           maxPosition.x);
            TargetPosition.y = Mathf.Clamp(TargetPosition.y,
                                           minPosition.y,
                                           maxPosition.y);

            transform.position = Vector3.Lerp(transform.position, TargetPosition, Smoothing);
        }
    }
}
