using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelGenerator : MonoBehaviour 
{
	#region variables

	[Space(10)]
	[Header("Theme Manager")]
	public ThemeManager[] themes;


	#region Lists
	[Header("Lists")]


	[Space(1)]

	[Header("Environment Lists")]
	public List<Vector3> createdTiles;
	public List<GameObject> createdTileGameObjects;
	public List<GameObject> createdWalls;
	public List<GameObject> createdBoarderWalls;
	#endregion

	[Space(1)]

	[Header("Prefab Lists")]
	public List<GameObject> createdEnemies;
	public List<Vector3> objectsAndCharacters;

	[Space(10)]

	public int tileAmount;
	public int tileSize;

	public float waitTime;

	public float chanceUp;
	public float chanceRight;
	public float chanceDown;

	public int seed;

	public float minY = 999999999;
	public float maxY = 0;
	public float minX = 999999999;
	public float maxX = 0;

	public float xAmount;
	public float yAmount;

	public float extraWallX;
	public float extraWallY;

	public Transform parent;
	public Transform objectParent;

	public int startTileAmount;
	public int startEnemyAmount;
	#endregion

	AstarPath Astar;

	// Use this for initialization
	void Start () 
	{
		objectParent = new GameObject().transform;
		objectParent.name = "ObjectParent";

		parent = new GameObject().transform;
		parent.name = "LevelParent";

		//StartCoroutine(GenerateLevel());

		Astar = GameObject.Find("A*").GetComponent<AstarPath>();

		GenerateOnStart();
	}
	
	// Update is called once per frame
	void Update () 
	{
		//UpdateUI();
	}

	void GenerateOnStart()
	{
		for(int i = 0; i < tileAmount; i ++)
		{
			float dir = Random.Range(0f, 1f);
			int tile = Random.Range(0, themes[0].environments[0].environmentPrefabs.Length);

			CreateTile(tile);
			CallMoveGenerator(dir);

			//yield return new WaitForSeconds(waitTime);

			if(i == tileAmount - 1)
			{
				Finish();

				CreateWallsBoarder();

				TestDistance();

				SpawnDoor();

				SpawnPlayer();

				SpawnChester();

				SpawnConsole();

				SpawnObjects();

				SpawnEnemy();

				CreateTraps();

				PathFindingScan();

				Camera.main.GetComponent<CameraFollow>().target = GameObject.FindWithTag("Player").transform;
			}
		}
	}

	/*IEnumerator GenerateLevel()
	{
		for(int i = 0; i < tileAmount; i ++)
		{
			float dir = Random.Range(0f, 1f);
			int tile = Random.Range(0, themes[0].environments[0].environmentPrefabs.Length);

			CreateTile(tile);
			CallMoveGenerator(dir);

			yield return new WaitForSeconds(waitTime);
				
			if(i == tileAmount - 1)
			{
				Finish();

				CreateWallsBoarder();

				TestDistance();

				SpawnDoor();

				SpawnPlayer();

				SpawnChester();

                SpawnConsole();

				SpawnObjects();

				SpawnEnemy();

				CreateTraps();

				PathFindingScan();

				Camera.main.GetComponent<CameraFollow>().target = GameObject.FindWithTag("Player").transform;
			}
		}
	}*/

	void PathFindingScan()
	{
		Astar.Scan();
	}

	void CreateWall(Vector3 pos)
	{
		GameObject wallInstance;
		wallInstance = Instantiate(themes[0].environments[1].environmentPrefabs[0], pos, transform.rotation) as GameObject;
		createdWalls.Add(wallInstance.gameObject);
	}

	void CallMoveGenerator(float randomDirection)
	{
		if(randomDirection < chanceUp)
		{
			MoveGenerator(0);
		}
		else if(randomDirection < chanceRight)
		{
			MoveGenerator(1);
		}
		else if(randomDirection < chanceDown)
		{
			MoveGenerator(2);
		}
		else
		{
			MoveGenerator(3);
		}
	}

	void MoveGenerator(int direction)
	{
		switch(direction)
		{
		case 0:

			transform.position = new Vector3(transform.position.x, transform.position.y + tileSize, 0); //up

			break;
		case 1:

			transform.position = new Vector3(transform.position.x + tileSize, transform.position.y, 0); // right

			break;
		case 2:

			transform.position = new Vector3(transform.position.x, transform.position.y - tileSize, 0); // down

			break;
		case 3:

			transform.position = new Vector3(transform.position.x - tileSize, transform.position.y, 0); // left

			break;
		}
	}

	void CreateTile(int tileIndex)
	{
		if(!createdTiles.Contains(transform.position))
		{
			GameObject tileInstance;
			tileInstance = Instantiate(themes[0].environments[0].environmentPrefabs[tileIndex], transform.position, transform.rotation);
			createdTiles.Add(tileInstance.transform.position);
			createdTileGameObjects.Add(tileInstance);

			tileInstance.transform.parent = parent;
		}
		else
		{
			tileAmount++;
		}
	}

	void Finish()
	{
		CreateWallValues();
		CreateWalls();
	}

	void CreateWallValues()
	{
		for(int i = 0; i < createdTiles.Count; i ++)
		{
			if(createdTiles[i].y < minY)
			{
				minY = createdTiles[i].y;
			}

			if(createdTiles[i].y > maxY)
			{
				maxY = createdTiles[i].y;
			}

			if(createdTiles[i].x < minX)
			{
				minX = createdTiles[i].x;
			}

			if(createdTiles[i].x > maxX)
			{
				maxX = createdTiles[i].x;
			}

			xAmount = ((maxX - minX) / tileSize + extraWallX);
			yAmount = ((maxY - minY) / tileSize + extraWallY);
		}
	}

	void CreateWalls()
	{
		for(int x = 0; x < xAmount; x ++)
		{
			for(int y = 0; y < yAmount; y ++)
			{
				if(!createdTiles.Contains(new Vector3((minX - (extraWallX * tileSize) / 2 ) + (x * tileSize), (minY -(extraWallY * tileSize) / 2) + (y * tileSize))))
				{
					GameObject wallInstance;
					wallInstance = Instantiate(themes[0].environments[1].environmentPrefabs[0], new Vector3((minX - (extraWallX * tileSize) / 2 ) + (x * tileSize), (minY -(extraWallY * tileSize) / 2) + (y * tileSize)), transform.rotation) as GameObject;

					wallInstance.transform.parent = parent;

					createdWalls.Add(wallInstance);
				}
			}
		}
	}

	void CreateWallsBoarder()
	{
		foreach(GameObject wall in createdWalls)
		{
			if(createdTiles.Contains(new Vector3(wall.transform.position.x, wall.transform.position.y - tileSize, wall.transform.position.z)))
			{
				GameObject wallBoarderInstance;
				wallBoarderInstance = Instantiate(themes[0].environments[2].environmentPrefabs[0], wall.transform.position, Quaternion.identity) as GameObject;

				wallBoarderInstance.transform.parent = parent;

				createdBoarderWalls.Add(wallBoarderInstance);

				Destroy(wall.gameObject);
			}
		}
	}

	void CreateTraps()
	{
		for(int i = 0; i < themes[0].environments[3].amount; i ++)
		{
			int randomIndex;
			randomIndex = Random.Range(0, createdTiles.Count);
			Vector3 randPos;
			randPos = createdTiles[randomIndex];

			if(!objectsAndCharacters.Contains(randPos))
			{
				GameObject trapInstance;

				trapInstance = Instantiate(themes[0].environments[3].environmentPrefabs[0], randPos, Quaternion.identity) as GameObject;

				Destroy(createdTileGameObjects[randomIndex].gameObject);

				objectsAndCharacters.Add(trapInstance.transform.position);
			}
			else
			{
				themes[0].environments[3].amount ++;
			}
		}
	}

	public Vector3 startDist;
	public Vector3 endDist;

	void TestDistance()
	{
		endDist = createdTiles[0];

		for(int i = 0; i < createdTiles.Count; i ++)
		{
			if(Vector2.Distance(createdTiles[0], createdTiles[i]) > Vector2.Distance(createdTiles[0], endDist))
			{
				endDist = createdTiles[i];
			}
		}
		for(int i = 0; i < createdTiles.Count; i ++)
		{
			if(Vector2.Distance(endDist, createdTiles[i]) > Vector2.Distance(endDist, createdTiles[0]))
			{
				startDist = createdTiles[i];
			}
		}
	}

	public void SpawnPlayer()
	{
		GameObject playerInstance;
		playerInstance = Instantiate(themes[0].singletons[0].singletonPrefab, startDist, Quaternion.identity) as GameObject;
		objectsAndCharacters.Add(playerInstance.transform.position);
		//playerInstance.transform.parent = objectParent;
	}

	public void SpawnDoor()
	{
		GameObject doorInstance;
		doorInstance = Instantiate(themes[0].singletons[1].singletonPrefab, endDist, Quaternion.identity) as GameObject;
		objectsAndCharacters.Add(doorInstance.transform.position);
		doorInstance.transform.parent = objectParent;
	}

	void SpawnChester()
	{
		GameObject chesterInstance;
		chesterInstance = Instantiate(themes[0].singletons[2].singletonPrefab, createdTiles[Random.Range(0, createdTiles.Count)], Quaternion.identity) as GameObject;
		objectsAndCharacters.Add(chesterInstance.transform.position);
		chesterInstance.transform.parent = objectParent;
	}

    void SpawnConsole()
    {
        GameObject consoleInstance;
		consoleInstance = Instantiate(themes[0].singletons[3].singletonPrefab, createdTiles[Random.Range(0, createdTiles.Count)], Quaternion.identity) as GameObject;
        objectsAndCharacters.Add(consoleInstance.transform.position);
    }

	void SpawnEnemy()
	{
		for(int i = 0; i < themes[0].enemies.Length; i ++)
		{
			for(int j = 0; j < themes[0].enemies[i].enemyAmount; j ++)
			{
				int randIndex;
				randIndex = Random.Range(0, createdTiles.Count);
				Vector3 randPos;
				randPos = createdTiles[randIndex];

				if(!objectsAndCharacters.Contains(randPos))
				{
					GameObject enemyInstance;
					enemyInstance = Instantiate(themes[0].enemies[i].enemyPrefab, randPos, Quaternion.identity) as GameObject;
					objectsAndCharacters.Add(enemyInstance.transform.position);

					enemyInstance.transform.parent = objectParent;
				}
				else
				{
					themes[0].enemies[i].enemyAmount ++;
				}
			}
		}
	}

	void SpawnObjects()
	{
		for(int i = 0; i < themes[0].collectables.Length; i ++)
		{
			for(int j = 0; j < themes[0].collectables[i].objectAmount; j ++)
			{
				int randIndex;
				randIndex = Random.Range(0, createdTiles.Count);
				Vector3 randPos;
				randPos = createdTiles[randIndex];

				if(!objectsAndCharacters.Contains(randPos))
				{
					GameObject objectInstance;
					objectInstance = Instantiate(themes[0].collectables[i].objectPrefab, randPos, Quaternion.identity) as GameObject;
					objectsAndCharacters.Add(objectInstance.transform.position);
				}
				else
				{
					themes[0].collectables[i].objectAmount ++;
				}
			}
		}
	}

	#region GIZMOS
	void OnDrawGizmos()
	{
		Gizmos.color = Color.red;
		Gizmos.DrawSphere(endDist, 5f);

		Gizmos.color = Color.blue;
		Gizmos.DrawSphere(startDist, 5f);
	}
	#endregion
}
