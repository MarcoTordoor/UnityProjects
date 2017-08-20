using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SecondaryWeapon : Weapon
{

	void Start()
	{
		shotSound = GetComponent<AudioSource> ();
	}

	public override void fire()
	{
		if (Time.time > nextFire) {
			nextFire = Time.time + fireRate;
			// arc 12 shots around the ships center
			foreach (var shotSpawn in shotSpawns) {
				for (int i = 0; i < 12; i++) {
					Instantiate (bolt, shotSpawn.position, shotSpawn.rotation);
					shotSpawn.Rotate (0.0f, 30.0f, 0.0f);
				}
			}
			shotSound.Play ();
		}	
	}
}