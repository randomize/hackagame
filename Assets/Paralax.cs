using UnityEngine;
using System.Collections;

[UnityEngine.ExecuteInEditMode]
public class Paralax : MonoBehaviour {

	[Range(0.01f, 5)]
	public float obj;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {

		transform.position = new Vector3(transform.position.x, Camera.main.transform.position.y * obj);


	
	}
}
