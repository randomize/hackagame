using UnityEngine;

public class Hen : MonoBehaviour
{

    public float maxFallVelocity = 15f;
    public float maxFligthVelocity = 50f;

    public AudioClip[] kicks;

    public GameObject helmet;
    public GameObject boot;

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

        if (Camera.main.transform.position.y > 900)
        {
            helmet.SetActive(true);
        }

    }

    private bool sup = false;

    void FixedUpdate()
    {
        if (sup)
        {
            rigidbody2D.AddForce(new Vector3(0,80,0));
        }
    }

    public void PlayRanomKick()
    {
        //int indx = Random.Range(0, kicks.Length);
        //AudioSource.PlayClipAtPoint(kicks[indx], transform.position);
        boot.GetComponent<Animator>().SetTrigger("Kick");

    }

    public void StartSuperman()
    {
        sup = true;

    }

    public void StopSuperman()
    {
        if (sup)
        {
        sup = false;
        rigidbody2D.velocity = Vector2.zero;
        }
    }
}
