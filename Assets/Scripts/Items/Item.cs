using UnityEngine;
using System.Collections;

public abstract class Item : CharacterInteractable, ILootable<Player>
{
    public ItemInfo Info;

    public Item()
    {
        //renderer.sprite = DataLoader.Instance.GetItemSpriteFromName(info.ImageName);
    }
    
    public abstract void Interact(CharacterController interractor);

    public abstract Item Interact();

    public void PickUp(Player looter)
    {
        looter.Items.Inventory.Place(this);
    }
}

public class ItemInfo : IEventLogItem
{
    public string Name { get; set; }
    public string Description { get; set; }
    public string ImageName { get; set; }

    public string DamageDice { get; set; }
    public string ArmorClass { get; set; }

    public string GetEventLogBody()
    {
        return Name + ": " + Description;
    }
}
