using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewBehaviourScript : MonoBehaviour
{
    public Eventlogger logger;          
    public string switchId = "";       
    public float cooldownSeconds = 1.0f;

    float lastLogTime = -999f;

    private void OnTriggerEnter(Collider other)
    {
        if (!logger) return;

       
        if (!other.CompareTag("Player") && !other.transform.root.CompareTag("Player"))
            return;

        if (Time.time - lastLogTime < cooldownSeconds)
            return;
        lastLogTime = Time.time;

        string id = string.IsNullOrWhiteSpace(switchId) ? gameObject.name : switchId;
        logger.LogSwitch(transform.position, id);
    }
}
