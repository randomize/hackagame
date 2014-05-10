using UnityEngine;
using System.Collections;

public class BulletScript : MonoBehaviour {
	public Vector3 speed;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		var t = transform.position;
		t += speed * Time.deltaTime;

		transform.position = t;
	}
}
