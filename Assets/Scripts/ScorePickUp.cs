using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class ScorePickUp : MonoBehaviour
{
    public int scoreValue;

    public AudioSource pickUpAudio;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            UIManager.instance.ScoreUpdate(scoreValue, collision.gameObject.GetComponent<PlayerInput>());
            pickUpAudio.Play();

            Destroy(gameObject, 0.2f);
        }
    }
}
