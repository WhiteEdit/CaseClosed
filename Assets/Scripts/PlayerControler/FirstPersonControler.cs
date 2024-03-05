using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using UnityEngine;

public class FirstPersonControler : MonoBehaviour
{
    public bool CanMove {get; private set;} = true; 

    [Header("Functional Options")]
    [SerializeField] private bool canInteract = true;
    [SerializeField] private  bool canZoom = true;
    [SerializeField] private bool useFootsteps = true;


    [Header("Controls")] //Adds a header in the inspector for a easier navigation and to keep things organized
    [SerializeField] private KeyCode interactKey = KeyCode.E;
    [SerializeField] private KeyCode zoomKey = KeyCode.Mouse1; 



  
  [Header("Movement Parameters")] //Adds a header in the inspector for a easier navigation and to keep things organized
  [SerializeField] private float walkSpeed = 3.0f; //value for the Walkspeed 
  [SerializeField] private float gravity = 30.0f; //value for the gravity multiplier


  [Header("Look Parameters" )]
 
  [SerializeField,Range(1,10)] private float lookSpeedX = 2.0f; //Sets Value for the mouse/look speed on the X axis. The Range atribute limits the values that can be entered in the inspector (in this case it can be between 1 and 10). 
  [SerializeField,Range(1,10)] private float lookSpeedY = 2.0f; //Sets Value for the mouse/look speed on the Y axis. 
  [SerializeField,Range(1,180)] private float upperLookLimit = 80.0f; //limits how much the player can look up before the camera stops.
  [SerializeField,Range(1,180)] private float lowerLookLimit = 80.0f; //limits how much the player can look down before the camera stops.

[Header("Interaction")]

[SerializeField] private Vector3 interactionRayPoint = default;
[SerializeField] private float interactionDistance = default;
[SerializeField] private LayerMask interactionLayer = default;
private Interactable currentInteractable; //refernece to object that is currently interacted with
  private Camera playerCamera;
  private CharacterController characterController;

  private Vector3 moveDirection;
  private Vector2 currentInput;
  private float rotationX = 0; //needed to track the angle of the players rotation for the upper/lower look limit

  [Header("Interaction")]
  [SerializeField] private float timeToZoom = 0.3f; //Sets the time it takes to zoom.
  [SerializeField] private float ZoomFov =  30f; //field of view for the zoom, defines how much the camera zooms in or out
  private float defaultFOV; 
  private Coroutine zoomRoutine;

    [Header("Footsteps")]
    [SerializeField] private float baseStepSpeed = 0.5f;
    [SerializeField] private AudioSource footstepAudioSource = default;
    [SerializeField] private AudioClip[] woodClips = default;
    [SerializeField] private AudioClip[] carpetClips = default;
    [SerializeField] private AudioClip[] concreteClips = default;
    private float footstepTimer = 0;






    private void HandleInteractionCheck()
{
    if(Physics.Raycast(playerCamera.ViewportPointToRay(interactionRayPoint), out RaycastHit hit, interactionDistance ) && hit.collider.gameObject.layer == 20)
    {
        if(hit.collider.gameObject.layer== 20 && (currentInteractable == null || hit.collider.gameObject.GetInstanceID() != currentInteractable.gameObject.GetInstanceID()) )  //if game object is on layer 20 and player is not currently looking at an interactabel object, script tries to get an interactable component from the object.  
            {
                hit.collider.TryGetComponent(out currentInteractable); //this tries to get the interactable component as mentioned above
                
                if (currentInteractable)
                currentInteractable.OnFocus();

            }    
    }
    else if(currentInteractable)
    {
      currentInteractable.OnLoseFocus();
      currentInteractable = null;  
    }
}

private void HandleInteractionInput()
{
    if (Input.GetKeyDown(interactKey) && currentInteractable != null && Physics.Raycast(playerCamera.ViewportPointToRay(interactionRayPoint),out RaycastHit hit,interactionDistance,interactionLayer)) //if player presses left mouse button (interaction key) and if the current interactable is not equal null (handled by HandleInteractionCheck), a ray is cast out from the camera by a distance equal to interactionDistance and if the object is on the interactionLayer, the player can interact with the object
    {
        currentInteractable.OnInteract();
    }
}

    
    void Awake()
    {
        playerCamera = GetComponentInChildren<Camera>(); //searches throught the Children to find a camera
        characterController = GetComponent<CharacterController>(); //searches a CharacterController component
        Cursor.lockState = CursorLockMode.Locked; //locks the cursor by default
        Cursor.visible = false; //makes the cursor invisible

        defaultFOV = playerCamera.fieldOfView;
    }

