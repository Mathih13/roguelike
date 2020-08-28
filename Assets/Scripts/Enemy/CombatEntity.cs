using DG.Tweening;
using mhartveit;
using Rougelike.EventLog;
using System;

public class CombatEntity : IDamageable, IHealable
{
    public int MaxHP;
    public int HP;
    public CharacterAttackData AttackData { get; private set; }
    public int ArmorClass { get => CalculateArmorClass(); private set => ArmorClass = value; }
    public string Name { get; private set; }
    public CharacterController Controller { get; }

    public Action<CombatEntity, AttackCalculationData> onDamaged;
    public Action<CombatEntity, HealData> onHealed;
    private Action<CombatEntity, AttackCalculationData> onDeath;

    private Equipment equipment;
    private CharacterStats stats;

    public CombatEntity(string name, Equipment equipment, CharacterStats stats, CharacterController controller, CombatEntityEvents events)
    {
        onDamaged = events.onDamaged;
        onHealed = events.onHealed;
        onDeath = events.onDeath;
        Name = name;
        this.equipment = equipment;
        this.stats = stats;
        Controller = controller;
        UpdateAttackData();
    }

    private void UpdateAttackData()
    {
        AttackData = new CharacterAttackData
        {
            NumberOfAttacks = 1,
            AttackDice = DiceType.d20,
            DamageDiceCount = 1,
            DamageModifier = 0
        };

        if (equipment != null)
        {
            AttackData.DamageDice = equipment.MainWeapon.GetItem() != null ? equipment.MainWeapon.GetItem().DamageDice : DiceType.d4;
        } else
        {
            AttackData.DamageDice = DiceType.d4;
        }

    }



    public void TakeDamage(AttackCalculationData data)
    {
        if ((HP - data.Damage) <= 0)
        {
            Die(data);
            return;
        }

        onDamaged?.Invoke(this, data);
        HP = HP - data.Damage;
        CombatEventHub.Instance?.CombatEntityDamaged(data);
        EventLogHub.Instance?.NewEventLogItem(new EventLogItemData { Item = data, Type = EventLogItemType.Warning });
    }

    public void TakeDamage(int amount, CombatEntity target)
    {
        var data = new AttackCalculationData(null, target);
        if ((HP - amount) <= 0)
        {
            Die(new AttackCalculationData(null, target));
            return;
        }

        onDamaged?.Invoke(this, data);
        HP = HP - amount;
        CombatEventHub.Instance?.CombatEntityDamaged(data);
        EventLogHub.Instance?.NewEventLogItem(new EventLogItemData { Item = data, Type = EventLogItemType.Warning });
    }


    protected void Die(AttackCalculationData data)
    {
        onDeath?.Invoke(this, data);
    }

    public void Attack(CombatEntity target)
    {
        UpdateAttackData();
        AttackData.AttackModifier = ResolveAttackModifiers();
        for (int i = 0; i < AttackData.NumberOfAttacks; i++)
        {
            var attack = new AttackCalculationData(this, target);
            attack.Execute();
            if (attack.Hit)
            {
                string modifierText = AttackData.DamageModifier > 0 ? $"+{AttackData.DamageModifier.ToString()}" : "";
   
                target.TakeDamage(attack);
            }
            else
            {
                EventLogHub.Instance.NewEventLogItem(new EventLogItemData { Item = attack, Type = EventLogItemType.Info });
            }
        }
    }

    private int ResolveAttackModifiers()
    {
        var attackType = equipment.MainWeapon.GetItem()?.AttackType;

        switch (attackType)
        {
            case WeaponAttackType.Strength:
                return stats.Strength.Modifier;
            case WeaponAttackType.Dexterity:
                return stats.Dexterity.Modifier;
            default:
                return 0;
        }
    }

    public void Heal(HealData healData)
    {
        onHealed?.Invoke(this, healData);
        if ((HP + healData.Amount) > MaxHP)
        {
            healData.Amount = MaxHP - HP;
            HP = MaxHP;
            EventLogHub.Instance.NewEventLogItem(new EventLogItemData { Item = healData, Type = EventLogItemType.Info });
            return;
        }

        HP += healData.Amount;
        EventLogHub.Instance.NewEventLogItem(new EventLogItemData { Item = healData, Type = EventLogItemType.Info });
    }

    public void SetMaxHP(int newMaxHP)
    {
        MaxHP = newMaxHP;
        HP = MaxHP;
    }

    private int CalculateArmorClass()
    {
        int baseAC = 10;
        Armor armor = equipment.BodyArmor.GetItem();
        if (armor != null)
        {
            baseAC = armor.ArmorClass;

            if (armor.DexModifier)
            {
                if (armor.MaxDexBonus == 0)
                {
                    baseAC += stats.Dexterity.Modifier;
                }
                else
                {
                    if (stats.Dexterity.Modifier <= armor.MaxDexBonus)
                    {
                        baseAC += stats.Dexterity.Modifier;
                    }
                    else
                    {
                        baseAC += armor.MaxDexBonus;
                    }
                }

            }
        }

        return baseAC;
    }
}


public struct CombatEntityEvents
{
    public Action<CombatEntity, AttackCalculationData> onDamaged;
    public Action<CombatEntity, HealData> onHealed;
    public Action<CombatEntity, AttackCalculationData> onDeath;
}