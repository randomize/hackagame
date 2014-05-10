﻿using UnityEngine;
using System.Collections;

public class BulletScript : MonoBehaviour {
	public Vector3 speed;

	// Use this for initialization
	void Awake () {
		BulletUpd ();
		//transform.localRotation = Quaternion.AngleAxis (0, speed);
			//FromToRotation (Vector3.zero, speed);


	}
	
	// Update is called once per frame
	void Update () {
		var t = transform.position;
		t += speed * Time.deltaTime;

		transform.position = t;

		if (t.x < -40 || t.x > 40){
			BulletUpd();
		}
	}
	void BulletUpd() {
		transform.position = 
			new Vector3 (Random.Range (0, 2) == 0 ? 30 : -30, Random.Range (0, 100), 0);
		speed = 5 * new Vector3 (Random.Range (-1, 1f), Random.Range (-1, 1f));
		var angle = Mathf.Atan2(speed.y, speed.x) * Mathf.Rad2Deg;
		transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle +180));
	}
	void OnTriggerEnter2D(Collider2D other) {
		//play delink
		//AudioSource.PlayClipAtPoint (delink, transform.position);
		Destroy(gameObject);
	}
}
