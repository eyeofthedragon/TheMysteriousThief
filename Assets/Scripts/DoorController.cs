using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class DoorController : MonoBehaviour
{
    public bool isOpen = false;

    public void OpenDoor() {
        if (!isOpen) {
            transform.DOMoveY(transform.position.y + 2.5f, 0.2f);
            isOpen = true;
        }
    }
}
