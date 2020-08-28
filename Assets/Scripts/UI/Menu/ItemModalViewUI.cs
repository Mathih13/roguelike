using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using TMPro;
using UnityEngine.EventSystems;
using System;

public class ItemModalViewUI : MonoBehaviour, UIElement<ItemModalViewData>
{
    [SerializeField]
    private Image itemImage;

    [SerializeField]
    private TMP_Text itemName;

    [SerializeField]
    private TMP_Text statInfo;

    [SerializeField]
    private TMP_Text itemDescription;

    [SerializeField]
    private TMP_Text proficiencyRequirement;

    public void Initialize(ItemModalViewData data)
    {
        CleanCurrentText();

        itemImage.sprite = SpriteDB.Instance.GetSpriteFromName(data.Item.Info.ImageName);
        itemName.text = data.Item.Info.Name;
        itemDescription.text = data.Item.Info.Description;

        if (data.Item is Weapon)
        {
            DrawWeaponInfo((Weapon)data.Item);
            return;
        }

        if (data.Item is Armor)
        {
            DrawArmorInfo((Armor)data.Item);
            return;
        }

        statInfo.text = "";
        proficiencyRequirement.text = "";
    }

    private void DrawArmorInfo(Armor item)
    {
        statInfo.text = "Armor Class: " + item.Info.ArmorClass;
        proficiencyRequirement.text = $"{item.Proficiency.ToString()} armor";

    }

    private void DrawWeaponInfo(Weapon item)
    {
        statInfo.text = "Damage Dice: " + item.Info.DamageDice;
        proficiencyRequirement.text = $"{item.Proficiency.ToString()} weapon";
    }

    private void CleanCurrentText()
    {
        itemName.text = "";
        statInfo.text = "";
        itemDescription.text = "";
    }
}

public struct ItemModalViewData
{
    public Item Item { get; set; }
    public PointerEventData PointerData { get; set; }
}
