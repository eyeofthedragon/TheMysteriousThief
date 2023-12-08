using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class MainMenuText : DisplayText {
    public TMP_Text theMysteriousThief;
    public TMP_Text startGame;
    public TMP_Text quit;

    private void Start() {
        SetText();
    }

    public override void SetText() {
        theMysteriousThief.text = displayInfo.currentLanguage.languageDict["theMysteriousThief"];
        startGame.text = displayInfo.currentLanguage.languageDict["startGame"];
        quit.text = displayInfo.currentLanguage.languageDict["quit"];
    }
}
