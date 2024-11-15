using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TouchMove : MonoBehaviour
{
    Touch touch;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.touchCount == 2)
        {
            touch = Input.GetTouch(1);

            if (touch.phase == TouchPhase.Began)
            {
                transform.localScale = new Vector3(0.75f, 0.75f, 0.75f);
            }

            if (touch.phase == TouchPhase.Ended)
            {
                transform.localScale = new Vector3(1.0f, 1.0f, 1.0f);
            }
        }

        if (Input.touchCount > 0)
        {
            touch = Input.GetTouch(0);
            Vector3 screenCoordinates = new Vector3(touch.position.x, touch.position.y, -Camera.main.transform.position.z);
            Vector3 worldCoordinates = Camera.main.ScreenToWorldPoint(screenCoordinates);
            worldCoordinates.z = 0.0f;
            transform.position = worldCoordinates;
        }
    }
}
