using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class TreasureChestBehaviour : MonoBehaviour, IItemContainerView<TreasureChest>
{

    private TreasureChest currentTreasureChest;
    [SerializeField]
    private ItemPanelUI ItemsPanel;

    public IEnumerable<Item> Items
    {
        set
        {
            ItemsPanel.Initialize(new ItemPanelData<IEnumerable<Item>> { Data = value, Title = currentTreasureChest.DisplayName, Options = new ItemPanelOptions { Draggable = true } }, new UIElementEvents<Item> { OnSelect = SelectItem, OnUIDropFrom = OnUIDropFrom });
        }
    }

    private void OnUIDropFrom(Item obj)
    {
        // When a ui element leaves this panel via drag n drop
        if (currentTreasureChest.Contains(obj))
        {
            currentTreasureChest.Take(obj);
        }
    }

    private void SelectItem(Item obj)
    {
        BoardManager.Instance.player.Items.Inventory.Place(currentTreasureChest.Take(obj));
    }

    public void Hide()
    {
        ItemsPanel.Hide();
    }

    public void SetData(TreasureChest treasureChest)
    {
        currentTreasureChest = treasureChest;
        Items = treasureChest.GetAll();
    }

    public void Show()
    {
        ItemsPanel.Show();
    }
}

public interface IItemContainerView<T>
{
    void Show();
    void Hide();
    IEnumerable<Item> Items { set; }
    void SetData(T data);
}