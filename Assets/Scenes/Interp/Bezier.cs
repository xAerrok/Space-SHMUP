using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bezier : MonoBehaviour
{
    public Transform P0, P1, C0, C1;
    Vector3 pos_P0, pos_P1, pos_C0, pos_C1;
    float u = 0;
    LineRenderer line;

    // Start is called before the first frame update
    void Start()
    {
        pos_P0 = P0.position; pos_P1 = P1.position; pos_C0 = C0.position; pos_C1 = C1.position;
        line = GetComponent<LineRenderer>();
        line.positionCount = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (u<1f)
        {
            u += 0.001f;

            Vector3 d = (1-u) * (1-u) * (1-u) * pos_P0 + 3 * (1-u) * (1-u) * u * pos_C0 + 3 * (1 - u) * u * u * pos_C1 + u * u * u * pos_P1;

            transform.position = d;

            line.positionCount++;
            line.SetPosition(line.positionCount - 1, d);
        }
    }
}
