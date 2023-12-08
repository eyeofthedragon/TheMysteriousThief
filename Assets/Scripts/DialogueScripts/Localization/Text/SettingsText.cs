using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class SettingsText : DisplayText {
    public TMP_Text language;
    public TMP_Text english;
    public TMP_Text french;

    private void Start() {
        SetText();
    }

    public override void SetText() {
        language.text = displayInfo.currentLanguage.languageDict["language"];
        english.text = displayInfo.currentLanguage.languageDict["english"];
        french.text = displayInfo.currentLanguage.languageDict["french"];
    }

}
