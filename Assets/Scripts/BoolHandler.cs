using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoolHandler : MonoBehaviour
{
    public bool musicIsPlaying;
    public bool hasSafeKey;
    public bool hasDoorKey;
    public bool hasCrowbar;


    private void Start()
    {
        hasSafeKey = false;
        hasDoorKey = false;
        hasCrowbar = false;
    }

    private void boolCheck()
    {
        if (musicIsPlaying)
        {
            print("Music is playing");
        }

        else if (!musicIsPlaying) 
        {
            print("Music is not playing");
        }
    }

}

