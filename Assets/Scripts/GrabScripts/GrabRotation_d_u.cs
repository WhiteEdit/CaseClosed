using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrabRotation_d_u : MonoBehaviour
{
   float rotationSpeed = 3f;
    public Quaternion initialRotation;


    private void Start()
    {
        initialRotation = transform.rotation;
    }



    void Update() 
   {
    if (Input.GetMouseButton(0))
   {
    
    float XaxisRotation = Input.GetAxis("Mouse X") * rotationSpeed;
    float YaxisRotation = Input.GetAxis("Mouse Y") * rotationSpeed;

    transform.Rotate(Vector3.down, XaxisRotation);
    transform.Rotate(Vector3.forward, YaxisRotation);
    
    }


 if (Input.GetMouseButton(1))
   {
            transform.rotation = initialRotation;
    
    }

   }



}
