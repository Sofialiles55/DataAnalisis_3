using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Eventlogger : MonoBehaviour
{

    public DataVisualizer visualizer;
    public Transform player;
    public float sampleInterval = 0.5f;

    float timer;

    void Update()
    {
        if (!visualizer || !player) return;

        timer += Time.deltaTime;
        if (timer >= sampleInterval)
        {
            timer = 0f;
            visualizer.events.Add(new EventScript
            {
                type = "path",
                pos = player.position,
                t = System.DateTimeOffset.UtcNow.ToUnixTimeSeconds(),
                meta = ""
            });
        }

        if (Input.GetKeyDown(KeyCode.E))
        {
            visualizer.events.Add(new EventScript
            {
                type = "button",
                pos = player.position,
                t = System.DateTimeOffset.UtcNow.ToUnixTimeSeconds(),
                meta = "E"
            });
        }
    }

    public void LogSwitch(Vector3 pos, string id = "")
    {
        if (!visualizer) return;

        visualizer.events.Add(new EventScript
        {
            type = "switch",
            pos = pos,
            t = System.DateTimeOffset.UtcNow.ToUnixTimeSeconds(),
            meta = id
        });
    }
    public void LogDeath(Vector3 pos, string meta = "")
    {
        if (!visualizer) return;

        visualizer.events.Add(new EventScript
        {
            type = "death",
            pos = pos,
            t = System.DateTimeOffset.UtcNow.ToUnixTimeSeconds(),
            meta = meta
        });
    }
    public void LogPopup(Vector3 pos, string id = "")
    {
        if (!visualizer) return;

        visualizer.events.Add(new EventScript
        {
            type = "popup",
            pos = pos,
            t = System.DateTimeOffset.UtcNow.ToUnixTimeSeconds(),
            meta = id
        });
    }

}
