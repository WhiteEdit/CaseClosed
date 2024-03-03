using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class EnemyScript : MonoBehaviour

{
    [SerializeField] private Animator enemyAnimation;
    [SerializeField] private AudioSource playerAudio;
    [SerializeField] private AudioClip enemyScream;
    [SerializeField] private GameObject enemyFace;



    private void OnTriggerEnter(Collider other)
    {
        enemyAnimation.SetTrigger("EnemyIsTriggered");
        StartCoroutine(Wait());
    }

    private IEnumerator Wait()
    {
        yield return new WaitForSeconds(0.5f);

        // Add your actions after the delay here
    }
}

