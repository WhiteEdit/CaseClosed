using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoolHandler : MonoBehaviour
{
    public bool musicIsPlaying;

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

