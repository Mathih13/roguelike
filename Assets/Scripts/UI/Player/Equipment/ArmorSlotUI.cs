using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;

public class ArmorSlotUI : EquipmentSlotUI, UIElement<EquipmentSlotUIData<Armor>>, IDropHandler
{
    private EquipmentSlot<Armor> currentSlot;
    private GameObject currentItemGO;


    public void Initialize(EquipmentSlotUIData<Armor> data)
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

        if (objectDropped != null && objectDropped.Item is Armor)
        {
            if (from != null)
            {
                from.ItemEvents.OnUIDropFrom?.Invoke(objectDropped.Item);
            }
            currentSlot.Equip((Armor)objectDropped.Item);

        }
    }
}
