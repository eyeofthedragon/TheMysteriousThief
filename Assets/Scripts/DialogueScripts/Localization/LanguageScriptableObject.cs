using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObjects/Language", order=1)]
public class LanguageScriptableObject : ScriptableObject {
    public string langName;
    public StringStringDictionary languageDict;
}
