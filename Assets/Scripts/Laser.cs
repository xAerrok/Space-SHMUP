using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Laser : MonoBehaviour
{
    public GameObject tip;
    LineRenderer laserLine;
    Material damageMat;
    Material[] mats;

    // Start is called before the first frame update
    void Start()
    {
        laserLine = tip.GetComponent<LineRenderer>();
        laserLine.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButton("Fire1"))
        {
            laserLine.enabled = true;
            Ray ray = new Ray(tip.transform.position, new Vector3(0, 1, 0));
            RaycastHit hit;
            laserLine.SetPosition(0, ray.origin);

            if (Physics.Raycast(ray, out hit))
            {
                laserLine.SetPosition(1, hit.point);
                mats = Utils.GetAllMaterials(hit.collider.gameObject);
                foreach (Material mat in mats)
                {
                    mat.color = Color.red;
                }
            }
            else
            {
                laserLine.SetPosition(1, ray.GetPoint(10));
                if (mats != null)
                {
                    foreach (Material mat in mats)
                    {
                        mat.color = Color.white;
                    }
                }
            }
        }
        else
        {
            laserLine.enabled=false;
        }
    }
}
