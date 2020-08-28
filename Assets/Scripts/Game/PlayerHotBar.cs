using UnityEngine;
using System.Collections;
using System;

public class PlayerHotBar
{
    HotBarSlot[] slots = new HotBarSlot[10];
    IPlayerHotBarView _view;

    public PlayerHotBar(IPlayerHotBarView view)
    {
        _view = view;
        for (int i = 0; i < slots.Length; i++)
        {
            slots[i] = new HotBarSlot(UpdateUI);
        }
        UpdateUI();
    }

    public HotBarSlot[] GetSlots() => slots;

    public void AddItem(IHotBarItem item)
    {
        for (int i = 0; i < slots.Length; i++)
        {
            if (slots[i].Item == null)
            {
                slots[i].AddItem(item);
                UpdateUI();
                return;
            }
        }
        UpdateUI();
    }

    public void AddItem(IHotBarItem item, int index)
    {
        if (slots[index].Item == null)
        {
            slots[index].AddItem(item);
        }
        UpdateUI();
    }

    private void UpdateUI()
    {
        _view.SetData(this);
    }
}

public class HotBarSlot
{
    public IHotBarItem Item { get; set; }
    
    private Action onSlotUpdated;

    public HotBarSlot(Action onSlotUpdated)
    {
        this.onSlotUpdated = onSlotUpdated;
    }

    public void AddItem(IHotBarItem item)
    {
        if (Item == null)
        {
            Item = item;
        }
        onSlotUpdated();
    }

    public void UseCurrentItem(CombatEntity combatEntity)
    {
        Item.Use(combatEntity);
        RemoveItem();
        onSlotUpdated();
    }

    private void RemoveItem()
    {
        Item = null;
    }
}

public interface IHotBarItem
{
    void Use(CombatEntity user);
    Item GetGameItem();
}

public interface IPlayerHotBarView
{
    void SetData(PlayerHotBar data);
}