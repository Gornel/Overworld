using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class WeaponController : MonoBehaviour {
	public Hand hand;
	public Weapon weapon;
	public AudioClip hitSound;
	public GameObject hitParticle;
	public CharacterMotor motor;
	
	private string axis;
	private bool canAttack = true;
	private bool instancedMat = false;
	
	public enum Hand {
		LeftHand,
		RightHand
	}
	
	// Use this for initialization
	void Start ()
	{
		if (hand == Hand.LeftHand)
		{
			axis = "Fire1";
		} else if (hand == Hand.RightHand) {
			axis = "Fire2";
		}
	}
	
	void OnValidate ()
	{
		SwapWeapon();
	}
	
	void SwapWeapon ()
	{
		// Get the Renderer
		MeshRenderer r = GetComponentInChildren<MeshRenderer>();
		if (weapon == null || r == null)
			return;
		Material mat = r.sharedMaterial;
		// Insantiate new material
		if(!instancedMat)
		{
			mat = new Material(r.sharedMaterial);
			instancedMat = true;
		}
		mat.mainTexture = weapon.Graphic;
		// Set specular or diffuse
		if(weapon.Specular)
		{
			mat.shader = Shader.Find("Transparent/Cutout/Specular");
		} else {
			mat.shader = Shader.Find("Transparent/Cutout/Diffuse");
		}		
		r.sharedMaterial = mat;
	}
	
	// Update is called once per frame
	void Update ()
	{
		if (Input.GetAxis(axis) > 0.5f && canAttack)
		{
			StartCoroutine("Attack");
		}
		/*if (hand == Hand.LeftHand){
			if (!canAttack && motor.grounded)
			{
				motor.movement.maxForwardSpeed = 1;
				motor.movement.maxBackwardsSpeed = 1;
				motor.movement.maxSidewaysSpeed = 1;
			} else {
				motor.movement.maxForwardSpeed = 8;
				motor.movement.maxBackwardsSpeed = 8;
				motor.movement.maxSidewaysSpeed = 8;
			}
		}*/
	}
	
	IEnumerator Attack ()
	{
		int swingRand = Mathf.FloorToInt(Random.Range(0, weapon.sounds.Length));
		AudioSource.PlayClipAtPoint(weapon.sounds[swingRand], transform.position);
		
		if (weapon.type == WeaponType.Secondary)
		{
			GunActions.Fire(this);
		}
		
		if (GetComponent<Animation>())
		{
			AttackAnimation();
		}
		
		canAttack = false;
		yield return new WaitForSeconds(weapon.Speed);
		canAttack = true;
	}
	
	void DealDamage ()
	{
		int damage = Mathf.CeilToInt(Random.Range(weapon.MinDamage, weapon.MaxDamage));
		
		// Perform the shot
		Ray ray = Camera.main.ViewportPointToRay(new Vector3(0.5f , 0.5f , 0));
		RaycastHit[] hits = Physics.RaycastAll(ray, 6f);
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
			hit.rigidbody.AddForceAtPosition(Camera.main.transform.forward * 500f, hit.point);
		}
		
		// Deal damage to damageables
		hit.collider.SendMessageUpwards("Damage", damage , SendMessageOptions.DontRequireReceiver);
		
		// Temporary
		AudioSource.PlayClipAtPoint(hitSound, hit.point);
		GameObject.Instantiate(hitParticle, hit.point, Quaternion.LookRotation(Vector3.up));
	}
	
	void AttackAnimation ()
	{
		animation.Stop();
		
		int rnd = Random.Range(0, 100);
		if (rnd <= 50)
		{
			animation["swing1"].speed = 1 / weapon.Speed;
			animation.Play("swing1");
		} else {
			animation["swing2"].speed = 1 / weapon.Speed;
			animation.Play("swing2");
		}
	}
}
