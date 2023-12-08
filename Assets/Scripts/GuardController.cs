using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GuardController : MonoBehaviour {
    public Transform[] patrolPoints;
    public GameObject player;
    public LayerMask groundLayer;
    public DialogueManager dialogueManager;
    public AudioClip alert;
    public AudioClip footstep;
    public UIController uiController;

    public float patrolSpeed = 0.2f;
    public float chaseSpeed = 0.5f;
    public float viewDistance = 3;
    public float viewAngle = 30;

    public string mode = "searchMode";
    private bool canSeePlayer = false;
    private int hidingTimer = -1;

    private int patrolDestination = 1;
    private Vector2 lookDir;

    private bool moving = true;

    private PlayerMovement playerScript;
    private Animator animator;
    private AudioSource audioSource;

    void Start() {
        playerScript = player.GetComponent<PlayerMovement>();
        animator = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();

        lookDir = (Vector2)patrolPoints[1].position - (Vector2)transform.position;
    }

    void FixedUpdate() {
        if (!dialogueManager.dialogueIsPlaying) {
            if (mode == "searchMode") {
                switch (patrolDestination) {
                    case 0:
                        transform.position = Vector2.MoveTowards(transform.position, patrolPoints[0].position, patrolSpeed);
                        if (Vector2.Distance(transform.position, patrolPoints[0].position) < 0.2f) {
                            StartCoroutine(Wait(1));
                        }
                        break;
                    case 1:
                        transform.position = Vector2.MoveTowards(transform.position, patrolPoints[1].position, patrolSpeed);
                        if (Vector2.Distance(transform.position, patrolPoints[1].position) < 0.2f) {
                            StartCoroutine(Wait(0));
                        }
                        break;
                }
            }
            else if (mode == "chaseMode") {
                if (transform.position.x > player.transform.position.x) {
                    transform.position += Vector3.left * chaseSpeed;
                }
                else if (transform.position.x < player.transform.position.x) {
                    transform.position += Vector3.right * chaseSpeed;
                }
            }
        }

    }

    void AnimateSprite() {
        animator.SetFloat("lookDir", lookDir.x);
        if (dialogueManager.dialogueIsPlaying) {
            animator.SetBool("moving", false);
        }
        else {
            animator.SetBool("moving", moving);
        }
    }

    void Update() {
       Vision();
       AnimateSprite();
    }

    IEnumerator Wait(int destination) {
        moving = false;
        yield return new WaitForSecondsRealtime(3);
        moving = true;
        patrolDestination = destination;

        //look direction depends on where the patrol points is relative to the guard (can move around in chase mode)
        if (patrolDestination == 0) {
            lookDir = (Vector2)patrolPoints[0].position - (Vector2)transform.position;

        }
        else {
            lookDir = (Vector2)patrolPoints[1].position - (Vector2)transform.position;
        }
    }

    void Vision() {
  
        Vector2 playerDir = (Vector2)player.transform.position - (Vector2)transform.position;
        float playerAngle = Vector2.Angle(lookDir, playerDir);

        //find distance to player
        float playerDist = Vector3.Distance(player.transform.position, transform.position);

        bool playerInShadows = playerScript.getInShadows();

        if (playerAngle <= viewAngle && playerDist <= viewDistance) {
            RaycastHit2D hit = Physics2D.Raycast(transform.position, playerDir, playerDist, groundLayer);
            Debug.DrawRay(transform.position, playerDir, Color.green);
            if (!hit && !playerInShadows) {
                mode = "chaseMode";
                if (!canSeePlayer) {
                    audioSource.PlayOneShot(alert);
                }
                canSeePlayer = true;
                viewAngle = 60; //the guard can see more when they're actively searching
            }
            else {
                canSeePlayer = false;
                if (mode == "chaseMode") {
                    mode = "lostSightMode";
                    StartCoroutine(LostSight());
                }
            }
        }
        else {
            canSeePlayer = false;
            if (mode == "chaseMode") {
                mode = "lostSightMode";
                StartCoroutine(LostSight());
            }
        }
    }

    IEnumerator LostSight() {
        moving = false;
        if (hidingTimer < 0 && !canSeePlayer) {
            hidingTimer = 3;
        }

        while (hidingTimer >= 0) {
            yield return new WaitForSecondsRealtime(1);
            if (hidingTimer < 0 && !canSeePlayer) {
                hidingTimer = 3;
            }
            else if (hidingTimer == 0 && !canSeePlayer) {
                mode = "searchMode";
                viewAngle = 45; //the view angle reverts once they go back to patrolling
                hidingTimer = -1;
                moving = true;
                //reset the look direction
                if (patrolDestination == 0) {
                    lookDir = (Vector2)patrolPoints[0].position - (Vector2)transform.position;

                }
                else {
                    lookDir = (Vector2)patrolPoints[1].position - (Vector2)transform.position;
                }

            }
            else if (!canSeePlayer) {
                hidingTimer--;
                lookDir *= -1; //look back and forth
            }
            else {
                hidingTimer = -1;
                mode = "chaseMode";
                moving = true;
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D collision) {
        print("collided");
        if (collision.gameObject.tag == "Player") {
            uiController.GameOver();
        }
    }

    public void playFootstep() {
        audioSource.PlayOneShot(footstep);
    }
}
