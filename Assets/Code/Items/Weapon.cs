using UnityEngine;
using System.Collections;

public class Weapon : Item {
	public Mesh meshCollider;
	public int MinDamage = 0;
	public int MaxDamage = 0;
	public float Speed;
	public WeaponType type;
	public AudioClip[] sounds;
	
	new public static Weapon genItem ()
	{
		Weapon i = new Weapon();
		i.itemID = ItemDatabase.GenerateID();
		i.billboard = false;
		return i;
	}
	
	public void Fire (WeaponController ctrl)
	{}
	
}

public enum WeaponType
{
	Primary,
	Secondary
}