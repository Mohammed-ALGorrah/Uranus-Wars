using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Fusion;

public class MoveObjects : MonoBehaviour
{
	public Transform platform;
	Camera cam;
	Vector3 dis;
	float posX,posY;
	bool touched,dragging;
	Transform toDrag;
	Rigidbody toDragRigid;
	Vector3 perviousPosi;
	bool canInstantiate;
	Text t,t2;
	Vector3 mousePo;

	private void Awake()
    {
		cam = Camera.main;
		cam.GetComponent<ScrollZoom>().enabled = true;
		t = GameObject.Find("ttt").GetComponent<Text>();
		t2 = GameObject.Find("tttt").GetComponent<Text>();
		platform = GameObject.Find("Platform").transform;

	}

	void FixedUpdate()
	{
		t2.text = canInstantiate.ToString();

	    if (Input.touchCount != 1)
	    {
	    	dragging = touched = false;
	    	return;
	    }
	    
	    Touch touch = Input.touches[0];
	    Vector3 pos = touch.position;
	    
	    if (touch.phase == TouchPhase.Began)
	    {
			print("Began");
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

			cam.GetComponent<ScrollZoom>().enabled = false;
			print("Moved");
	    	float posXnow = Input.GetTouch(0).position.x - posX;
		    float posYnow = Input.GetTouch(0).position.y - posY;
		    Vector3 curPos = new Vector3(posXnow,posYnow,dis.z);
		    
		    Vector3 worldPos = cam.ScreenToWorldPoint(curPos) - perviousPosi;
		    worldPos = new Vector3(worldPos.x,0, worldPos.y);
		    
		    toDragRigid.velocity = worldPos /(Time.deltaTime * 15);
		    
		    perviousPosi = toDrag.position;


        }
        else
        {

            cam.GetComponent<ScrollZoom>().enabled = true;
        }

        if (touch.phase == TouchPhase.Canceled || touch.phase == TouchPhase.Ended)
		{
			cam.GetComponent<ScrollZoom>().enabled = true;
			print("Canceled");
	    	dragging = touched = false;
	    	perviousPosi = Vector3.zero;
	    	SetFreeProperties(toDragRigid);

            if (canInstantiate)
			{
				t.text = "Done";
				GetComponent<TowerAttack>().enabled = true;
				GetComponent<HealthSystem>().enabled = true;
				GetComponent<MoveObjects>().enabled = false;
            }
            else
			{
				t.text = "reset";
				transform.position = platform.position;
            }
	    }
	    
    }
	
    private void OnMouseDown()
    {
		mousePo = new Vector3(Input.mousePosition.x, 2, Input.mousePosition.y) - GetMousePos();
    }

    private void OnMouseDrag()
    {
		
		transform.position = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x,2, Input.mousePosition.y) - new Vector3(mousePo.x,2,mousePo.y));
    }

    Vector3 GetMousePos()
    {
		return Camera.main.ScreenToWorldPoint(transform.position);
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

    private void OnTriggerStay(Collider coll)
    {
        if (coll.CompareTag("Targetable") || coll.CompareTag("Platform"))
        {
            if (coll.CompareTag("Targetable"))
			{
				t.text = "Targetable";
			}
			canInstantiate = false;
        }
        else
		{
			t.text = "canInstantiate";
			canInstantiate = true;
		}
    }

}
