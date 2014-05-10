using UnityEngine;  

public class GameController : MonoBehaviour
{

    public GameObject hen;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update ()
	{
	    Texture2D spriteTexture = hen.GetComponent<SpriteRenderer>().sprite.texture;
	    Vector3 henPosition = hen.transform.position;
	    Vector3 toScreen = Camera.main.WorldToScreenPoint(henPosition);

	    if (toScreen.x > Screen.width)
	    {
	        toScreen.x = 0;
            hen.transform.position = toScreen;
            hen.transform.position = Camera.main.ScreenToWorldPoint(toScreen);
        }
        else if (toScreen.x < 0)
        {
            toScreen.x = Screen.width;
            hen.transform.position = Camera.main.ScreenToWorldPoint(toScreen);
        }
	}
}
