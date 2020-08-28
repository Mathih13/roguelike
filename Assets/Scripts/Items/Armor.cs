using System;

[Serializable]
public class Armor : Item
{
    public int ArmorClass { get; }
    public bool DexModifier { get; }
    public int MaxDexBonus { get; }


    public ArmorProficiency Proficiency { get; }


    public Armor(int armorClass, bool allowDexModifierBonus, int maxDexBonus, ArmorProficiency proficiency, ItemInfo info)
    {
        ArmorClass = armorClass;
        DexModifier = allowDexModifierBonus;
        MaxDexBonus = maxDexBonus;
        Proficiency = proficiency;
        Info = info;
        if (Info != null)
        {
            Info.ArmorClass = ArmorClass.ToString();
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
}