using UnityEngine;
using System.Collections;


public class BulletSpawner : MonoBehaviour
{
    public int count = 0;
    public int rate = 10;

    // Use this for initialization
    /*void Start () {
        for (int i = 0; i< count; i++) {
            var bulletInstance = Resources.Load("bullet");
            var pos = new Vector3(0, 0, 0);
            Instantiate( bulletInstance, 
                                   pos+transform.position, 
                                   Quaternion.identity ) as GameObject;
        }
    }*/

    // Update is called once per frame
    void Update()
    {
        var currPosition = Camera.main.transform.position;
        int c = (int)(currPosition.y / rate - count);
        for (int i = 0; i < c; i++)
        {
            var bulletInstance = Resources.Load("bullet");
            var pos = new Vector3(0, 0, 0);
            var g = Instantiate(bulletInstance,
                        pos + transform.position,
                        Quaternion.identity) as GameObject;
        }

        count += c;
    }
}
