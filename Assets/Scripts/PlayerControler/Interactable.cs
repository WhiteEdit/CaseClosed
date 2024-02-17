using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Interactable : MonoBehaviour //abstract makes the script inheritable
{
    public virtual void Awake()
    {
        gameObject.layer = 20; //puts all objects with this script on layer 9 to make sure they can be interacted with 
    }
   public abstract void OnInteract(); 
   public abstract void OnFocus(); 
   public abstract void OnLoseFocus();
}
