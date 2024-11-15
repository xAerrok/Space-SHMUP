using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ButtonTouch : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ChangeColor(GameObject go)
    {
        Renderer rend = go.GetComponent<Renderer>();
        rend.material.color = Random.ColorHSV();
    }
}
