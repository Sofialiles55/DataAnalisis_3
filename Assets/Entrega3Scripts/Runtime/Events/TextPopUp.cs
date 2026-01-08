using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TextPopUp : MonoBehaviour
{
    public Eventlogger logger;
    public string popupId = "Popup";
    public float cooldownSeconds = 2f;

    float lastTime = -999f;

    private void OnTriggerEnter(Collider other)
    {
        if (!logger) return;

        // player detection (tag recommended)
        if (!other.CompareTag("Player") && !other.transform.root.CompareTag("Player"))
            return;

        if (Time.time - lastTime < cooldownSeconds) return;
        lastTime = Time.time;

        // Log at player position (best for “where user saw it”)
        Vector3 p = other.transform.root.position;
        logger.LogPopup(p, popupId);
    }
}
