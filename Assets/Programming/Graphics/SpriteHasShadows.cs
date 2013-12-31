using UnityEngine;
using System.Collections;

public class SpriteHasShadows : MonoBehaviour {

	// Use this for initialization
	void OnValidate () {
		SpriteRenderer sprt = GetComponent<SpriteRenderer>();
		sprt.castShadows = true;
		sprt.receiveShadows = true;
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
