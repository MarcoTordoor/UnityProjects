﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour {

	public GameObject[] hazards;
	public GameObject[] buffs;
	public Vector3 spawnValues;
	public int hazardCount;
	public int buffCount;
	public float spawnWait;
	public float buffWait;
	public float startWait;
	public float waveWait;
	public float enemySpeedIncrease;

	public GUIText scoreText;
	public GUIText waveText;
	public GUIText restartText;
	public GUIText gameOverText;
	public GUIText wulText;
	public GUIText wfrText;

	private bool gameOver;
	private bool restart;
	private int score;
	private int wave;
	private int wul;
	private int wfr;
	// used to control the speed of the astroids and enemy ships 
	private float enemySpeed;
	private int buffTotal;

	// Use this for initialization
	void Start () 
	{
		gameOver = false;
		restart = false;
		restartText.text = "";
		gameOverText.text = "";
		score = 0;
		wave = 1;
		wul = 1;
		wfr = 1;
		enemySpeed = -5.0f;
		buffTotal = 0;
		UpdateScore ();
		UpdateWave ();
		UpdateWULText ();
		UpdateWFRText ();
		StartCoroutine(SpawnWaves ());
		StartCoroutine(BuffWaves ());
	}
		
	void Update()
	{
		if (restart) {
			if (Input.GetKeyDown (KeyCode.R)) {
				// new way to load a scene, there is an optional 2nd param to load additive
				// default all open scenes are closed before opening the one provider as first param
				SceneManager.LoadScene (SceneManager.GetActiveScene ().name);
				// old and obsolete way to loading a level/scene
				//Application.LoadLevel (Application.loadedLevel);
			}
		}
	}

	IEnumerator SpawnWaves()
	{
		yield return new WaitForSeconds (startWait);
		while (true) {
			for (int i = 0; i < hazardCount; i++) {
				GameObject hazard = hazards [Random.Range (0, hazards.Length)];
				Vector3 spawnPosition = new Vector3 (Random.Range (-spawnValues.x, spawnValues.x), spawnValues.y, spawnValues.z);
				Quaternion spawnRotation = Quaternion.identity;
				Instantiate (hazard, spawnPosition, spawnRotation);
				// set speed of the enemy
				Mover m = hazard.GetComponent<Mover> ();
				m.speed = enemySpeed;
				yield return new WaitForSeconds (spawnWait);
			}
			// after each wave we increase the enemies speed, negative because we have to go down
			enemySpeed += -enemySpeedIncrease;
			yield return new WaitForSeconds (waveWait);
			wave++;
			UpdateWave ();

			if (gameOver) {
				restartText.text = "Press 'R' to restart!";
				restart = true;
				break;
			}
		}
	}

	IEnumerator BuffWaves()
	{
		yield return new WaitForSeconds (startWait);
		//Debug.Log ("init");
		while (true){
			for (int i = 0; i < buffCount; i++) {
				GameObject buff = buffs [Random.Range (0, buffs.Length)];
				//GameObject buff = buffs [2];
				Vector3 spawnPosition = new Vector3 (Random.Range (-spawnValues.x, spawnValues.x), spawnValues.y, spawnValues.z);
				Quaternion spawnRotation = Quaternion.identity;
				Instantiate (buff, spawnPosition, spawnRotation);
				yield return new WaitForSeconds (3);
			}
			yield return new WaitForSeconds (buffWait);
			if (gameOver) {
				break;
			}
		}
	}

	void UpdateScore () {
		scoreText.text = "Score: " + score;
	}

	void UpdateWave () {
		waveText.text = "Wave: " + wave;
	}

	void UpdateWULText () {
		wulText.text = "Weapon upgrades: " + wul;
	}

	void UpdateWFRText () {
		wfrText.text = "Fire rate: " + wfr;
	}

	public void AddScore(int newScoreValue)
	{
		score += newScoreValue;
		UpdateScore ();
	}

	public void UpdateWUL(int value)
	{
		wul += value;
		UpdateWULText ();
	}

	public void UpdateWFR(int value)
	{
		wfr += value;
		UpdateWFRText ();
	}

	public void GameOver()
	{
		gameOverText.text = "GAME OVER";
		gameOver = true;
	}


}
