using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MatressScript : Interactable
{
    [SerializeField] private GameObject matressDisabel;
    [SerializeField] private GameObject matressEnable;
    [SerializeField] private GameObject uiElements;
    [SerializeField] private AudioClip moveSound;
    [SerializeField] private AudioSource audSource;


    private void Start()
    {
        matressEnable.SetActive(false);
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
       matressDisabel.SetActive(false);
       matressEnable.SetActive(true);
        audSource.PlayOneShot(moveSound);
   }
}
