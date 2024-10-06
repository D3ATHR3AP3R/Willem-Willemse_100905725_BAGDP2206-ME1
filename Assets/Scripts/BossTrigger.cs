using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossTrigger : MonoBehaviour
{
    public GameObject bossBattle;

    public AudioSource bossMusic;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "Player")
        {
            bossBattle.SetActive(true);
            bossMusic.Play();
            Destroy(this.gameObject);
        }
    }
}
