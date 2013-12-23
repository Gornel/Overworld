using UnityEngine;
using System.Collections;

public class DestroyParticle : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
		
	}
	
	// Update is called once per frame
	void Update () {
		if (particleSystem.time >= 0.5f)
		{
			Destroy(this.gameObject);
		}
	}
}
