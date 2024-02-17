using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

public class VinylInteract : Interactable
{
    //Bools
    private bool canInspect;
    private bool canvasOpen;
    public bool musicPlays;
    public BoolHandler boolHandler;


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
    private GameObject inspectText;
    



    [Header("Interact")]
    [SerializeField]
    private GameObject vinylDisc;
    [SerializeField]
    private AudioSource vinylPlayerAudio;
    [SerializeField]
    private AudioClip vinylClip;
    [SerializeField]
    private float vinylDuration;

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
        musicPlays = false;
        inspectObject.SetActive(false);
        inspectPostProcess.SetActive(false);
        vinylDisc.SetActive(false);
        inspectControls.SetActive(false);
        inspectText.SetActive(false);
    }




    public override void OnFocus()
    {
        canInspect = true;
        inspectText.SetActive(true);
        StartCoroutine(CheckForInput());

    }
    public override void OnLoseFocus()
    {
        StopCoroutine(CheckForInput());
        inspectText.SetActive(false);
        canInspect = false;
    }

    public override void OnInteract()
    {
        if (!boolHandler.musicIsPlaying)
        {
            boolHandler.musicIsPlaying = true;
            musicPlays = true;
            vinylPlayerAudio.PlayOneShot(vinylClip);
            vinylDisc.SetActive(true);
            StartCoroutine(VinylWait());
        }

        else if (boolHandler.musicIsPlaying)
        {
            boolHandler.musicIsPlaying = false;
            musicPlays = false;
            vinylDisc.SetActive(false);
            vinylPlayerAudio.Stop();
            StopCoroutine(VinylWait());


        }


    }

    private IEnumerator VinylWait()
    {
        float counter = 0;
        
        
        while (boolHandler.musicIsPlaying)
        {
            counter += Time.deltaTime;
            if (counter>=vinylDuration)
            {
                boolHandler.musicIsPlaying = false;
                musicPlays = false;
                vinylDisc.SetActive(false);
                vinylPlayerAudio.Stop();
            }

            yield return null;
        }
    }

    private IEnumerator CheckForInput()
    {
        while (canInspect)
        {
            if (Input.GetKeyDown(KeyCode.E) && !canvasOpen) //Open Inspect
            {
                canvasOpen = true;
                inspectText.SetActive(false);
                inspectControls.SetActive(true);
                crossHair.SetActive(false);
                inspectObject.SetActive(true);
                inspectPostProcess.SetActive(true);
                firstPersonControler.enabled = false;
                Cursor.lockState = CursorLockMode.Confined;
                Cursor.visible = true;

                int index = UnityEngine.Random.Range(0,voiceOverArray.Length);
                voiceOverClip = voiceOverArray[index];
                playerAudioSource.clip = voiceOverClip;
                playerAudioSource.Play();
                
            }
            else if (Input.GetKeyDown(KeyCode.E) && canvasOpen) //Close Inspect
            {
                canvasOpen = false;
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




}


