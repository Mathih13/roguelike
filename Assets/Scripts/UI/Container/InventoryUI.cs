using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;

public class InventoryUI : ItemPanelUI, IDropHandler
{
    [SerializeField]
    private GameObject actionButtons;

    public void OnDrop(PointerEventData eventData)
    {
        MenuComponent from = eventData.pointerDrag.GetComponent<DraggableItemUI>().OriginalParent.GetComponent<MenuComponent>();
        ItemUI objectDropped = eventData.pointerDrag.GetComponent<ItemUI>();

        if (from != null)
        {
            from.ItemEvents.OnUIDropFrom?.Invoke(objectDropped.Item);
        }

        if (objectDropped != null)
        {
            ItemEvents.OnUIDropTo?.Invoke(objectDropped.Item);
        }
    }

    public override void Select(SelectableItemUI item)
    {
        base.Select(item);
        actionButtons.SetActive(true);
    }
}
