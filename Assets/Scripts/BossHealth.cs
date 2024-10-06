using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BossHealth : MonoBehaviour
{
    public static BossHealth instance;

    public float totalHealth;
    public float curHealth;

    public float regenTime;
    private float regenCounter;
    public int regenAmount;
    public int scoreValue;

    public Image health;

    public GameObject deathEffect;
    public GameObject bossObj;

    private void Awake()
    {
        instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        curHealth = totalHealth;
        regenCounter = regenTime;
    }

    // Update is called once per frame
    void Update()
    {
        /*if (curHealth < totalHealth)
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
        }*/

        if (curHealth <= 0)
        {
            if (deathEffect != null)
            {
                Instantiate(deathEffect, transform.position, transform.rotation);
            }

            //PlayerController.instance.ScoreGain(scoreValue);
            //PlayerController.instance.EndMenu();
            Destroy(bossObj);
        }

        if (curHealth != 0)
        {
            health.fillAmount = curHealth / totalHealth;
        }
    }

    public void DamageEnemy(int damage)
    {
        curHealth -= damage;
    }
}
