using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenuController : MonoBehaviour {
    public Canvas pauseMenu;
    public AudioSource gameMusic;
    public AudioClip pauseOpen;
    public AudioClip pauseClose;
    
    AudioSource soundEffectSource;
    
    bool pauseMenuOpen = false;

    private void Start() {
        pauseMenu.enabled = false;
        soundEffectSource = GetComponent<AudioSource>();
    }

    private void Update() {
        if (Input.GetKeyDown(KeyCode.Escape) && !pauseMenuOpen) {
            pauseMenuOpen = true;
            Time.timeScale = 0;
            pauseMenu.enabled = true;
            gameMusic.Pause();
            soundEffectSource.PlayOneShot(pauseOpen);
        }
        else if (Input.GetKeyDown(KeyCode.Escape) && pauseMenuOpen) {
            CloseMenu();
        }
    }

    public void CloseMenu() {
        pauseMenuOpen = false;
        Time.timeScale = 1;
        pauseMenu.enabled = false;
        soundEffectSource.PlayOneShot(pauseClose);
        gameMusic.Play();
    }
}
