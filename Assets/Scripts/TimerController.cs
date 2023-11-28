using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TimerController : MonoBehaviour {
    public float timeLeft;
    public int numMinutes = 2;
    public TMP_Text timeText;
    public DialogueManager dialogueManager;
    public UIController uiController;
    
    private void Start() {
        timeLeft = numMinutes * 60;
    }

    private void Update() {
        if (!dialogueManager.dialogueIsPlaying) {

            timeLeft -= Time.deltaTime;
            TimeSpan time = TimeSpan.FromSeconds(timeLeft);
            timeText.text = time.ToString("mm':'ss");

            if (timeLeft < 10) {
                timeText.color = Color.red;
            }

            if (timeLeft <= 0) {
                uiController.GameOver();
            }
        }
    }
}
