using System;
using UnityEngine;
using System.Collections;
using Random = UnityEngine.Random;

[RequireComponent(typeof(AudioSource))]

public class WeaponLauncher : WeaponBase
{
	public Transform[] MissileOuter;
	public GameObject Missile;
	public float FireRate = 0.1f;
	public float Spread = 1;
	public float ForceShoot = 8000;
	public int NumBullet = 1;
	public int Ammo = 10;
	public int AmmoMax = 10;
	public bool InfinityAmmo = false;
	public float ReloadTime = 1;
	public bool ShowHUD = true;
	public Texture2D TargetLockOnTexture;
	public Texture2D TargetLockedTexture;
	public float DistanceLock = 200;
	public float TimeToLock = 2;
	public float AimDirection = 0.8f;
	public bool Seeker;
	public GameObject Shell;
	public float ShellLifeTime = 4;
	public Transform[] ShellOuter;
	public int ShellOutForce = 300;
	public GameObject Muzzle;
	public float MuzzleLifeTime = 2;
	public AudioClip[] SoundGun;
	public AudioClip SoundReloading;
	public AudioClip SoundReloaded;
	private float timetolockcount = 0;
	private float nextFireTime = 0;
	private GameObject target;
	private Vector3 torqueTemp;
	private float reloadTimeTemp;
	private AudioSource audio;
	[HideInInspector]
	public bool Reloading;
	[HideInInspector]
	public float ReloadingProcess;
	
	private void Start ()
	{
		//PlayerPrefs.SetInt ("ammos",20);
	

		Ammo= PlayerPrefs.GetInt ("ammos");
		if (!Owner) 
			Owner = this.transform.root.gameObject;
		
		if (!audio) {
			audio = this.GetComponent<AudioSource> ();
			if (!audio) {
				this.gameObject.AddComponent<AudioSource>();	
			}
		}

	}

	private void Update ()
	{
		if (TorqueObject) {
			TorqueObject.transform.Rotate (torqueTemp * Time.deltaTime);
			torqueTemp = Vector3.Lerp (torqueTemp, Vector3.zero, Time.deltaTime);
		}
		if (Seeker) {
			for (int t=0; t<TargetTag.Length; t++) {
				if (GameObject.FindGameObjectsWithTag (TargetTag [t]).Length > 0) {
					GameObject[] objs = GameObject.FindGameObjectsWithTag (TargetTag [t]);
					float distance = int.MaxValue;
					for (int i = 0; i < objs.Length; i++) {
						if (objs [i]) {
							Vector3 dir = (objs [i].transform.position - transform.position).normalized;
							float direction = Vector3.Dot (dir, transform.forward);
							float dis = Vector3.Distance (objs [i].transform.position, transform.position);
							if (direction >= AimDirection) {
								if (DistanceLock > dis) {
									if (distance > dis) {
										if (timetolockcount + TimeToLock < Time.time) {	
											distance = dis;
											target = objs [i];
										}
									}
								}
							}
						}
					}
				}
			}
		}
		if (target) {
			float targetdistance = Vector3.Distance (transform.position, target.transform.position);
			Vector3 dir = (target.transform.position - transform.position).normalized;
			float direction = Vector3.Dot (dir, transform.forward);

			if (targetdistance > DistanceLock || direction <= AimDirection) {
				Unlock ();
			}
		}
		
		if (Reloading) {
			ReloadingProcess = ((1 / ReloadTime) * (reloadTimeTemp + ReloadTime - Time.time));
			if (Time.time >= reloadTimeTemp + ReloadTime) {
				Reloading = false;
				if (SoundReloaded) {
					if (audio) {
						audio.PlayOneShot (SoundReloaded);
					}
				}
				Ammo = AmmoMax;
			}
		} else {
			if (Ammo <= 0) {
//				Unlock ();
//				Reloading = true;
//				reloadTimeTemp = Time.time;
//				
//				if (SoundReloading) {
//					if (audio) {
//						audio.PlayOneShot (SoundReloading);
//					}
//				}
			}
		}

	}

	private void DrawTargetLockon (Transform aimtarget, bool locked)
	{
		if (!ShowHUD)
			return;
		
		if (Camera.current) {
			Vector3 dir = (aimtarget.position - Camera.current.GetComponent<Camera>().transform.position).normalized;
			float direction = Vector3.Dot (dir, Camera.current.GetComponent<Camera>().transform.forward);
			if (direction > 0.5f) {
				Vector3 screenPos = Camera.current.GetComponent<Camera>().WorldToScreenPoint (aimtarget.transform.position);
				float distance = Vector3.Distance (transform.position, aimtarget.transform.position);
				if (locked) {
					if (TargetLockedTexture)
						GUI.DrawTexture (new Rect (screenPos.x - TargetLockedTexture.width / 2, Screen.height - screenPos.y - TargetLockedTexture.height / 2, TargetLockedTexture.width, TargetLockedTexture.height), TargetLockedTexture);
					GUI.Label (new Rect (screenPos.x + 40, Screen.height - screenPos.y, 200, 30), aimtarget.name + " " + Mathf.Floor (distance) + "m.");
				} else {
					if (TargetLockOnTexture)
						GUI.DrawTexture (new Rect (screenPos.x - TargetLockOnTexture.width / 2, Screen.height - screenPos.y - TargetLockOnTexture.height / 2, TargetLockOnTexture.width, TargetLockOnTexture.height), TargetLockOnTexture);
				}
            	
			}
		} else {
			//Debug.Log("Can't Find camera");
		}
	}

