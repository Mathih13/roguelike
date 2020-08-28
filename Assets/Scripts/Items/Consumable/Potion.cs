using UnityEngine;
using System.Collections;

public class Potion : Consumable, IHotBarItem
{
    private IEffect<CombatEntity> effect;

    public Potion(IEffect<CombatEntity> consumptionEffect)
    {
        effect = consumptionEffect;
    }

    public override void Consume(CombatEntity consumer)
    {
        base.Consume(consumer);
        effect.ApplyEffect(consumer);
    }

    public Item GetGameItem() => this;

    public void Use(CombatEntity user)
    {
        Consume(user);
    }
}
