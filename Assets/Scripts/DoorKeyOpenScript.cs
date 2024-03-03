using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorKeyOpenScrit : Interactable
{


    public Vector3 rotationAxis = Vector3.up; // Axis to rotate around
    public float rotationAmount = 90f; // Amount to rotate
    public float rotationDuration = 1f; // Duration of rotation in seconds
    private bool isRotating = false;
    private bool rotateClockwise = true; // Flag to track rotation direction
    public BoolHandler boolHandler;
    [SerializeField] private GameObject destroyInventory;
    [SerializeField] private GameObject scratchInventory;
    




    public override void OnFocus()
    {

    }
    public override void OnLoseFocus()
    {

    }


    public override void OnInteract()
    {
        if (!isRotating && boolHandler.hasDoorKey )
        {
            StartCoroutine(RotateObject());
            destroyInventory.SetActive(false);
            scratchInventory.SetActive(true);
        }
       

   }


    IEnumerator RotateObject()
    {
        isRotating = true;

        Quaternion startRotation = transform.rotation;
        Quaternion endRotation;

        // Determine end rotation based on current rotation direction
        if (rotateClockwise)
            endRotation = transform.rotation * Quaternion.Euler(rotationAxis * rotationAmount);
        else
            endRotation = transform.rotation * Quaternion.Euler(rotationAxis * -rotationAmount);

        float t = 0f;
        while (t < 1f)
        {
            t += Time.deltaTime / rotationDuration;
            transform.rotation = Quaternion.Slerp(startRotation, endRotation, t);
            yield return null;
        }

        // Toggle rotation direction for next interaction
        rotateClockwise = !rotateClockwise;

        isRotating = false;
    }
}
