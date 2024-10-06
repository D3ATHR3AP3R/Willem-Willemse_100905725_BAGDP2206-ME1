using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class EnemyHealthController : MonoBehaviour
{
    public float totalHealth;
    public float curHealth;

    public float regenTime;
    private float regenCounter;
    public int regenAmount;

    public Image health;

    public int scoreValue;

    private PlayerInput playerindex;

    public GameObject deathEffect;
    public EnemyController enemyController;

    private void Start()
    {
        curHealth = totalHealth;
        regenCounter = regenTime;
    }

    private void Update()
    {
        if (curHealth < totalHealth)
        {
            regenCounter -= Time.deltaTime;
            if (regenCounter <= 0)
            {
                curHealth += regenAmount;
                if (curHealth > totalHealth)
                {
                    curHealth = totalHealth;
                }
                regenCounter = regenTime;
            }
        }

        if (curHealth <= 0)
        {
            if (deathEffect != null)
            {
                Instantiate(deathEffect, transform.position, transform.rotation);
            }

            UIManager.instance.ScoreUpdate(scoreValue, playerindex);

            Destroy(gameObject);
        }

        if (curHealth != 0)
        {
            health.fillAmount = curHealth / totalHealth;
        }
    }

    public void DamageEnemy(int damage, PlayerInput input)
    {
        curHealth -= damage;
        playerindex = input;
    }
}
