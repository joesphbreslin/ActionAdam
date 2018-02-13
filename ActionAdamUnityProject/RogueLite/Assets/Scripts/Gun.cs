using System.Collections;
using System.Collections.Generic;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Gun : MonoBehaviour 
{
	#region Variables
	#region Gun Type
	public enum CharacterType{Player, AI};
	public CharacterType myCharacterType;

	public enum GunType {Single_Shot, Burst_Shot, Fully_Auto};
	public GunType myGunType;
	#endregion

	#region Ammo
	[Header("Ammo")]
	public int ammoAmount;
	public int clipAmonut;
	public int clipCapactiy;
	#endregion

	[Space(10)]

	#region Fire Rates
	[Header("Fire Rates")]
	public float roundsPerMinute;
	public float fireRate;
	public float shotTime;
	private float startShotTime;
	#endregion

	[Space(10)]

	#region Bullet and FirePoints
	[Header("Bullet and FirePoints")]
	public Rigidbody2D bulletPrefab;
	public Transform defaultFirePoint;
	public GameObject defaultMuzzleFlash;
	public Transform[] firePoints;
	public GameObject[] muzzleFlashes;
	#endregion

	[Space(10)]

	#region Force / Damage / Recoil
	[Header("Force of Bullet and Damage")]
	public float force;
	public int damage;
	public float recoil;
	public GameObject arm;
	#endregion

	[Space(10)]

	#region Gun/Ammo UI Text
	[Header("Gun & Ammo UI Text")]
	public TextMeshProUGUI firingModeText1;
	public TextMeshProUGUI ammoAmountText1;
	public TextMeshProUGUI clipAmountText1;

	private bool updateUI;
	#endregion

	[Space(10)]

	#region Debugs
	[Header("Debugs")]
	public bool debug;
	public Color debugColor;
	public float debugFirePointSize;
	#endregion

	#endregion

	private int index;

	// Use this for initialization
	void Start () 
	{
		fireRate = roundsPerMinute / 60;

		startShotTime = 1 / fireRate;
	}
	
	// Update is called once per frame
	void FixedUpdate () 
	{
		if(updateUI)
		{
			UpdateUI();
		}

		Reload();
		//ChangeFireType();
		Fire();
	}

	void SpawnBullet(Transform _firePoint)
	{
		Rigidbody2D bulletPrefabInstance;
		bulletPrefabInstance = Instantiate(bulletPrefab, _firePoint.position, Quaternion.identity) as Rigidbody2D;

		clipAmonut -= 1;

		Camera.main.GetComponent<CameraShake>().shakeSwitch = true;

		//Sound_Effect
		//Particle_System

		this.transform.parent.Translate(-_firePoint.forward * recoil);

		bulletPrefabInstance.GetComponent<Bullet>().DamageValue = damage;

		Rigidbody2D rb = bulletPrefabInstance.GetComponent<Rigidbody2D>();
		rb.AddForce(_firePoint.forward * force);
	}

	void UpdateUI()
	{
		firingModeText1.SetText("Firing Mode " + myGunType.ToString());
		ammoAmountText1.SetText("Total AMMO " + ammoAmount.ToString());
		clipAmountText1.SetText("Clip AMMO Amount " + clipAmonut.ToString() + " / " + clipCapactiy.ToString()); 
	}

	void MuzzleFlash(GameObject _muzzleFlash)
	{
		_muzzleFlash.SetActive(true);
	}

	void AIReload()
	{
		if(clipAmonut < clipCapactiy)
		{
			int reloadAmmoAmount;

			reloadAmmoAmount = clipCapactiy - clipAmonut;

			if(ammoAmount > reloadAmmoAmount)
			{
				ammoAmount -= reloadAmmoAmount;

				clipAmonut += reloadAmmoAmount;
			}
			else if(ammoAmount < reloadAmmoAmount || clipAmonut == 0)
			{
				clipAmonut += ammoAmount;
				ammoAmount -= ammoAmount;
			}
		}
	}

	public void AIFire()
	{
		if(myCharacterType == CharacterType.AI)
		{
			if(clipAmonut > 0)
			{
				shotTime -= Time.deltaTime;

				if(shotTime <= 0)
				{
					MuzzleFlash(defaultMuzzleFlash);

					SpawnBullet(defaultFirePoint);

					shotTime = startShotTime;
				}
			}
			if(clipAmonut <= 0)
			{
				AIReload();
			}
		}
	}

	void Fire()
	{
		#region FullyAuto Input
		if(myGunType == GunType.Fully_Auto && myCharacterType == CharacterType.Player)
		{
			if(Input.GetMouseButton(0) && clipAmonut > 0)
			{
				shotTime -= Time.deltaTime;

				if(shotTime <= 0)
				{
					MuzzleFlash(defaultMuzzleFlash);

					SpawnBullet(defaultFirePoint);

					shotTime = startShotTime;
				}
			}

			if(Input.GetMouseButtonUp(0) && myCharacterType == CharacterType.Player)
			{
				shotTime = 0;
			}
		}
		#endregion

		#region SingleShot Input
		if(myGunType == GunType.Single_Shot && myCharacterType == CharacterType.Player)
		{
			if(Input.GetMouseButtonDown(0) && clipAmonut > 0)
			{
				MuzzleFlash(defaultMuzzleFlash);

				SpawnBullet(defaultFirePoint);
			}
		}
		#endregion

		#region BurstShot Input
		if(myGunType == GunType.Burst_Shot && myCharacterType == CharacterType.Player)
		{
			if(Input.GetMouseButtonDown(0) && clipAmonut >= firePoints.Length)
			{
				foreach(GameObject flash in muzzleFlashes)
				{
					MuzzleFlash(flash);
				}

				foreach(Transform point in firePoints)
				{
					SpawnBullet(point);
				}
			}
		}
		#endregion
	}

	void Reload()
	{
		if(Input.GetKeyDown(KeyCode.R) && clipAmonut < clipCapactiy && myCharacterType == CharacterType.Player)
		{
			int reloadedAmmoAmount;

			reloadedAmmoAmount = clipCapactiy - clipAmonut;

			if(ammoAmount > reloadedAmmoAmount)
			{
				ammoAmount -= reloadedAmmoAmount;

				clipAmonut += reloadedAmmoAmount;
			}
			else if(ammoAmount < reloadedAmmoAmount || clipAmonut == 0)
			{
				clipAmonut += ammoAmount;
				ammoAmount -= ammoAmount;
			}
		}
	}

	void ChangeFireType()
	{
		if(Input.GetKeyDown(KeyCode.C))
		{
			index += 1;
		}
		if(index >= 3)
		{
			index = 0;
		}

		switch(index)
		{
		case 0:
			myGunType = GunType.Single_Shot;
			break;
		case 1:
			myGunType = GunType.Burst_Shot;
			break;
		case 2:
			myGunType = GunType.Fully_Auto;
			break;
		default:
			myGunType = GunType.Single_Shot;
			break;
		}
	}

	void OnDrawGizmos()
	{
		if(debug)
		{
			foreach(Transform point in firePoints)
			{
				Gizmos.color = debugColor;
				Gizmos.DrawWireSphere(point.position, debugFirePointSize);


				Debug.DrawRay(point.position, point.forward * debugFirePointSize, Color.red);
			}
		}
	}
}



