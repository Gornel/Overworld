using UnityEngine;
using System.Collections;
/*
 * This Script is for static world objects
 * such as trees. Place it on their shadow
 * and it will face the position of the main
 * directional light in the scene.
 */
public class FaceMainLight : MonoBehaviour {
	public GameObject SpriteBoard;
	private GameObject mainLight;
	private bool instancedMaterial = false;
	private Vector3 targetPos;

	// Use this for initialization
	void OnValidate ()
	{
		if (SpriteBoard == null)
			return;

		mainLight = GameObject.FindGameObjectWithTag("MainLight");
		BillBoard bb = SpriteBoard.GetComponent<BillBoard>();
		MeshRenderer spritemesh = SpriteBoard.GetComponent<MeshRenderer>();
		MeshRenderer thismesh = GetComponent<MeshRenderer>();

		if (spritemesh.sharedMaterial == null)
			return;

		Material mat = thismesh.sharedMaterial;
		if (!instancedMaterial)
		{
			mat = new Material(thismesh.sharedMaterial);
			instancedMaterial = true;
		}
		thismesh.sharedMaterial = mat;
		mat.mainTexture = spritemesh.sharedMaterial.mainTexture;

		if (!bb.Flip){
			targetPos = transform.position + mainLight.transform.rotation * Vector3.back;
		} else {
			targetPos = transform.position + mainLight.transform.rotation * Vector3.forward;
		}
		
		// Ignore camera Y position to stay upright
		if (bb.LockY)
			targetPos.y = transform.position.y;
		
		transform.LookAt(targetPos);	// Inefficient, slowdowns with massive billboards
		if(bb.Rotate90){
			transform.Rotate (Vector3.left, 90F);
		}

	}
}
