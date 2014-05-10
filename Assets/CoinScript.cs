using UnityEngine;
using System.Collections;

public class CoinScript : MonoBehaviour {

	public AudioClip delink;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnTriggerEnter2D(Collider2D other) {
		//play delink
		AudioSource.PlayClipAtPoint (delink, transform.position);
		Destroy(gameObject);
	}
}
