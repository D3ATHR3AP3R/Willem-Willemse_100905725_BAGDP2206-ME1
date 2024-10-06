using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class PlayerHealthController : MonoBehaviour
{

    public Rigidbody2D playerChar;
    public float knockForce;

    public float totalHealth;
    public float curHealth;

    public float regenTime;
    private float regenCounter;
    public int regenAmount;

    public bool flashing;
    public bool colorFlashed;
    public float flashLength;
    private float flashCount;
    public float betweenFlash;
    private float betweenFlashCount;
    public Color original;
    private Color flashColor;

    private int forceDir;

    private PlayerInput playerIndex;

    public SpriteRenderer playerSprite;

    private void Awake()
    {
        curHealth = totalHealth;
    }

    // Start is called before the first frame update
    void Start()
    {
        curHealth = totalHealth;
        regenCounter = regenTime;
        betweenFlashCount = betweenFlash;

        playerIndex = GetComponent<PlayerInput>();
        original = PlayerSpawnManager.instance.spawnColors[playerIndex.playerIndex];
    }

    // Update is called once per frame
    private void Update()
    {
        if (curHealth < totalHealth)
        {
            regenCounter -= Time.deltaTime;
            if (regenCounter <= 0 && curHealth != 0)
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
            curHealth = 0;

            gameObject.SetActive(false);
        }

        if (curHealth != 0)
        {
            UIManager.instance.HealthUpdate(curHealth, totalHealth, playerIndex);
        }

        if (flashing)
        {
            if (flashCount > 0)
            {
                flashCount -= Time.deltaTime;

                betweenFlashCount -= Time.deltaTime;
                if (betweenFlashCount <= 0)
                {
                    if (colorFlashed)
                    {
                        playerSprite.color = original;
                        colorFlashed = false;
                    }
                    else
                    {
                        playerSprite.color = flashColor;
                        colorFlashed = true;
                    }

                    betweenFlashCount = betweenFlash;
                }
            }
            else
            {
                playerSprite.color = original;
                colorFlashed = false;
                betweenFlashCount = betweenFlash;
            }
        }
        else
        {
            playerSprite.color = original;
        }
    }

    public void DamageFromEnemy(int damage)
    {
        curHealth -= damage;
        KnockBack(Color.red);
    }

    public void KnockBack(Color flash)
    {
        if (playerChar.velocity.x < 0 && playerChar != null)
        {
            playerChar.AddForce(transform.up + (transform.right * knockForce) * -1, ForceMode2D.Force);
        }
        else if (playerChar.velocity.x > 0 && playerChar != null)
        {
            playerChar.AddForce(transform.up + (transform.right * knockForce), ForceMode2D.Force);
        }
        else
        {
            if(gameObject.GetComponent<PlayerController>().flipped)
            {
                forceDir = 1;
            }
            else
            {
                forceDir = -1;
            }
            playerChar.AddForce(transform.up + (transform.right * knockForce) * forceDir, ForceMode2D.Force);
        }

        flashing = true;
        flashCount = flashLength;
        flashColor = flash;
    }
}
