using UnityEngine;
using System.Collections;
using System;

public class EquipmentBehaviour : MonoBehaviour, IEquipmentView
{
    private Equipment currentEquipment;
    [SerializeField]
    private EquipmentUI ui;

    public void Hide()
    {
        ui.Hide();
    }

    public void SetData(Equipment data)
    {
        currentEquipment = data;
        ui.Initialize(currentEquipment, new UIElementEvents<Item> { OnUIDropFrom = OnUIDropFrom });
    }

    private void OnUIDropFrom(Item obj)
    {
        currentEquipment.Remove(obj);
    }

    public void Show()
    {
        ui.Show();
    }

    public void ShowHide()
    {
        if (ui.Open)
        {
            Hide();
            return;
        }

        Show();
    }
}
