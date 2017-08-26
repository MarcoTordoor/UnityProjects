using System.Collections;
using System.Collections.Generic;
using UnityEngine;
	
public class MainWeapon : Weapon
{
	private GameController gameController;

	void Start()
	{
		GameObject gameControllerObject = GameObject.FindWithTag ("GameController");
		if (gameControllerObject != null) {
			gameController = gameControllerObject.GetComponent<GameController> ();
		}	
		shotSound = GetComponent<AudioSource> ();
	}

	public override void fire()
	{
		if (Time.time > nextFire) {
			nextFire = Time.time + fireRate;
			foreach (var shotSpawn in shotSpawns) {
				//Debug.Log (shotSpawn.position.ToString () + " " + shotSpawn.rotation.ToString ());
				Instantiate (bolt, shotSpawn.position, shotSpawn.rotation);
			}
			shotSound.Play ();
		}
	}

	public void IncreaseFireRate()
	{
		gameController.UpdateWFR (1);
		fireRate /= 2;
		Invoke ("resetFireRate", 35);
	}

	private void resetFireRate()
	{
		fireRate *= 2;
		gameController.UpdateWFR (-1);
	}

	public void upgrade(int amount)
	{
		//Debug.Log ("performing upgrade of main weapon");

		// get a spawn and duplicate it to add an extra gun to the main weapon's gun arsenal
		Transform shotSpawn = shotSpawns [0];

		// Don't get a spawn but create one from scratch and attach it to the mainweapon
		// then we don't have to link a spawn in unity and we can "upgrade" our weapon by 1
		// to get the default weapon with 1 bolt
		// move shipOffset and spacing vars to weapon properties so they can be set from unity

		Transform newShotSpawn = Instantiate (shotSpawn, shotSpawn.position, shotSpawn.rotation, shotSpawn.parent);
		shotSpawns.Add (newShotSpawn);

		// loop through shotspawns and set them on the correct position and angle based on total shotspawns present
		// we define our avaiable area as half a circle in front of the ship
		int totalGuns = shotSpawns.Count;
		List<Vector3> vec3 = MainWeapon.CalculateWeaponArc (totalGuns);
		for (int i = 0; i < totalGuns; i++) {
			//Debug.Log ("shotspawn pos " + shotSpawns [i].position + " changing to " + vec3 [i]);
			shotSpawns [i].localPosition = vec3 [i];
		}

	}

	// Calculate the positions and rotations of the weapons based on the total 
	private static List<Vector3> CalculateWeaponArc(int points)
	{
		float shipOffset = 1.25f;
		float horizontalSpacing = 0.3f;
		float verticalSpacing = 0.3f;

		//Vector3[] vec3 = new Vector3[points];
		List<Vector3> vec3 = new List<Vector3>();

		int middle;
		if (points % 2 == 0) {
			// if we have an even total we cannot use [0;0;1.25] as a point
			// the space between the shotspawns should always be horizontalSpacing on the x-axis
			// and verticalSpacing on the z-axis
			middle = points / 2;
		} else {
			// we have an uneven total so we can use [0;0;1.25] as a valid point
			vec3.Add (new Vector3(0.0f, 0.0f, shipOffset));
			middle = (points - 1) / 2;
		}

		for (float i = 1; i <= middle; i++) {
			// positive side (right side of the ship)
			vec3.Add(new Vector3 (i * horizontalSpacing, 0.0f, shipOffset - (i * verticalSpacing)));
			// negative side (left side of the ship)
			vec3.Add (new Vector3 (-(i * horizontalSpacing), 0.0f, shipOffset - (i * verticalSpacing)));				
		}
		//vec3.ForEach (x => Debug.Log (x.ToString ()));
		return vec3;
	}

}
