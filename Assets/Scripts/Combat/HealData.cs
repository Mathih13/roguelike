public class HealData : IEventLogItem
{
    public int Amount { get; set; }
    public CombatEntity Target { get; set; }

    public HealData(int amount, CombatEntity target)
    {
        Amount = amount;
        Target = target;
    }

    public string GetEventLogBody()
    {
        return $"{Target.Name} was healed for {Amount}.";
    }
}