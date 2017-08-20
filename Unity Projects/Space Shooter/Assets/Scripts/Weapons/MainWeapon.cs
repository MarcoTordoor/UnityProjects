using System.Collections;
using System.Collections.Generic;
using UnityEngine;
	
public class MainWeapon : Weapon
{

	void Start()
	{
		shotSound = GetComponent<AudioSource> ();
	}

	public override void fire()
	{
		if (Time.time > nextFire) {
			nextFire = Time.time + fireRate;
			foreach (var shotSpawn in shotSpawns) {
				Instantiate (bolt, shotSpawn.position, shotSpawn.rotation);
			}
			shotSound.Play ();
		}
	}

}
