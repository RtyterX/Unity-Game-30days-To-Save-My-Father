using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class TransitionAnimation : MonoBehaviour
{
    public bool enterScene = false;
    public bool animationOn = false;

    [SerializeField] private GameObject startingAnimation;
    [SerializeField] private GameObject endingAnimation;

    public CameraFollowPlayer mainCamera;

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
        mainCamera.isPaused = true;

        startingAnimation.SetActive(true);
        yield return new WaitForSeconds(5);
        
        endingAnimation.SetActive(true); 
        yield return new WaitForSeconds(3);

        endingAnimation.SetActive(false);
        startingAnimation.SetActive(false);
        animationOn = false;

        mainCamera.isPaused = false;
;
    }

    IEnumerator EnterSceneTransition()
    {
        mainCamera.isPaused = true;

        endingAnimation.SetActive(true);
        yield return new WaitForSeconds(3);

        endingAnimation.SetActive(false);
        startingAnimation.SetActive(false);
        enterScene = false;
        animationOn = false;

        mainCamera.isPaused = false;

    }

}
