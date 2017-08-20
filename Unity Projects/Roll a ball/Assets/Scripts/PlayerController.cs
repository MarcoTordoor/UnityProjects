using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour {

	public float speed;
	public Text collected_pickups_text;
	public Text winText;

	private Rigidbody rb;
	private int collected_Pickups;

	void Start ()
	{
		rb = GetComponent<Rigidbody> ();
		collected_Pickups = 0;
		setCollectedPickupText ();
		winText.text = "";
	}

	void FixedUpdate ()
	{
		float moveHorizontal = Input.GetAxis ("Horizontal");
		float moveVertical = Input.GetAxis ("Vertical");

		Vector3 movement = new Vector3 (moveHorizontal, 0, moveVertical);

		rb.AddForce (movement * speed);

	}
		
	void OnTriggerEnter(Collider other) 
	{
		if (other.gameObject.CompareTag ("Pick Up")) {
			other.gameObject.SetActive (false);
			collected_Pickups++;
			setCollectedPickupText ();
		}
	}

	void setCollectedPickupText()
	{
		collected_pickups_text.text = "Collected " + collected_Pickups.ToString () + " / 12 pick ups";
		if (collected_Pickups >= 12) {
			winText.text = "We have a winner!!";
		}
	}
}
	