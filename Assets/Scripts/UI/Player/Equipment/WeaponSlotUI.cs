using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;

public class WeaponSlotUI : EquipmentSlotUI, UIElement<EquipmentSlotUIData<Weapon>>, IDropHandler
{
    private EquipmentSlot<Weapon> currentSlot;
    private GameObject currentItemGO;

    // Todo register and unregister GOs

    public void Initialize(EquipmentSlotUIData<Weapon> data)
    {
        currentSlot = data.Slot;
        ItemEvents = data.Events;

        if (currentItemGO != null)
        {
            Destroy(currentItemGO);
            defaultImage.gameObject.SetActive(true);
        }

        if (data.Item != null && data.Item.Info != null)
        {
            defaultImage.gameObject.SetActive(false);
            currentItemGO = Instantiate(itemTemplate, transform);
            currentEquipped = currentItemGO.GetComponent<ItemUI>();
            currentEquipped.MakeDraggable();
            var uidata = new ITemUIData { Item = data.Item };
            currentEquipped.Initialize(uidata);
        }
    }

    public void OnDrop(PointerEventData eventData)
    {
        ItemPanelUI from = eventData.pointerDrag.GetComponent<DraggableItemUI>().OriginalParent.GetComponent<ItemPanelUI>();
        ItemUI objectDropped = eventData.pointerDrag.GetComponent<ItemUI>();
        
        if (objectDropped != null && objectDropped.Item is Weapon)
        {
            if (from != null)
            {
                from.ItemEvents.OnUIDropFrom?.Invoke(objectDropped.Item);
            }
            currentSlot.Equip((Weapon)objectDropped.Item);

        }
    }
}
