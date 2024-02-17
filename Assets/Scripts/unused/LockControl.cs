using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LockControl : MonoBehaviour
{
    [SerializeField]
    private AudioSource playerAudioSource;
    [SerializeField]
    private AudioClip padlockOpen;
    [SerializeField] private GameObject padlock;

    public int number1;
    public int number2;
    public int number3;

    private int[] result, correctCombination;
    void Start()
    {
        result = new int[] { 7, 7, 7 };
        correctCombination = new int[] { number1, number2, number3 };
        Rotate.Rotated += CheckResults;
    }

    private void CheckResults(string wheelName, int number)
    {
        switch (wheelName)
        {
            case "wheel1":
                result[0] = number;
                break;

            case "wheel2":
                result[1] = number;
                break;

            case "wheel3":
                result[2] = number;
                break;

        }

        if (result[0] == correctCombination[0] && result[1] == correctCombination[1] && result[2] == correctCombination[2])
        {
            Debug.Log("Opened");
            Destroy(padlock);
            playerAudioSource.PlayOneShot(padlockOpen);


        }
    }
    private void OnDestroy()
    {
        Rotate.Rotated -= CheckResults;
    }
}