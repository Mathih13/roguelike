using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using InfallibleCode;
using mhartveit;
using System.Linq;
using System;

public class TreasureChest : BoardItemContainer, IInteractable<IEnumerable<Item>>
{
    private IItemContainerView<TreasureChest> _view;

    protected override void Awake()
    {
        base.Awake();
        BoardEventHub.Instance.onBoardReady += Initialize;
    }

    private void Initialize()
    {
        DisplayName = "Treasure Chest";
        PieceType = BoardPieceType.Interactable;
        BoardManager.Instance.AddPieceToGameBoard(this, transform.position.ToVector3Int());

        GenerateLoot();
    }

    private void GenerateLoot()
    {
        for (int i = 0; i < UnityEngine.Random.Range(0, 4); i++)
        {
            int rng = UnityEngine.Random.Range(0, 5);
            switch (rng)
            {
                case 0:
                    items.Add(ItemData.Instance.MakeWeapon());
                    break;
                case 1:
                    items.Add(ItemData.Instance.MakeArmor());
                    break;
                default:
                    break;
            }
        }
    }

    public override void Open()
    {
        open = true;
        _view = UI.Instance.GetUIElement<MenuUI>().TreasureChest;
        _view.SetData(this);
        _view.Show();
    }



    public IEnumerable<Item> Interact() => GetAll();

    public override void Place(Item element)
    {
        base.Place(element);
        _view.SetData(this);
    }

    public override Item Take(Item element)
    {
        var result = base.Take(element);
        _view.SetData(this);

        return result;
    }
}
