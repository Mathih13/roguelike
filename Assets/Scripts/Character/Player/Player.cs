using mhartveit;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Player : CharacterController
{

    public PlayerItems Items;
    public CombatEntity Combat;
    public CharacterSheet CharacterSheet;
    public PlayerHotBar HotBar;

    private PlayerEventsProvider playerEventsProvider;


    private void Awake()
    {
        BoardEventHub.Instance.onBoardReady += Initialize;
    }

    private void Initialize()
    {
        DisplayName = "Player";
        playerEventsProvider = new PlayerEventsProvider();
        HotBar = new PlayerHotBar(UI.Instance.GetUIElement<PlayerUI>().HotBar);
        CharacterSheet = new CharacterSheet(UI.Instance.GetUIElement<MenuUI>().CharacterSheet);
        InitializeCharacterSheet();

        Items = new PlayerItems(CharacterSheet, UI.Instance.GetUIElement<MenuUI>().Inventory, UI.Instance.GetUIElement<MenuUI>().Equipment);
        Combat = new CombatEntity(DisplayName, Items.Equipment, CharacterSheet.Stats, this, new CombatEntityEvents());
        CharacterSheet.CharacterClass.SetCombatEntity(Combat);

        AddTempWeapons();

        UI.Instance.GetUIElement<PlayerUI>().Health.Initialize(Combat);
        transform.position += new Vector3(0.5f, 0.5f, 0);

    }

    private void AddTempWeapons()
    {
        var weapon = ItemData.Instance.MakeWeapon("dagger");
        var armor = ItemData.Instance.MakeArmor("Chain Shirt");

        Items.Inventory.Place(weapon);
        Items.Equipment.MainWeapon.Equip(weapon);

        Items.Inventory.Place(armor);
        Items.Equipment.BodyArmor.Equip(armor);

        var effect = new HealEffect(DiceType.d4, 1, CharacterSheet.Stats.Constitution);
        var potion = new Potion(effect)
        {
            Info = new ItemInfo
            {
                 Name = "Healing Potion",
                 Description = effect.GetDescription(),
                 ImageName = "colored_transparent_656"
            }
        };
        Items.Inventory.Place(potion);
        HotBar.AddItem(potion, 0);

    }

    private void InitializeCharacterSheet()
    {
        CharacterSheet.SetStats(new CharacterStats
        {
            Charisma = new CharacterStat(12),
            Constitution = new CharacterStat(14),
            Dexterity = new CharacterStat(10),
            Intelligence = new CharacterStat(8),
            Strength = new CharacterStat(15),
            Wisdom = new CharacterStat(10),
        });


        CharacterSheet.SetClass(new Class(
            "Barbarian", 12,
            DiceType.d12,
            new Proficiencies { Armor = new List<ArmorProficiency> { ArmorProficiency.Medium }, Weapon = new List<WeaponProficiency> { WeaponProficiency.Simple } },
            CharacterSheet.Stats
            ));

        CharacterSheet.InstantiateSkills(playerEventsProvider);
    }



    // Update is called once per frame
    void Update()
    {
        var moveVector = Vector2.zero;

        if (Input.GetKeyUp(KeyCode.W) || Input.GetKey(KeyCode.W) && Input.GetKey(KeyCode.LeftControl))
        {
            moveVector += Vector2.up;
        }
        else if (Input.GetKeyUp(KeyCode.S) || Input.GetKey(KeyCode.S) && Input.GetKey(KeyCode.LeftControl))
        {
            moveVector += Vector2.down;
        }

        if (Input.GetKeyUp(KeyCode.A) || Input.GetKey(KeyCode.A) && Input.GetKey(KeyCode.LeftControl))
        {
            moveVector += Vector2.left;
        }
        else if (Input.GetKeyUp(KeyCode.D) || Input.GetKey(KeyCode.D) && Input.GetKey(KeyCode.LeftControl))
        {
            moveVector += Vector2.right;
        }
        else if (Input.GetKeyUp(KeyCode.Space) || Input.GetKey(KeyCode.Space) && Input.GetKey(KeyCode.LeftControl))
        {
            PassTurn(true);
        }
        else if (Input.GetKeyUp(KeyCode.F))
        {
            DisplayNonCharacterBoardPiecesAtTile();
        }
        else if (Input.GetKeyUp(KeyCode.G))
        {
            HandleNonCharacterPieceInteraction();
        }

        HandleMoveInput(moveVector);
    }

    private void HandleNonCharacterPieceInteraction()
    {
        var tile = BoardManager.Instance.GetGameTileUnderCharacter(this);
        var piece = tile.NonCharacterPiece;

        if (piece != null)
        {
            if (piece.GetType() == typeof(TreasureChest))
            {
                var tc = (TreasureChest)piece;
                tc.Open();

            }
        }
    }


    private void HandleMoveInput(Vector2 moveVector)
    {
        if (moveVector == Vector2.zero) return;

        var direction = Direction.NearestFromVector(moveVector);

        MoveOrAttack(direction, true);
    }

    private void DisplayNonCharacterBoardPiecesAtTile()
    {
        var tile = BoardManager.Instance.GetGameTileUnderCharacter(this);
        var piece = tile.NonCharacterPiece;

        if (piece != null)
        {
            EventLogHub.Instance.NewEventLogItem(new EventLogItemData { Item = new EventLogHubMessage("You see the following on the ground:"), Type = EventLogItemType.Info });
            EventLogHub.Instance.QuickEventLogMessage(piece.DisplayName);
            return;
        }

        EventLogHub.Instance.NewEventLogItem(new EventLogItemData { Item = new EventLogHubMessage("There is nothing here."), Type = EventLogItemType.Info });

    }

    protected override void Attack(CombatEntity target)
    {
        Combat.Attack(target);
    }


}
