using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GunActions : WeaponActions
{

	new public static void Fire (WeaponController weapctrl)
	{
		float accuracy = 70f;	// TODO: DERIVE FROM STATS
		float damage = Random.Range(weapctrl.weapon.MinDamage, weapctrl.weapon.MaxDamage);	// TODO: DERIVE FROM PLAYER WEAPON POWER RATHER THAN ITEM DAMAGE
		Camera cam = Camera.main; // TODO: DERIVE FROM CONTROLLER
		
		// Calculate hit range based on accuracy
		float portX = Random.Range(0.4f + (accuracy / 1000), 0.6f - (accuracy / 1000));
		float portY = Random.Range(0.4f + (accuracy / 1000), 0.6f - (accuracy / 1000));
		
		// Perform the shot
		Ray ray = cam.ViewportPointToRay(new Vector3(portX , portY , 0));
		RaycastHit[] hits = Physics.RaycastAll(ray);
		if (hits.Length < 1)
			return;
		RaycastHit hit = hits[0];
		List<RaycastHit> nonTriggers = new List<RaycastHit>();
		
		// filter out triggers
		foreach(RaycastHit i in hits)
		{
			if (!i.collider.isTrigger)
			{
				nonTriggers.Add(i);
			}
		}
		
		// find closest hit
		if (nonTriggers.Count < 1)
			return;
		hit = nonTriggers[0];
		foreach(RaycastHit h in nonTriggers)
		{
			if (h.distance < hit.distance)
			{
				hit = h;
			}
		}
		// If it's got physics, hit it.			
		if (hit.rigidbody)
		{
			hit.rigidbody.AddForceAtPosition(cam.transform.forward * 500f, hit.point);
		}
		
		// Deal damage to damageables
		hit.collider.SendMessageUpwards("Damage", damage , SendMessageOptions.DontRequireReceiver);
		
		// Temporary
		AudioSource.PlayClipAtPoint(weapctrl.hitSound, hit.point);
		GameObject.Instantiate(weapctrl.hitParticle, hit.point, Quaternion.LookRotation(Vector3.up));
	}
}
