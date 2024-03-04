using System.Collections;
using System.Collections.Generic;
using UnityEditor.UIElements;
using UnityEngine;

public class BathroomDoor : Interactable
{

    [SerializeField] private GameObject uiElements;
    
    [SerializeField] private AudioSource playerAudioSource;
    
    public AudioClip[] voiceOverArray;
    
    private AudioClip voiceOverClip;


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
        int index = UnityEngine.Random.Range(0, voiceOverArray.Length);
        voiceOverClip = voiceOverArray[index];
        playerAudioSource.clip = voiceOverClip;
        playerAudioSource.Play();
    }
}
