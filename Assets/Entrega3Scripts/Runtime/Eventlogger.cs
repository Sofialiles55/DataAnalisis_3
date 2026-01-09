using UnityEngine;
using UnityEngine.Networking;
using System.Text;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System;




public class Eventlogger : MonoBehaviour
{

    public static Eventlogger Instance { get; private set; }

    [Header("Auto-wired (no dragging needed)")]
    public DataVisualizer visualizer;
    public Transform player;

    [Header("Path Sampling")]
    public float sampleInterval = 0.5f;

    [Header("Session")]
    public string sessionId;

    [Header("Server Upload")]
    public string serverUploadUrl = "http://localhost/telemetry/uploadEvents.php";
    public bool enableServerUpload = false;

    [Header("Server Download")]
    public string serverDownloadUrl = "http://localhost/telemetry/downloadEvents.php";
    public bool enableServerDownload = false;

    [Tooltip("Session ID to download from server (set this in inspector)")]
    public string downloadSessionId = "";

    [Header("Server Filter")]
    public TelemetryEventType selectedEventType = TelemetryEventType.All;


    public bool clearEventsBeforeDownload = true;


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

        if (string.IsNullOrEmpty(sessionId))
            sessionId = System.DateTime.Now.ToString("yyyy-MM-dd_HH-mm-ss");
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

    //Upload MYSQL
    IEnumerator UploadEventsCoroutine(List<EventScript> events)
    {
        foreach (var e in events)
        {
            ServerEventDTO dto = new ServerEventDTO
            {
                session_id = sessionId,
                event_type = e.type,
                x = e.pos.x,
                y = e.pos.y,
                z = e.pos.z,
                t = e.t,
                meta = e.meta
            };

            string json = JsonUtility.ToJson(dto);
            Debug.Log("UPLOAD JSON:\n" + json);

            if (!enableServerUpload)
                continue;

            byte[] body = Encoding.UTF8.GetBytes(json);

            UnityWebRequest req = new UnityWebRequest(serverUploadUrl, "POST");
            req.uploadHandler = new UploadHandlerRaw(body);
            req.downloadHandler = new DownloadHandlerBuffer();
            req.SetRequestHeader("Content-Type", "application/json");

            yield return req.SendWebRequest();

            if (req.result != UnityWebRequest.Result.Success)
                Debug.LogError("Upload failed: " + req.error);
            else
                Debug.Log("Upload success");
        }
    }

    public void UploadFiltered()
    {
        if (!visualizer) return;

        string filter = selectedEventType == TelemetryEventType.All
            ? null
            : selectedEventType.ToString();

        var filteredEvents = visualizer.events
            .Where(e => filter == null || e.type == filter)
            .ToList();

        StartCoroutine(UploadEventsCoroutine(filteredEvents));
    }


    //Download MYSQL
    IEnumerator DownloadFilteredCoroutine(string sid)
    {
        string typeParam = selectedEventType == TelemetryEventType.All
            ? ""
            : "&event_type=" + selectedEventType.ToString();

        string url = serverDownloadUrl
            + "?session_id=" + UnityWebRequest.EscapeURL(sid)
            + typeParam;

        Debug.Log("DOWNLOAD URL: " + url);

        if (!enableServerDownload)
        {
            Debug.LogWarning("enableServerDownload = false (no server call)");
            yield break;
        }

        using (UnityWebRequest req = UnityWebRequest.Get(url))
        {
            yield return req.SendWebRequest();

            if (req.result != UnityWebRequest.Result.Success)
            {
                Debug.LogError("Download failed: " + req.error);
                yield break;
            }

            string json = req.downloadHandler.text;
            Debug.Log("DOWNLOAD JSON RAW:\n" + json);

            ServerEventsResponse resp =
                JsonUtility.FromJson<ServerEventsResponse>(json);

            if (resp == null || resp.events == null)
            {
                Debug.LogWarning("No events received.");
                yield break;
            }

            if (clearEventsBeforeDownload)
                visualizer.events.Clear();

            foreach (var e in resp.events)
            {
                visualizer.events.Add(new EventScript
                {
                    type = e.event_type,
                    pos = new Vector3(e.x, e.y, e.z),
                    t = e.t,
                    meta = e.meta
                });
            }

            Debug.Log($"Downloaded {resp.events.Length} events");
        }
    }


    public void DownloadFiltered()
    {
        if (!visualizer) return;

        string sid = string.IsNullOrWhiteSpace(downloadSessionId)
            ? sessionId
            : downloadSessionId;

        if (string.IsNullOrWhiteSpace(sid))
        {
            Debug.LogWarning("No session id to download.");
            return;
        }

        StartCoroutine(DownloadFilteredCoroutine(sid));
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

    public void LogSwitch(Vector3 pos, string id = "") => LogEvent("switch_event", pos, id);
    public void LogDeath(Vector3 pos, string meta = "") => LogEvent("death", pos, meta);
    public void LogPopup(Vector3 pos, string id = "") => LogEvent("popup", pos, id);
    public void LogEnemyDeath(Vector3 pos, string id = "") => LogEvent("enemy_death", pos, id);
}
