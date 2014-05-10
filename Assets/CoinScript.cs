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
        //play delink
        AudioSource.PlayClipAtPoint(delink, transform.position);
        GameController.instance.addPoints(100);
        Destroy(gameObject);
    }
}
