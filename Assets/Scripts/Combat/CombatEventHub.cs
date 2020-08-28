using UnityEngine;
using System.Collections;
using System;
using Rougelike.EventLog;
using mhartveit;

public class CombatEventHub: Singleton<CombatEventHub>
{

    #region events
    public event Action<DeathData> onCombatEntityDies;
    public void CombatEntityDies(DeathData deathData)
    {
        if (onCombatEntityDies != null)
        {
            onCombatEntityDies(deathData);
        }
    }

    public event Action<AttackCalculationData> onCombatEntityDamaged;
    public void CombatEntityDamaged(AttackCalculationData attackData)
    {
        if (onCombatEntityDamaged != null)
        {
            onCombatEntityDamaged(attackData);
        }
    }

    #endregion
}
