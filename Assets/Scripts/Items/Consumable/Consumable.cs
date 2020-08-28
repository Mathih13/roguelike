using UnityEngine;
using System.Collections;

public class Consumable : Item, IConsumable
{
    public virtual void Consume(CombatEntity consumer)
    {
        EventLogHub.Instance.QuickEventLogMessage($"{consumer.Name} used {Info.Name}");
    }

    public override void Interact(CharacterController interractor)
    {
        EventLogHub.Instance.NewEventLogItem(new EventLogItemData { Item = Info });
    }

    public override Item Interact()
    {
        return this;
    }
}

public interface IConsumable
{
    void Consume(CombatEntity consumer);
}

