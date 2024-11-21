using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponMissile : MonoBehaviour
{
    public Weapon control;
    public Transform shotPoint;
    public GameObject targetedEnemy;
    LineRenderer targetGuide;
    bool locked = false;

    // Start is called before the first frame update
    void Start()
    {
        GameObject parent = gameObject.transform.parent.gameObject;
        control = parent.GetComponent<Weapon>();
        shotPoint = parent.GetComponentInChildren<Transform>();
        targetGuide = gameObject.GetComponent<LineRenderer>();
        targetGuide.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (control.missileTracking)
        {
            targetGuide.enabled = true;
            Ray ray = new Ray(shotPoint.transform.position, new Vector3(0, 1, 0));
            RaycastHit hit;
            targetGuide.SetPosition(0, ray.origin);

            if (Physics.Raycast(ray, out hit))
            {
                Enemy e = hit.collider.gameObject.GetComponent<Enemy>(); if (e != null)
                { // Only target enemies
                    targetedEnemy = hit.collider.gameObject;
                    locked = true;
                    targetGuide.SetPosition(1, hit.point);
                }
            }
            else
            {
                if (targetedEnemy == null) targetGuide.SetPosition(1, ray.GetPoint(100));
                else targetGuide.SetPosition(1, targetedEnemy.transform.position);
            }
        }
        else
        {
            targetGuide.enabled = false;
            if (locked)
            {
                locked = false;
                control.LaunchMissile(targetedEnemy);
                targetedEnemy = null;
            }
        }
    }
}
