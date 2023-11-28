using TMPro;
using UnityEngine;

public class DisplayText : MonoBehaviour {
    public TMP_Text displayText;
    public DisplayInfo displayInfo;

    public void Start() {
        displayText.text = displayInfo.currentLanguage.languageDict[displayInfo.displayKey.ToString()];
    }
}
