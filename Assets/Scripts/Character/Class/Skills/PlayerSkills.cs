using UnityEngine;
using System.Collections;
using Rougelike.EventLog;

public interface ICharacterSkill
{

}

public class OneHandedSkill : CharacterSkill, ICharacterSkill
{
    public OneHandedSkill(IPlayerEventsProvider eventsProvider) : base(eventsProvider)
    {
        eventsProvider.onPlayerHitEnemy += (AttackCalculationData data) => {
            IncreaseXP(10);
        };
    }

    

    protected override string GetDisplayName() => "One Handed";
}

public class HeavyArmorSkill : CharacterSkill, ICharacterSkill
{
    public HeavyArmorSkill(IPlayerEventsProvider eventsProvider) : base(eventsProvider)
    {
        eventsProvider.onPlayerTakesDamage += (Player player) =>
        {
            if (player.Items.Equipment.BodyArmor.GetItem().Proficiency == ArmorProficiency.Heavy)
            {
                IncreaseXP(10);
            }
                
        };
    }

    protected override string GetDisplayName() => "Heavy Armor";
}

public class LightArmorSkill : CharacterSkill, ICharacterSkill
{
    public LightArmorSkill(IPlayerEventsProvider eventsProvider) : base(eventsProvider)
    {
        eventsProvider.onPlayerTakesDamage += (Player player) =>
        {
            if (player.Items.Equipment.BodyArmor.GetItem().Proficiency == ArmorProficiency.Light)
            {
                IncreaseXP(10);
            }

        };
    }

    protected override string GetDisplayName() => "Light Armor";

}
