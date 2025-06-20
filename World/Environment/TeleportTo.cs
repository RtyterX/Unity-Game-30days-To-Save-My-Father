using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeleportTo : MonoBehaviour
{
    // Objects
    [Header("Objects")]
    [SerializeField] private TransitionAnimation transition;
    [SerializeField] public UnityEngine.GameObject touchedObject;

    // Destination
    [Header("Destination")]
    public Transform destinationPoint;

    // Delay
    [Header("Timer")]
    public bool haveDelay;
    public float delayTime;

    public void OnTriggerEnter2D(Collider2D col)
    {
        touchedObject = col.gameObject;
        StartCoroutine(TeleportObject());

        if (transition != null)
        {
            transition.animationOn = true;
        }

    }

    public IEnumerator TeleportObject()
    {
        Debug.Log("Começou o Teleport");

        if (touchedObject.TryGetComponent<MovementController>(out MovementController objectMovement))
        {
            objectMovement.isPaused = true;

            if (haveDelay)
            {
                yield return new WaitForSeconds(delayTime);
            }
            else
            {
                yield return new WaitForSeconds(0.4f);
            }

            // Transform Object position to destination position
            touchedObject.transform.position = new Vector2(destinationPoint.position.x, destinationPoint.position.y);
            
            // Delay before can move again
            yield return new WaitForSeconds(0.4f);
            objectMovement.isPaused = false;

        }
    }
}
