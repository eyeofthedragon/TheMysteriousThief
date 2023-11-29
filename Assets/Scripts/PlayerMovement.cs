using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerMovement : MonoBehaviour {
    //movement variables
    public float playerSpeed = 0.1f;
    public Vector2 jumpHeight;
    public Vector2 wallJumpHeightLeft;
    public Vector2 wallJumpHeightRight;
    
    public float groundCheckRadius = 1; 
    public float wallCheckRadius = 0.5f;

    //gem variables
    public bool hasGem = false;
    public GameObject gem;
    public GameObject gemDialogueTrigger;

    //door variables
    public bool hasDoorKey = false;
    public GameObject doorKey;
    public GameObject doorKeyDialogueTrigger;
    public GameObject doorMechanismDialogueTrigger;

    //other objects
    public LayerMask groundLayer;
    public LayerMask shadowLayer;
    public LayerMask guardLayer;
    public UIController uiController;
    public DialogueManager dialogueManager;

    //sound effects
    AudioSource audioSource;
    public AudioClip footstep;
    public AudioClip jumpSound;


    //private movement variables
    private float horizontal;
    
    private bool jump = false;
    private bool wallJumpRight = false;
    private bool wallJumpLeft = false;
    private bool onGround = true;
    private bool doubleJump = false;

    private bool wallOnRight = false;
    private bool wallOnLeft = false;

    private bool nearbyShadows = false;
    private bool inShadows = false;

    //direction vectors
    private Vector2 groundDirection = new Vector2(0, -1);
    private Vector2 wallRightDirection = new Vector2(1, 0);
    private Vector2 wallLeftDirection = new Vector2(-1, 0);

    //animation bools
    private bool isJumping;
    private bool isDoubleJumping;
    private bool isWallJumpingLeft;
    private bool isWallJumpingRight;
    private bool facingLeft;

    //components
    private Rigidbody2D rb;
    private SpriteRenderer spriteRenderer;
    private BoxCollider2D boxCollider;
    private Animator animator;


    private void Start() {
        rb = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        boxCollider = GetComponent<BoxCollider2D>();
        animator = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();
    }

    private void Update() {
        if (!dialogueManager.dialogueIsPlaying) {

            horizontal = Input.GetAxis("Horizontal");

            //the walljump function handles the direction, so just ignore the player input
            if (isWallJumpingLeft || isWallJumpingRight) {
                horizontal = 0;
            }

            if (horizontal < 0) {
                    facingLeft = true;
            }
            else if (horizontal > 0) { //don't change when horizontal = 0 (ie not moving)
                    facingLeft = false;
            }
            

            //regular jump
            if (Input.GetKeyDown(KeyCode.Space) && onGround) {
                isJumping = true;
                jump = true;
                doubleJump = true;
            }
            //wall jump to the left
            else if (Input.GetKeyDown(KeyCode.Space) && !onGround && wallOnRight) {
                doubleJump = false; //just make sure this is off
                isJumping = false;
                isWallJumpingLeft = true;
                wallJumpLeft = true;
                facingLeft = true;
            }
            //wall jump to the right
            else if (Input.GetKeyDown(KeyCode.Space) && !onGround && wallOnLeft) {
                doubleJump = false;
                isJumping = false;
                isWallJumpingRight = true;
                wallJumpRight = true;
                facingLeft = false;
            }
            //double jump
            else if (Input.GetKeyDown(KeyCode.Space) && doubleJump && !onGround) {
                isDoubleJumping = true;
                jump = true;
                doubleJump = false;
            }
            //on the ground, not about to jump
            else if (onGround && !jump) {
                isJumping = false;
                isDoubleJumping = false;
                isWallJumpingLeft = false;
                isWallJumpingRight = false;
            }

            //once we hit the opposite wall, we are no longer wall jumping
            if (isWallJumpingLeft && wallOnLeft) {
                isWallJumpingLeft = false;
            }
            if (isWallJumpingRight && wallOnRight) {
                isWallJumpingRight = false;
            }

            //hide in the shadows
            if ((Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift)) && nearbyShadows) {
                inShadows = true;
                spriteRenderer.sortingOrder = -10; //ensure the player sprite appears behind the shadows while they are concealed
                boxCollider.excludeLayers = guardLayer;
            }
            else {
                inShadows = false;
                spriteRenderer.sortingOrder = 0;
            }

        }
        //dialogue is currently playing
        else {
            isJumping = false;
            isDoubleJumping = false;
            isWallJumpingLeft = false;
            isWallJumpingRight = false;
            onGround = true; //or at least it's about to be
        }

        AnimateSprite();

    }

    private void FixedUpdate() {
        if (!dialogueManager.dialogueIsPlaying) {
            Vector2 position = transform.position;
            position.x += horizontal * playerSpeed;
            transform.position = position;

            onGround = Physics2D.Raycast(transform.position, groundDirection, groundCheckRadius, groundLayer);
            Debug.DrawRay(transform.position, groundDirection, Color.green);

            wallOnRight = Physics2D.Raycast(transform.position, wallRightDirection, wallCheckRadius, groundLayer);
            wallOnLeft = Physics2D.Raycast(transform.position, wallLeftDirection, wallCheckRadius, groundLayer);

            Debug.DrawRay(transform.position, wallRightDirection, Color.blue);
            Debug.DrawRay(transform.position, wallLeftDirection, Color.red);

            nearbyShadows = Physics2D.Raycast(transform.position, wallRightDirection, wallCheckRadius / 2, shadowLayer) || Physics2D.Raycast(transform.position, wallLeftDirection, wallCheckRadius / 2, shadowLayer);


            if (jump) {
                Jump();
                jump = false;
                onGround = false; //ensure that the jump animation has a chance to trigger
            }
            else if (wallJumpRight) {
                WallJumpRight();
                wallJumpRight = false;
                wallOnLeft = false;
            }
            else if (wallJumpLeft) {
                WallJumpLeft();
                wallJumpLeft = false;
                wallOnRight = false;
            }
        }
    }

    private void Jump() {
        playJump();
        rb.AddForce(jumpHeight, ForceMode2D.Impulse);
    }

    private void WallJumpLeft() {
        playJump();
        rb.AddForce(wallJumpHeightLeft, ForceMode2D.Impulse);
    }

    private void WallJumpRight() {
        playJump();
        rb.AddForce(wallJumpHeightRight, ForceMode2D.Impulse);
    }

    public bool getInShadows() {
        return inShadows;
    }

    private void AnimateSprite() {
        //ensure that moving is always rounded up/down, not rounded to 0
        if (horizontal > 0) {
            animator.SetInteger("moving", 1);
        }
        else if (horizontal < 0) {
            animator.SetInteger("moving", -1);
        }
        else {
            animator.SetInteger("moving", 0);
        }

        //animator.SetInteger("moving", (int)horizontal);
        animator.SetBool("facingLeft", facingLeft);
        animator.SetBool("isJumping", isJumping);
        animator.SetBool("isDoubleJumping", isDoubleJumping);
        animator.SetBool("isWallJumpingLeft", isWallJumpingLeft);
        animator.SetBool("isWallJumpingRight", isWallJumpingRight);
        animator.SetBool("wallOnLeft", wallOnLeft);
        animator.SetBool("wallOnRight", wallOnRight);
        animator.SetBool("onGround", onGround);

        if (dialogueManager.dialogueIsPlaying) {
            animator.SetInteger("moving", 0);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.gameObject.tag == "Gem") {
            hasGem = true;
            gem.SetActive(false);
            gemDialogueTrigger.SetActive(true);
        }

        if (collision.gameObject.tag == "DoorKey") {
            hasDoorKey = true;
            doorKey.SetActive(false);
            doorKeyDialogueTrigger.SetActive(true);
            doorMechanismDialogueTrigger.SetActive(true);
        }

        if (collision.gameObject.tag == "Finish") {
            if (hasGem) {
                uiController.Credits();
            }
        }
    }

    //sound effects
    public void playFootstep() {
        audioSource.PlayOneShot(footstep);
    }

    private void playJump() {
        audioSource.PlayOneShot(jumpSound);
    }
}
