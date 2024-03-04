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



    public void QuitGame()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }



    private void OnTriggerEnter(Collider other)
    {
        enemyAnimation.SetTrigger("EnemyIsTriggered");
        playerAudio.PlayOneShot(enemyScream);
        StartCoroutine(EnemyWait());
    }

    private IEnumerator EnemyWait()
    {
        yield return new WaitForSeconds(0.1f);
        enemyFace.SetActive(true);
        yield return new WaitForSeconds(1f);
        QuitGame();

     
    }
}

