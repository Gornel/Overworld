using UnityEngine;
using System.Collections;

public class killgriswold : MonoBehaviour {
	public SpriteController griswold;
	public SpriteAnimation death;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if(Input.GetMouseButtonUp(0))
		{
			griswold.BeginAnimation(death);
		}
	}
}
