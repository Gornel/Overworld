using UnityEngine;
using System.Collections;

public class CameraBob : MonoBehaviour {
	
	public CharacterMotor motor;
	public float BobAmount = 1f;
	public float BobSpeed = 10f;
	private float sineIterator = 0;
	private Vector3 originalPosition;
	
	// Use this for initialization
	void Awake () {
		originalPosition = transform.localPosition;
	}

	void Update () {
		if (!motor.grounded)
			return;
		
		//Get input and clamp. This will keep diagonal input from being faster than straight input.
		//float vertAxis = Mathf.Abs(Input.GetAxis("Vertical")) + Mathf.Abs(Input.GetAxis("Horizontal"));
		float vertAxis = (Mathf.Abs(motor.movement.velocity.x) + Mathf.Abs(motor.movement.velocity.z)) / 8;
		vertAxis = Mathf.Clamp01(vertAxis);
		
		//Distance to bob
		float targetDistance = (Mathf.Sin(sineIterator) * BobAmount);
		Vector3 bobPosition = new Vector3(originalPosition.x, originalPosition.y + targetDistance, originalPosition.z);
		Vector3 newPos = bobPosition;
		
		//Iterate
		if (vertAxis != 0)
		{
			sineIterator += BobSpeed * vertAxis * Time.deltaTime ;
		}
		
		// Clean up the iterator
		if (sineIterator > 2 * Mathf.PI){
			sineIterator -= Mathf.PI * 2;
		}
		
		transform.localPosition = newPos;
	}
}
