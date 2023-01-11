using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterInputHandler : MonoBehaviour
{
    Vector2 moveInputVector = Vector2.zero;
    Vector2 viewInputVector = Vector2.zero;
    bool isJumpButtonPressed = false;
    bool isFireButtonPressed = false;
    bool isGrenadeFireButtonPressed = false;
    bool isRocketLauncherFireButtonPressed = false;
    
    LocalCameraHandler localCamraHandler;
    CharacterMovementHandler characterMovementHandler;


    private void Awake()
    {
        localCamraHandler = GetComponentInChildren<LocalCameraHandler>();
        characterMovementHandler = GetComponent<CharacterMovementHandler>();
    }

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void Update()
    {
        if (!characterMovementHandler.Object.HasInputAuthority)
        {
            return;
        }

        viewInputVector.x = Input.GetAxis("Mouse X");
        viewInputVector.y = Input.GetAxis("Mouse Y") * -1;

        moveInputVector.x = Input.GetAxis("Horizontal");
        moveInputVector.y = Input.GetAxis("Vertical");

        if (Input.GetButtonDown("Jump"))
        {
            isJumpButtonPressed = true;
        }

        if (Input.GetButtonDown("Fire1"))
        {
            isFireButtonPressed = true;
        }

        if (Input.GetButtonDown("Fire2"))
        {
            isRocketLauncherFireButtonPressed = true;
        }

        if (Input.GetKeyDown(KeyCode.G))
        {
            isGrenadeFireButtonPressed = true;
        }

        localCamraHandler.SetViewInputVector(viewInputVector);
    }

    public NetworkInputData GetNetworkInput()
    {
        NetworkInputData networkInputData = new NetworkInputData();

        networkInputData.aimForwardVector = localCamraHandler.transform.forward;

        networkInputData.movementInput = moveInputVector;

        networkInputData.isJumpPressed = isJumpButtonPressed;

        networkInputData.isFireButtonPressed = isFireButtonPressed;

        networkInputData.isGrenadeFireButtonPressed = isGrenadeFireButtonPressed;

        networkInputData.isRocketLauncherFireButtonPressed = isRocketLauncherFireButtonPressed;

        isJumpButtonPressed = false;
        isFireButtonPressed = false;
        isGrenadeFireButtonPressed = false;
        isRocketLauncherFireButtonPressed = false;

        return networkInputData;
    }
}
