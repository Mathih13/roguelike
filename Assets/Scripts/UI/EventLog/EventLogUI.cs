using UnityEngine;
using System.Collections;
using mhartveit;

public class EventLogUI : MonoBehaviour, UIElementCollection
{
    public EventLogPanelUI EventLogPanel;

    private void Awake()
    {
        EventLogPanel = GetComponent<EventLogPanelUI>();
    }
}

public class EventLogMessage
{
    public IEventLogItem eventMessage {get; set; }
    public GameObject GameObject { get; set; }
}