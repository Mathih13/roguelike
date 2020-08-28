using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Inventory : ItemContainer
{
    private IItemContainerView<Inventory> _view;


    public Inventory(IItemContainerView<Inventory> view)
    {
        _view = view;
        UpdateInventory();
    }

    public void Show()
    {
        UpdateInventory();
        _view.Show();
    }

    public void Hide()
    {
        _view.Hide();
    }

    public void UpdateInventory()
    {
        _view.SetData(this);
    }

    public override void Place(Item element)
    {
        base.Place(element);
        UpdateInventory();
    }

    public override void Place(Item element, int index)
    {
        base.Place(element, index);
        UpdateInventory();
    }

    public override Item Take(int index)
    {
        var result = base.Take(index);
        UpdateInventory();
        return result;
    }

    public override Item Take(Item element)
    {
        var result = base.Take(element);
        UpdateInventory();
        return result;
    }
}
