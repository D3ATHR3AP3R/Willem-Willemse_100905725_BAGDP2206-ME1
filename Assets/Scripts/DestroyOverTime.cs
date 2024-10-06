using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyOverTime : MonoBehaviour
{
    public float lifetime;

    public AudioSource explosionSound;

    // Start is called before the first frame update
    void Start()
    {
        explosionSound.Play();
        Destroy(gameObject, lifetime);
    }
}
