using UnityEngine;
using System.Collections;

public class StupidRotator : MonoBehaviour
{

    public float speed = 50f;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(0, 0, speed * Time.deltaTime);
    }
}
