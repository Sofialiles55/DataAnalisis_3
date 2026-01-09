using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TextPopUp : MonoBehaviour
{
    public string popupId = "Popup";
    public float cooldownSeconds = 2f;

    float lastTime = -999f;

    private void OnTriggerEnter(Collider other)
    {
        if (Eventlogger.Instance == null) return;

        // player detection
        if (!other.CompareTag("Player") && !other.transform.root.CompareTag("Player"))
            return;

        if (Time.time - lastTime < cooldownSeconds) return;
        lastTime = Time.time;

        // Log at player position
        Vector3 p = other.transform.root.position;
        Eventlogger.Instance.LogPopup(p, popupId);
    }
}