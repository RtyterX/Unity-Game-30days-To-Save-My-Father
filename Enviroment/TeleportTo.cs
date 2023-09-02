using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeleportTo : MonoBehaviour
{
    [SerializeField] private TransitionAnimation transition;
    [SerializeField] public GameObject touchedObject;

    public Transform destineLocation;

    
    public void OnTriggerEnter2D(Collider2D col)
    {
        touchedObject = col.gameObject;
        StartCoroutine(TeleportScene());

        if (transition != null)
        {
            transition.animationOn = true;
        }
    }

    public IEnumerator TeleportScene()
    {

        if(touchedObject.tag == "Player")
        {
            
            if (touchedObject.TryGetComponent<PlayerBehaviour>(out PlayerBehaviour playerBehaviour))
            {
                playerBehaviour.isPaused = true;
                yield return new WaitForSeconds(1f);

                touchedObject.transform.position = new Vector2(destineLocation.position.x, destineLocation.position.y);
                yield return new WaitForSeconds(0.4f);

                playerBehaviour.isPaused = false;
            }

            

        }

    }
}
