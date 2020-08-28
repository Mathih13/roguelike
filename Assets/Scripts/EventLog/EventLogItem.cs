using UnityEngine;
using System.Collections;

public interface IEventLogItem
{
    string GetEventLogBody();
}

public class EventLogHubMessage : IEventLogItem
{
    private string body;

    public EventLogHubMessage(string message)
    {
        body = message;
    }

    public string GetEventLogBody() => body;
}
