using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class InventoryBehaviour : MonoBehaviour, IItemContainerView<Inventory>
{
    private Inventory currentInventory;

    [SerializeField]
    private InventoryUI ItemsPanel;





    public IEnumerable<Item> Items { set => ItemsPanel.Initialize(new ItemPanelData<IEnumerable<Item>> { Data = value, Title = "Inventory", Options = new ItemPanelOptions { Draggable = true } }, new UIElementEvents<Item> { OnSelect = SelectItem, OnEquip = EquipItem, OnDrop = DropItem, OnUIDropTo = OnUIDropTo, OnUIDropFrom = OnUIDropFrom }); }

    private void OnUIDropFrom(Item obj)
    {
    }

    private void OnUIDropTo(Item obj)
    {
        //When an item is dragged onto inventory UI

        if (!currentInventory.Contains(obj))
        {
            currentInventory.Place(obj);
            
        }
    }

    private void DropItem(Item obj)
    {
         // Move this logic to the inventory?
         // Create a container on drop that can take the item(s) dropped

        //var item = currentInventory.Take(obj.Item);

        //obj.transform.SetParent(null);
        //obj.ShowSprite(true);
        //BoardManager.Instance.AddPieceToGameBoard(item, BoardManager.Instance.GetPlayerPosition());
    }

    private void EquipItem(Item obj)
    {
        var player = BoardManager.Instance.player;
        player.Items.Equipment.MainWeapon.Equip((Weapon)obj);
    }

    private void SelectItem(Item obj)
    {
    }

    public void Hide()
    {
        ItemsPanel.Hide();
    }

    public void Show()
    {
        ItemsPanel.Show();
    }

    public void SetData(Inventory inventory)
    {
        currentInventory = inventory;
        Items = inventory.GetAll();
    }

    public void ShowHide()
    {
        if (ItemsPanel.Open)
        {
            Hide();
            return;
        }

        Show();
    }
}

