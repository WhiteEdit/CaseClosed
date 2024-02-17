using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InspectScript : Interactable
{

    //Bools
    private bool canInspect;
    private bool canvasOpen;


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




    [Header("VoiceOver")]
    [SerializeField]
    private AudioSource playerAudioSource;
    public AudioClip[] voiceOverArray;
    private AudioClip voiceOverClip;

    [SerializeField]
    private FirstPersonControler firstPersonControler;



    void Start()
    {
        canvasOpen = false;
        canInspect = false;
        inspectObject.SetActive(false);
        inspectPostProcess.SetActive(false);
        inspectControls.SetActive(false);
        lookAtText.SetActive(false);
    }




    public override void OnFocus()
    {
        canInspect = true;
        lookAtText.SetActive(true);
        StartCoroutine(CheckForInput());

    }
    public override void OnLoseFocus()
    {
        StopCoroutine(CheckForInput());
        lookAtText.SetActive(false);
        canInspect = false;
    }

    public override void OnInteract()
    {
        


    }

   

    private IEnumerator CheckForInput()
    {
        while (canInspect)
        {
            if (Input.GetKeyDown(KeyCode.E) && !canvasOpen) //Open Inspect
            {
                canvasOpen = true;
                lookAtText.SetActive(false);
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
}
