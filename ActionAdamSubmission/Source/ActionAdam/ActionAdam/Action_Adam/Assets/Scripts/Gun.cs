using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using FMOD;
using FMODUnity;

public class Gun : MonoBehaviour 
{
	#region Variables
	#region Gun Type
	public enum CharacterType{Player, AI};
	public CharacterType myCharacterType;

	public enum GunType {Single_Shot, Burst_Shot, Fully_Auto};
	public GunType myGunType;
    bool MiniGunOn = false;
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
	public GameObject particleEffect;
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
	#endregion

	[Space(10)]

	#region Debugs
	[Header("Debugs")]
	public bool debug;
	public Color debugColor;
	public float debugFirePointSize;
    #endregion

    #endregion
    StudioEventEmitter SE;
	// Use this for initialization
	void Start () 
	{
        SE = GetComponent<StudioEventEmitter>();
		fireRate = roundsPerMinute / 60;
       
		startShotTime = 1 / fireRate;
      
        //ui


    }

    void Awake()
    {
		if(myCharacterType == CharacterType.Player)
		{
			firingModeText1 = GameObject.Find("PlayerGunText").GetComponent<TextMeshProUGUI>();
			ammoAmountText1 = GameObject.Find("PlayerGunAmmoAmountText").GetComponent<TextMeshProUGUI>();
		}
	}

    // Update is called once per frame
    void FixedUpdate () 
	{
		UpdateUI();

		Reload();
		Fire();
	}

    IEnumerator PlayAudio()
    {
        SE.Play();
        yield return new WaitForSeconds(0.5f);
    }

	void SpawnBullet(Transform _firePoint)
	{
        //one.OneShot();
		Rigidbody2D bulletPrefabInstance;
		bulletPrefabInstance = Instantiate(bulletPrefab, _firePoint.position, Quaternion.identity) as Rigidbody2D;

		clipAmonut -= 1;

		Camera.main.GetComponent<CameraShake>().shakeSwitch = true;

        //Sound_Effect
       
        //Particle_System

        this.transform.parent.Translate(-_firePoint.forward * recoil);

		bulletPrefabInstance.GetComponent<Bullet>().DamageValue = damage;
		bulletPrefabInstance.GetComponent<Bullet>().characterName = this.transform.parent.gameObject.name;
		bulletPrefabInstance.GetComponent<Bullet>().characterTag = this.transform.parent.gameObject.tag;

		Rigidbody2D rb = bulletPrefabInstance.GetComponent<Rigidbody2D>();
		rb.AddForce(_firePoint.forward * force);
	}

	void UpdateUI()
	{
		ammoAmountText1.SetText(clipAmonut.ToString() + " / " + ammoAmount.ToString());
		firingModeText1.SetText(myGunType.ToString());
	}

	void MuzzleFlash(GameObject _muzzleFlash)
	{
		_muzzleFlash.SetActive(true);
        if (this.gameObject.name != "MiniGun")
        {
            StartCoroutine(PlayAudio());
        }

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
              
				shotTime -= Time.fixedDeltaTime;

				if(shotTime <= 0)
				{
					MuzzleFlash(defaultMuzzleFlash);
            
                    SpawnBullet(defaultFirePoint);                
                    shotTime = startShotTime;
				}
            }
         

            if (gameObject.name == "MiniGun")
            {
                if (Input.GetMouseButton(0) && clipAmonut > 0)
                {
                    if (!MiniGunOn)
                    {
                        SE.SetParameter("mini_gun_on_off", 0);
                        SE.Play();
                        MiniGunOn = true;
                    }
                 
                   
                    shotTime -= Time.fixedDeltaTime;

                    if (shotTime <= 0)
                    {
                        MuzzleFlash(defaultMuzzleFlash);

                        SpawnBullet(defaultFirePoint);
                        shotTime = startShotTime;
                    }
                }
                else
                {
                   
                    SE.SetParameter("mini_gun_on_off", 1);
                    MiniGunOn = false;


                }
             
            }
            else
            {
                if (Input.GetMouseButton(0) && clipAmonut > 0)
                {

                    shotTime -= Time.fixedDeltaTime;

                    if (shotTime <= 0)
                    {
                        MuzzleFlash(defaultMuzzleFlash);

                        SpawnBullet(defaultFirePoint);
                        shotTime = startShotTime;
                    }
                }
            }

            if (Input.GetMouseButtonUp(0) && myCharacterType == CharacterType.Player)
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
               
                foreach (GameObject flash in muzzleFlashes)
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

	void OnDrawGizmos()
	{
		if(debug)
		{
			foreach(Transform point in firePoints)
			{
				Gizmos.color = debugColor;
				Gizmos.DrawWireSphere(point.position, debugFirePointSize);


				UnityEngine.Debug.DrawRay(point.position, point.forward * debugFirePointSize, Color.red);
			}
		}
	}
}



