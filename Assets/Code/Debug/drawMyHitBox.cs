using UnityEngine;
using System.Collections;

public class drawMyHitBox : MonoBehaviour {
	
	public Color color = Color.green;

	// Use this for initialization
	void OnDrawGizmos()
	{
		   // set to whatever color you want to represent
		Gizmos.color = color;
			
		// we’re going to draw the gizmo in local space
		Gizmos.matrix = transform.localToWorldMatrix;
		
		// draw a box collider based on its size
		BoxCollider box = GetComponent<BoxCollider>();
		Gizmos.DrawWireCube(box.center, box.size);
	}
}
