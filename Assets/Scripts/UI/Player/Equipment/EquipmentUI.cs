using UnityEngine;
using System.Collections;
using TMPro;
using mhartveit;
using DG.Tweening;

public class EquipmentUI : MenuComponent, UIElement<Equipment, UIElementEvents<Item>>, IModalView
{
    public bool Open;


    [SerializeField]
    private TMP_Text title;


    //Slots
    [SerializeField]
    private WeaponSlotUI weaponSlot;

    [SerializeField]
    private ArmorSlotUI bodyArmor;

    public void Initialize(Equipment data, UIElementEvents<Item> events)
    {
        ItemEvents = events;
        weaponSlot.Initialize(new EquipmentSlotUIData<Weapon> { Item = data.MainWeapon.GetItem(), Slot = data.MainWeapon, Events = events });
        bodyArmor.Initialize(new EquipmentSlotUIData<Armor> { Item = data.BodyArmor.GetItem(), Slot = data.BodyArmor, Events = events });
        title.text = "Equipment";
    }

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
}
