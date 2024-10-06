using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    public Rigidbody2D playerRB;
    public SpriteRenderer playerSR;
    public bool flipped;
    public float moveSpeed;
    public float jumpForce;

    public Transform groundPoint;
    public LayerMask groundLayer;
    public Animator animator;

    public BulletController bulletPrefab;
    public Transform bulletSpawnPoint;

    public Transform bombPoint;
    public BombController bomb;
    public float bombCoolTime;
    private float bombCooler;
    public bool bombCooled = false;

    private bool isGrounded;
    private bool canDoubbleJump;
    public bool doubleJumpUnlock = false;
    public int extraJumpCount;
    private int extraJumps;
    private int jumpCounts;

    private Vector2 direction;
    private bool moving;

    private PlayerInput thisPlayerInput;

    public AudioSource landingAudio;
    public AudioSource shotAudio;

    private void Start()
    {
        thisPlayerInput = this.GetComponent<PlayerInput>();
        PlayerSpawnManager.instance.PlayerId(thisPlayerInput);
    }

    private void Update()
    {
        //Check Ground
        isGrounded = Physics2D.OverlapCircle(groundPoint.position, 0.2f, groundLayer);
        if (isGrounded)
        {
            extraJumps = extraJumpCount;
            if (jumpCounts >= 1)
                landingAudio.Play();
            jumpCounts = 0;
            animator.SetBool("IsGrounded", true);
        }
        else
        {
            animator.SetBool("IsGrounded", false);
            jumpCounts = 1;
        }

        if (bombCooler > 0 && !bombCooled)
        {
            bombCooler -= Time.deltaTime;
            if (bombCooler < 0)
            {
                bombCooled = true;
            }
        }
    }

    private void FixedUpdate()
    {
        if(moving)
        {
            playerRB.velocity = new Vector2(direction.x * moveSpeed, playerRB.velocity.y);
        }
        if (playerRB.velocity.x > 0)
        {
            transform.localScale = Vector3.one;
            flipped = false;
            animator.SetFloat("Speed", Mathf.Abs(playerRB.velocity.x));
        }
        else if (playerRB.velocity.x < 0)
        {
            transform.localScale = new Vector3(-1f, transform.localScale.y, transform.localScale.z);
            flipped = true;
            animator.SetFloat("Speed", Mathf.Abs(playerRB.velocity.x));
        }
        else
        {
            animator.SetFloat("Speed", 0);
            return;
        }
    }

    public void OnMoveInput(InputAction.CallbackContext context)
    {
        if(context.phase == InputActionPhase.Performed)
        {
            direction = context.ReadValue<Vector2>();
            moving = true;
        }
        else if(context.phase == InputActionPhase.Canceled)
        {
            direction = Vector2.zero;
            moving = false;
        }
    }

    public void OnJumpInput(InputAction.CallbackContext context)
    {
        if (isGrounded || canDoubbleJump)
        {
            if (isGrounded && extraJumps >= 0 && doubleJumpUnlock)
            {
                canDoubbleJump = true;
            }
            else if (isGrounded || extraJumps < 0)
            {
                canDoubbleJump = false;
            }

            if (jumpCounts != 0 && extraJumps >= 0 && doubleJumpUnlock && !isGrounded)
            {
                animator.SetTrigger("DoubleJump");
            }

            if (canDoubbleJump || !doubleJumpUnlock)
            {
                playerRB.velocity = new Vector2(playerRB.velocity.x, jumpForce);
                extraJumps -= 1;
            }
        }
    }

    public void OnAttackInput(InputAction.CallbackContext context)
    {
        if(context.phase == InputActionPhase.Started)
        {
            Debug.Log("Attack " + thisPlayerInput.playerIndex);
            Vector3 spawnPoint = new Vector3(bulletSpawnPoint.position.x, bulletSpawnPoint.position.y, bulletSpawnPoint.position.z);
            BulletController spawnedBullet = Instantiate(bulletPrefab, bulletSpawnPoint.position, bulletSpawnPoint.rotation);
            spawnedBullet.moveDir = new Vector2(transform.localScale.x, 0);
            spawnedBullet.flipped = flipped;
            spawnedBullet.playerIndex = thisPlayerInput;
            animator.SetTrigger("Shooting");
            shotAudio.Play();
        }
    }

    public void OnBombInput(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Started)
        {
            if(bombCooled)
            {
                Instantiate(bomb, bombPoint.position, bombPoint.rotation).playerIndex = thisPlayerInput;
                bombCooled = false;
                bombCooler = bombCoolTime;
            }
            Debug.Log("Dodge " + thisPlayerInput.playerIndex);
        }
    }
}
