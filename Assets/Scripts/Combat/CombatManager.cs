using UnityEngine;
using System.Collections;
using System;

public class CombatManager : Singleton<CombatManager>
{
    private void Start()
    {
        CombatEventHub.Instance.onCombatEntityDies += OnCombatEntityDeath;
    }

    private void OnCombatEntityDeath(DeathData deathData)
    {
        BoardManager.Instance.RemoveCharacterFromGameBoard(deathData.Target.Controller);
        EventLogHub.Instance.NewEventLogItem(new EventLogItemData { Item = deathData, Type = EventLogItemType.Danger });
    }
}
