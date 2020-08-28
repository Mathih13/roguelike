using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;
using System;
using UnityEngine.UI;
using mhartveit;

public class ItemUI : MonoBehaviour, UIElement<ITemUIData>, IPointerEnterHandler, IPointerExitHandler
{
    public Item Item;
    public Image itemImage;

    private ItemPanelUI panel;
    private bool isShowingHoverData;
    private ItemModalViewBehaviour itemModalView;

    protected Color defaultColor;
    private DraggableItemUI draggable;


    public void Initialize(ITemUIData data)
    {
        itemModalView = UI.Instance.GetUIElement<MenuUI>().ItemModal;
        Item = data.Item;
        panel = data.Panel;

        itemImage.sprite = SpriteDB.Instance.GetSpriteFromName(Item.Info.ImageName);
    }


    public void OnPointerEnter(PointerEventData eventData)
    {
        if (draggable == null)
        {
            draggable = GetComponent<DraggableItemUI>();
        }

        if (draggable != null && !draggable.IsDragging)
        {
            itemModalView.Show(new ItemModalViewData { Item = Item, PointerData = eventData });
            return;
        }

        itemModalView.Show(new ItemModalViewData { Item = Item, PointerData = eventData });

    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (draggable != null && !draggable.IsDragging)
        {
            itemModalView.Hide();
            return;

        }

        itemModalView.Hide();

    }

    public DraggableItemUI MakeDraggable()
    {
        return gameObject.AddComponent<DraggableItemUI>();
    }

    public SelectableItemUI MakeSelectable()
    {
        return gameObject.AddComponent<SelectableItemUI>();
    }
}


public struct ITemUIData
{
    public Item Item { get; set; }
    public ItemPanelUI Panel { get; set; }
}
