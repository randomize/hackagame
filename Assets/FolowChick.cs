using UnityEngine;
using System.Collections;

public class FolowChick : MonoBehaviour
{
    public float upperBound = 0.1f;
    public GameObject hen;

    // Update is called once per frame
    void Update()
    {
        float diff = hen.transform.position.y - (transform.position.y + Camera.main.orthographicSize * (1 - upperBound));

        if (diff > 0)
        {
            transform.position += new Vector3(0, diff, 0);
        }
    }


}
