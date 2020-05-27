﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Paint : MonoBehaviour
{
	private Material thisMaterial;
	[SerializeField] private LayerMask layermask;
	public bool isCastingRay = false;
	[SerializeField] private float moveSpeed = 10f;

	private Rigidbody rb;
	private const float MAX_RAY_DISTANCE = 100f;
	// Start is called before the first frame update
	void Start()
    {
		thisMaterial = GetComponent<Renderer>().material;
		rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
		RayDetect();
		Move();
    }

	void RayDetect()
	{
		if (isCastingRay)
		{
			Vector3 origin = transform.position;
			Vector3 rayDirection = Vector3.down;
			RaycastHit hitInfo;
			Ray ray = new Ray(origin, rayDirection);

			if(Physics.Raycast(ray,out hitInfo, MAX_RAY_DISTANCE, layermask))
			{
				hitInfo.transform.GetComponent<Renderer>().material = thisMaterial;
			}

		}

	}

	void Move()
	{
		if (isCastingRay)
		{
			rb.isKinematic = false;
			rb.velocity = transform.forward * moveSpeed;
		}
		else
		{
			rb.velocity = Vector3.zero;
			rb.isKinematic = true;
		}
		
	}

	private void OnCollisionEnter(Collision collision)
	{
		if(collision.collider.tag == "Wall")
		{
			rb.velocity = Vector3.zero;
			rb.isKinematic = true;
		}
	}
}
