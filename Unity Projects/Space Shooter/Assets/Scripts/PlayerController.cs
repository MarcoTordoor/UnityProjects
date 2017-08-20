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

	// Normal shots
	public GameObject shot;
	public Transform[] shotSpawns;
	public float fireRate;
	private float nextCommonFire;

	// Special arced shot around the ship
	public GameObject specialShot;
	public Transform specialShotSpawn;
	public float fireRateSpecial;
	private float nextSpecialFire;

	private Rigidbody rb;
	private AudioSource shotSound;

	void Start()
	{
		rb = GetComponent<Rigidbody> ();
		shotSound = GetComponent<AudioSource> ();
	}

	void Update()
	{
		if (Input.GetButton ("Fire1") && Time.time > nextCommonFire) {
			nextCommonFire = Time.time + fireRate;
			foreach (var shotSpawn in shotSpawns) {
				Instantiate (shot, shotSpawn.position, shotSpawn.rotation);
			}
			shotSound.Play ();
		}

		if (Input.GetButton ("Fire2") && Time.time > nextSpecialFire) {
			nextSpecialFire = Time.time + fireRateSpecial;
			// arc 12 shots around the ships center
			for (int i = 0; i < 12; i++) {
				Instantiate (specialShot, specialShotSpawn.position, specialShotSpawn.rotation);
				specialShotSpawn.Rotate(0.0f, 30.0f, 0.0f);
			}
			shotSound.Play ();
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

}
