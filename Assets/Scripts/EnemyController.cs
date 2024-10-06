using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public EnemyType type;

    public Transform[] patrolPoints;
    public int curPoint;

    public float moveSpeed;
    public float turnSpeed;
    public float waitAtPoint;
    public float waitCounter;

    public float jumpForce;
    public Rigidbody2D theRB;

    public bool isGrounded = true;
    public float wallTime;
    public float wallCounter;
    public bool isWalled;
    public bool walled;
    public Transform groundPoint;
    public Transform wallPoint;
    public LayerMask groundLayer;

    public Animator anim;

    public bool enemyDetect;
    public GameObject playerChar;

    private void Start()
    {
        waitCounter = waitAtPoint;
        wallCounter = wallTime;

        foreach (Transform pPoint in patrolPoints)
        {
            pPoint.SetParent(null);
        }
    }

    private void Update()
    {
        if (type == EnemyType.Patroller)
        {
            if (Mathf.Abs(transform.position.x - patrolPoints[curPoint].position.x) > 0.2f && !enemyDetect)
            {
                if (transform.position.x < patrolPoints[curPoint].position.x)
                {
                    theRB.velocity = new Vector2(moveSpeed, theRB.velocity.y);
                    transform.localScale = new Vector3(-1f, 1f, 1f);
                }
                else
                {
                    theRB.velocity = new Vector2(-moveSpeed, theRB.velocity.y);
                    transform.localScale = new Vector3(1f, 1f, 1f);
                }

                walled = Physics2D.OverlapCircle(wallPoint.position, 0.2f, groundLayer);
                if (walled)
                {
                    wallCounter -= Time.deltaTime;
                    if (wallCounter <= 0)
                    {
                        wallCounter = wallTime;
                        isWalled = true;
                    }
                }
                else
                {
                    isWalled = false;
                    wallCounter = wallTime;
                }

                isGrounded = Physics2D.OverlapCircle(groundPoint.position, 0.2f, groundLayer);
                if (transform.position.y < patrolPoints[curPoint].position.y - 0.3f && theRB.velocity.y < 0.1f && isGrounded)
                {
                    theRB.velocity = new Vector2(theRB.velocity.x, jumpForce);
                    isGrounded = false;
                }
                else if (isGrounded && isWalled)
                {
                    theRB.velocity = new Vector2(theRB.velocity.x, jumpForce);
                    isGrounded = false;
                    isWalled = false;
                }
            }
            else if (Mathf.Abs(transform.position.x - patrolPoints[curPoint].position.x) < 0.2f && !enemyDetect)
            {
                theRB.velocity = new Vector2(0f, theRB.velocity.y);

                waitCounter -= Time.deltaTime;
                if (waitCounter <= 0)
                {
                    waitCounter = waitAtPoint;

                    curPoint++;
                    if (curPoint >= patrolPoints.Length)
                    {
                        curPoint = 0;
                    }
                }
            }
            else if (enemyDetect && playerChar != null)
            {
                if (transform.position.x < playerChar.transform.position.x)
                {
                    theRB.velocity = new Vector2(moveSpeed, theRB.velocity.y);
                    transform.localScale = new Vector3(-1f, 1f, 1f);
                }
                else
                {
                    theRB.velocity = new Vector2(-moveSpeed, theRB.velocity.y);
                    transform.localScale = new Vector3(1f, 1f, 1f);
                }

                isGrounded = Physics2D.OverlapCircle(groundPoint.position, 0.2f, groundLayer);
                if (transform.position.y < playerChar.transform.position.y - 0.6f && theRB.velocity.y < 0.1f && isGrounded)
                {
                    theRB.velocity = new Vector2(theRB.velocity.x, jumpForce);
                    isGrounded = false;
                }
            }
        }
        else if (type == EnemyType.Flyer)
        {
            if (Mathf.Abs(transform.position.x - patrolPoints[curPoint].position.x) > 0.2f && !enemyDetect)
            {
                Vector2 direction = (transform.position - patrolPoints[curPoint].transform.position).normalized;
                float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
                Quaternion targetRot = Quaternion.AngleAxis(angle, Vector3.forward);

                transform.rotation = Quaternion.Slerp(transform.rotation, targetRot, turnSpeed * Time.deltaTime);

                theRB.velocity = new Vector2(-direction.x * moveSpeed, -direction.y * moveSpeed) * Time.deltaTime;

                walled = Physics2D.OverlapCircle(wallPoint.position, 0.2f, groundLayer);
                if (walled)
                {
                    wallCounter -= Time.deltaTime;
                    if (wallCounter <= 0)
                    {
                        wallCounter = wallTime;
                        isWalled = true;
                    }
                }
                else
                {
                    isWalled = false;
                    wallCounter = wallTime;
                }

                isGrounded = Physics2D.OverlapCircle(groundPoint.position, 0.2f, groundLayer);
                if (transform.position.y < patrolPoints[curPoint].position.y - 0.3f && theRB.velocity.y < 0.1f && isGrounded)
                {
                    theRB.velocity = new Vector2(theRB.velocity.x, jumpForce);
                    isGrounded = false;
                }
                else if (isGrounded && isWalled)
                {
                    theRB.velocity = new Vector2(theRB.velocity.x, jumpForce);
                    isGrounded = false;
                    isWalled = false;
                }
            }
            else if (Mathf.Abs(transform.position.x - patrolPoints[curPoint].position.x) < 0.2f && !enemyDetect)
            {
                theRB.velocity = new Vector2(0f, theRB.velocity.y);

                waitCounter -= Time.deltaTime;
                if (waitCounter <= 0)
                {
                    waitCounter = waitAtPoint;

                    curPoint++;
                    if (curPoint >= patrolPoints.Length)
                    {
                        curPoint = 0;
                    }
                }
            }
            else if (enemyDetect && playerChar != null)
            {

                Vector2 direction = (transform.position - playerChar.transform.position).normalized;
                float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
                Quaternion targetRot = Quaternion.AngleAxis(angle, Vector3.forward);

                transform.rotation = Quaternion.Slerp(transform.rotation, targetRot, turnSpeed * Time.deltaTime);

                //transform.position += (-transform.right * moveSpeed) * Time.deltaTime;
                theRB.velocity = new Vector2(-direction.x * moveSpeed, -direction.y * moveSpeed) * Time.deltaTime;

                walled = Physics2D.OverlapCircle(wallPoint.position, 1.5f, groundLayer);
                if (walled)
                {
                    wallCounter -= Time.deltaTime;
                    if (wallCounter <= 0)
                    {
                        wallCounter = wallTime;
                        isWalled = true;
                    }
                }
                else
                {
                    isWalled = false;
                    wallCounter = wallTime;
                }
            }
        }

        anim.SetFloat("Speed", Mathf.Abs(theRB.velocity.x));
    }
}

public enum EnemyType
{
    Patroller,
    Flyer,
}
