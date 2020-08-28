using DG.Tweening;
using InfallibleCode;
using Rougelike.EventLog;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Enemy : CharacterController
{
    [SerializeField]
    private SpriteRenderer spriteRenderer;

    public CombatEntity Combat;
    public Equipment Equipment { get; private set; }
    public CharacterStats Stats { get; private set; }

    private void Awake()
    {
        BoardEventHub.Instance.onBoardReady += Initialize;
    }

    private void Initialize()
    {
        var loadedData = EnemyData.Instance.GetEnemyData();
        Equipment = new Equipment(null, null, null, new EquipmentEvents());
        Stats = new CharacterStats(loadedData.Dexterity, loadedData.Constitution, loadedData.Strength, loadedData.Intelligence, loadedData.Wisdom, loadedData.Charisma);
        DisplayName = loadedData.Name;

        Combat = new CombatEntity(DisplayName, Equipment, Stats, this, new CombatEntityEvents { onDeath = OnDeath });
        Combat.SetMaxHP(loadedData.HP);

        spriteRenderer.sprite = SpriteDB.Instance.GetSpriteFromName(loadedData.ImageName);

        BoardManager.Instance.AddCharacterToGameBoard(this, transform.position.ToVector3Int());
        transform.position += new Vector3(0.5f, 0.5f, 0);
    }

    private void OnDeath(CombatEntity entity, AttackCalculationData data)
    {
        transform.DOScale(0.1f, 0.2f).OnComplete(() => {
            CombatEventHub.Instance.CombatEntityDies(new DeathData(data.Attacker, data.Target, data.Damage));
            Destroy(this);
        });

    }

    public void DoTurn()
    {
        var playerPos = BoardManager.Instance.GetPlayerPosition();
        var myPos = GetPosition();

        if ((playerPos - myPos).magnitude <= 5)
        {
            var firstAttemptDestination = Direction.NearestFromVector(playerPos - myPos);
            var firstAttempt = MoveOrAttack(firstAttemptDestination);

            if (firstAttempt.Blocked)
            {
                foreach (var dir in Direction.Directions.Where(d => d != firstAttemptDestination && d != Direction.None))
                {
                    var attempt = MoveOrAttack(dir);

                    if (!attempt.Blocked)
                    {
                        break;
                    }
                }
            }
        }

    }

    protected override void Attack(CombatEntity target)
    {
        Combat.Attack(target);
    }
}