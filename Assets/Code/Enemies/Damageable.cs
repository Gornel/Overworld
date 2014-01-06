using UnityEngine;
using System.Collections;

public class Damageable : MonoBehaviour {
	
	public int Health = 10;
	public AudioClip hitSound;

	public void Damage (int damage)
	{
		Health -= damage;
		
		if (Health <= 0)
		{
			//Call the Kill function on the object
			SendMessage("Kill");
			Destroy(this);
			return;
		}
		audio.PlayOneShot(hitSound);
	}
}
