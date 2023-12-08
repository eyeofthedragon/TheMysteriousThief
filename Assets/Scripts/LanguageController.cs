using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LanguageController : MonoBehaviour
{
    public DisplayInfo displayInfo;
    public DisplayText displayText;

    public LanguageScriptableObject englishKeys;
    public LanguageScriptableObject frenchKeys;

    public void SelectEnglish() {
        displayInfo.currentLanguage = englishKeys;
        displayText.ResetLanguage();
    }

    public void SelectFrench() {
        displayInfo.currentLanguage = frenchKeys;
        displayText.ResetLanguage();
    }
}
