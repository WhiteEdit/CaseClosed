using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxInspect : Interactable
{

    //Bools
    private bool canInspect;
    private bool canvasOpen;
    public bool boxUnlocked;


    [Header("Inspect")]
    [SerializeField]
    private GameObject inspectObject;
    [SerializeField]
    private GameObject inspectPostProcess;
    [SerializeField]
    private GameObject crossHair;
    [SerializeField]
    private GameObject inspectControls;
    [SerializeField]
    private GameObject lookAtText;
    [SerializeField]
    private GameObject findWayBox;





    [Header("VoiceOver")]
    [SerializeField]
    private AudioSource playerAudioSource;
    public AudioClip[] voiceOverArray;
    private AudioClip voiceOverClip;

    [SerializeField]
    private FirstPersonControler firstPersonControler;

    [Header("LockCamera")]
    [SerializeField] private GameObject lockCamera;
    [SerializeField] private GameObject playerCamera;
    [SerializeField] private GameObject inspectText;
    [SerializeField] Collider lockTrigger;
    [SerializeField] private Inventory_script inventoryScript;

    [Header("Open")]
    [SerializeField] private AudioClip openedSound;
    [SerializeField] private AudioClip lockedSound;
    [SerializeField] private Animator openAnimation;
    [SerializeField] private Collider boxCollider; 


    private bool lookingAtCamera;
    private bool lookingAtLock;



    void Start()
    {
        canvasOpen = false;
        canInspect = false;
        boxUnlocked = false;
        inspectObject.SetActive(false);
        inspectPostProcess.SetActive(false);
        inspectControls.SetActive(false);
        lookAtText.SetActive(false);
        findWayBox.SetActive(false);
        
        //LockScript
        lookingAtLock = false;
        lockCamera.SetActive(false);
        inspectText.SetActive(false);




    }




    public override void OnFocus()
    {
        canInspect = true;
        lookAtText.SetActive(true);
        StartCoroutine(CheckForInput());
        findWayBox.SetActive(true);

        //LockScript

        lookingAtLock = true;
        inspectText.SetActive(true);
        StartCoroutine(LookAtLock());

    }
    public override void OnLoseFocus()
    {
        StopCoroutine(CheckForInput());
        lookAtText.SetActive(false);
        canInspect = false;

        //LockScript

        StopCoroutine(LookAtLock());
        inspectText.SetActive(false);
        lookingAtLock = false;
    }

    public override void OnInteract()
    {
        if(!boxUnlocked) 
        {
            playerAudioSource.PlayOneShot(lockedSound);
        }

        else if(boxUnlocked) 
        {
            boxCollider.enabled = false;
            playerAudioSource.PlayOneShot(openedSound);
            openAnimation.SetTrigger("OpenBox");
        }
    }

   

    private IEnumerator CheckForInput()
    {
        while (canInspect)
        {
            if (Input.GetKeyDown(KeyCode.E) && !canvasOpen) //Open Inspect
            {
                canvasOpen = true;
                lookAtText.SetActive(false);
                inspectText.SetActive(false);
                inspectControls.SetActive(true);
                crossHair.SetActive(false);
                inspectObject.SetActive(true);
                inspectPostProcess.SetActive(true);
                firstPersonControler.enabled = false;
                Cursor.lockState = CursorLockMode.Confined;
                Cursor.visible = true;

                int index = UnityEngine.Random.Range(0, voiceOverArray.Length);
                voiceOverClip = voiceOverArray[index];
                playerAudioSource.clip = voiceOverClip;
                playerAudioSource.Play();

            }
            else if (Input.GetKeyDown(KeyCode.E) && canvasOpen) //Close Inspect
            {
                canvasOpen = false;
                lookAtText.SetActive(true);
                inspectText.SetActive(true);
                inspectControls.SetActive(false);
                crossHair.SetActive(true);
                inspectObject.SetActive(false);
                inspectPostProcess.SetActive(false);
                firstPersonControler.enabled = true;
                Cursor.lockState = CursorLockMode.Locked;
                Cursor.visible = false;
                playerAudioSource.Stop();
            }
            yield return null;
        }
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
                crossHair.SetActive(false);
                lookAtText.SetActive(false);
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
                crossHair.SetActive(true);
                lookAtText.SetActive(true);
                Cursor.lockState = CursorLockMode.Locked;
                Cursor.visible = false;
                playerAudioSource.Stop();
            }
            yield return null;
        }
    }
}
