using UnityEngine;
using System.Collections;

public class SpriteHasShadows : MonoBehaviour {
	private SpriteRenderer sprt;
	// Use this for initialization
	void OnValidate () {
		 sprt = GetComponent<SpriteRenderer>();
		if (sprt == null)
			return;
		sprt.castShadows = true;
		sprt.receiveShadows = true;
	}
}
