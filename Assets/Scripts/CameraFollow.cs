using UnityEngine;
using System.Collections;

public class CameraFollow : MonoBehaviour {

	#region private serialized variables
	[SerializeField]
	private Vector3 followOffset;	//the follow offset of the camera
	[SerializeField]
	private float smoothFollowAmount = 0f;
	[SerializeField]
	private string followTag;
	#endregion

	#region private variables
	private Transform meCamera;		//the camera it self
	private Transform targetObject;	//the object to follow
	#endregion

	void Awake(){
		meCamera = this.transform;	//assign itself to variable
		targetObject = GameObject.FindWithTag(followTag).transform;		//get and assign the player object
	}

	void LateUpdate(){
		if (targetObject == null) {
			return;
		}
		meCamera.position = new Vector3(Mathf.SmoothStep(meCamera.position.x, targetObject.position.x + followOffset.x, Time.deltaTime * smoothFollowAmount),
                                        Mathf.SmoothStep(meCamera.position.y, targetObject.position.y + followOffset.y, Time.deltaTime * smoothFollowAmount) ,
                                        Mathf.SmoothStep(meCamera.position.z, targetObject.position.z + followOffset.z, Time.deltaTime * smoothFollowAmount));		//follow the player
	}
}
