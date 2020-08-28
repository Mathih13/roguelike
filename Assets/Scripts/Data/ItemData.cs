using UnityEngine;
using System.Collections;
using System.IO;
using System.Collections.Generic;
using System;
using System.Linq;

public class ItemData : Singleton<ItemData>
{
    public List<SerializeableItem> Weapons { get; private set; }
    public List<SerializeableItem> Armors { get; private set; }
    public List<SerializeableItem> Items { get; private set; }

    [SerializeField]
    private DataSource dataSource;

    public void LoadItems()
    {
        Weapons = new List<SerializeableItem>();
        Armors = new List<SerializeableItem>();
        Items = new List<SerializeableItem>();
        

        var loader = new DataLoader<JsonDataContainer<SerializeableItem>>(dataSource);
        loader.LoadData();

        foreach (var item in loader.Memory.data)
        {
            switch (item.Type)
            {
                case "weapon":
                    Weapons.Add(item);
                    break;
                case "armor":
                    Armors.Add(item);
                    break;
                default:
                    Items.Add(item);
                    break;
            }
        }
    }

    public Armor MakeArmor()
    {
        var item = Armors[UnityEngine.Random.Range(0, Armors.Count)];

        return new Armor(item.ArmorClass, item.AllowDexBonus, item.MaxDexBonus, (ArmorProficiency)item.Proficiency, new ItemInfo { ArmorClass = item.ArmorClass.ToString(), Description = item.Description, ImageName = item.ImageName, Name = item.Name });
    }

    public Armor MakeArmor(string name)
    {
        var item = Armors.FirstOrDefault(x => x.Name.ToLower() == name.ToLower());

        if (item == null)
        {
            return MakeArmor();
        }

        return new Armor(item.ArmorClass, item.AllowDexBonus, item.MaxDexBonus, (ArmorProficiency)item.Proficiency, new ItemInfo { ArmorClass = item.ArmorClass.ToString(), Description = item.Description, ImageName = item.ImageName, Name = item.Name });
    }

    public Weapon MakeWeapon()
    {

        var item = Weapons[UnityEngine.Random.Range(0, Weapons.Count)];

        return new Weapon(
                        (DiceType)item.DamageDice,
                        (WeaponProficiency)item.Proficiency,
                        WeaponAttackType.Strength,
                        new ItemInfo
                        {
                            Description = item.Description,
                            Name = item.Name,
                            ImageName = item.ImageName,
                            DamageDice = Enum.GetName(typeof(DiceType), item.DamageDice)
                        });
    }

    public Weapon MakeWeapon(string name)
    {

        var item = Weapons.FirstOrDefault(x => x.Name.ToLower() == name.ToLower());

        if (item == null)
        {
            return MakeWeapon();
        }

        return new Weapon(
                        (DiceType)item.DamageDice,
                        (WeaponProficiency)item.Proficiency,
                        WeaponAttackType.Strength,
                        new ItemInfo
                        {
                            Description = item.Description,
                            Name = item.Name,
                            ImageName = item.ImageName,
                            DamageDice = Enum.GetName(typeof(DiceType), item.DamageDice)
                        });
    }
}
