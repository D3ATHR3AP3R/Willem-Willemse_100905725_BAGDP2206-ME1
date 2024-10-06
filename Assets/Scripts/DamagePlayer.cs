using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamagePlayer : MonoBehaviour
{
    public int damageAmount;
    public Transform deathZone;

    private GameObject playerObj;

   /* private void Update()
    {
        if (deathZone != null)
        {
            if (PlayerController.instance.transform.position.y < deathZone.position.y)
            {
                PlayerHealthController.instance.DamageFromEnemy(damageAmount);
            }
        }
    }*/

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            playerObj = collision.gameObject;
            DealDamage();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            playerObj = collision.gameObject;
            DealDamage();
        }
    }

    void DealDamage()
    {
        playerObj.GetComponent<PlayerHealthController>().DamageFromEnemy(damageAmount);
    }
}
