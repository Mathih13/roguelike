using System;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHotBarUI : MonoBehaviour, UIElement<PlayerHotBar>
{
    [SerializeField]
    GameObject uiItem;

    GameObject hotBarGO;

    HotBarSlot[] hotbarslots;
    List<Transform> uiSlots;

    public void Initialize(PlayerHotBar data)
    {
        hotBarGO = gameObject;
        GetUiSlots();
        UnRegisterItems();
        hotbarslots = data.GetSlots();
        RegisterItems();
    }

    private void GetUiSlots()
    {
        uiSlots = new List<Transform>();
        for (int i = 0; i < hotBarGO.transform.childCount; i++)
        {
            uiSlots.Add(hotBarGO.transform.GetChild(i).GetChild(0));
        }
    }

    private void UnRegisterItems()
    {
        uiSlots.ForEach(x => {
            if (x.childCount > 0)
            {
                Destroy(x.GetChild(0).gameObject);
            }
        });
    }

    private void RegisterItems()
    {
        for (int i = 0; i < hotbarslots.Length; i++)
        {
            if (hotbarslots[i].Item != null)
            {

                var obj = Instantiate(uiItem, uiSlots[i]);
                var itemUI = obj.GetComponent<ItemUI>();

                itemUI.Initialize(new ITemUIData { Item = hotbarslots[i].Item.GetGameItem() });

                var selectable = itemUI.MakeSelectable();
                selectable.Initialize(new SelectableItemUiData { Item = hotbarslots[i].Item.GetGameItem(), OnSelect = Select });

            }
        }
    }

    private void Select(ItemUI obj)
    {
        for (int i = 0; i < hotbarslots.Length; i++)
        {
            var hotbarItem = hotbarslots[i].Item;
            if (hotbarItem != null)
            {
                if (hotbarItem.GetGameItem().Equals(obj.Item))
                {
                    hotbarslots[i].UseCurrentItem(BoardManager.Instance.player.Combat);
                }
            }
            
        }
    }
}