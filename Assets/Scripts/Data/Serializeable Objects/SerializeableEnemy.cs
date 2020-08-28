using UnityEngine;
using System.Collections;
using System;

[Serializable]
public class SerializeableEnemy
{
    public string Name;
    public string Description;
    public string ImageName;
    public int Strength;
    public int Dexterity;
    public int Constitution;
    public int Intelligence;
    public int Wisdom;
    public int Charisma;
    public int HP;
}

public interface ISerializeableEnemy
{
    SerializeableEnemy Serialize();
}
