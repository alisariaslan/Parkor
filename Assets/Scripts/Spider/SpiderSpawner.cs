using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpiderSpawner : MonoBehaviour
{
	[Header("Spider Prefab")]
	public GameObject mobPrefab;
	[Header("Spawn Chance (Chance calculated from maxSpawnChance value distance to 0)")]
	public int maxSpawnChance = 1;
	[Header("Spawn Rate")]
	public int minSpawnRate = 1;
	public int maxSpawnRate = 3;
	[Header("Minimum Spawn Distance")]
	public int minSpawnDistance = 50;
	[Header("Spawner Spawn Distance")]
	public int minSpawnerSpawnDistanceX = 0;
	public int maxSpawnerSpawnDistanceX = 5;
	public int minSpawnerSpawnDistanceY = 0;
	public int maxSpawnerSpawnDistanceY = 5;
	[Header("Spawner Lifetime (-1 = infinite)")]
	public int lifeSpan = -1;

	private GameObject player;

	// Start is called before the first frame update
	void Start()
	{
		player = GameObject.Find("PlayerHuman");
	}
	private int nextUpdate = 1;
	void CheckForSpawn()
	{
		int uzaklik = (int)(player.transform.position.x - transform.position.x);
		if (uzaklik > minSpawnDistance || uzaklik < -minSpawnDistance)
		{
			//Debug.Log("yeterince uzak: "+uzaklik);
			if (Random.Range(0, maxSpawnRate) != 0)
				SpawnNow(Random.Range(minSpawnRate, maxSpawnRate));
		}
		else
		{
			//Debug.Log("fazla yakin: " + uzaklik);
		}

	}

	void SpawnNow(float c)
	{
		while (c != 0 && lifeSpan != 0)
		{
			Vector2 randomizeSpawn = this.transform.position + new Vector3(Random.Range(0, 5), Random.Range(0, 5));
			GameObject spider = Instantiate(mobPrefab, randomizeSpawn, Quaternion.identity, transform);
			lifeSpan--;
			SpiderController spiderController = spider.GetComponent<SpiderController>();
			spiderController.player = player;
			spiderController.seen = true;
			c--;
		}
		if (lifeSpan == 0 )
		{
			transform.tag = "Untagged";
			GameObject.Destroy(this);
		}
	}

	// Update is called once per frame
	void Update()
	{
		if (Time.time >= nextUpdate)
		{
			nextUpdate = Mathf.FloorToInt(Time.time) + 5;
			CheckForSpawn();
		}
	}
}
