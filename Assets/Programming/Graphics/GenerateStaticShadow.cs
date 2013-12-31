using UnityEngine;
using System.Collections;

public class GenerateStaticShadow : MonoBehaviour {
	GameObject shadowBoard;
	GameObject mainLight;
	public Material ShadowMaterial;

	void OnValidate ()
	{
		Vector3 targetPos;
		BillBoard bb = GetComponent<BillBoard>();
		mainLight = GameObject.FindGameObjectWithTag("MainLight");

		if (transform.parent.FindChild("shadowboard") == null) {
			shadowBoard = new GameObject("shadowboard");
			shadowBoard.transform.position = this.transform.position;
			shadowBoard.transform.rotation = this.transform.rotation;
			shadowBoard.transform.parent = this.transform.parent;
			shadowBoard.isStatic = true;
			shadowBoard.AddComponent<MeshRenderer>();
			shadowBoard.AddComponent<MeshFilter>();

		} else {
			shadowBoard = transform.parent.FindChild("shadowboard").gameObject;
		}

		shadowBoard.GetComponent<MeshFilter>().sharedMesh = GetComponent<MeshFilter>().sharedMesh;
		Material mat = new Material(ShadowMaterial);
		mat.mainTexture = GetComponent<MeshRenderer>().sharedMaterial.mainTexture;
		shadowBoard.GetComponent<MeshRenderer>().sharedMaterial = mat;


		if (!bb.Flip){
			targetPos = shadowBoard.transform.position + mainLight.transform.rotation * Vector3.back;
		} else {
			targetPos = shadowBoard.transform.position + mainLight.transform.rotation * Vector3.forward;
		}
		
		// Ignore camera Y position to stay upright
		if (bb.LockY)
			targetPos.y = shadowBoard.transform.position.y;
		
		shadowBoard.transform.LookAt(targetPos);	// Inefficient, slowdowns with massive billboards
		if(bb.Rotate90){
			shadowBoard.transform.Rotate (Vector3.left, 90F);
		}
	}
}
