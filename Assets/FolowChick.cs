using UnityEngine;
using System.Collections;

public class FolowChick : MonoBehaviour {

	public float upperBound = 0.1f; 

	public GameObject hen;

	// Use this for initialization
	void Start () {


	
	}
	
	// Update is called once per frame
	void Update () {


		//var pos = Camera.main.WorldToScreenPoint (hen.transform.position);
		var pos = hen.transform.position;
		pos.z = -10;
		pos.x = 0;
		transform.position = pos;
		
		return;

		//Debug.Log ("gg" + pos + "hhh" + Screen.height);
		//var diff = pos.y - Screen.height * LowerBound;
		///var 
		/*if (diff < 0) {
			Debug.Log("Low");
			transform.position = new Vector3(transform.position.x,
			                                 transform.position.y  + diff,
			                                 transform.position.z);
		}*/
		//var camPosScreen = Camera.main.WorldToScreenPoint (transform.position);
		//Debug.Log (camPosScreen);

		var diff2 =  pos.y - Screen.height * (1-upperBound);
		//Debug.Log ("gg" + diff2);
		var superdiff = Camera.main.ScreenToWorldPoint (new Vector3 (Screen.width/2, Screen.height/2 + diff2, 10));
		//superdiff.z = 0;
		if (diff2 > 0) {
			//Debug.Log("High");
			//Debug.Log(superdiff);
			transform.position = new Vector3( 0, superdiff.y + transform.position.y, transform.position.z);
				                                 
        }
	
	
	}
}
