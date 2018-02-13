using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawPlayers : MonoBehaviour 
{
	public GameObject playerPrefab;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void SpawnPlayer()
	{
		int randomSpawnPOS;

		randomSpawnPOS = Random.Range(0, this.GetComponent<LevelGenerator>().createdTiles.Capacity - 1);

		GameObject playerInstance;

		playerInstance = Instantiate(playerPrefab, this.GetComponent<LevelGenerator>().createdTiles[randomSpawnPOS], Quaternion.identity) as GameObject;
	}
}
