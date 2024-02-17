using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotate : MonoBehaviour
{
    public static event Action<string, int> Rotated = delegate { };

    [SerializeField] private AudioClip lockRotate;

    [SerializeField] private AudioSource lockSound;

    private bool coroutineAllowed;

    public int numberShown;

    private void Start()
    {
        coroutineAllowed = true;
    }

    private void OnMouseDown()
    {
        if (coroutineAllowed)
        {
            StartCoroutine("RotateWheel");

        }
    }

    private IEnumerator RotateWheel()
    {
        coroutineAllowed = false;
        lockSound.PlayOneShot(lockRotate);

        for (int i = 0; i <= 11; i++)
        {
            transform.Rotate(0f, 0f, 3f);
            yield return new WaitForSeconds(0.01f);
        }

        coroutineAllowed = true;

        numberShown += 1;

        if (numberShown > 9)
        {
            numberShown = 0;
        }
        Rotated(name, numberShown);
    }
}