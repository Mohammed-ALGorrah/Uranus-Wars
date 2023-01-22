using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MoveObjects : MonoBehaviour
{
	public Camera cam;
	Vector3 dis;
	float posX,posY;
	bool touched,dragging;
	Transform toDrag;
	Rigidbody toDragRigid;
	Vector3 perviousPosi;
	Touch touch;
	Text txt;


	private void Awake()
    {
		cam = Camera.main;
		txt = GameObject.Find("txt").GetComponent<Text>();
    }

    void FixedUpdate()
	{
		print(Input.touchCount);
	    if (Input.touchCount != 1)
	    {
	    	dragging = touched = false;
	    	return;
	    }
	    
	    touch = Input.touches[0];
	    Vector3 pos = touch.position;
	    
	    if (touch.phase == TouchPhase.Began)
	    {
	    	print("Began");
			txt.text = "begin";
	    	RaycastHit hit;
	    	Ray ray = cam.ScreenPointToRay(pos);
	    	if (Physics.Raycast(ray,out hit))
	    	{
	    		toDrag = hit.transform;
	    		perviousPosi = toDrag.position;
	    		toDragRigid = toDrag.GetComponent<Rigidbody>();
	    		dis = cam.WorldToScreenPoint(perviousPosi);
	    		posX = Input.GetTouch(0).position.x - dis.x;
	    		posY = Input.GetTouch(0).position.y - dis.y;
	    		
	    		SetDraggingProperties(toDragRigid);
	    		touched = true;
	    	}
	    }
	    
	    if (touched && touch.phase == TouchPhase.Moved)
	    {
	    	print("Moved");
			txt.text = "Moved";
			float posXnow = Input.GetTouch(0).position.x - posX;
		    float posYnow = Input.GetTouch(0).position.y - posY;
		    Vector3 curPos = new Vector3(posXnow,posYnow,dis.z);
		    
		    Vector3 worldPos = cam.ScreenToWorldPoint(curPos) - perviousPosi;
		    worldPos = new Vector3(worldPos.x,0, worldPos.y);
		    
		    toDragRigid.velocity = worldPos /(Time.deltaTime * 10);
		    
		    perviousPosi = toDrag.position;
		    
		    
	    }
	    
	    if (dragging && touch.phase == TouchPhase.Ended && touch.phase == TouchPhase.Canceled)
	    {
	    	print("Canceled");
			txt.text = "Canceled";
			dragging = touched = false;
	    	perviousPosi = Vector3.zero;
	    	SetFreeProperties(toDragRigid);
	    }
	    
    }
	void SetDraggingProperties(Rigidbody rg)
	{
		rg.isKinematic = false;
		rg.useGravity = false;
		rg.drag = 20;
		
	}
	
	void SetFreeProperties(Rigidbody rg)
	{
		rg.isKinematic = true;
		rg.useGravity = true;
		rg.drag = 5;
		
	}

	void OnTriggerEnter(Collider coll)
	{
		if (coll.CompareTag("Platform"))
		{
			if (coll.GetComponent<Platform>() != null && touch.phase == TouchPhase.Ended)
			{

				if (coll.GetComponent<Platform>().Tower == null )
				{
					transform.SetParent(coll.transform);
					coll.GetComponent<Platform>().SetTower(transform);
				}
			}
		}
	}

}
