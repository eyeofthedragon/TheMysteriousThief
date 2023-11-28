using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundEffects : MonoBehaviour {

    AudioSource audioSource;
    public AudioClip footstep;
    public AudioClip jump;

    private void Start() {
        audioSource = GetComponent<AudioSource>();
    }
    public void playFootstep() {
        audioSource.PlayOneShot(footstep);
    }

    public void playJump() {
        audioSource.PlayOneShot(jump);
    }
}
