using DG.Tweening;
using Rougelike.EventLog;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class CharacterController : BoardPiece
{
    public float JumpPower = 0.75f;
    public float JumpDuration = .1f;
    public bool wanders;


    private bool _isMoving;

    private Vector2Int position;

    private void Awake()
    {
        BoardEventHub.Instance.onBoardReady += SetUpCharacter;
    }

    private void SetUpCharacter()
    {
        PieceType = BoardPieceType.Character;
    }

    public MoveOrAttackResult MoveOrAttack(Direction direction, bool isPlayer = false)
    {
        var result = new MoveOrAttackResult();
        if (_isMoving) return result;

        _isMoving = true;


        var destination = transform.position + direction.Vector;
        GameTile destinationTile = GetDestinationTile(destination);
        result.Destination = destinationTile;

        if (destinationTile.Cost == 0)
        {
            _isMoving = false;
            result.Blocked = true;
            return result;
        }

        // Check if cell has a character
        // Check if character is not us (I.e standing still or an order getting messed up)
        if (destinationTile.HasCharacter && destinationTile.GetCharacter != this)
         {
            var loc = transform.position;
            Attack(GetCombatEntity(destinationTile.GetCharacter));
            if (isPlayer)
            {
                EnemiesManager.Instance.DoEnemyTurn();
            }
            _isMoving = false;
            result.Attacked = true;
            result.DestinationOccupant = destinationTile.GetCharacter;
            return result;
        }
        else if (!destinationTile.HasCharacter)
        {
            //If unoccupied, move
            destinationTile.MoveCharacterToTile(BoardManager.Instance.GetGameTileUnderCharacter(this), this);
            transform.DOMove(destination, JumpDuration)
            .OnComplete(() =>
            {
                if (isPlayer)
                {
                    EnemiesManager.Instance.DoEnemyTurn();
                }
                _isMoving = false;
            });
            result.Moved = true;
            return result;
        }
        _isMoving = false;
        return result;
    }

    private GameTile GetDestinationTile(Vector3 destination)
    {
        return BoardManager.Instance.GetTileFromWorldCoords(destination);
    }

    private CombatEntity GetCombatEntity(CharacterController character)
    {
        if (character is Enemy)
        {
            var obj = (Enemy)character;
            return obj.Combat;
        }

        if (character is Player)
        {
            var obj = (Player)character;
            return obj.Combat;
        }

        Debug.Log("Unable to find combat entity on controller " + character);
        return null;
    }

    public void PassTurn(bool isPlayer = false)
    {
        if (isPlayer)
        {
            EnemiesManager.Instance.DoEnemyTurn();
        }
    }

    public void Wander()
    {
        if (wanders)
        {
            Wander();
        }
    }

    private void simpleMove(Vector3 destination)
    {
        transform.DOMove(destination, JumpDuration)
           .OnComplete(() =>
           {
               _isMoving = false;
           });
    }

    protected abstract void Attack(CombatEntity target);

    public Vector3Int GetPosition() => BoardManager.Instance.GetPositionOfCharacter(this);
}

public class CharacterAttackData
{
    public int NumberOfAttacks { get; set; }
    public DiceType AttackDice { get; set; }
    public int AttackModifier { get; set; }
    public DiceType DamageDice { get; set; }
    public int DamageDiceCount { get; set; }
    public int DamageModifier { get; set; }
}

public struct MoveOrAttackResult
{
    public bool Moved { get; set; }
    public bool Attacked { get; set; }
    public bool Blocked { get; set; }
    public GameTile Destination { get; set; }
    public CharacterController DestinationOccupant { get; set; }
}
