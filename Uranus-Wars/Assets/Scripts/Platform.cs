using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Platform : MonoBehaviour
{
	public Transform Tower;
	
	
	
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    
 public void SetTower(Transform t)
    {
        Tower = t;
        StartCoroutine(setPos(t));
    }
	
	IEnumerator setPos(Transform t)
	{
		yield return new WaitForSeconds(0.3f);
		t.position = transform.Find("Point").transform.position;
	}



}
