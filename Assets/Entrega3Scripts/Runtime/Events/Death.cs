using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Death : MonoBehaviour
{
    public Eventlogger logger;

    [Header("Detection")]
    public float teleportDistance = 6f;   
    public float cooldownSeconds = 1.0f;

    Vector3 lastPos;
    float lastLogTime = -999f;

    void Start()
    {
        lastPos = transform.position;
    }

    void Update()
    {
        if (!logger) return;

        float moved = Vector3.Distance(transform.position, lastPos);

        if (moved >= teleportDistance && Time.time - lastLogTime >= cooldownSeconds)
        {
            lastLogTime = Time.time;

            // Log death at position before respawn
            logger.LogDeath(lastPos, "respawn");
        }

        lastPos = transform.position;
    }
}
