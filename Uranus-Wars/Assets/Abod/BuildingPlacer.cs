using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BuildingPlacer : MonoBehaviour
{
    public GameObject buildPrefab;  // The prefab for the building to be placed
    public LayerMask buildLayerMask;  // The layer mask for the buildable area
    public LayerMask buildingLayerMask;  // The layer mask for the buildable area
    public Text m_Text;
    Touch touch; 
    void Update()
    {
        if (Input.touchCount > 0)
        {
            touch = Input.GetTouch(0);

            m_Text.text = "Touch Position : " + touch.position;
            if (buildPrefab != null)
            {
                buildPrefab.transform.position = touch.position;
            }
        }
        else
        {
            m_Text.text = "No touch contacts";
        }
        // Check if the player has pressed the mouse button down
        if (Input.GetMouseButtonDown(0))
        {
            // Create a ray from the mouse position
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            // Perform a raycast to check if the player has clicked on the buildable area
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit, Mathf.Infinity, buildingLayerMask))
            {
                Debug.Log("Build");
                return;
            }
            else if (Physics.Raycast(ray, out hit, Mathf.Infinity, buildLayerMask))
            {
                // Instantiate the building prefab at the hit point

                //  Instantiate(buildPrefab, hit.point, Quaternion.identity);

            }

        }
        if (Input.GetMouseButtonUp(0))
        {
            Debug.Log("SS");
        }
    }

    public void OnTryBuild()
    {
        buildPrefab = Resources.Load<GameObject>("SettingBtn");



        Instantiate(buildPrefab, touch.position, Quaternion.identity);



    }
}