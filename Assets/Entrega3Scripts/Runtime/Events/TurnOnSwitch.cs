using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewBehaviourScript : MonoBehaviour
{
    public string switchId = "";        
    public float cooldownSeconds = 1.0f;

    float lastLogTime = -999f;

    private void OnTriggerEnter(Collider other)
    {
        
        if (Eventlogger.Instance == null) return;

        // Player detection
        if (!other.CompareTag("Player") && !other.transform.root.CompareTag("Player"))
            return;

        // Prevent spam
        if (Time.time - lastLogTime < cooldownSeconds)
            return;

        lastLogTime = Time.time;

       
        string id = string.IsNullOrWhiteSpace(switchId) ? gameObject.name : switchId;

        // Log switch activation
        Eventlogger.Instance.LogSwitch(transform.position, id);
    }
}
