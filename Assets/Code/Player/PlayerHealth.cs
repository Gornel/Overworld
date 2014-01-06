using UnityEngine;
using System.Collections;

public class PlayerHealth : MonoBehaviour {
	
	public int Health = 100;
	
	public Vector3 Position;
	private Vector3 screenPos;

	// Use this for initialization
	void Start ()
	{
		screenPos = Camera.main.ViewportToScreenPoint(Position);
	}
	
	// Update is called once per frame
	void Update ()
	{
	
	}
	
	void OnGUI ()
	{
		GUI.Label( new Rect(screenPos.x, screenPos.y, 100, 20), Health.ToString() );
	}
	
	void HurtPlayer (int dmg)
	{
		Health -= dmg;
		
		if (Health <= 0)
		{
			Application.LoadLevel(0);
		}
	}
}