	private void OnGUI ()
	{
		if (Seeker) {
           
			if (target) {
				DrawTargetLockon (target.transform, true);
			}
            
			for (int t=0; t<TargetTag.Length; t++) {
				if (GameObject.FindGameObjectsWithTag (TargetTag [t]).Length > 0) {
					GameObject[] objs = GameObject.FindGameObjectsWithTag (TargetTag [t]);
					for (int i = 0; i < objs.Length; i++) {
						if (objs [i]) {
							Vector3 dir = (objs [i].transform.position - transform.position).normalized;
							float direction = Vector3.Dot (dir, transform.forward);
							if (direction >= AimDirection) {
								float dis = Vector3.Distance (objs [i].transform.position, transform.position);
								if (DistanceLock > dis) {
									DrawTargetLockon (objs [i].transform, false);
								}
							}
						}
					}
				}
			}
		}

	}

	private void Unlock ()
	{
		timetolockcount = Time.time;
		target = null;
	}
	
	private int currentOuter = 0;


	void hideMuzzle()
	{
		Muzzle.SetActive (false);
	}
	public void Shoot ()
	{
		if (InfinityAmmo) {
			Ammo = 1;	
		}
		//PlayerPrefs.SetInt ("ammos", 1000);
		Ammo = PlayerPrefs.GetInt ("ammos");
//		print (Ammo);
		if (Ammo > 0) {
			PlayerPrefs.SetInt("ammos",(PlayerPrefs.GetInt ("ammos")-1));
			if (Time.time > nextFireTime + FireRate) {
				nextFireTime = Time.time;
				torqueTemp = TorqueSpeedAxis;
				Ammo -= 1;
				Vector3 missileposition = this.transform.position;
				Quaternion missilerotate = this.transform.rotation;
				if (MissileOuter.Length > 0) {
					missilerotate = MissileOuter [currentOuter].transform.rotation;	
					missileposition = MissileOuter [currentOuter].transform.position;	
				}

				if (MissileOuter.Length > 0) {
					currentOuter += 1;
					if (currentOuter >= MissileOuter.Length)
						currentOuter = 0;
				}
			
//				if (Muzzle) {
//					
//					GameObject muzzle = (GameObject)GameObject.Instantiate (Muzzle, missileposition, missilerotate);
//					muzzle.transform.parent = this.transform;
//					GameObject.Destroy (muzzle, MuzzleLifeTime);
//					if (MissileOuter.Length > 0) {
//						muzzle.transform.parent = MissileOuter [currentOuter].transform;
//					}
//				}

				CancelInvoke("hideMuzzle");
				if (Muzzle) {
					Muzzle.SetActive (true);
					Muzzle.transform.position=missileposition;
					Muzzle.transform.rotation=missilerotate;
					//GameObject muzzle = (GameObject)GameObject.Instantiate (Muzzle, missileposition, missilerotate);
					Invoke("hideMuzzle",MuzzleLifeTime);
					//muzzle.transform.parent = this.transform;
					//GameObject.Destroy (muzzle, MuzzleLifeTime);
					if (MissileOuter.Length > 0) {
						Muzzle.transform.parent = MissileOuter [currentOuter].transform;
					}
				}
				for (int i = 0; i < NumBullet; i++) {
					if (Missile) {
						Vector3 spread = new Vector3 (Random.Range (-Spread, Spread), Random.Range (-Spread, Spread), Random.Range (-Spread, Spread)) / 100;
						Vector3 direction = this.transform.forward + spread;
					
					
					
						GameObject bullet = (GameObject)Instantiate (Missile, missileposition, missilerotate);
					
						if (bullet.GetComponent<DamageBase> ()) {
							bullet.GetComponent<DamageBase> ().Owner = Owner;
						}
						if (bullet.GetComponent<WeaponBase> ()) {
							bullet.GetComponent<WeaponBase> ().Owner = Owner;
							bullet.GetComponent<WeaponBase> ().Target = target;
						}
						bullet.transform.forward = direction;
						if (RigidbodyProjectile) {
							if (bullet.GetComponent<Rigidbody>()) {
								if (Owner != null && Owner.GetComponent<Rigidbody>()) {
									bullet.GetComponent<Rigidbody>().velocity = Owner.GetComponent<Rigidbody>().velocity;
								}
								bullet.GetComponent<Rigidbody>().AddForce (direction * ForceShoot);	
							}
						}
					
					}
				}
				if (Shell) {
					Transform shelloutpos = this.transform;
					if (ShellOuter.Length > 0) {
						shelloutpos = ShellOuter [currentOuter];
					}
				
					GameObject shell = (GameObject)Instantiate (Shell, shelloutpos.position, Random.rotation);
					GameObject.Destroy (shell.gameObject, ShellLifeTime);
					if (shell.GetComponent<Rigidbody>()) {
						shell.GetComponent<Rigidbody>().AddForce (shelloutpos.forward * ShellOutForce);
					}
				}
				
				if (SoundGun.Length > 0) {
					if (audio && PlayerPrefs.GetInt ("sound", 1) == 1) {
						audio.PlayOneShot (SoundGun [Random.Range (0, SoundGun.Length)]);
					}
				}
			
				nextFireTime += FireRate;
			}
		} 
		
	}

}
