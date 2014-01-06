using UnityEngine;
using System.Collections;

public class SpriteHasNoShadows : MonoBehaviour {

	// Use this for initialization
	void OnValidate () {
		SpriteRenderer sprt = GetComponent<SpriteRenderer>();
		sprt.castShadows = false;
		sprt.receiveShadows = true;
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
