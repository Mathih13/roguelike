using UnityEngine;
using mhartveit;
using TMPro;
using System.Collections.Generic;
using System.Linq;

public class EventLogPanelUI : MonoBehaviour, UpdatableUIElement<Queue<EventLogItemData>>
{
    [SerializeField]
    GameObject TextObjectPrefab;

    [SerializeField]
    GameObject MessagePanel;

    [SerializeField]
    EventLogTextColorPalette colors;

    Queue<EventLogMessage> eventLogs;


    public void UpdateUI(Queue<EventLogItemData> hubEvents)
    {
        if (hubEvents.Count < eventLogs.Count)
        {
            while (hubEvents.Count < eventLogs.Count)
            {
                RemoveExcessMessage();
            }
        }

        if (hubEvents.Count > eventLogs.Count)
        {
            for (int i = eventLogs.Count; i < hubEvents.Count; i++)
            {
                AddNewMessage(hubEvents, i);
            }
        }
    }

    private void RemoveExcessMessage()
    {
        var eventlog = eventLogs.Dequeue();
        Destroy(eventlog.GameObject);
    }

    private void AddNewMessage(Queue<EventLogItemData> hubEvents, int i)
    {
        var currentElement = hubEvents.ElementAt(i);
        var newItem = new EventLogMessage { eventMessage = currentElement.Item };
        newItem.GameObject = Instantiate(TextObjectPrefab, MessagePanel.transform);

        var tmpro = newItem.GameObject.GetComponent<TextMeshProUGUI>();
        tmpro.text = newItem.eventMessage.GetEventLogBody();
        tmpro.color = GetTextColorFromType(currentElement.Type);
        eventLogs.Enqueue(newItem);
    }

    public Color32 GetTextColorFromType(EventLogItemType type)
    {
        switch (type)
        {
            case EventLogItemType.Default:
                return colors.Default;
            case EventLogItemType.Info:
                return colors.Info;
            case EventLogItemType.Warning:
                return colors.Warning;
            case EventLogItemType.Danger:
                return colors.Danger;
            case EventLogItemType.Success:
                return colors.Success;
        }

        Debug.LogError("Unable to resolve Event Color from event type " + type);
        return colors.Default;
    }

    public void Initialize(Queue<EventLogItemData> data)
    {
        eventLogs = new Queue<EventLogMessage>();
        UpdateUI(data);
    }
}



