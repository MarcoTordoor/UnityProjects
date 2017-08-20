using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Weapon : MonoBehaviour
{
	public GameObject bolt;
	public Transform[] shotSpawns;

	public float fireRate;

	protected AudioSource shotSound;
	protected float nextFire { get; set; }

	public abstract void fire();
}