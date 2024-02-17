using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory_script : MonoBehaviour
{

    public bool menuIsOpen;

    [SerializeField] private GameObject inventory;
    [SerializeField] private GameObject inspectPostProcess;
    [SerializeField] private KeyCode inventoryKey = KeyCode.Tab;
    [SerializeField] private FirstPersonControler firstPersonControler;


    [Header ("Audio")]
    [SerializeField] private AudioSource playerAudioSource;
    [SerializeField] private AudioClip inventoryOpen;
    [SerializeField] private AudioClip inventoryClose;

    [Header("Text")]
    [SerializeField] private GameObject[] textArray;












    // Start is called before the first frame update
    void Start()
    {
        menuIsOpen = false;
        inventory.SetActive(false);
        foreach (GameObject obj in textArray)
        {
            obj.SetActive(false);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if ((Input.GetKeyDown(inventoryKey)) && (!menuIsOpen))
        {
            playerAudioSource.Stop();
            inventory.SetActive(true);
            playerAudioSource.PlayOneShot(inventoryOpen);
            menuIsOpen = true;

            inspectPostProcess.SetActive(true);
            firstPersonControler.enabled = false;
            Cursor.lockState = CursorLockMode.Confined;
            Cursor.visible = true;
        }

        else if ((Input.GetKeyDown(inventoryKey)) && (menuIsOpen))
        {
            playerAudioSource.Stop();
            inventory.SetActive(false);
            playerAudioSource.PlayOneShot(inventoryClose); 
            menuIsOpen = false;

            inspectPostProcess.SetActive(false);
            firstPersonControler.enabled = true;
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;

        }
    }

    
}
