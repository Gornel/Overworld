using UnityEngine;
using System.Collections;

public class BillBoard : MonoBehaviour
{
	public bool LockY = false;
	public bool Rotate90 = true;
	public bool Flip = true;
	
	private Camera referenceCamera;
	private Vector3 targetPos;
	
	// Use this for initialization
	void Start ()
	{
		referenceCamera = Camera.main;
	}
	
	// After all actions the object takes, we face the billboard	
	void LateUpdate ()
	{
		if (!RendererExtensions.IsVisibleFrom(renderer, referenceCamera))
				return;
			if (Flip){
			targetPos = transform.position + referenceCamera.transform.rotation * Vector3.back;
		} else {
			targetPos = transform.position + referenceCamera.transform.rotation * Vector3.forward;
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
