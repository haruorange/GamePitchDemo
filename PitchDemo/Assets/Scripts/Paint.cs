using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Paint : MonoBehaviour
{
	private Material thisMaterial;
	[SerializeField] private LayerMask layermask;
	[SerializeField] public bool isCastingRay = false;

	private const float MAX_RAY_DISTANCE = 100f;
	// Start is called before the first frame update
	void Start()
    {
		thisMaterial = GetComponent<Renderer>().material;
    }

    // Update is called once per frame
    void Update()
    {
		RayDetect();
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
}
