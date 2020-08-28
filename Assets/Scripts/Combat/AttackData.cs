using UnityEngine;
using System.Collections;

namespace Rougelike.EventLog
{
    public class AttackCalculationData : IEventLogItem
    {
        public CombatEntity Attacker { get; private set; }
        public CombatEntity Target { get; private set; }
        public bool Hit { get; protected set; }
        public int Damage { get; protected set; }

        public AttackCalculationData(CombatEntity attacker, CombatEntity target)
        {
            Attacker = attacker;
            Target = target;
        }

        public virtual AttackCalculationData Execute()
        {
            var attackData = Attacker.AttackData;
            var attackRoll = new DiceRoll(attackData.AttackDice, 1, attackData.AttackModifier, Attacker);
            EventLogHub.Instance.NewEventLogItem(new EventLogItemData { Item = attackRoll });

            if (attackRoll.Result >= Target.ArmorClass)
            {
                Hit = true;
                Damage = new DiceRoll(attackData.DamageDice, attackData.DamageDiceCount, attackData.DamageModifier).Result;
            }

            return this;
        }

        public string GetEventLogBody()
        {
            var result = $"{Attacker.Name} attacked {Target.Name}";

            if (Hit)
            {
                result += $", and dealt {Damage} HP damage!";
            } else
            {
                result += ", but it missed.";
            }

            return result;
        }
    }

}
