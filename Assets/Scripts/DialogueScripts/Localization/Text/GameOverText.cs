using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameOverText : DisplayText {
    public TMP_Text gameOver;
    public TMP_Text restart;
    public TMP_Text mainMenu;

    private void Start() {
        SetText();
    }

    public override void SetText() {
        gameOver.text = displayInfo.currentLanguage.languageDict["gameOver"];
        restart.text = displayInfo.currentLanguage.languageDict["restart"];
        mainMenu.text = displayInfo.currentLanguage.languageDict["mainMenu"];
    }
}
