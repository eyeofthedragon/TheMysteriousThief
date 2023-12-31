using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using XNode;
using DG.Tweening;

public class DialogueManager : MonoBehaviour
{
    public TMP_Text dialogueText;
    public Button nextButton;
    public TMP_Text speakerName;

    public DisplayInfo displayInfo;

    public Vector3 showPanelPos = new Vector3(0, -1, 0);
    public Vector3 hidePanelPos = new Vector3(0, -400, 0);

    public float panelAnimationTime = 0.7f;
    public float textSpeed = 0.2f;

    public bool dialogueIsPlaying = false;

    Node currentNode;
    Queue<string> sentences;
    AudioSource source;
    AudioClip talkingClip;

    private void Start() {
        //set up audio
        sentences = new Queue<string>();
        source = GetComponent<AudioSource>();

    }

    private void Update() {
        if (dialogueIsPlaying && (Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.E))) {
            nextButton.onClick.Invoke();
        }
    }

    public void StartDialogue(Node rootNode) {
        StopAllCoroutines();
        dialogueIsPlaying = true;
        currentNode = rootNode;

        //check for types (either regular or end)
        if (currentNode.GetType() == typeof(DialogueEndNode)) {
            EndDialogue();
        }
        else {

            DialogueNode dialogueNode = currentNode as DialogueNode;
            Dialogue dialogue = dialogueNode.dialogue;

            //set panel
            speakerName.text = displayInfo.currentLanguage.languageDict[dialogue.speakerName];
            talkingClip = dialogue.talkingClip;

            //set buttons
            if (dialogueNode.responses != null) {
                nextButton.gameObject.SetActive(false);
            }
            else {
                nextButton.gameObject.SetActive(true);
            }

            sentences.Clear();
            for (int i = 0; i < dialogue.sentenceKeys.Length; i++) {
                sentences.Enqueue(displayInfo.currentLanguage.languageDict[dialogueNode.dialogue.sentenceKeys[i]]);
            }

            //bring up the panel
            transform.DOLocalMove(showPanelPos, panelAnimationTime).OnComplete(() => DisplaySentence());


        }
    }

    public void DisplaySentence() {
        StopAllCoroutines();
        StartCoroutine(RenderSentence(sentences.Dequeue()));
    }

    IEnumerator RenderSentence(string sentence) {
        dialogueText.text = "";
        char[] letters = sentence.ToCharArray();
        for (int i=0; i<letters.Length; i++) {
            dialogueText.text += letters[i];
            if (i % 4 == 0) {
                source.PlayOneShot(talkingClip);
            }
            yield return new WaitForSeconds(textSpeed);
        }
    }

    public void DisplayNext() {
        DialogueNode dialogueNode = currentNode as DialogueNode;
        NodePort port = dialogueNode.GetOutputPort("optionA").Connection;

        if (port != null) {
            currentNode = port.node;
        }

        StartDialogue(currentNode);
    }

    public void EndDialogue() {
        nextButton.gameObject.SetActive(false); //ensure that this doesn't get pressed by accident
        StopAllCoroutines();
        dialogueText.text = ""; //ensure that the old text doesn't show when the panel moves back up
        transform.DOLocalMove(hidePanelPos, panelAnimationTime).OnComplete(() => { dialogueIsPlaying = false;  });
    }
}
