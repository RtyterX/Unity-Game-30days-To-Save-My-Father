using System;
using System.Collections.Generic;
using UnityEngine;

public class Shoot : MonoBehaviour
{
    public UnityEngine.GameObject projectile;
    public Transform shotPoint;

    public void Update()
    {
        Instantiate(projectile, shotPoint.transform.position, transform.rotation);
    }

}
