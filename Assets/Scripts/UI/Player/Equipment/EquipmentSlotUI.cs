using UnityEngine;
using System;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class EquipmentSlotUI : MenuComponent
{
    [SerializeField]
    protected GameObject itemTemplate;

    [SerializeField]
    protected Image defaultImage;

    protected ItemUI currentEquipped;

    
}

public struct EquipmentSlotUIData<T>
{
    public Item Item { get; set; }
    public EquipmentSlot<T> Slot { get; set; }
    public UIElementEvents<Item> Events { get; set; }
}