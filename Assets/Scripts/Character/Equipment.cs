using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;

public class Equipment
{
    public WeaponSlot MainWeapon { get; set; }
    public ArmorSlot BodyArmor { get; set; }

    private IEquipmentView view;
    private EquipmentEvents events;
    

    public Equipment(List<WeaponProficiency> weaponProficiencies, List<ArmorProficiency> armorProficiencies, IEquipmentView View, EquipmentEvents events)
    {
        this.events = events;

        MainWeapon = new WeaponSlot(OnEquip, OnUnEquip, weaponProficiencies);
        BodyArmor = new ArmorSlot(OnEquip, OnUnEquip, armorProficiencies);

        view = View;
        UpdateView();
    }

    private void OnEquip(Item item)
    {
        events.OnEquipped?.Invoke(item);
        UpdateView();
    }

    private void OnUnEquip(Item item)
    {
        events.OnUnEquipped?.Invoke(item);
        UpdateView();
    }

    private void UpdateView()
    {
        if (view != null)
        {
            view.SetData(this);
        }
    }

    public void Remove(Item item)
    {
        if (MainWeapon.GetItem() == item)
        {
            MainWeapon.Remove();
            UpdateView();
            return;
        }

        if (BodyArmor.GetItem() == item)
        {
            BodyArmor.Remove();
            UpdateView();
            return;
        }
    }

    
}

public struct EquipmentEvents
{
    public Action<Item> OnEquipped { get; set; }
    public Action<Item> OnUnEquipped { get; set; }
}

public class WeaponSlot : EquipmentSlot<Weapon>
{
    private List<WeaponProficiency> allowedWeapons;

    public WeaponSlot(Action<Weapon> OnEquip, Action<Weapon> OnUnEquip, List<WeaponProficiency> proficiencies) : base(OnEquip, OnUnEquip)
    {
        allowedWeapons = proficiencies;

    }

    public override void Equip(Weapon item)
    {
        if (allowedWeapons.Contains(item.Proficiency))
        {
            base.Equip(item);
        } else
        {
            EventLogHub.Instance.QuickEventLogMessage($"You are not proficient with {item.Proficiency.ToString()} weapons", EventLogItemType.Warning);
        }
    }
}

public class ArmorSlot : EquipmentSlot<Armor>
{
    private List<ArmorProficiency> allowedArmors;

    public ArmorSlot(Action<Armor> OnEquip, Action<Armor> OnUnEquip, List<ArmorProficiency> proficiencies) : base(OnEquip, OnUnEquip)
    {
        allowedArmors = proficiencies;
    }

    public override void Equip(Armor item)
    {
        if (allowedArmors.Contains(item.Proficiency))
        {
            base.Equip(item);
        } else
        {
            EventLogHub.Instance.QuickEventLogMessage($"You are not proficient with {item.Proficiency.ToString()} armor", EventLogItemType.Warning);
        }
    }
}

public class EquipmentSlot<T> : IEquipSlot<T>
{
    private Action<T> onEquip;
    private Action<T> onUnEquip;
    private T currentItem;

    public EquipmentSlot(Action<T> OnEquip, Action<T> OnUnEquip)
    {
        onEquip = OnEquip;
        onUnEquip = OnUnEquip;
    }

    public virtual void Equip(T item)
    {
        if (currentItem != null)
        {
            UnEquip();
        }

        currentItem = item;
        onEquip?.Invoke(currentItem);
    }

    public T GetItem()
    {
        return currentItem;
    }

    public void Remove()
    {
        currentItem = default(T);
    }

    public void UnEquip()
    {
        onUnEquip?.Invoke(currentItem);
        currentItem = default(T);
    }


}

public interface IEquipSlot<T>
{
    void Equip(T item);
    void UnEquip();
    T GetItem();
    void Remove();
}

public interface IEquipmentView
{
    void Show();
    void Hide();
    void SetData(Equipment data);
}
