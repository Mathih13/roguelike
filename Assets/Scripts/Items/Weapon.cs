using UnityEngine;
using System.Collections;
using System;

[Serializable]
public class Weapon : Item, ISerializeableItem
{
    public DiceType DamageDice { get; }
    public WeaponProficiency Proficiency { get; }
    public WeaponAttackType AttackType { get; }

    public Weapon(DiceType damageDice, WeaponProficiency proficiency, WeaponAttackType attackType, ItemInfo info)
    {
        DamageDice = damageDice;
        Proficiency = proficiency;
        AttackType = attackType;
        Info = info;
        if (Info != null)
        {
            Info.DamageDice = DamageDice.ToString();
        }
    }

    public override void Interact(CharacterController interractor)
    {
        EventLogHub.Instance.NewEventLogItem(new EventLogItemData { Item = Info });
    }

    public override Item Interact()
    {
        return this;
    }

    public SerializeableItem Serialize()
    {
        return new SerializeableItem
        {
            DamageDice = (int)DamageDice,
            Description = Info.Description,
            ImageName = Info.ImageName,
            Name = Info.Name,
            Proficiency = (int)Proficiency,
            Type = "weapon"

        };
    }
}
