using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartAudio : MonoBehaviour

{

    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioClip startAudio;
    void Start()
    {
       audioSource.volume = 4f;
       audioSource.PlayOneShot(startAudio); 
    }

   
}
