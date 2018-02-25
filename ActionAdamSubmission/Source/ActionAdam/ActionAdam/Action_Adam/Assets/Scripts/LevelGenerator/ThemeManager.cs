using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class ThemeManager 
{
	public string themeName;

	#region Environemnt
	[Header("Randomised Environment Prefabs")]

	public Environent[] environments;

	[System.Serializable]
	public class Environent
	{
		public string environmentName;
		public int amount;
		public GameObject[] environmentPrefabs;
	}
	#endregion

	[Space(1)]

	#region Singleton Objects
	[Header("Singleton Prefabs")]

	public Singleton[] singletons;

	[System.Serializable]
	public class Singleton
	{
		public string singletonName;
		public GameObject singletonPrefab;
	}
	#endregion

	[Space(1)]

	#region Enemies
	[Header("Enemies")]

	public Enemy[] enemies;

	[System.Serializable]
	public class Enemy
	{
		public string enemyName;
		public int enemyAmount;
		public GameObject enemyPrefab;
	}
	#endregion

	[Space(1)]

	#region objects
	[Header("Objects")]

	public Collectable[] collectables;

	[System.Serializable]
	public class Collectable
	{
		public string objectName;
		public int objectAmount;
		public GameObject objectPrefab;
	}
	#endregion

}
