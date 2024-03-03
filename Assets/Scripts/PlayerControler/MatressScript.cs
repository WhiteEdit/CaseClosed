using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MatressScript : Interactable
{
    [SerializeField] private GameObject matressDisabel;
    [SerializeField] private GameObject matressEnable;

    private void Start()
    {
        matressEnable.SetActive(false);
    }

    public override void OnFocus() 
   {
     
   }
   public override void OnLoseFocus()
   {
       
   }
   public override void OnInteract()
   {
       matressDisabel.SetActive(false);
       matressEnable.SetActive(true);
   }
}
