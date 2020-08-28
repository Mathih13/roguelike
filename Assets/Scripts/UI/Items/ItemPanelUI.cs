using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;
using DG.Tweening;
using TMPro;
using mhartveit;

public class ItemPanelUI : MenuComponent, UIElement<ItemPanelData<IEnumerable<Item>>, UIElementEvents<Item>>
{

    public RectTransform ContentPanel;
    public GameObject ItemTemplate;
    public bool Open;


    [SerializeField]
    protected TMP_Text Title;

    [SerializeField]
    private Color SelectedColor;

    protected List<ItemUI> Items;

    private SelectableItemUI lastSelected;



    public void Initialize(ItemPanelData<IEnumerable<Item>> itemPanelData, UIElementEvents<Item> events)
    {
        if (Items != null)
        {
            UnregisterItems();
        }

        ItemEvents = events;
        Title.text = itemPanelData.Title;
        Items = new List<ItemUI>();

        foreach (var item in itemPanelData.Data)
        {
            RegisterItem(item, itemPanelData.Options);
        }
    }



    public void Deselect(ItemUI item) => ItemEvents.OnDeselect?.Invoke(item.Item);

    public void Equip() => ItemEvents.OnEquip?.Invoke(lastSelected.Item);

    public void Drop() => ItemEvents.OnDrop?.Invoke(lastSelected.Item);

    public void Show()
    {
        if (!Open)
        {
            transform.localScale = new Vector3(0, 0);
            gameObject.SetActive(true);
            UI.Instance.OpenAnimation(transform).OnComplete(() =>
            {
                ItemEvents.OnShow?.Invoke();
                Open = true;
            });
        }
    }

    public void Hide()
    {
        if (Open)
        {
            UI.Instance.CloseAnimation(transform).OnComplete(() =>
            {
                ItemEvents.OnHide?.Invoke();
                Open = false;
                gameObject.SetActive(false);
            });
        }
    }


    private void RegisterItem(Item item, ItemPanelOptions options)
    {
        var obj = Instantiate(ItemTemplate, ContentPanel);
        var uiRect = (RectTransform)obj.transform;
        uiRect.SetParent(ContentPanel);


        var itemUI = obj.GetComponent<ItemUI>();
        InitializeOptionalComponents(options, itemUI, item);


        Items.Add(itemUI);
        itemUI.Initialize(new ITemUIData { Item = item, Panel = this });
    }

    private void InitializeOptionalComponents(ItemPanelOptions options, ItemUI itemUI, Item item)
    {
        if (options.Selectable)
        {
            var selectable = itemUI.MakeSelectable();
            selectable.Initialize(new SelectableItemUiData { Item = item, Panel = this, SelectedColor = SelectedColor });
        }

        if (options.Draggable)
        {
            itemUI.MakeDraggable();
        }
    }

    private void UnregisterItems()
    {
        foreach (var item in Items)
        {
            var cast = (ItemUI)item;
            Destroy(cast.gameObject);
        }
    }

    public virtual void Select(SelectableItemUI selected)
    {
        lastSelected?.DeSelect();
        ItemEvents.OnSelect?.Invoke(selected.GetComponent<ItemUI>().Item);
        lastSelected = selected;
    }
}

public struct ItemPanelData<T>
{
    public T Data { get; set; }
    public string Title { get; set; }
    public ItemPanelOptions Options { get; set; }
}

public struct ItemPanelOptions
{
    public bool Draggable { get; set; }
    public bool Selectable { get; set; }
}