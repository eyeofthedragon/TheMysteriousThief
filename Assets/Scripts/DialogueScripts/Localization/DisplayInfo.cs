using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObjects/DisplayInfo", order = 2)]
public class DisplayInfo : ScriptableObject {
    public LanguageScriptableObject currentLanguage;
    public string displayKey;
}
