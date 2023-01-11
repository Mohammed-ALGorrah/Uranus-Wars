using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
	ParticleSystem onHitfx;
	ParticleSystem projectileFx;
	[HideInInspector]
	public Transform FirePoint;
	[HideInInspector]
	public float damage; 
	Transform parent;
	
    void Start()
    {
	    parent = transform.parent;
	    Destroy(gameObject,0.6f);

    }

    // Update is called once per frame
    void Update()
    {
	    transform.position += FirePoint.forward * Time.deltaTime * 8;
    }
}
