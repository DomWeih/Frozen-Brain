using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    Camera main;
    Vector3 cameraPosition;
    Quaternion cameraRotation;
    public float cameraSpeed;


    // Start is called before the first frame update
    void Start()
    {
        main = Camera.main;
        cameraPosition = main.transform.position;
        cameraRotation = main.transform.rotation;
    }

    // Update is called once per frame
    void Update()
    {
        #region camera movement
        //Movement
        if(Input.GetKey(KeyCode.W))
            cameraPosition.z += cameraSpeed / 10;          
        if(Input.GetKey(KeyCode.D))
            cameraPosition.x += cameraSpeed / 10;       
        if(Input.GetKey(KeyCode.S))
            cameraPosition.z -= cameraSpeed / 10;
        if(Input.GetKey(KeyCode.A))
            cameraPosition.x -= cameraSpeed / 10;
        //Zoom
        if(Input.mouseScrollDelta.y > 0)
            cameraPosition.y -=1;       
            
        if(Input.mouseScrollDelta.y < 0)
            cameraPosition.y +=1;


        main.transform.position = cameraPosition;
        #endregion

        #region Resources
        
        #endregion
    }
}
