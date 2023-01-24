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
    
    
	void OnTriggerEnter(Collider coll)
	{
		if (coll.CompareTag("Tower"))
		{
			if (Tower == null)
			{
				coll.transform.SetParent(transform);
				Tower = coll.transform;
				StartCoroutine(setPos(coll.transform));
			}
		}
	}
	
	IEnumerator setPos(Transform t)
	{
		yield return new WaitForSeconds(0.3f);
		t.position = transform.Find("Point").transform.position;
	}
}
