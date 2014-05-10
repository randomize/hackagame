using UnityEngine;
using System.Collections;

public class BootSound : MonoBehaviour
{
    public AudioClip[] kicks;
    public void PlayKick()
    {

        int indx = Random.Range(0, kicks.Length);
        AudioSource.PlayClipAtPoint(kicks[indx], transform.position);

    }
}
