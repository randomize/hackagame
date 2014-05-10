using UnityEngine;
using System.Collections;

public class BulletScript : MonoBehaviour {
	public Vector3 speed;

	//GameObject han;
	public float frame_h = 100;

	// Use this for initialization
	public void Awake(){

		BulletUpd ();

	}
	
	// Update is called once per frame
	void Update () {
		var t = transform.position;
		t += speed * Time.deltaTime;

		transform.position = t;

		if (t.x < -40 || t.x > 40 || t.y < Camera.main.transform.position.y - frame_h 
		    || t.y > Camera.main.transform.position.y + frame_h ){
			BulletUpd();
		}
	}
	void BulletUpd() {
		transform.position = 
			new Vector3 (Random.Range (0, 2) == 0 ? 30 : -30, 
			             Camera.main.transform.position.y+Random.Range (-frame_h, frame_h),
			             0);
		var nspeed = new Vector3 (Random.Range (-1, 1f), Random.Range (-1, 1f));
		speed = 20 * nspeed.normalized;
		var angle = Mathf.Atan2(speed.y, speed.x) * Mathf.Rad2Deg;
		transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle +180));
	}
	void OnTriggerEnter2D(Collider2D other) {
		//play delink
		//AudioSource.PlayClipAtPoint (delink, transform.position);
		if (other.gameObject.name == "Hen") {
			Destroy(gameObject);

		}
	}
}
