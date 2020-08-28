using UnityEngine;
using System.Collections;
using System;
using Rougelike.EventLog;

public class PlayerEventsProvider : IPlayerEventsProvider
{
    public PlayerEventsProvider()
    {
        CombatEventHub.Instance.onCombatEntityDamaged += PlayerHitEnemy;
    }

    public event Action<AttackCalculationData> onPlayerHitEnemy;
    public void PlayerHitEnemy(AttackCalculationData attackData)
    {
        if (attackData.Attacker?.Controller.GetType() == typeof(Player) && attackData.Hit)
        {
            onPlayerHitEnemy?.Invoke(attackData);
        }
    }

    public event Action<Player> onPlayerTakesDamage;
    public void PlayerTakesDamage(AttackCalculationData attackData)
    {
        if (attackData.Target?.Controller.GetType() == typeof(Player) && attackData.Hit)
        {
            onPlayerTakesDamage?.Invoke((Player)attackData.Target.Controller);
        }
    }
}


public interface IPlayerEventsProvider
{
    event Action<AttackCalculationData> onPlayerHitEnemy;
    event Action<Player> onPlayerTakesDamage;
}