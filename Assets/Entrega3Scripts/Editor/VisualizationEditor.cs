using UnityEditor;
using UnityEngine;
using System.Linq;

[CustomEditor(typeof(DataVisualizer))]
public class VisualizationEditor : Editor
{
    private void OnSceneGUI()
    {
        DataVisualizer v = (DataVisualizer)target;
        if (v.events == null || v.events.Count == 0) return;

        float y = 0.02f;

        // Death 
        if (v.showDeaths)
        {
            Handles.color = v.deathColor;
            foreach (var e in v.events.Where(e => e.type == "death"))
            {
                Handles.SphereHandleCap(0, e.pos + Vector3.up * y, Quaternion.identity, v.markerSize, EventType.Repaint);
            }
        }

    

        //Switch
        if (v.showSwitches)
        {
            Handles.color = v.switchColor;
            foreach (var e in v.events.Where(e => e.type == "switch"))
            {
                Handles.SphereHandleCap(
                    0,
                    e.pos + Vector3.up * 0.02f,
                    Quaternion.identity,
                    v.switchMarkerSize,
                    EventType.Repaint
                );
            }
        }

        // Collect path points once
        var pathPts = v.events
            .Where(e => e.type == "path")
            .OrderBy(e => e.t)
            .Select(e => e.pos + Vector3.up * y)
            .ToArray();

        // Movement path 
        if (v.showPath && pathPts.Length > 1)
        {
            Handles.color = v.pathColor;
            Handles.DrawAAPolyLine(v.pathWidth, pathPts);
        }

        // Heatmap 
        if (v.showHeatmap && pathPts.Length > 0)
        {
            
            var c = v.heatColor;
            float a = Mathf.Clamp01(v.heatAlpha * v.heatIntensity);
            Handles.color = new Color(c.r, c.g, c.b, a);

            int step = Mathf.Max(1, v.heatDownsample);
            for (int i = 0; i < pathPts.Length; i += step)
            {
                Handles.DrawSolidDisc(pathPts[i], Vector3.up, v.heatRadius);
            }
        }

        //Text popups
        if (v.showPopups)
        {
            Handles.color = v.popupColor;
            foreach (var e in v.events.Where(e => e.type == "popup"))
            {
                Handles.SphereHandleCap(
                    0,
                    e.pos + Vector3.up * 0.02f,
                    Quaternion.identity,
                    v.popupMarkerSize,
                    EventType.Repaint
                );
            }
        }

    }
}
