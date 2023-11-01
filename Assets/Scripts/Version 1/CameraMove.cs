using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class CameraMove : MonoBehaviour
{

    // [SerializeField]
    // float camSpeed = 10f;

    [SerializeField]
    GameObject target;

    float xOffset = 0;
    float yOffset = 0;
    float zOffset = 0;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
        /*
        // Need to add a check to see if the camera is at the edge of the map
        if (Input.GetKey(KeyCode.W))
        {
            transform.Translate(0f, camSpeed * Time.deltaTime, 0f);
        }

        
        if (Input.GetKey(KeyCode.S) && transform.position.z > -4.13f)
        {
            transform.Translate(0f, -camSpeed * Time.deltaTime, 0f);
        }
        */

        
        

    }

    private void LateUpdate()
    {
        this.transform.position = new Vector3(xOffset,
                                              this.transform.position.y,
                                              target.transform.position.z + zOffset);
    }
}
