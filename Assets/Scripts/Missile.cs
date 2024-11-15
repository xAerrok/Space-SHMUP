using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Missile : MonoBehaviour
{
    GameObject target;
    Vector3 origin;
    bool live = false;

    //public GameObject Target { set { target = value; } }
    //public Vector3 Origin { set { origin = value; } }
    //public Missile(GameObject target, Vector3 origin)
    //{
    //    this.target = target;
    //    this.origin = origin;
    //}
    public void Wake(GameObject target, Vector3 origin)
    {
        this.target = target;
        this.origin = origin;
        this.live = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (live)
        {
            Debug.Log(target.transform.position);
            transform.position = Vector3.Lerp(origin, target.transform.position, .3f);
        }
    }
}
