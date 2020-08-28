using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ClassInfoUI : MonoBehaviour, UIElement<Class>
{

    [SerializeField]
    private TextMeshProUGUI classTitle;

    [SerializeField]
    private TextMeshProUGUI weaponProficiencies;

    [SerializeField]
    private TextMeshProUGUI armorProficiencies;


    public void Initialize(Class data)
    {
        if (data != null)
        {
            classTitle.text = data.Name;

            string delimiter = ",";
            var weaponTypes = data.Proficiencies.Weapon.Select(x => Enum.GetName(typeof(WeaponProficiency), x));
            var armorTypes = data.Proficiencies.Armor.Select(x => Enum.GetName(typeof(ArmorProficiency), x));

            var weaponString = weaponTypes.Aggregate((i, j) => i + delimiter + j);
            var armorString = armorTypes.Aggregate((i, j) => i + delimiter + j);

            weaponProficiencies.text = weaponString;
            armorProficiencies.text = armorString;
        }
    }
}
