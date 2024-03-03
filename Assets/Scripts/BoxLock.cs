using System.Collections;
using UnityEngine;
using static UnityEngine.UIElements.UxmlAttributeDescription;

public class BoxLock : MonoBehaviour
{
    [Header("Correct Code")]
    [SerializeField] private int correctNumber1;
    [SerializeField] private int correctNumber2;
    [SerializeField] private int correctNumber3;

    [Header("CodeLock Wheels")]

    public Rotate wheel1;
    public Rotate wheel2;
    public Rotate wheel3;

    [Header("Audio&Padlock")]

    [SerializeField] private AudioSource playerAudioSource;
    [SerializeField] private AudioClip padlockOpen;

    [Header("UI")]
    [SerializeField] private GameObject textDestroy;
    [SerializeField] private GameObject textShow;
    [SerializeField] private float waitTime;
    [SerializeField] private float disapearTime;
    [SerializeField] private AudioClip writingAudio;
    [SerializeField] private GameObject notebookUpdated;
    [SerializeField] private GameObject crosshair;

    [Header("ZoomElements")]
    [SerializeField] private GameObject playerCamera;
    [SerializeField] private GameObject lockCamera;
    [SerializeField] FirstPersonControler firstPersonControler;
    [SerializeField] Inventory_script inventoryScript;

    [Header("Rotate")]
    [SerializeField] private Rotate rotate1;
    [SerializeField] private Rotate rotate2;
    [SerializeField] private Rotate rotate3;

    [Header("Trigger")]
    [SerializeField] private Collider trigger;

    public BoxInspect boxInspect;






    private void Start()
    {
        StartCoroutine(CheckNumbersCoroutine());
        notebookUpdated.SetActive(false);
    }

    private IEnumerator CheckNumbersCoroutine()
    {
        
           while (true) 
        { 
           
            if ((correctNumber1 == wheel1.numberShown) && (correctNumber2 == wheel2.numberShown) && (correctNumber3 == wheel3.numberShown)) 
            {
                
                playerAudioSource.PlayOneShot(padlockOpen);
              

                yield return new WaitForSeconds(waitTime);

                boxInspect.boxUnlocked=true;
                notebookUpdated.SetActive(true);
                trigger.enabled=true;
                Debug.Log("UI Changed");
                Destroy(textDestroy);
                textShow.SetActive(true);
                playerAudioSource.PlayOneShot(writingAudio);
                rotate1.enabled = false;
                rotate2.enabled = false;
                rotate3.enabled = false;


                playerCamera.SetActive(true);
                lockCamera.SetActive(false);
                Destroy(lockCamera);
                crosshair.SetActive(true);
                firstPersonControler.enabled = true;
                inventoryScript.enabled = true;
                Cursor.lockState = CursorLockMode.Locked;
                Cursor.visible = false;
            

                
                yield return new WaitForSeconds(disapearTime);
                notebookUpdated.SetActive(false);
                Destroy(this);
                yield break;
            }
            yield return null;
        }

    }



}

