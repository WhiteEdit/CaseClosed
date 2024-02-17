using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestInteractable : Interactable
{
    
   public override void OnFocus() 
   {
       print("lOOKING AT" + gameObject.name);
   }
   public override void OnLoseFocus()
   {
        print("STOPPED lOOKING AT" + gameObject.name);
   }
   public override void OnInteract()
   {
        print("interacted with" + gameObject.name);
   }
}
