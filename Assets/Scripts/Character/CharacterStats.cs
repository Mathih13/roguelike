using UnityEngine;
using System.Collections;
using System;

public class CharacterStats
{
    public CharacterStat Dexterity { get; set; }
    public CharacterStat Constitution { get; set; }
    public CharacterStat Strength { get; set; }
    public CharacterStat Intelligence { get; set; }
    public CharacterStat Wisdom { get; set; }
    public CharacterStat Charisma { get; set; }

    public CharacterStats()
    {
        Dexterity = new CharacterStat();
        Constitution = new CharacterStat();
        Strength = new CharacterStat();
        Intelligence = new CharacterStat();
        Wisdom = new CharacterStat();
        Charisma = new CharacterStat();
    }

    public CharacterStats(int dexterity, int constitution, int strength, int intelligence, int wisdom, int charisma)
    {
        Dexterity = new CharacterStat();
        Constitution = new CharacterStat();
        Strength = new CharacterStat();
        Intelligence = new CharacterStat();
        Wisdom = new CharacterStat();
        Charisma = new CharacterStat();
    }
}

public class CharacterStat
{
    public int Value { get; set; }
    public int Modifier { get => GetModifier(Value); private set => Modifier = value; }

    public CharacterStat()
    {
        Value = 10;
    }

    public CharacterStat(int value)
    {
        Value = value;
    }

    private int GetModifier(int value)
    {
        return (value - 10) / 2;
    }
}
