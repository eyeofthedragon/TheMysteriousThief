using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorMechanismController : MonoBehaviour
{
    public GameObject door;
    public GameObject player;

    private bool playerInRange;
    private PlayerMovement playerController;

    private void Start() {
        playerController = player.GetComponent<PlayerMovement>();
    }

    private void Update() {
       if (playerInRange) {
            if (Input.GetKeyDown(KeyCode.E) && playerController.hasDoorKey) {
                door.GetComponent<DoorController>().OpenDoor();
            }
       }
    }

    private void OnTriggerEnter2D(Collider2D collision) {
        print("collide");
        if (collision.gameObject.tag == "Player") {
            playerInRange = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision) {
        if (collision.gameObject.tag == "Player") {
            playerInRange = false;
        }
    }
}
