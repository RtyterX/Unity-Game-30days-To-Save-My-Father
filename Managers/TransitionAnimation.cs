using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TransitionAnimation : MonoBehaviour
{
    public bool enterScene = false;
    public bool animationOn = false;

    [SerializeField] private GameObject startingAnimation;
    [SerializeField] private GameObject endingAnimation;

    public CameraFollowPlayer camera;

    void Awake()
    {
        animationOn = false;
        enterScene = true;
    }

    void Update()
    {
        if(startingAnimation == true)
        {
            StartCoroutine(StartTransition());
        }

        if (enterScene == true)
        {
            StartCoroutine(EnterSceneTransition());
        }
    }

    IEnumerator StartTransition()
    {
        camera.isPaused = true;

        startingAnimation.SetActive(true);
        yield return new WaitForSeconds(5);
        
        endingAnimation.SetActive(true); 
        yield return new WaitForSeconds(3);

        endingAnimation.SetActive(false);
        startingAnimation.SetActive(false);
        animationOn = false;

        camera.isPaused = false;
;
    }

    IEnumerator EnterSceneTransition()
    {
        camera.isPaused = true;

        endingAnimation.SetActive(true);
        yield return new WaitForSeconds(3);

        endingAnimation.SetActive(false);
        startingAnimation.SetActive(false);
        enterScene = false;
        animationOn = false;

        camera.isPaused = false;

    }

}
