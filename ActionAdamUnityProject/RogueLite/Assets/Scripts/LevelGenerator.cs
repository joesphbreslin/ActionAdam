using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelGenerator : MonoBehaviour 
{
	public GameObject playerPrefab;
	public GameObject EnemyPrefab;

	public GameObject[] tiles;
	public GameObject wall;
	public GameObject wallBoarder;

	public List<Vector3> createdTiles;

	public int tileAmount;
	public int tileSize;

	public float waitTime;

	public float chanceUp;
	public float chanceRight;
	public float chanceDown;

	public int seed;

	public float minY = 999999999;
	public float maxY = 0;
	public float minX = 0;
	public float maxX = 999999999;

	public float xAmount;
	public float yAmount;

	public float extraWallX;
	public float extraWallY;


	public List<GameObject> createdWalls;
	public List<GameObject> createdBoarderWalls;

	public Text seedText;
	public Text levelSizeText;

	public Transform parent;

	public int startTileAmount;

	// Use this for initialization
	void Start () 
	{
		parent = new GameObject().transform;
		parent.name = "LevelParent";

		//StartCoroutine("GenerateLevel");

		//GenerateLevel();
	}
	
	// Update is called once per frame
	void Update () 
	{
		seedText.text = "Seed: " + seed.ToString();
		levelSizeText.text = "Tile Amount: " + tileAmount.ToString();

		Distance();
	}

	IEnumerator GenerateLevel()
	{
		for(int i = 0; i < tileAmount; i ++)
		{
			float dir = Random.Range(0f, 1f);
			int tile = Random.Range(0, tiles.Length);

			CreateTile(tile);
			CallMoveGenerator(dir);

			yield return new WaitForSeconds(waitTime);
				
			if(i == tileAmount - 1)
			{
				Finish();
				CreateWallsBoarder();
				SpawnPlayer();
			}

//			if(i == tileAmount - 1)
//			{
//				foreach(Vector3 t in createdTiles)
//				{
//					Vector3 posUp = new Vector3(t.x, t.y + tileSize, t.z);
//
//					if(!createdWalls.Contains(posUp) && !createdTiles.Contains(posUp))
//					{
//						CreateWall(posUp);
//					}
//
//					Vector3 posDown = new Vector3(t.x, t.y - tileSize, t.z);
//
//					if(!createdWalls.Contains(posDown) && !createdTiles.Contains(posDown))
//					{
//						CreateWall(posDown);
//					}
//
//					Vector3 posLeft = new Vector3(t.x - tileSize, t.y , t.z);
//
//					if(!createdWalls.Contains(posLeft) && !createdTiles.Contains(posLeft))
//					{
//						CreateWall(posLeft);
//					}
//
//					Vector3 posRight = new Vector3(t.x + tileSize, t.y, t.z);
//
//					if(!createdWalls.Contains(posRight) && !createdTiles.Contains(posRight))
//					{
//						CreateWall(posRight);
//					}
//				}
//			}
		}
	}

	void CreateWall(Vector3 pos)
	{
		GameObject wallInstance;
		wallInstance = Instantiate(wall, pos, transform.rotation) as GameObject;
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
			tileInstance = Instantiate(tiles[tileIndex], transform.position, transform.rotation) as GameObject;
			createdTiles.Add(tileInstance.transform.position);

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
					wallInstance = Instantiate(wall, new Vector3((minX - (extraWallX * tileSize) / 2 ) + (x * tileSize), (minY -(extraWallY * tileSize) / 2) + (y * tileSize)), transform.rotation) as GameObject;

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
				wallBoarderInstance = Instantiate(wallBoarder, wall.transform.position, Quaternion.identity) as GameObject;

				wallBoarderInstance.transform.parent = parent;

				createdBoarderWalls.Add(wallBoarderInstance);

				Destroy(wall.gameObject);
			}
		}
	}

	public Vector3 startDist;
	public Vector3 endDist;

	void Distance()
	{
		//Vector3 startDist;
	
		//Vector3 endDist;
		for(int i = 0; i < createdTiles.Capacity - 1; i ++)
		{
			startDist = createdTiles[i];

			for(int j = i + 1; j < createdTiles.Capacity - 1; i ++)
			{
				endDist = createdTiles[j];

				if(Vector3.Distance(createdTiles[i], endDist) > Vector3.Distance(startDist, endDist))
				{
					startDist = createdTiles[i];
				}

				if(Vector3.Distance(startDist, createdTiles[j]) > Vector3.Distance(startDist, endDist))
				{
					endDist = createdTiles[j];
				}

			}
		}

		foreach(Transform child in parent)
		{
			if(child.position == startDist || child.position == endDist)
			{
				SpriteRenderer rend;
				rend = child.GetComponent<SpriteRenderer>();

				rend.color = Color.red;
			}
		}
	}

	public void SpawnPlayer()
	{
		int randomSpawnPOS;
		Random.seed = Random.Range(0, 9999);

		randomSpawnPOS = Random.Range(0, createdTiles.Capacity - 1);

		GameObject playerInstance;

		playerInstance = Instantiate(playerPrefab, this.GetComponent<LevelGenerator>().createdTiles[randomSpawnPOS], Quaternion.identity) as GameObject;
	}

	#region All UI Methods

	public void StartLevelGen()
	{
		startTileAmount = tileAmount;

		Random.seed = seed;
		StartCoroutine(GenerateLevel());
	}

	public void SetSeed(string newSeed)
	{
		seed = int.Parse(newSeed);
	}

	public void SetLevelSize(string newSize)
	{
		tileAmount = int.Parse(newSize);
	}

	public void RandomSeed()
	{
		seed = Random.Range(0, 1000000);
	}

	public void ClearLevel()
	{
		for(int i = 0; i < parent.childCount; i ++)
		{
			Destroy(parent.GetChild(i).gameObject);
			createdTiles.Clear();
			createdWalls.Clear();
			createdBoarderWalls.Clear();
		}

		tileAmount = startTileAmount;
		xAmount = 0;
		yAmount = 0;

		maxX = 0;
		maxY = 0;

		minX = 1e+25f;
		minY = 1e+25f;

		this.gameObject.transform.position = new Vector3(0f, 0f, 0f);
	}

	#endregion
}
