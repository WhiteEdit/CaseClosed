using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SafeDoorOpen : Interactable
{
    public BoolHandler boolHandler;

    [SerializeField]
    private GameObject safeKey;
    [SerializeField]
    private Animator doorAnimtion;
    [SerializeField]
    private GameObject uiElements;
    [SerializeField]
    private AudioClip doorOpenSound;
    [SerializeField]
    private GameObject removeKey;
    [SerializeField]
    private GameObject scratchedKey;



    [Header("VoiceOver")]
    [SerializeField]
    private AudioSource playerAudioSource;
    public AudioClip[] voiceOverArray;
    private AudioClip voiceOverClip;

    private void Start()
    {
        uiElements.SetActive(false);
        safeKey.SetActive(false);
    }
    public override void OnFocus() 
   {
        uiElements.SetActive(true);
    }
   public override void OnLoseFocus()
   {
        uiElements.SetActive(false);
    }
   public override void OnInteract()
   {
        if (boolHandler.hasSafeKey == true) 
        {
            playerAudioSource.PlayOneShot(doorOpenSound);
            doorAnimtion.SetTrigger("DoorOpen");
            uiElements.SetActive(false);
            safeKey.SetActive(true);
            removeKey.SetActive(false);
            scratchedKey.SetActive(true);
            Destroy(this); 

        }

        else if (!boolHandler.hasSafeKey )
        {
            int index = UnityEngine.Random.Range(0, voiceOverArray.Length);
            voiceOverClip = voiceOverArray[index];
            playerAudioSource.clip = voiceOverClip;
            playerAudioSource.Play();


        }
    }
}
