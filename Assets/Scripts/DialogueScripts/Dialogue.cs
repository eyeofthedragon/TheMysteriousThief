using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Dialogue", menuName = "ScriptableObjects/Dialogue")]
public class Dialogue : ScriptableObject {
    public string speakerName;
    public Sprite portrait;
    public AudioClip talkingClip;

    [TextArea(3, 10)]
    public string[] sentenceKeys;
}
