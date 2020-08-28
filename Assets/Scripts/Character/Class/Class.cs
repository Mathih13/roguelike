using UnityEngine;
using System.Collections;

public class Class
{

    public int StartingHitPoints { get; private set; }
    public DiceType PerLevelHP { get; private set; }
    public Proficiencies Proficiencies { get; private set; }
    public string Name { get; private set; }
    public int Level { get; set; }

    private readonly CharacterStats characterStats;
    private CombatEntity combatEntity;

    public Class(string name, int startingHP, DiceType perLevelHP, Proficiencies proficiencies, CharacterStats characterStats)
    {
        Name = name;
        StartingHitPoints = startingHP;
        PerLevelHP = perLevelHP;
        Proficiencies = proficiencies;
        this.characterStats = characterStats;
        Level = 1;
    }

    private void SetupStartingHP()
    {
        combatEntity.MaxHP = StartingHitPoints + characterStats.Constitution.Modifier;
        combatEntity.HP = combatEntity.MaxHP;
    }

    public void SetCombatEntity(CombatEntity combatEntity)
    {
        this.combatEntity = combatEntity;
        SetupStartingHP();
    }

    public void AddProficiency(WeaponProficiency proficiency)
    {
        if (!Proficiencies.Weapon.Contains(proficiency))
        {
            Proficiencies.Weapon.Add(proficiency);
        }
    }

    public void AddProficiency(ArmorProficiency proficiency)
    {
        if (!Proficiencies.Armor.Contains(proficiency))
        {
            Proficiencies.Armor.Add(proficiency);
        }
    }

    public virtual void LevelUp()
    {
        Level++;
        combatEntity.MaxHP += new DiceRoll(PerLevelHP, 1, characterStats.Constitution.Modifier).Result;
        combatEntity.HP = combatEntity.MaxHP;
    }
}
