using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class BombController : MonoBehaviour
{
    public float bombTime = 0.5f;
    public GameObject explosion;
    public int damageAmount;
    public PlayerInput playerIndex;

    public float blastRadius;
    public LayerMask destructableType;
    public LayerMask enemies;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        bombTime -= Time.deltaTime;
        if (bombTime <= 0)
        {
            if (explosion != null)
            {
                Instantiate(explosion, transform.position, transform.rotation);
            }

            Collider2D[] objectsToDestroy = Physics2D.OverlapCircleAll(transform.position, blastRadius, destructableType);

            if (objectsToDestroy.Length > 0)
            {
                foreach (Collider2D collider in objectsToDestroy)
                {
                    Destroy(collider.gameObject);
                }
            }

            Collider2D[] enemiesToDamage = Physics2D.OverlapCircleAll(transform.position, blastRadius, enemies);

            if (enemiesToDamage.Length > 0)
            {
                foreach (Collider2D collider in enemiesToDamage)
                {
                    if(collider != null)
                    collider.GetComponent<EnemyHealthController>().DamageEnemy(damageAmount, playerIndex);
                }
            }

            Destroy(gameObject);
        }
    }
}
