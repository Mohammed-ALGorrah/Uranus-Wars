using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LocalCameraHandler : MonoBehaviour
{
    public Transform cameraAnchorPoint;
    Vector2 viewInput;
    float cameraRotationX = 0;
    float cameraRotationY = 0;
    Camera localCamera;
    NetworkCharacterControllerPrototypeCustome networkCharacterControllerPrototypeCustome;


    private void Awake()
    {
        localCamera = GetComponent<Camera>();
        networkCharacterControllerPrototypeCustome = GetComponentInParent<NetworkCharacterControllerPrototypeCustome>();

    }

    void Start()
    {
        if (localCamera.enabled)
        {
            localCamera.transform.parent = null;
        }
    }

    private void LateUpdate()
    {
        if (cameraAnchorPoint == null)
        {
            return;
        }

        if (!localCamera.enabled)
        {
            return;
        }

        localCamera.transform.position = cameraAnchorPoint.position;

        cameraRotationX += viewInput.y * Time.deltaTime * networkCharacterControllerPrototypeCustome.viewUpDownRotaionSpeed;
        cameraRotationX = Mathf.Clamp(cameraRotationX,-90,90);

        cameraRotationY += viewInput.x * Time.deltaTime * networkCharacterControllerPrototypeCustome.rotationSpeed;

        localCamera.transform.rotation = Quaternion.Euler(cameraRotationX,cameraRotationY,0);
    }

    public void SetViewInputVector(Vector2 viewInput)
    {
        this.viewInput = viewInput;
    }
}
