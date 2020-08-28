using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class BoardItemContainer : NonCharacterBoardPiece, IContainer<Item>
{
    protected List<Item> items;
    protected bool open;

    protected virtual void Awake()
    {
        items = new List<Item>();

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
        if (items.Count >= index + 1)
        {
            var item = items[index];
            items.RemoveAt(index);
            return item;
        }

        Debug.LogError("Trying to take an item from a container outside of its index range");
        return null;
    }

    public virtual Item Take(Item element)
    {
        var item = items[items.IndexOf(element)];
        items.Remove(element);
        return item;
    }

    public virtual void Open()
    {
        open = true;
    }

    public virtual void Close()
    {
        open = false;
    }

    public bool Contains(Item element)
    {
        return items.Contains(element);
    }
}
