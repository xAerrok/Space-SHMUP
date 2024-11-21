using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Laser : MonoBehaviour
{
    public Weapon control;
    public Transform shotPoint;
    LineRenderer laserLine;

    // Start is called before the first frame update
    void Start()
    {
        GameObject parent = gameObject.transform.parent.gameObject;
        control = parent.GetComponent<Weapon>();
        shotPoint = parent.GetComponentInChildren<Transform>();
        laserLine = gameObject.GetComponent<LineRenderer>();
        laserLine.enabled = false;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (control.laserOn)
        {
            laserLine.enabled = true;
            Ray ray = new Ray(shotPoint.transform.position, new Vector3(0, 1, 0));
            RaycastHit hit;
            laserLine.SetPosition(0, ray.origin);
            
            if (Physics.Raycast(ray, out hit) && hit.collider.gameObject.layer != LayerMask.GetMask(nameof(Enemy)))
            {
                laserLine.SetPosition(1, hit.point);
                ProjectileHero p = control.LaserContact(hit.collider.gameObject);
            }
            else laserLine.SetPosition(1, ray.GetPoint(100));
        }
        else laserLine.enabled = false;
    }
}
