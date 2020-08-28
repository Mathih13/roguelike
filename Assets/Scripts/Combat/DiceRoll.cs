using UnityEngine;
using System.Collections;

public class DiceRoll : IEventLogItem
{
    public int Result { get; private set; }
    public DiceType Dice { get; private set; }
    public int Amount { get; private set; }
    public int Modifier { get; private set; }

    private CombatEntity source;

    public DiceRoll(DiceType dice, int amount, int modifier)
    {
        GenerateResult(dice, amount, modifier);
    }

    public DiceRoll(DiceType dice, int amount, int modifier, CombatEntity source)
    {
        this.source = source;
        GenerateResult(dice, amount, modifier);
    }

    private void GenerateResult(DiceType dice, int amount, int modifier)
    {

        Dice = dice;
        Amount = amount;
        Modifier = modifier;

        switch (dice)
        {
            case DiceType.d20:
                Result = RollDice(1, 20, amount) + modifier;
                break;
            case DiceType.d12:
                Result = RollDice(1, 12, amount) + modifier;
                break;
            case DiceType.d10:
                Result = RollDice(1, 10, amount) + modifier;
                break;
            case DiceType.d8:
                Result = RollDice(1, 8, amount) + modifier;
                break;
            case DiceType.d6:
                Result = RollDice(1, 6, amount) + modifier;
                break;
            case DiceType.d4:
                Result = RollDice(1, 4, amount) + modifier;
                break;
            default:
                break;
        }

    }

    private int RollDice(int min, int max, int times)
    {
        int result = 0;
        if (times == 0) times = 1;
        for (int i = 1; i <= times; i++)
        {
            result += Random.Range(min, max);
        }

        return result;
    }

    public string GetEventLogBody()
    {
        string modifierText = Modifier > 0 ? $"+{Modifier.ToString()}" : "";
        return $"{source.Name} rolled {Amount}{Dice.ToString()}{modifierText} for a result of {Result}";
    }
}

public enum DiceType
{
    d20 = 0,
    d12,
    d10,
    d8,
    d6,
    d4
}
