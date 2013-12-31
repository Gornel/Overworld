using UnityEngine;
using System.Collections;
/*
 * This Script is for static world objects
 * such as trees. Place it on their shadow
 * and it will face the position of the main
 * directional light in the scene.
 */
public class FaceMainLight : MonoBehaviour {
	public bool LockY = true;
	public bool Rotate90 = true;
	public bool Flip = false;
	
	private GameObject mainLight;
	private Vector3 targetPos;

	// Use this for initialization
	void OnValidate ()
	{
		mainLight = GameObject.FindGameObjectWithTag("MainLight");
		if (Flip){
			targetPos = transform.position + mainLight.transform.rotation * Vector3.back;
		} else {
			targetPos = transform.position + mainLight.transform.rotation * Vector3.forward;
		}
		
		// Ignore camera Y position to stay upright
		if (LockY)
			targetPos.y = transform.position.y;
		
		transform.LookAt(targetPos);	// Inefficient, slowdowns with massive billboards
		if(Rotate90){
			transform.Rotate (Vector3.left, 90F);
		}

	}
}
