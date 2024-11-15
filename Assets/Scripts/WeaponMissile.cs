using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponMissile : MonoBehaviour
{
    public GameObject missilePrefab;
    public GameObject targetedEnemy;
    LineRenderer targetGuide;
    public GameObject tip;
    bool tracking;
    List<GameObject> missiles = new List<GameObject>();

    // Start is called before the first frame update
    void Start()
    {
        targetGuide = tip.GetComponent<LineRenderer>();
        targetGuide.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButton("Jump"))
        {
            tracking = true;
            targetGuide.enabled = true;
            Ray ray = new Ray(tip.transform.position, new Vector3(0, 1, 0));
            RaycastHit hit;
            targetGuide.SetPosition(0, ray.origin);

            if (Physics.Raycast(ray, out hit))
            {
                if (hit.collider.gameObject.layer == 7) // Only target enemies
                    targetedEnemy = hit.collider.gameObject;
                targetGuide.SetPosition(1, hit.point);
            }
        }
        else
        {
            if (tracking)
            {
                tracking = false;
                if (targetedEnemy != null)
                {
                    targetGuide.enabled = false;
                    ProjectileHero p;
                    targetedEnemy = null;
                }
            }
        }        
    }

    void FireMissile(GameObject target)
    {
        GameObject go = Instantiate<GameObject>(missilePrefab); Missile missile = go.GetComponent<Missile>();
        missile.Wake(target, tip.transform.position);
        missiles.Add(go);

    }
}
