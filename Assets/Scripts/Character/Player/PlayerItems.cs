using UnityEngine;
using System.Collections;
using mhartveit;

public class PlayerItems
{
    public Inventory Inventory { get; private set; }
    public Equipment Equipment { get; private set; }


    public PlayerItems(CharacterSheet characterSheet, IItemContainerView<Inventory> inventoryView, IEquipmentView equipmentView)
    {
        Inventory = new Inventory(inventoryView);
        Equipment = new Equipment(
            characterSheet.CharacterClass.Proficiencies.Weapon, 
            characterSheet.CharacterClass.Proficiencies.Armor, 
            equipmentView, 
            new EquipmentEvents { OnEquipped = OnItemEquipped, OnUnEquipped = OnItemUnEquipped }
            );

    }

    private void OnItemEquipped(Item obj)
    {
        Inventory.Take(obj);
    }

    private void OnItemUnEquipped(Item obj)
    {
        Inventory.Place(obj);
    }
}
