using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewBehaviourScript : MonoBehaviour
{
    public GameObject buildPrefab;  // The prefab for the building to be placed
    public LayerMask buildLayerMask;  // The layer mask for the buildable area

    private GameObject currentBuildObject;  // The current building object being placed
    private Vector3 currentBuildObjectOriginalScale;  // The original scale of the current building object
    private Vector3 currentBuildObjectOriginalPosition;  // The original position of the current building object
    private Quaternion currentBuildObjectOriginalRotation;  // The original rotation of the current building object

    void Update()
    {
        // Check if the player has pressed the mouse button down
        if (Input.GetMouseButtonDown(0))
        {
            // Create a ray from the mouse position
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            // Perform a raycast to check if the player has clicked on the buildable area
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit, Mathf.Infinity, buildLayerMask))
            {
                // Check if there is already a building at the hit point
                Collider[] colliders = Physics.OverlapBox(hit.point, new Vector3(0.1f, 0.1f, 0.1f), Quaternion.identity, buildLayerMask);
                if (colliders.Length == 0)
                {
                    // If not, instantiate the building prefab at the hit point
                    currentBuildObject = Instantiate(buildPrefab, hit.point, Quaternion.identity);

                    // Save the original scale, position, and rotation of the building object
                    currentBuildObjectOriginalScale = currentBuildObject.transform.localScale;
                    currentBuildObjectOriginalPosition = currentBuildObject.transform.position;
                    currentBuildObjectOriginalRotation = currentBuildObject.transform.rotation;
                }
            }
        }

        // Check if the player is holding the mouse button down
        if (Input.GetMouseButton(0) && currentBuildObject != null)
        {
            // Create a ray from the mouse position
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            // Perform a raycast to check if the player has clicked on the buildable area
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit, Mathf.Infinity, buildLayerMask))
            {
                // Update the position and rotation of the building object
                currentBuildObject.transform.position = hit.point;
                currentBuildObject.transform.rotation = Quaternion.LookRotation(hit.normal);
            }// Check if the player has released the mouse button
            if (Input.GetMouseButtonUp(0) && currentBuildObject != null)
            {
                // Check if there is already a building at the final position of the object
                Collider[] colliders = Physics.OverlapBox(currentBuildObject.transform.position, new Vector3(0.1f, 0.1f, 0.1f), Quaternion.identity, buildLayerMask);
                if (colliders.Length > 0)
                {
                    // If there is already a building, destroy the current building object
                    Destroy(currentBuildObject);
                }
                else
                {
                    // If not, reset the scale, position, and rotation of the building object
                    currentBuildObject.transform.localScale = currentBuildObjectOriginalScale;
                    currentBuildObject.transform.position = currentBuildObjectOriginalPosition;
                    currentBuildObject.transform.rotation = currentBuildObjectOriginalRotation;
                }

                // Reset the current building object
                currentBuildObject = null;
            }
        }
    }
}

