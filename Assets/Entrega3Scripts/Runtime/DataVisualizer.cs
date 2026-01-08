using System.Collections.Generic;
using UnityEngine;

public class DataVisualizer : MonoBehaviour
{
    [Header("Toggles")]
    public bool showPath = true;
    public bool showHeatmap = true;
    public bool showDeaths = true;
    public bool showButtons = true;
    public bool showSwitches = true;

    [Header("Path")]
    public Color pathColor = Color.cyan;
    public float pathWidth = 3f;

    [Header("Death")]
    public Color deathColor = Color.red;
    public float markerSize = 0.3f;

    [Header("Heatmap")]
    public Color heatColor = Color.red;
    public float heatRadius = 1.5f;
    public float heatAlpha = 0.05f;
    public float heatIntensity = 1f;
    [Range(1, 10)] public int heatDownsample = 1;

    [Header("Data")]
    public List<EventScript> events = new();

    [Header("Switches")]
    public Color switchColor = Color.magenta;
    public float switchMarkerSize = 0.35f;

    [Header("Runtime options")]
    public bool clearEventsOnPlay = true;

    private void Awake()
    {
        if (clearEventsOnPlay)
            events.Clear();
    }
}
