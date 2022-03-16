using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Destroyer : MonoBehaviour
{
	public List<SpawnPoint> spawnPoints;

	void OnTriggerEnter2D(Collider2D other)
	{
		if(other.CompareTag("SpawnPoint"))
		{
			other.gameObject.GetComponent<SpawnPoint>().spawned = true;
			//	Don't do CleanDestroy()!!!
		}
	}
}
