using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class BulletController : MonoBehaviour
{
    public float bulletSpeed;
    public Rigidbody2D bulletRB;
    public Vector2 moveDir;
    public bool flipped;
    public SpriteRenderer _bulletSR;

    public GameObject impactEffect;

    public int damageAmount = 1;
    public PlayerInput playerIndex;

    void Start()
    {
        StartCoroutine(DestroyTime());
    }

    void Update()
    {
        bulletRB.velocity = moveDir * bulletSpeed;
        if (flipped)
        {
            _bulletSR.flipX = false;
        }
        else
        {
            _bulletSR.flipX = true;
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log(other);

        if (other.tag == "Enemy")
        {
            other.GetComponent<EnemyHealthController>().DamageEnemy(damageAmount, playerIndex);
        }

        if (other.tag == "Boss")
        {
            BossHealth.instance.DamageEnemy(damageAmount);
        }

        if(other.tag == "Player")
        {
            if(other.gameObject.GetComponent<PlayerInput>().playerIndex != playerIndex.playerIndex)
            {
                other.gameObject.GetComponent<PlayerHealthController>().KnockBack(Color.cyan);
            }
        }

        if (impactEffect != null && other.tag != "Detection")
        {
            Instantiate(impactEffect, transform.position, Quaternion.identity);
        }

        if (other.tag != "Detection")
        {
            Destroy(gameObject);
        }
    }

    private IEnumerator DestroyTime()
    {
        yield return new WaitForSeconds(5f);
        Destroy(gameObject);
    }
}
