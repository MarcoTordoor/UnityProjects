using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyByTime : MonoBehaviour {

	public float lifetime;

	// Use this for initialization
	void Start () {
		//Debug.Log("time destroying: this: " + gameObject.ToString());
		Destroy (gameObject, lifetime);
	}
}
