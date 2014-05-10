using UnityEngine;

public class Hen : MonoBehaviour {

    public float maxFallVelocity = 2f;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {

        if (gameObject.rigidbody2D.velocity.y < -maxFallVelocity)
        {
            Vector2 velocity = new Vector2(gameObject.rigidbody2D.velocity.x, -maxFallVelocity);
            gameObject.rigidbody2D.velocity = velocity;
	    }

	}
}
