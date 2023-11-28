using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XNode;

public class DialogueTrigger : MonoBehaviour {
    public DialogueGraph tree;

    bool alreadyPlayed = false;


    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.gameObject.tag == "Player" && !alreadyPlayed) {
            TriggerDialogue();
            alreadyPlayed = true;
        }
    }

    public void TriggerDialogue() {
        FindAnyObjectByType<DialogueManager>().StartDialogue(tree.nodes[0]);
    }
}
