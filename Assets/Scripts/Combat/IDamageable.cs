using UnityEngine;
using System.Collections;
using Rougelike.EventLog;

public interface IDamageable
{
    void TakeDamage(AttackCalculationData attackData);
}
