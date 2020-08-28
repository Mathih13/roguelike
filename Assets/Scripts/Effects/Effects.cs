using UnityEngine;
using System.Collections;
using Rougelike.EventLog;

public class HealEffect : IEffect<CombatEntity>
{
    private readonly DiceType diceType;
    private readonly int diceRolls;
    private readonly CharacterStat stat;
    int amount;
    bool diceroll;


    public HealEffect(int amount)
    {
        this.amount = amount;
        diceroll = false;
    }

    public HealEffect(DiceType diceType, int diceRolls, CharacterStat stat)
    {
        diceroll = true;

        amount = 0;
        this.diceType = diceType;
        this.diceRolls = diceRolls;
        this.stat = stat;
    }

    public void ApplyEffect(CombatEntity target)
    {
        if (diceroll)
        {
            var dice = new DiceRoll(diceType, diceRolls, stat.Modifier, target);
            EventLogHub.Instance.NewEventLogItem(new EventLogItemData { Item = dice });
            target.Heal(new HealData(dice.Result, target));
            return;
        }

        target.Heal(new HealData(amount, target));
    }

    public string GetDescription()
    {
        if (diceroll)
        {
            var modifier = stat != null ? stat.Modifier.ToString() : "";
            return $"Heals the target for {diceRolls}{diceType.ToString()}+{modifier}";
        } else
        {
            return $"Heals the target for {amount}";
        }
    }
}


public class DamageEffect : IEffect<CombatEntity>
{
    private readonly DiceType diceType;
    private readonly int diceRolls;
    private readonly CharacterStat stat;
    int amount;
    bool diceroll;


    public DamageEffect(int amount)
    {
        this.amount = amount;
        diceroll = false;
    }

    public DamageEffect(DiceType diceType, int diceRolls, CharacterStat stat)
    {
        diceroll = true;

        amount = 0;
        this.diceType = diceType;
        this.diceRolls = diceRolls;
        this.stat = stat;
    }

    public void ApplyEffect(CombatEntity target)
    {
        if (diceroll)
        {
            var dice = new DiceRoll(diceType, diceRolls, stat.Modifier);
            EventLogHub.Instance.NewEventLogItem(new EventLogItemData { Item = dice });
            
            target.TakeDamage(dice.Result, target);
            return;
        }

        target.Heal(new HealData(amount, target));
    }

    public string GetDescription()
    {
        if (diceroll)
        {
            var modifier = stat != null ? stat.Modifier.ToString() : "";
            return $"Heals the target for {diceRolls}{diceType.ToString()}+{modifier}";
        }
        else
        {
            return $"Heals the target for {amount}";
        }
    }
}
