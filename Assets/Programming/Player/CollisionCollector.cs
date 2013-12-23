using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CollisionCollector : MonoBehaviour {
	
	private List<Damageable> damageables;

	// Use this for initialization
	void Awake ()
	{
		damageables = new List<Damageable>();
	}
	
	void OnTriggerEnter (Collider col)
	{
		if (!damageables.Contains(col.gameObject.GetComponent<Damageable>()))
		{
			damageables.Add (col.gameObject.GetComponent<Damageable>());
		}
	}
	
	void OnTriggerExit (Collider col)
	{
		damageables.Remove(col.gameObject.GetComponent<Damageable>());
	}
	
	public List<Damageable> GetCollisions ()
	{
		
		return damageables;
	}
}
