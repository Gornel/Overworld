using UnityEngine;
using System.Collections;

public class LightFlicker : MonoBehaviour {

	public float variance = 0.1f;
	private float oIntensity;
	private float sineIterator;
	
	void Awake ()
	{
		oIntensity = light.intensity;
		sineIterator = 0;
	}
	
	// Update is called once per frame
	void Update () {
		
		light.intensity = oIntensity + (Mathf.Sin (sineIterator) * variance);
		sineIterator += Time.deltaTime * Random.Range(0, 60f);
		
		if (sineIterator > 2 * Mathf.PI)
			sineIterator -= 2 * Mathf.PI;
	}
	
}
