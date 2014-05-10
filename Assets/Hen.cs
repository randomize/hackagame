using UnityEngine;

public class Hen : MonoBehaviour
{

    public float maxFallVelocity = 15f;
    public float maxFligthVelocity = 50f;

    public AudioClip[] kicks;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

        if (gameObject.rigidbody2D.velocity.y < -maxFallVelocity)
        {
            Vector2 velocity = new Vector2(gameObject.rigidbody2D.velocity.x, -maxFallVelocity);
            gameObject.rigidbody2D.velocity = velocity;
        }
        else if (gameObject.rigidbody2D.velocity.y > maxFligthVelocity)
        {
            Vector2 velocity = new Vector2(gameObject.rigidbody2D.velocity.x, maxFligthVelocity);
            gameObject.rigidbody2D.velocity = velocity;
        }

    }

    public void PlayRanomKick()
    {
        Debug.Log("Played kicvk");
        int indx = Random.Range(0, kicks.Length);
        AudioSource.PlayClipAtPoint(kicks[indx], transform.position);
    }
}
