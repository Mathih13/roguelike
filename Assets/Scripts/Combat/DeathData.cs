using UnityEngine;
using System.Collections;

public class DeathData : IEventLogItem
{
    public CombatEntity Attacker { get; private set; }
    public CombatEntity Target { get; private set; }
    public int Damage { get; private set; }
    public bool Player { get; set; }

    public DeathData(CombatEntity attacker, CombatEntity target, int damage)
    {
        Attacker = attacker;
        Target = target;
        Damage = damage;
        Player = target.GetType() == typeof(Player);
    }

    public string GetEventLogBody()
    {
        return $"{Attacker.Name} deals {Damage} damage to {Target.Name}, killing it!";
    }
}
