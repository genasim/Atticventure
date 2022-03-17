using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Destroyer : MonoBehaviour
{
	public List<SpawnPoint> spawnPoints;

	void OnTriggerEnter2D(Collider2D other)
	{
		if(other.TryGetComponent(out SpawnPoint sp))
		{
			other.gameObject.GetComponent<SpawnPoint>().spawned = true;
			//	Don't do CleanDestroy()!!!
		}

		if (other.TryGetComponent(out Destroyer d))
			Destroy(other.transform.parent.gameObject);
	}
}
