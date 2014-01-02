using UnityEngine;
using System.Collections;

public class ParticleBurst : MonoBehaviour {
	public int Amount = 5;
	public ParticleSystem[] parts;
	void Damage ()
	{
		foreach(ParticleSystem p in parts)
		{
			p.Emit(Amount);
		}
	}
}
