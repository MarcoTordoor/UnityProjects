using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpgradeByContact : MonoBehaviour {

	private GameController gameController;

	void Start()
	{
		GameObject gameControllerObject = GameObject.FindWithTag ("GameController");
		if (gameControllerObject != null) {
			gameController = gameControllerObject.GetComponent<GameController> ();
		}		
	}

	void OnTriggerEnter(Collider other)
	{
		// Only a player can pick up buffs, everything else gets ignored
		if (other.gameObject.CompareTag ("Player")) {
			// Perform an upgrade to the ship based on which upgrade we picked up!	
			PlayerController pc = other.gameObject.GetComponent<PlayerController> ();
			if (gameObject.CompareTag ("Common Weapon Upgrade"))  {
				gameController.UpdateWUL (1);
				pc.UpgradeMainWeapon (1);
			}
			if (gameObject.CompareTag ("Shield buff"))  {
				pc.ActivateShield ();
			}
			if (gameObject.CompareTag ("Firerate buff"))  {
				pc.IncreaseFireRate ();
			}
			Destroy (gameObject);
		}
	}
}
