using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AranhaShoot : MonoBehaviour
{
    public Aranha thisObj;
    public Animator myAnimator;

    public UnityEngine.GameObject projectile;
    public Transform shotPoint;

    public float timer;

    public float aranhaStopTime;


    // Start is called before the first frame update
    void Start()
    {
        thisObj = gameObject.GetComponent<Aranha>();
        myAnimator = gameObject.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;

        if (Vector3.Distance(thisObj.target.transform.position, transform.position) <= thisObj.attackRadius)
        {

            if (timer > 2)
            {
                timer = 0;
                StartCoroutine(Shoot());
            }
        }
    }

    public IEnumerator Shoot()
    {

        Debug.Log("Aranha Shoot");

        // thisObj.canMove = true;
        myAnimator.SetBool("Attack", true);

        Instantiate(projectile, shotPoint.transform.position, transform.rotation);

        yield return new WaitForSeconds(aranhaStopTime);

        // thisObj.canMove = false;
        myAnimator.SetBool("Attack", false);

    }
}
