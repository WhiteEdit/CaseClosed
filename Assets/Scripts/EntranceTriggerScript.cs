using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntranceTriggerScript : MonoBehaviour
{


    [SerializeField] GameObject searchApart;
    [SerializeField] GameObject noteUpd;
    [SerializeField] AudioClip writing;
    [SerializeField] AudioSource sourceAudio;

    private void OnTriggerEnter(Collider other)
    {
       searchApart.SetActive(true);
       sourceAudio.PlayOneShot(writing);
        StartCoroutine(NoteUpdate());
    }

    private IEnumerator NoteUpdate()
    {

        noteUpd.SetActive(true);
        yield return new WaitForSeconds(3f);
        noteUpd.SetActive(false);
        Destroy(this);
        yield break;


    }
}
