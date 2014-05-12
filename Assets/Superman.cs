using UnityEngine;
using System.Collections;

public class Superman : MonoBehaviour
{

    public GameObject hen;

	// Use this for initialization
	void Start ()
	{

	    var vec = new Vector3(Random.Range(-20.0f, 20.0f), Random.Range(100, 300), 0);
	    transform.position = vec;

	}

    private bool one = true;

    void OnTriggerEnter2D(Collider2D other )
    {
        if (!one)
        {
            return;
        }
        Debug.Log("Super");
        if (other.gameObject == hen)
        {
            hen.GetComponent<Animator>().SetTrigger("SuperMan");
            StartCoroutine("CoroutinePower");
            one = false;
            GetComponent<SpriteRenderer>().color = new Color(0,0,0,0);
        }
    }


    IEnumerator CoroutinePower()
    {

        yield return new WaitForSeconds(6);
        hen.GetComponent<Animator>().SetTrigger("SuperStop");

    }
}
