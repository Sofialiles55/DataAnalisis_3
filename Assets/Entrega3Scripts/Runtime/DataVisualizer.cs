using System.Collections.Generic;
using UnityEngine;

public class DataVisualizer : MonoBehaviour
{
    [Header("Toggles")]
    public bool showPath = true;
    public bool showHeatmap = true;
    public bool showDeaths = true;
    public bool showButtons = true;

    [Header("Path")]
    public Color pathColor = Color.cyan;
    public float pathWidth = 3f;

    [Header("Markers")]
    public Color deathColor = Color.red;
    public Color buttonColor = Color.yellow;
    public float markerSize = 0.3f;

    [Header("Heatmap")]
    public Color heatColor = Color.red;
    public float heatRadius = 1.5f;
    public float heatAlpha = 0.05f;
    public float heatIntensity = 1f;
    [Range(1, 10)] public int heatDownsample = 1;

    [Header("Data")]
    public List<EventScript> events = new();

  
    // Dummy data for testing 
 
    [ContextMenu("Generate Dummy Events")]
    public void GenerateDummyEvents()
    {
        events.Clear();

        // Death
        events.Add(new EventScript
        {
            type = "death",
            pos = transform.position + new Vector3(2, 0, 2),
            t = 1,
            meta = ""
        });

        // Buttons
        events.Add(new EventScript
        {
            type = "button",
            pos = transform.position + new Vector3(-2, 0, 1),
            t = 2,
            meta = "E"
        });

        events.Add(new EventScript
        {
            type = "button",
            pos = transform.position + new Vector3(0, 0, -3),
            t = 3,
            meta = "E"
        });

        // Path
        for (int i = 0; i < 120; i++)
        {
            events.Add(new EventScript
            {
                type = "path",
                pos = transform.position + new Vector3(i * 0.2f, 0, Mathf.Sin(i * 0.15f) * 2f),
                t = 10 + i,
                meta = ""
            });
        }

        // Heatmap
        for (int i = 0; i < 150; i++)
        {
            events.Add(new EventScript
            {
                type = "path",
                pos = transform.position + new Vector3(
                    8f + Random.Range(-1f, 1f),
                    0,
                    2f + Random.Range(-1f, 1f)
                ),
                t = 200 + i,
                meta = ""
            });
        }
    }
}
