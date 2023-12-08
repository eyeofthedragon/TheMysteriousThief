using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PauseMenuText : DisplayText {
    public TMP_Text paused;
    public TMP_Text resume;
    public TMP_Text restart;
    public TMP_Text mainMenu;

    private void Start() {
        SetText();
    }

    public override void SetText() {
        paused.text = displayInfo.currentLanguage.languageDict["paused"];
        resume.text = displayInfo.currentLanguage.languageDict["resume"];
        restart.text = displayInfo.currentLanguage.languageDict["restart"];
        mainMenu.text = displayInfo.currentLanguage.languageDict["mainMenu"];
    }
}
