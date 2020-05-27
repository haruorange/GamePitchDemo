using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
	[SerializeField] private float rotateSpeed = 4f;
	[SerializeField] private float moveSpeed = 8f;
	[SerializeField] private bool ableToPush = false;
	[SerializeField] private LayerMask layermask;

	private const float MAX_RAY_DISTANCE = 100f;
	private Rigidbody rb;
	// Start is called before the first frame update
	void Start()
    {
		rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
		Move();
		Push();
    }

	void Move()
	{
		Vector3 rightMovement = Vector3.right * Input.GetAxis("HorizontalKey");
		Vector3 upMovement = Vector3.forward * Input.GetAxis("VerticalKey");
		Vector3 heading = Vector3.Normalize(rightMovement + upMovement);

		if (Input.GetAxis("HorizontalKey") != 0 || Input.GetAxis("VerticalKey") != 0)
		{
			transform.forward = Vector3.RotateTowards(transform.forward, heading, rotateSpeed, 0f);
		}
		rb.velocity = heading * moveSpeed;
	}

	void Push()
	{
		if (Input.GetKeyDown(KeyCode.Space) && ableToPush)
		{
			Vector3 origin = new Vector3(transform.position.x, 0.5f, transform.position.z);
			Vector3 rayDirection = transform.forward;
			RaycastHit hitInfo;
			Ray ray = new Ray(origin, rayDirection);

			if (Physics.Raycast(ray, out hitInfo, MAX_RAY_DISTANCE, layermask))
			{
				hitInfo.transform.forward = transform.forward;
				hitInfo.transform.GetComponent<Paint>().isCastingRay = true;
			}
		}
	}

	private void OnTriggerEnter(Collider other)
	{
		if (other.CompareTag("Paint"))
		{
			ableToPush = true;
		}
	}

	private void OnTriggerExit(Collider other)
	{
		if (other.CompareTag("Paint"))
		{
			ableToPush = false;
		}
	}
}
