using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Death : MonoBehaviour
{
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
        if (Eventlogger.Instance == null) return;

        float moved = Vector3.Distance(transform.position, lastPos);

        if (moved >= teleportDistance && Time.time - lastLogTime >= cooldownSeconds)
        {
            lastLogTime = Time.time;

            // Log death at position before respawn
            Eventlogger.Instance.LogDeath(lastPos, "respawn");
        }

        lastPos = transform.position;
    }
}
