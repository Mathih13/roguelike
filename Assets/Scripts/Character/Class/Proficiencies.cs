using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Proficiencies
{
    public List<ArmorProficiency> Armor { get; set; }
    public List<WeaponProficiency> Weapon { get; set; }
}

public enum ArmorProficiency
{
    Light,
    Medium,
    Heavy,
    Shields
}

public enum WeaponProficiency
{
    Simple,
    Martial,
    Exotic
}

public enum WeaponAttackType
{
    Strength,
    Dexterity
}
