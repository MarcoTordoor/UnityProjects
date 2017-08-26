using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Boundary
{
	public float xmin, xmax, zmin, zmax;
}

public class PlayerController : MonoBehaviour 
{
	public float speed;
	public float tilt;
	public Boundary boundary;

	public MainWeapon mainWeapon;
	public SecondaryWeapon secondaryWeapon;

	public Shield shield; 

	private Rigidbody rb;

	void Start()
	{
		rb = GetComponent<Rigidbody> ();
	}

	void Update()
	{
		if (Input.GetButton ("Fire1")) {
			mainWeapon.fire ();
		}

		if (Input.GetButton ("Fire2")) {
			secondaryWeapon.fire ();
		}
	}

	void FixedUpdate()
	{
		float moveHorizontal = Input.GetAxis ("Horizontal");
		float moveVertical = Input.GetAxis ("Vertical");

		rb.velocity = new Vector3 (moveHorizontal, 0.0f, moveVertical) * speed;

		rb.position = new Vector3 (
			Mathf.Clamp(rb.position.x, boundary.xmin, boundary.xmax), 
			0.0f, 
			Mathf.Clamp(rb.position.z, boundary.zmin, boundary.zmax)
		);

		rb.rotation = Quaternion.Euler(0.0f, 0.0f, rb.velocity.x * -tilt);
	}

	public void UpgradeMainWeapon(int amount)
	{
		mainWeapon.upgrade (amount);
	}

	public void IncreaseFireRate()
	{
		mainWeapon.IncreaseFireRate ();
	}

	// Give a temporary shield to the player
	public void ActivateShield()
	{
		// add an instance of our shield object to the player
		Instantiate (shield, gameObject.transform.position, gameObject.transform.rotation, gameObject.transform);		
	}
}
