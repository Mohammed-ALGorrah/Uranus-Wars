using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NewBehaviourScript1 : MonoBehaviour
{
    public Text m_Text;
    public GameObject buildPrefab;
    void Update()
    {
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);

            m_Text.text = "Touch Position : " + touch.position;
            
            Instantiate(buildPrefab, touch.position , Quaternion.identity);
        }
        else
        {
            m_Text.text = "No touch contacts";
        }
    }
}
