using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Easing
{

    public enum Type
    {                                                // a
        linear,
        easeIn,
        easeOut,
        easeInOut,
        sin,
        sinIn,
        sinOut
    }

    static public float Ease(float u, Type eType, float eMod = 2)
    {  // c
        float u2 = u;

        switch (eType)
        {                                              // b

            case Type.linear:
                u2 = u;
                break;

            case Type.easeIn:
                u2 = Mathf.Pow(u, eMod);
                break;

            case Type.easeOut:
                u2 = 1 - Mathf.Pow(1 - u, eMod);
                break;

            case Type.easeInOut:
                if (u <= 0.5f)
                {
                    u2 = 0.5f * Mathf.Pow(u * 2, eMod);
                }
                else
                {
                    u2 = 0.5f + 0.5f * (1 - Mathf.Pow(1 - (2 * (u - 0.5f)), eMod));
                }
                break;

            case Type.sin:
                // Try eMod values of 0.15f and -0.2f for Easing.Type.sin // c
                u2 = u + eMod * Mathf.Sin(2 * Mathf.PI * u);
                break;

            case Type.sinIn:
                // eMod is ignored for SinIn
                u2 = 1 - Mathf.Cos(u * Mathf.PI * 0.5f);
                break;

            case Type.sinOut:
                // eMod is ignored for SinOut
                u2 = Mathf.Sin(u * Mathf.PI * 0.5f);
                break;
        }

        return (u2);
    }
}
