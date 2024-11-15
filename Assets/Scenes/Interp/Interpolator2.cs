using UnityEngine;
using System.Collections;

public class Interpolator2 : MonoBehaviour
{
    [Header("Set in Inspector")]
    public Transform c0;
    public Transform c1;
    public float uMin = 0;
    public float uMax = 1;
    public float timeDuration = 1;
    public bool loopMove = true; // Repeats the move
    public Easing.Type easingType = Easing.Type.linear;
    public float easingMod = 2;

    // Click the checkToStart checkbox to start moving
    public bool checkToStart = false;

    [Header("Set Dynamically")]
    public Vector3 p01;
    public Color c01;
    public Quaternion r01;
    public Vector3 s01;
    public bool moving = false;
    public float timeStart;

    private Material mat, matC0, matC1;

    void Awake()
    {
        mat = GetComponent<Renderer>().material;
        matC1 = c1.GetComponent<Renderer>().material;
        matC0 = c0.GetComponent<Renderer>().material;
    }

    // Update is called once per frame
    void Update()
    {
        if (checkToStart)
        {
            checkToStart = false;

            moving = true;
            timeStart = Time.time;
        }

        if (moving)
        {
            float u = (Time.time - timeStart) / timeDuration;
            if (u >= 1)
            {
                u = 1;
                if (loopMove)
                {
                    timeStart = Time.time;
                }
                else
                {
                    moving = false;
                }
            }

            // Adjust u to the range from uMin to uMax
            u = (1 - u) * uMin + u * uMax;

            // The Easing.Ease function modifies u to change tweak movement
            u = Easing.Ease(u, easingType, easingMod);

            // This is the standard linear interpolation function
            p01 = (1 - u) * c0.position + u * c1.position;
            c01 = (1 - u) * matC0.color + u * matC1.color;
            s01 = (1 - u) * c0.localScale + u * c1.localScale;
            // Rotations are treated differently because Quaternions are tricky
            r01 = Quaternion.Slerp(c0.rotation, c1.rotation, u);
            // Apply these to this Cube01
            transform.position = p01;
            mat.color = c01;
            transform.localScale = s01;
            transform.rotation = r01;
        }
    }
}
