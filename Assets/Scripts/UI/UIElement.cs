using UnityEngine;
using System.Collections;
using System;

public interface UIElement
{
    void Initialize();
    void UpdateUI();
}

public interface UIElement<T>
{
    void Initialize(T data);
}

public interface UIElement<T, UIElementEvents>
{
    void Initialize(T data, UIElementEvents events);
}


public interface UpdatableUIElement<T> : UIElement<T>
{
    void UpdateUI(T data);
}

public struct UIElementEvents<T>
{
    public Action<T> OnSelect { get; set; }
    public Action<T> OnDeselect { get; set; }
    public Action OnHide { get; set; }
    public Action OnShow { get; set; }
    public Action<T> OnEquip { get; set; }
    public Action<T> OnUnEquip { get; set; }
    public Action<T> OnDrop { get; set; }
    public Action<T> OnUIDropTo { get; set; }
    public Action<T> OnUIDropFrom { get; set; }
}


