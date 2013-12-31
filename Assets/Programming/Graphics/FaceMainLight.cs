using UnityEngine;
using System.Collections;
/*
 * This Script is for static world objects
 * such as trees. Place it on their shadow
 * and it will face the position of the main
 * directional light in the scene.
 */
public class FaceMainLight : MonoBehaviour {
	private GameObject mainLight;
	private Vector3 targetPos;
	private bool instancedMaterial = false;
	public Texture2D tex;
	public GameObject SpriteBoard;

	// Use this for initialization
	void OnValidate ()
	{
		if (SpriteBoard == null)
			return;

		mainLight = GameObject.FindGameObjectWithTag("MainLight");
		BillBoard bb = SpriteBoard.GetComponent<BillBoard>();
		MeshRenderer thismesh = GetComponent<MeshRenderer>();
		if (thismesh.sharedMaterial == null)
			return;

		Material mat = thismesh.sharedMaterial;
		if (!instancedMaterial)
		{
			mat = new Material(thismesh.sharedMaterial);
			instancedMaterial = true;
		}
		thismesh.sharedMaterial = mat;
		mat.mainTexture = tex;

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
