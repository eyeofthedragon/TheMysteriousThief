using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using DG.Tweening;

public class UIController : MonoBehaviour {

    public Image fader;

    private void Start() {
        fadeIn();
    }
    public void Restart() {
        Time.timeScale = 1; //ensure time is turned on
        StartCoroutine(fadeOut("GameLevel"));
    }

    public void MainMenu() {
        Time.timeScale = 1; //turn time back on so that it will load even when the game is paused
        StartCoroutine(fadeOut("MainMenu"));
    }

    public void Quit() {
        Application.Quit();
    }

    public void GameOver() {
        StartCoroutine(fadeOut("GameOver"));
    }

    public void Credits() {
        StartCoroutine(fadeOut("Credits"));
    }

    IEnumerator fadeOut(string sceneName) {
        fader.DOFade(1, 0.5f);
        yield return new WaitForSeconds(0.5f);
        SceneManager.LoadScene(sceneName);
    }

    void fadeIn() {
        fader.DOFade(0, 0.5f);
    }
}
