using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class CameraMove : MonoBehaviour
{

    [SerializeField]
    float camSpeed = 10f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // Need to add a check to see if the camera is at the edge of the map
        if (Input.GetKey(KeyCode.W))
        {
            transform.Translate(0f, camSpeed * Time.deltaTime, 0f);
        }

        // Need to add a check to see if the camera is at the edge of the map
        if (Input.GetKey(KeyCode.S))
        {
            transform.Translate(0f, -camSpeed * Time.deltaTime, 0f);
        }
    }
}
