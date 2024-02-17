using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LockCamera : Interactable
{
    [SerializeField] private GameObject lockCamera;
    [SerializeField] private GameObject playerCamera;
    [SerializeField] private GameObject inspectText;
    [SerializeField] Collider lockTrigger;
    [SerializeField] private FirstPersonControler firstPersonControler;
    [SerializeField] private Inventory_script inventoryScript;
    [SerializeField] private GameObject crosshair;
    

    private bool lookingAtCamera;
    private bool lookingAtLock;


    void Start()
    {

        lookingAtLock = false;
        lockCamera.SetActive(false);
        inspectText.SetActive(false);   

    }
    
    
   public override void OnFocus() 
   {
        lookingAtLock = true;
        inspectText.SetActive(true);
        StartCoroutine(LookAtLock());
   }
   public override void OnLoseFocus()
   {
        StopCoroutine(LookAtLock());
        inspectText.SetActive(false);
        lookingAtLock = false;
   }
   public override void OnInteract()
   {
    

   }

   private IEnumerator LookAtLock()
    {
        while (lookingAtLock)
        {
            if (Input.GetKeyDown(KeyCode.F) && !lookingAtCamera) //Open Inspect
            {
                lookingAtCamera = true; 
                playerCamera.SetActive(false);
                lockCamera.SetActive(true);
                lockTrigger.enabled = false;
                firstPersonControler.enabled = false;
                inventoryScript.enabled = false;
                inspectText.SetActive(false);
                crosshair.SetActive(false);
                Cursor.lockState = CursorLockMode.Confined;
                Cursor.visible = true; 
                
                
                
            }
           
           else if (Input.GetKeyDown(KeyCode.F) && lookingAtCamera) //Close Inspect
            {
                lookingAtCamera = false;
                playerCamera.SetActive(true);
                lockCamera.SetActive(false);
                lockTrigger.enabled = true;
                firstPersonControler.enabled = true;
                inventoryScript.enabled = true;
                inspectText.SetActive(true);
                crosshair.SetActive(true);
                Cursor.lockState = CursorLockMode.Locked;
                Cursor.visible = false;
            }
            yield return null;
        }
    }
}

