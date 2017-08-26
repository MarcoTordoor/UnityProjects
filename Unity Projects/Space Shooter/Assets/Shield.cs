using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shield : MonoBehaviour {

	public int lifetime;
	public int health;

	void Start () {
		// shields only last for a while
		Destroy (gameObject, lifetime);
	}

}
