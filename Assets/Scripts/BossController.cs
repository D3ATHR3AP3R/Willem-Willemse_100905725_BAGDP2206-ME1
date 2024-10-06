using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossController : MonoBehaviour
{

    public int threshold1, threshold2;

    public float activeTime, fadeoutTime, inactiveTime;
    private float activeCounter, fadeCounter, inactiveCounter;

    public Transform[] spawnpoints;
    private Transform targetPoint;
    public float moveSpeed;

    public Animator anim;
    public Transform theBoss;

    public float timeBetweenShots1, timeBetweenShots2;
    private float shotCounter;
    public GameObject bullet;
    public Transform shotPoint;

    // Start is called before the first frame update
    void Start()
    {
        activeCounter = activeTime;

        shotCounter = timeBetweenShots1;
    }

    // Update is called once per frame
    void Update()
    {
        if(BossHealth.instance.curHealth > threshold1)
        {
            if(activeCounter > 0)
            {
                activeCounter -= Time.deltaTime;
                if(activeCounter <= 0)
                {
                    fadeCounter = fadeoutTime;
                    anim.SetTrigger("Vanish");
                }

                shotCounter -= Time.deltaTime;
                if(shotCounter <= 0)
                {
                    shotCounter = timeBetweenShots1;

                    Instantiate(bullet, shotPoint.position, Quaternion.identity);
                }
            }
            else if(fadeCounter > 0)
            {
                fadeCounter -= Time.deltaTime;
                if(fadeCounter <=0)
                {
                    theBoss.gameObject.SetActive(false);
                    inactiveCounter = inactiveTime;
                }
            }
            else if(inactiveCounter > 0)
            {
                inactiveCounter -= Time.deltaTime;
                if(inactiveCounter <= 0)
                {
                    theBoss.position = spawnpoints[Random.Range(0, spawnpoints.Length)].transform.position;
                    theBoss.gameObject.SetActive(true);

                    activeCounter = activeTime;
                    shotCounter = timeBetweenShots1;
                }
            }
        }
        else
        {
            if(targetPoint == null)
            {
                targetPoint = theBoss;
                fadeCounter = fadeoutTime;
                anim.SetTrigger("Vanish");
            }
            else
            {
                if (Vector3.Distance(theBoss.position, targetPoint.position) > 0.02f)
                {
                    theBoss.position = Vector3.MoveTowards(theBoss.position, targetPoint.position, moveSpeed * Time.deltaTime);

                    if (Vector3.Distance(theBoss.position, targetPoint.position) <= 0.02f)
                    {
                        fadeCounter = fadeoutTime;
                        anim.SetTrigger("Vanish");
                    }

                    shotCounter -= Time.deltaTime;
                    if (shotCounter <= 0)
                    {
                        if(BossHealth.instance.curHealth > threshold2)
                        {
                            shotCounter = timeBetweenShots1;
                        }
                        else
                        {
                            shotCounter = timeBetweenShots2;
                        }

                        Instantiate(bullet, shotPoint.position, Quaternion.identity);
                    }
                }
                else if (fadeCounter > 0)
                {
                    fadeCounter -= Time.deltaTime;
                    if (fadeCounter <= 0)
                    {
                        theBoss.gameObject.SetActive(false);
                        inactiveCounter = inactiveTime;
                    }
                }
                else if (inactiveCounter > 0)
                {
                    inactiveCounter -= Time.deltaTime;
                    if (inactiveCounter <= 0)
                    {
                        theBoss.position = spawnpoints[Random.Range(0, spawnpoints.Length)].transform.position;

                        targetPoint = spawnpoints[Random.Range(0, spawnpoints.Length)];
                        while(targetPoint.position == theBoss.position)
                        {
                            targetPoint = spawnpoints[Random.Range(0, spawnpoints.Length)];
                        }

                        theBoss.gameObject.SetActive(true);

                        if (BossHealth.instance.curHealth > threshold2)
                        {
                            shotCounter = timeBetweenShots1;
                        }
                        else
                        {
                            shotCounter = timeBetweenShots2;
                        }
                    }
                }
            }
        }
    }
}