    // Update is called once per frame
    void Update()
    {
        if (CanMove) //checks if the player can move. Usefull to have if one wants to force a perspective eg. during cutscenes.
        {
            HandleMovementInput();
            HandleMouseLook();
            ApplyFinalMovements();

            if (canInteract)

            HandleInteractionCheck(); //constantly raycast out to see if player can interact with object
            HandleInteractionInput(); // press input key to perfom interaction

            if (canZoom)
            HandleZoom();

            if (useFootsteps)
                Handle_Footsteps();

        }
    }

    private void HandleZoom()
    {
        if (Input.GetKeyDown(zoomKey)) //executes if the key set as zoomkey is pressed 
        {
            if (zoomRoutine !=null)
            {
                StopCoroutine(zoomRoutine);
                zoomRoutine = null;
            }
            zoomRoutine = StartCoroutine(ToggleZoom(true));

        }

        if (Input.GetKeyUp(zoomKey)) //executes if the key set as zoomkey is pressed 
        {
            if (zoomRoutine !=null)
            {
                StopCoroutine(zoomRoutine);
                zoomRoutine = null;
            }
            zoomRoutine = StartCoroutine(ToggleZoom(false));

        }

    }


    private void HandleMovementInput() 
    {
        currentInput = new Vector2(walkSpeed* Input.GetAxis("Vertical"), walkSpeed * Input.GetAxis("Horizontal")); //calculates the direction on a Vector2 from a players input multiplied with the speed of movement
        
        float moveDirectionY = moveDirection.y;
        moveDirection = (transform.TransformDirection(Vector3.forward) * currentInput.x) + (transform.TransformDirection(Vector3.right)*currentInput.y);
        moveDirection.y = moveDirectionY;
        //calculates a players position based on the direction they are facing + their input
    }

    private void HandleMouseLook()
    {
        rotationX -= Input.GetAxis("Mouse Y")* lookSpeedY; 
        rotationX = Mathf.Clamp(rotationX, -upperLookLimit, lowerLookLimit); //clamps the camera rotationX is the value that should be clamped, -upperLookLimit is the minimum value that is is allowed while lowerLookLimit is the highest value allowed.
        playerCamera.transform.localRotation = Quaternion.Euler(rotationX,0,0);//applies rotation to camera, Quaternion is used to avoid problems like gimbal lock ( 2 axis becoming parallel which cab cause weird behaviour)
        transform.rotation *= Quaternion.Euler(0, Input.GetAxis("Mouse X") * lookSpeedX,0); //left and right rotation, doesnt rotate the camera but the parent object which the camera is attacehd to.
    }


    private void ApplyFinalMovements()
    {
        //while not needed for the game I desided to add gravity to the character controller following the tutorial as it did not take much time and its always nice to have extra flexibility.
        if(!characterController.isGrounded) //if character is not on the ground 
        moveDirection.y -= gravity * Time.deltaTime; //this will pull down the character

        characterController.Move(moveDirection * Time.deltaTime);
    }


    private void Handle_Footsteps() 
    {
        if (!characterController.isGrounded) return;
        if (currentInput == Vector2.zero) return;

        footstepTimer -= Time.deltaTime;
        
        if (footstepTimer <= 0)
        { if(Physics.Raycast(characterController.transform.position, Vector3.down, out RaycastHit hit, 3))
            { 
                switch(hit.collider.tag) 
                {
                       case "Wood":
                        footstepAudioSource.PlayOneShot(woodClips[Random.Range(0, woodClips.Length-1)]);
                        break;
                       case "Carpet":
                        footstepAudioSource.PlayOneShot(carpetClips[Random.Range(0, carpetClips.Length - 1)]);
                        break;
                       case "Concrete":
                        footstepAudioSource.PlayOneShot(concreteClips[Random.Range(0, concreteClips.Length - 1)]);
                        break;
                    default:
                        footstepAudioSource.PlayOneShot(woodClips[Random.Range(0, woodClips.Length - 1)]);
                        break;
                } 

            }
            footstepTimer = baseStepSpeed;
        }
       
    }

    private IEnumerator ToggleZoom(bool isEnter)
    {
        float targetFOV = isEnter ? ZoomFov:  defaultFOV;
        float startingFOV = playerCamera.fieldOfView; 
        float timeElapsed = 0;

        while(timeElapsed < timeToZoom)
        {
            playerCamera.fieldOfView =Mathf.Lerp(startingFOV, targetFOV, timeElapsed / timeToZoom);
            timeElapsed += Time.deltaTime;
            yield return null;
        }

        playerCamera.fieldOfView = targetFOV;
        zoomRoutine = null;



    }
    
}
