using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyByContact : MonoBehaviour {

	public GameObject explosion;
	public GameObject playerExplosion;
	public int scoreValue;

	private GameController gameController;

	void Start()
	{
		GameObject gameControllerObject = GameObject.FindWithTag ("GameController");
		if (gameControllerObject != null) {
			gameController = gameControllerObject.GetComponent<GameController> ();
		}
		if (gameController == null) {
			Debug.Log ("Cannot find 'GameController' script");
		}
	}

	void OnTriggerEnter(Collider other)
	{
		//Debug.Log("contact destroying: this: " + gameObject.name + " other: " + other.name);
		if (other.CompareTag ("Boundary") || other.CompareTag ("Enemy") || other.CompareTag ("Common Weapon Upgrade") || gameObject.CompareTag ("Shield buff") || gameObject.CompareTag ("Firerate buff")) {
			return;
		}

		if (explosion != null) {
			Instantiate (explosion, transform.position, transform.rotation);
		}

		if (other.CompareTag ("Player")) {
			Instantiate (playerExplosion, other.transform.position, other.transform.rotation);
			gameController.GameOver ();
		}
			
		gameController.AddScore (scoreValue);
		Destroy (other.gameObject);
		Destroy (gameObject);
	}
}
