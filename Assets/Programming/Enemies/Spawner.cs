using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Spawner : MonoBehaviour
{
	public GameObject[] ObjectsToSpawn;
	public float SpawnRate = 2;
	
	private EnemyController[] currentEnemies;
	private bool spawning = false;
	private List<GameObject> players;
	
	
	void Awake ()
	{
		currentEnemies = new EnemyController[ObjectsToSpawn.Length];
		players = new List<GameObject>();
	}
	
	// Use this for initialization
	void Start ()
	{
		StartCoroutine("Spawn");
	}
	
	IEnumerator Spawn ()
	{
		while (true)
		{
			while (spawning)
			{
				
				// Cleanup
				for (int i = 0; i < currentEnemies.Length; i++)
				{
					if (currentEnemies[i] != null && !currentEnemies[i].enabled)
					{
						currentEnemies[i] = null;
					}
				}
				
				// Spawn enemies
				for (int i = 0; i < currentEnemies.Length; i++)
				{
					if (currentEnemies[i] == null)
					{
						GameObject go = Instantiate(ObjectsToSpawn[i], this.transform.position, this.transform.rotation) as GameObject;
						currentEnemies[i] = go.GetComponentInChildren<EnemyController>();
						break;
					}
				}
				
				// Check if we have players nearby
				if (players.Count <= 0)
				{
					spawning = false;
				}
				
				// We wait before checking once more.
				yield return new WaitForSeconds(SpawnRate);
				
			}
			// Check if we have players nearby
			if (players.Count > 0)
			{
				spawning = true;
			}
			
			yield return new WaitForSeconds(SpawnRate);
		}
	}
	
	void OnTriggerEnter (Collider col)
	{
		if (col.tag == "Player" && !players.Contains(col.gameObject))
			players.Add(col.gameObject);
	}
	
	void OnTriggerExit (Collider col)
	{
		if (col.tag == "Player" && players.Contains(col.gameObject))
			players.Remove(col.gameObject);
	}
	
	void Kill ()
	{
		StopAllCoroutines();
		this.enabled = false;
		
		// Placeholder
		Destroy(this.gameObject);
	}
}
