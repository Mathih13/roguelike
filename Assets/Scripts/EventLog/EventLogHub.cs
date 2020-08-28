using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;
using mhartveit;

public class EventLogHub : Singleton<EventLogHub>
{
    public int MaxEvents = 20;
    private EventLogUI ui;

    public void Start()
    {
        Events = new Queue<EventLogItemData>();
        ui = UI.Instance.GetUIElement<EventLogUI>();
        ui.EventLogPanel.Initialize(Events);
    }

    public void NewEventLogItem(EventLogItemData item)
    {
        if (Events != null)
        {
            if (Events.Count == MaxEvents)
            {
                Events.Dequeue();
                ui.EventLogPanel.UpdateUI(Events);
            }

            Events.Enqueue(item);
            ui.EventLogPanel.UpdateUI(Events);
        }
    }

    public void QuickEventLogMessage(string message)
    {
        NewEventLogItem(new EventLogItemData { Item = new EventLogHubMessage(message) });
    }
    public void QuickEventLogMessage(string message, EventLogItemType type)
    {
        NewEventLogItem(new EventLogItemData { Item = new EventLogHubMessage(message), Type = type });
    }

    public Queue<EventLogItemData> Events { get; private set; }
}


public struct EventLogItemData
{
    public IEventLogItem Item { get; set; }
    public EventLogItemType Type { get; set; }
}

public enum EventLogItemType
{
    Default = 0,
    Info,
    Warning,
    Danger,
    Success
}
