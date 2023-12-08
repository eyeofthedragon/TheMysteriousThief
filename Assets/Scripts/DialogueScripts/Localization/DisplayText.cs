using TMPro;
using UnityEngine;

public class DisplayText : MonoBehaviour {
    public DisplayInfo displayInfo;

    public void ResetLanguage() {
        SetText();
    }

    public virtual void SetText() {
        print("womp womp");
    }
}
