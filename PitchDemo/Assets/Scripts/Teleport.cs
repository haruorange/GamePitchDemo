using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Teleport : MonoBehaviour
{
	private Transform exitTransform;
	[SerializeField] private GameObject paint;
	[SerializeField] private float secondsToWait = 0.5f;
    // Start is called before the first frame update
    void Start()
    {
		exitTransform = transform.GetChild(0).transform;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

	IEnumerator PaintTeleport()
	{
		yield return new WaitForSeconds(secondsToWait);
		paint.transform.forward = exitTransform.forward;
		paint.transform.position = exitTransform.position + exitTransform.forward * 1f;

		paint.GetComponent<Paint>().StartPainting();


	}

	private void OnTriggerEnter(Collider other)
	{
		if(other.tag == "Paint")
		{
			paint = other.gameObject;
			paint.GetComponent<Paint>().StopPainting();
			paint.transform.position = new Vector3(paint.transform.position.x, 1000f, paint.transform.position.z);
			StartCoroutine(PaintTeleport());
		}
	}

}
