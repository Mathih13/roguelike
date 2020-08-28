using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ItemContainer : IContainer<Item>
{

    protected List<Item> items;

    public ItemContainer()
    {
        items = new List<Item>();
    }

    public bool Contains(Item element)
    {
        return items.Contains(element);
    }

    public Item Get(int index) => items[index];

    public Item Get(Item element)
    {
        if (items.Contains(element))
        {
            return Get(items.IndexOf(element));
        }

        Debug.LogError("Unable to find item " + element + " in " + GetType().Name + " data!");
        return null;
    }

    public List<Item> GetAll() => items;

    public virtual void Place(Item element) => items.Add(element);

    public virtual void Place(Item element, int index) => items.Insert(index, element);

    public virtual Item Take(int index)
    {
        var item = items[index];
        items.RemoveAt(index);
        return item;
    }

    public virtual Item Take(Item element)
    {
        var item = items[items.IndexOf(element)];
        items.Remove(element);
        return item;
    }
}
