using UnityEngine;
using System.Collections;

public class CameraFollow : MonoBehaviour {

	public GameObject target;
	public float smoothCam;
	public float camDistance;
	public float tmpx, tmpy, tmpz;

	void Update()
	{
        if(target)
		transform.position = Vector3.Slerp(transform.position, new Vector3 (target.transform.position.x + tmpx ,
		                                                                   target.transform.position.y + tmpy,
		                                                                   target.transform.position.z + tmpz ),
		                                  Time.deltaTime * smoothCam);

		// transform.position = Vector3.Slerp(transform.position, new Vector3 (target.transform.position.x + tmpx + Input.GetAxis("Horizontal")*camDistance,
		//                                                                    target.transform.position.y + tmpy,
		//                                                                    target.transform.position.z + tmpz + Input.GetAxis("Vertical")*camDistance),
		//                                   Time.deltaTime * smoothCam);

	}
}
