using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Eventlogger : MonoBehaviour
{

    public static Eventlogger Instance { get; private set; }

    [Header("Auto-wired (no dragging needed)")]
    public DataVisualizer visualizer;
    public Transform player;

    [Header("Path Sampling")]
    public float sampleInterval = 0.5f;

    float timer;

    void Awake()
    {
      
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
    }

    void Start()
    {
        AutoWire();
    }

    void AutoWire()
    {
       
        if (!visualizer)
            visualizer = GetComponent<DataVisualizer>();

     
        if (!player)
        {
            var go = GameObject.FindGameObjectWithTag("Player");
            if (go) player = go.transform;
        }

      
        if (!player)
        {
            var ellen = GameObject.Find("Ellen");
            if (ellen) player = ellen.transform;
        }
    }

    void Update()
    {
      
        if (!visualizer || !player)
        {
            AutoWire();
            if (!visualizer || !player) return;
        }

        timer += Time.deltaTime;
        if (timer >= sampleInterval)
        {
            timer = 0f;
            LogEvent("path", player.position, "");
        }

        if (Input.GetKeyDown(KeyCode.E))
        {
            LogEvent("button", player.position, "E");
        }
    }

    // Logger
    public void LogEvent(string type, Vector3 pos, string meta = "")
    {
        if (!visualizer) return;

        visualizer.events.Add(new EventScript
        {
            type = type,
            pos = pos,
            t = System.DateTimeOffset.UtcNow.ToUnixTimeSeconds(),
            meta = meta
        });
    }

    public void LogSwitch(Vector3 pos, string id = "") => LogEvent("switch", pos, id);
    public void LogDeath(Vector3 pos, string meta = "") => LogEvent("death", pos, meta);
    public void LogPopup(Vector3 pos, string id = "") => LogEvent("popup", pos, id);

}
