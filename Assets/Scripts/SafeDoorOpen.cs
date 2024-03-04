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
    [SerializeField]
    private GameObject taskUI;
    [SerializeField]
    private GameObject taskUIScratched;




    [Header("VoiceOver")]
    [SerializeField]
    private AudioSource playerAudioSource;
    public AudioClip[] voiceOverArray;
    private AudioClip voiceOverClip;

    [Header("Extra")]
    private bool firstTime;
    [SerializeField] AudioClip writingSound;
    [SerializeField] GameObject noteUpd;
    [SerializeField] AudioClip findingSafeAudio;


    private void Start()
    {
        uiElements.SetActive(false);
        safeKey.SetActive(false);
        firstTime = true;
    }
    public override void OnFocus() 
   {
        if (firstTime)
        {
            uiElements.SetActive(true);
            taskUI.SetActive(true);
            playerAudioSource.PlayOneShot(writingSound);
            StartCoroutine(NoteUpdate());
            firstTime= false;

        }
        else if (!firstTime)
        {
            uiElements.SetActive(true);
        }
        
    }

    private IEnumerator NoteUpdate()
    {

        noteUpd.SetActive(true);
        yield return new WaitForSeconds(1.5f);
        playerAudioSource.PlayOneShot(findingSafeAudio); 
        yield return new WaitForSeconds(1.5f);
        noteUpd.SetActive(false);
        yield break;


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
            taskUIScratched.SetActive(true);
            taskUI.SetActive(false);
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
