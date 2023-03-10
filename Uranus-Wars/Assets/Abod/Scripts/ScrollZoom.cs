using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScrollZoom : MonoBehaviour
{
    float MouseZoomSpeed = 15.0f;
    float TouchZoomSpeed = 0.1f;
    float ZoomMinBound = 20f;
    float ZoomMaxBound = 80f;
    Camera cam;

    public float panSpeed = 20f;
    private bool isPanning;
    private Vector3 panOrigin;

    // Use this for initialization
    void Start()
    {
        cam = GetComponent<Camera>();
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            // If it is, set the panning flag to true
            isPanning = true;

            // Store the position where the mouse button was pressed
            panOrigin = Input.mousePosition;
        }
        else if (Input.GetMouseButtonUp(0))
        {
            // If the mouse button is released, set the panning flag to false
            isPanning = false;
        }

        // If the panning flag is true, move the camera
        if (isPanning)
        {
            Vector3 pos = Camera.main.ScreenToViewportPoint(Input.mousePosition - panOrigin);
            Vector3 move = new Vector3(pos.x * panSpeed * Time.deltaTime, 0, pos.y * panSpeed * Time.deltaTime);
            if (Camera.main.transform.position.z > -1974 ) {
                Camera.main.transform.position = new Vector3(Camera.main.transform.position.x , Camera.main.transform.position.y , -1974);
                return;
            }
            else if (Camera.main.transform.position.z < -1979)
            {
                Camera.main.transform.position = new Vector3(Camera.main.transform.position.x, Camera.main.transform.position.y, -1979);
                return;
            }
            else if (Camera.main.transform.position.x >  975)
            {
                Camera.main.transform.position = new Vector3(975, Camera.main.transform.position.y, Camera.main.transform.position.z);
                return;
            }
            else if (Camera.main.transform.position.x < 945)
            {
                Camera.main.transform.position = new Vector3(945, Camera.main.transform.position.y, Camera.main.transform.position.z);
                return;
            }
            else
            {
                transform.Translate(move, Space.World);
            }
        }


        if (Input.touchSupported)
        {
            // Pinch to zoom
            if (Input.touchCount == 2)
            {

                // get current touch positions
                Touch tZero = Input.GetTouch(0);
                Touch tOne = Input.GetTouch(1);
                // get touch position from the previous frame
                Vector2 tZeroPrevious = tZero.position - tZero.deltaPosition;
                Vector2 tOnePrevious = tOne.position - tOne.deltaPosition;

                float oldTouchDistance = Vector2.Distance(tZeroPrevious, tOnePrevious);
                float currentTouchDistance = Vector2.Distance(tZero.position, tOne.position);

                // get offset value
                float deltaDistance = oldTouchDistance - currentTouchDistance;
                Zoom(deltaDistance, TouchZoomSpeed);
            }
        }
        else
        {

            float scroll = Input.GetAxis("Mouse ScrollWheel");
            Zoom(scroll, MouseZoomSpeed);
        }



        if (cam.fieldOfView < ZoomMinBound)
        {
            cam.fieldOfView = 0.1f;
        }
        else
        if (cam.fieldOfView > ZoomMaxBound)
        {
            cam.fieldOfView = 179.9f;
        }
    }

    void Zoom(float deltaMagnitudeDiff, float speed)
    {

        cam.fieldOfView += deltaMagnitudeDiff * speed;
        // set min and max value of Clamp function upon your requirement
        cam.fieldOfView = Mathf.Clamp(cam.fieldOfView, ZoomMinBound, ZoomMaxBound);
    }
}