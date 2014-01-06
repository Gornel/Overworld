using UnityEngine;
using System.Collections;

public class EnemyController : MonoBehaviour {
	
	protected GameObject player;
	protected Vector3 PlayerRelativePosition;
	protected Facing FaceDirection;
	protected Facing pFaceDirection;
	protected SpriteController Sprite;
	
	
	// Use this for initialization
	public virtual void Start ()
	{
		player = GameObject.FindGameObjectWithTag("Player");
		Sprite = GetComponentInChildren<SpriteController>();
	}
	public virtual void Update ()
	{
		Facing f = CalculateFacing();
		if (FacingChanged(f))
		{
			FaceDirection = f;
		}
		
	}
	void LateUpdate ()
	{
		pFaceDirection = FaceDirection;
	}
	
	protected bool FacingChanged (Facing f)
	{
		if (f != pFaceDirection)
			return true;
		else
			return false;
	}
	
	protected Facing CalculateFacing ()
	{
		float degreeFacing = 0;
		// Get relative player position
		Vector3 targetDir = transform.InverseTransformPoint(player.transform.position);
		PlayerRelativePosition = targetDir;
		try
		{
			degreeFacing = Mathf.Atan(targetDir.x / targetDir.z) * Mathf.Rad2Deg;
		} catch (UnityException e)
		{
			Debug.LogError(e);
		}
		
		// Correct for circular angle
		if(targetDir.z < 0)
			degreeFacing += 180f;
		if (targetDir.x < 0 && targetDir.z > 0)
			degreeFacing += 360f;
		

		
		// Return facing
		if (degreeFacing > 315f && degreeFacing < 45f)
			return Facing.Front;
		if (degreeFacing > 45f && degreeFacing < 135f)
			return Facing.Left;
		if (degreeFacing > 135f && degreeFacing < 225f)
			return Facing.Back;
		if (degreeFacing > 225f && degreeFacing < 315f)
			return Facing.Right;
		return Facing.Front;
	}
}