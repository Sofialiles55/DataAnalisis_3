[System.Serializable]
public class ServerEventDTO
{
    public string session_id;
    public string event_type;
    public float x;
    public float y;
    public float z;
    public long t;
    public string meta;
}

[System.Serializable]
public class ServerEventsResponse
{
    public ServerEventDTO[] events;
}
