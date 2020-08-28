using UnityEngine;
using System.Collections;
using System;

[Serializable]
public class SerializeableItem
{
    public string Name;
    public string Description;
    public string ImageName;
    public string Type;
    public int Proficiency;
    public int DamageDice;
    public int ArmorClass;
    public bool AllowDexBonus;
    public int MaxDexBonus;
}

public interface ISerializeableItem
{
    SerializeableItem Serialize();
}
