using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraZoom : MonoBehaviour
{
    public float zoomSpeed = 10f;
    public float minZoom = 20f;
    public float maxZoom = 80f;


    public float panSpeed = 15f;

    private bool isPanning;
    private Vector3 panOrigin;


    public GameObject settingPanel;
    void Update()
    {

        /*     float scroll = Input.GetAxis("Mouse ScrollWheel");
          Camera.main.fieldOfView = Mathf.Clamp(Camera.main.fieldOfView - scroll * zoomSpeed, minZoom, maxZoom);
       */
        /* if (Input.touchCount == 2)
         {
             settingPanel.SetActive(true);
             // Get the positions of the two touches
             Vector2 touch1 = Input.GetTouch(0).position;
             Vector2 touch2 = Input.GetTouch(1).position;

             // Get the distance between the touches
             float prevTouchDeltaMag = (touch1 - touch2).magnitude;
             float touchDeltaMag = (Input.GetTouch(0).position - Input.GetTouch(1).position).magnitude;

             // Calculate the difference between the distances
             float deltaMagnitudeDiff = prevTouchDeltaMag - touchDeltaMag;

             // Clamp the field of view to the min and max zoom values
             Camera.main.fieldOfView += deltaMagnitudeDiff * zoomSpeed;
             Camera.main.fieldOfView = Mathf.Clamp(Camera.main.fieldOfView, minZoom, maxZoom);
         }*/


        // Check if the left mouse button is being held down
        /*if (Input.GetMouseButtonDown(0))
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
            Vector3 move = new Vector3(0 , 0, -pos.x * panSpeed *Time.deltaTime);

            transform.Translate(move, Space.World);
        }*/

        /* if (Input.touchCount == 1)
         {
             // If it is, set the panning flag to true
             isPanning = true;

             // Store the position where the mouse button was pressed
             panOrigin = Input.touches[0].position;
         }
         else if (Input.touchCount == 0)
         {
             // If the mouse button is released, set the panning flag to false
             isPanning = false;
         }

         // If the panning flag is true, move the camera
         if (isPanning)
         {
             Vector3 pos = Camera.main.ScreenToViewportPoint((Vector3)Input.touches[0].position - panOrigin);
             Vector3 move = new Vector3(0, 0, -pos.x * panSpeed * Time.deltaTime);

             transform.Translate(move, Space.World);
         }*/
        // Zoom in or out using mouse scroll wheel
        /* float scroll = Input.GetAxis("Mouse ScrollWheel");
         transform.position += transform.forward * scroll * zoomSpeed;

         // Clamp the zoom level between the min and max values
         transform.position = new Vector3(transform.position.x, transform.position.y, Mathf.Clamp(transform.position.z, minZoom, maxZoom));*/

        if (Input.touchCount == 2)
        {
            // Store both touches.
            Touch touchZero = Input.GetTouch(0);
            Touch touchOne = Input.GetTouch(1);

            // Find the position in the previous frame of each touch.
            Vector2 touchZeroPrevPos = touchZero.position - touchZero.deltaPosition;
            Vector2 touchOnePrevPos = touchOne.position - touchOne.deltaPosition;

            // Find the magnitude of the vector (the distance) between the touches in each frame.
            float prevTouchDeltaMag = (touchZeroPrevPos - touchOnePrevPos).magnitude;
            float touchDeltaMag = (touchZero.position - touchOne.position).magnitude;

            // Find the difference in the distances between each frame.
            float deltaMagnitudeDiff = prevTouchDeltaMag - touchDeltaMag;

            // Zoom the camera based on the change in distance between the touches.
            transform.position += transform.forward * deltaMagnitudeDiff * zoomSpeed *Time.deltaTime;

            // Clamp the zoom level between the min and max values
            transform.position = new Vector3(transform.position.x, transform.position.y, Mathf.Clamp(transform.position.z, minZoom, maxZoom));
        }




    }

}
