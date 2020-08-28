using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using System;

public class SelectableItemUI : MonoBehaviour, IPointerClickHandler, UIElement<SelectableItemUiData>
{
    public Item Item;

    private Color SelectedColor;
    private Color DefaultColor;

    public void OnPointerClick(PointerEventData eventData) => Select();

    private ItemPanelUI panel;
    private Image background;
    private Action<ItemUI> onSelect;

    private void Start()
    {
        background = GetComponent<Image>();
        DefaultColor = background.color;
    }

    private void Select()
    {
        if (panel != null)
        {
            panel.Select(this);
            background.color = SelectedColor;
        }

        onSelect?.Invoke(GetComponent<ItemUI>());
    }

    public void DeSelect()
    {
        if (background != null)
        {
            background.color = DefaultColor;
        }
    }

    public void Initialize(SelectableItemUiData data)
    {
        panel = data.Panel;
        SelectedColor = data.SelectedColor;
        Item = data.Item;
        onSelect = data.OnSelect;
    }
}

public struct SelectableItemUiData
{
    public ItemPanelUI Panel { get; set; }
    public Item Item { get; set; }
    public Color SelectedColor { get; set; }
    public Action<ItemUI> OnSelect { get; set; }
}
