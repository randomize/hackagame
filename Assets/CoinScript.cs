using UnityEngine;

public class CoinScript : MonoBehaviour
{

    public AudioClip delink;
    public int points = 100;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnTriggerEnter2D(Collider2D other)
    {
        audio.PlayOneShot(delink);
        GameController.instance.addPoints(100);
        //Destroy(gameObject);
        enabled = false;
        GetComponent<Collider2D>().enabled = false;
        GetComponent<SpriteRenderer>().enabled = false;
    }
}
