using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.Drawing.Printing;

public class LanguageWindow : EditorWindow {

    public LanguageScriptableObject[] listLanguages;

    public LanguageScriptableObject currentLanguage;

    public DisplayInfo displayText;
    int languageIndex = 0;
    int keyIndex = 0;

    bool addLanguageMode = false;
    string newLanguageName = "";

    bool addKeyMode = false;
    string newKey = "";
    string newValue = "";

    bool editKeysMode = false;

    Vector2 scrollPos;

    [MenuItem("Window/Localization")]
    static public void ShowLanguageWindow() {
        LanguageWindow window = (LanguageWindow)GetWindow(typeof(LanguageWindow));
        window.Show();
    }

    private void OnGUI() {
        GUILayout.Label("Settings");

        // Load data
        LoadLanguages();
        int numLanguages = listLanguages.Length;
        string[] listLanguageNames = new string[numLanguages];

        for (int i=0; i<listLanguages.Length; i++) {
            //load the language names
            listLanguageNames[i] = listLanguages[i].langName;

            //ensure that any changes to any languages are saved
            EditorUtility.SetDirty(listLanguages[i]);
        }

        string[] keys = new List<string>(currentLanguage.languageDict.Keys).ToArray();
        string[] currentKeys = new List<string>(currentLanguage.languageDict.Keys).ToArray(); //used to keep track of keys when editing

        // Display language selection
        //reset the language in case it was changed in game
        currentLanguage = displayText.currentLanguage;
        languageIndex = System.Array.IndexOf(listLanguages, currentLanguage);

        EditorGUILayout.BeginHorizontal();
        GUILayout.Label("Language");
        languageIndex = EditorGUILayout.Popup(languageIndex, listLanguageNames);
        currentLanguage = listLanguages[languageIndex];
        EditorGUILayout.EndHorizontal();

        //set the current on-screen language
        displayText.currentLanguage = currentLanguage;

        

        if (!addLanguageMode && GUILayout.Button("Add language")) {
            addLanguageMode = true;
        }

        if (addLanguageMode) { 
            EditorGUILayout.BeginHorizontal();
            GUILayout.Label("Language Name:");
            newLanguageName = GUILayout.TextField(newLanguageName);
            EditorGUILayout.EndHorizontal();

            if (GUILayout.Button("Create")) {
                LanguageScriptableObject newLanguage = LanguageScriptableObject.CreateInstance<LanguageScriptableObject>();
                newLanguage.langName = newLanguageName;
                newLanguage.languageDict = new StringStringDictionary(currentLanguage.languageDict);

                string filename = newLanguageName + "Keys";
                string path = "Assets/Resources/Languages/" + filename + ".asset";
                AssetDatabase.CreateAsset(newLanguage, path);

                addLanguageMode = false;
            }
        }




        // Display text selection
        if (!addLanguageMode) {

            EditorGUILayout.BeginHorizontal();
            GUILayout.Label("Text to display");
            //if the user deletes the key that is currently selected, default to the first key in the list
            if (keyIndex < keys.Length) {
                keyIndex = EditorGUILayout.Popup(keyIndex, keys);
                displayText.displayKey = keys[keyIndex];
            }
            else {
                keyIndex = EditorGUILayout.Popup(0, keys);
                displayText.displayKey = keys[0];
            }
            EditorGUILayout.EndHorizontal();


            GUILayout.Label("Text strings");
            scrollPos = EditorGUILayout.BeginScrollView(scrollPos);
            // Display the keys and values for each entry in the dictionary
            for (int i = 0; i < currentLanguage.languageDict.Count; i++) {
                EditorGUILayout.BeginHorizontal();

                if (editKeysMode) {
                    keys[i] = GUILayout.TextField(keys[i]);
                    GUILayout.Label(currentLanguage.languageDict[currentKeys[i]]);
                    UpdateKeys(keys[i], currentKeys[i], listLanguages);
                }
                else {
                    GUILayout.Label(keys[i]);
                    currentLanguage.languageDict[keys[i]] = GUILayout.TextField(currentLanguage.languageDict[keys[i]]);

                    if (GUILayout.Button("Remove")) {
                        //remove the entry for all languages
                        for (int j = 0; j < listLanguages.Length; j++) {
                            listLanguages[j].languageDict.Remove(keys[i]);
                        }
                    }
                }
                EditorGUILayout.EndHorizontal();
            }
            EditorGUILayout.EndScrollView();

            if (!editKeysMode && !addKeyMode && GUILayout.Button("Add new entry")) {
                addKeyMode = true;
            }

            if (!editKeysMode && !addKeyMode && GUILayout.Button("Edit keys")) {
                editKeysMode = true;
            }

            if (editKeysMode && GUILayout.Button("Exit edit keys")) {
                editKeysMode = false;
            }

            if (addKeyMode) {
                AddNewKey(listLanguages);
            }
        }
    }

    void AddNewKey(LanguageScriptableObject[] listLanguages) {
        GUILayout.BeginHorizontal();
        GUILayout.Label("Enter key:");
        newKey = GUILayout.TextField(newKey);
        GUILayout.EndHorizontal();

        GUILayout.BeginHorizontal();
        GUILayout.Label("Enter value:");
        newValue = GUILayout.TextField(newValue);
        GUILayout.EndHorizontal();

        //all languages have the same keys so it doesn't matter which we check
        if (listLanguages[0].languageDict.ContainsKey(newKey)) {
            EditorGUILayout.HelpBox("Key must be unique.", MessageType.Error);
        }
        else {
            if (GUILayout.Button("Create")) {
                for (int i = 0; i < listLanguages.Length; i++) {
                    //The entered value will be added to all languages as the default value
                    listLanguages[i].languageDict.Add(newKey, newValue);
                    addKeyMode = false;
                }
            }
        }
    }

    void UpdateKeys(string newKey, string oldKey, LanguageScriptableObject[] langs) {
        for (int i=0; i<langs.Length; i++) {
            string value = langs[i].languageDict[oldKey];
            langs[i].languageDict.Remove(oldKey);
            langs[i].languageDict.Add(newKey, value);
        }
    }

    void LoadLanguages() {
        Object[] langObjects = Resources.LoadAll("Languages");
        listLanguages = new LanguageScriptableObject[langObjects.Length];
        langObjects.CopyTo(listLanguages, 0);
    }
}
