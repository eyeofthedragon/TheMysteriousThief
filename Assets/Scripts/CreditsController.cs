using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using UnityEngine.SceneManagement;

public class CreditsController : MonoBehaviour {
    public TMP_Text credits1;
    public TMP_Text credits2;
    public TMP_Text creditsTitle1;
    public TMP_Text creditsTitle2;
    public Image lights;
    public AudioClip flutePerformance;
    public AudioClip lightsOff;
    public AudioClip lightsOn;
    public UIController uiController;

    Animator animator;
    AudioSource audioSource;
    SpriteRenderer playerSprite;


    private float timer = 0;

    private bool[] eventsTriggered = new bool[13];

    private void Start() {
        credits1.alpha = 0;
        credits2.alpha = 0;

        creditsTitle1.alpha = 0;
        creditsTitle2.alpha = 0;

        animator = GetComponent<Animator>();
        playerSprite = GetComponent<SpriteRenderer>();
        audioSource = GetComponent<AudioSource>();

        audioSource.PlayOneShot(flutePerformance);
    }

    private void Update() {
        timer += Time.deltaTime;

        //Order of events:
        //Programming, Design, Sprites, Writing by Eileen Carette
        //Background Tiles 
            // Medieval pixel art asset by Blackspire
            // Classic Legacy Pack - Village by Anomaly Pixel
        // Background Art
            // 2D Neighborhood by Afnan Mahin
            // En scène by Anne Contet
        // UI
            // SteampunkUI by Gentleland
        // Tools
            // SerializableDictionary by Mathieu Le Ber
            // DOTween by Demigiant
            // xNode by Siccity
        // Sound
            // Dialog Text Sound Effects by Alan Dalcastagne
            // RPG Essentials Sound Effects by leohpaz
            // Spotlight by realsoundFX
            // Big switch sound effect by Her Ses
        // Music
            // Parallax Mix (New) 4 by Hayden Folker
            // Horde Of Geese by Alexander Nakarada (www.creatorchords.com)
            // Allegretto by B. Godard, performed by Ion Bogdan Stefanescu
            // Game Over - Repeating Dream by Yakikaze
        // Special Thanks
            // Gateway Games LARP

        //Lights off
        //Lights on, showtime
        //Lights off
        //Lights on, empty stage
        //Fade to main menu

        //Song is 88 seconds


        if (timer > 102) {
            uiController.MainMenu();
        }
        else if (timer > 99 && !eventsTriggered[12]) {
            playerSprite.enabled = false;
            lights.DOFade(0, 8);
            eventsTriggered[12] = true;
        }
        else if (timer > 95 && !eventsTriggered[11]) {
            turnLightsOff();
            eventsTriggered[11] = true;
        }
        else if (timer > 89 && !eventsTriggered[10]) {
            turnLightsOn();
            eventsTriggered[10] = true;
        }
        else if (timer > 86.5 && !eventsTriggered[9]) {
            turnLightsOff();
            eventsTriggered[9] = true;
        }
        else if (timer > 81 && !eventsTriggered[8]) {
            credits2.DOFade(0, 3);
            creditsTitle2.DOFade(0, 3);
            eventsTriggered[8] = true;
        }
        else if (timer > 72 && !eventsTriggered[7]) {
            credits1.DOFade(0, 3);
            creditsTitle1.DOFade(0, 3);
            creditsTitle2.text = "SPECIAL THANKS";
            credits2.text = "Gateway Games LARP";
            creditsTitle2.DOFade(1, 3);
            credits2.DOFade(1, 3);
            eventsTriggered[7] = true;
        }
        else if (timer > 62 && !eventsTriggered[6]) {
            credits2.DOFade(0, 3);
            creditsTitle2.DOFade(0, 3);
            creditsTitle1.text = "MUSIC";
            credits1.text = "Parallax Mix (New) 4 by Hayden Folker\nHorde Of Geese by Alexander Nakarada (www.creatorchords.com)\nGame Over - Repeating Dream by Yakikaze\nAllegretto by B. Godard, performed by Ion Bogdan Stefanescu";
            creditsTitle1.DOFade(1, 3);
            credits1.DOFade(1, 3);
            eventsTriggered[6] = true;

        }
        else if (timer > 52 && !eventsTriggered[5]) {
            credits1.DOFade(0, 3);
            creditsTitle1.DOFade(0, 3);
            creditsTitle2.text = "SOUND";
            credits2.text = "Dialog Text Sound Effects by Alan Dalcastagne\nRPG Essentials Sound Effects by leohpaz\nSpotlight by realsoundFX\nBig switch sound effect by Her Ses";
            creditsTitle2.DOFade(1, 3);
            credits2.DOFade(1, 3);
            eventsTriggered[5] = true;
        }
        else if (timer > 42 && !eventsTriggered[4]) {
            credits2.DOFade(0, 3);
            creditsTitle2.DOFade(0, 3);
            creditsTitle1.text = "TOOLS";
            credits1.text = "SerializableDictionary by Mathieu Le Ber\nDOTween by Demigiant\nxNode by Siccity";
            creditsTitle1.DOFade(1, 3);
            credits1.DOFade(1, 3);
            eventsTriggered[4] = true;
        }
        else if (timer > 32 && !eventsTriggered[3]) {
            credits1.DOFade(0, 3);
            creditsTitle1.DOFade(0, 3);
            creditsTitle2.text = "UI ASSETS";
            credits2.text = "SteampunkUI by Gentleland";
            creditsTitle2.DOFade(1, 3);
            credits2.DOFade(1, 3);
            eventsTriggered[3] = true;
        }
        else if (timer > 22 && !eventsTriggered[2]) {
            credits2.DOFade(0, 3);
            creditsTitle2.DOFade(0, 3);
            creditsTitle1.text = "BACKGROUND ART";
            credits1.text = "2D Neighborhood by Afnan Mahin\nEn scène by Anne Contet";
            creditsTitle1.DOFade(1, 3);
            credits1.DOFade(1, 3);
            eventsTriggered[2] = true;
        }
        else if (timer > 12 && !eventsTriggered[1]) {
            credits1.DOFade(0, 3);
            creditsTitle1.DOFade(0, 3);
            creditsTitle2.text = "BACKGROUND TILES";
            credits2.text = "Medieval pixel art asset by Blackspire\nClassic Legacy Pack - Village by Anomaly Pixel";
            creditsTitle2.DOFade(1, 3);
            credits2.DOFade(1, 3);
            eventsTriggered[1] = true;
        }
        else if (timer > 2 && !eventsTriggered[0]) {
            creditsTitle1.text = "PROGRAMMING, DESIGN, SPRITES, WRITING";
            credits1.text = "Eileen Carette";
            creditsTitle1.DOFade(1, 3);
            credits1.DOFade(1, 3);
            eventsTriggered[0] = true;
        }

    }

    void turnLightsOff() {
        lights.DOFade(1, 0.3f);
        audioSource.PlayOneShot(lightsOff);
    }

    void turnLightsOn() {
        audioSource.PlayOneShot(lightsOn);
        lights.DOFade(0, 0.3f);
        animator.SetBool("grandReveal", true);
    }

}
