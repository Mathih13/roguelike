using System.Collections;
using System.Collections.Generic;
using Moq;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

namespace Tests
{
    public class EquipmentTests
    {


        [Test]
        public void EquippingArmorEquipsAndCallsEvent()
        {
            var equipment = SetupBasicEquipment();
            var onEquipCalled = false;

            equipment.BodyArmor = new ArmorSlot((Armor arm) => onEquipCalled = true, null, new List<ArmorProficiency> { ArmorProficiency.Light });
            var armor = new Armor(11, true, 0, ArmorProficiency.Light, new ItemInfo { Name = "TestArmor" } );
            equipment.BodyArmor.Equip(armor);
            Assert.AreEqual(armor, equipment.BodyArmor.GetItem());

        }

        [Test]
        public void EquippingWeaponEquipsAndCallsEvent()
        {
            var equipment = SetupBasicEquipment();
            var onEquipCalled = false;

            equipment.MainWeapon = new WeaponSlot((Weapon wep) => onEquipCalled = true, null, new List<WeaponProficiency> { WeaponProficiency.Martial });
            var weapon = new Weapon(DiceType.d10, WeaponProficiency.Martial, WeaponAttackType.Strength, new ItemInfo { Name = "TestWeapon" });
            equipment.MainWeapon.Equip(weapon);
            Assert.AreEqual(weapon, equipment.MainWeapon.GetItem());
            Assert.AreEqual(true, onEquipCalled);
        }

        [Test]
        public void UnEquippingWeaponUnEquipsAndCallsEvent()
        {
            var equipment = SetupBasicEquipment();
            var onUnEquipCalled = false;

            equipment.MainWeapon = new WeaponSlot(null, (Weapon wep) => onUnEquipCalled = true, new List<WeaponProficiency> { WeaponProficiency.Martial });
            var weapon = new Weapon(DiceType.d10, WeaponProficiency.Martial, WeaponAttackType.Strength, new ItemInfo { Name = "TestWeapon" });
            equipment.MainWeapon.Equip(weapon);
            equipment.MainWeapon.UnEquip();
            Assert.AreEqual(null, equipment.MainWeapon.GetItem());
            Assert.AreEqual(true, onUnEquipCalled);
        }

        [Test]
        public void UnEquippingArmorUnEquipsAndCallsEvent()
        {
            var equipment = SetupBasicEquipment();
            var onUnEquipCalled = false;

            equipment.BodyArmor = new ArmorSlot(null, (Armor arm) => onUnEquipCalled = true, new List<ArmorProficiency> { ArmorProficiency.Light });
            var armor = new Armor(11, false, 0, ArmorProficiency.Light, new ItemInfo { Name = "TestArmor" } );
            equipment.BodyArmor.Equip(armor);
            equipment.BodyArmor.UnEquip();
            Assert.AreEqual(null, equipment.BodyArmor.GetItem());
            Assert.AreEqual(true, onUnEquipCalled);

        }

        private Equipment SetupBasicEquipment()
        {
            var view = new Mock<IEquipmentView>();
            var events = new EquipmentEvents();
            var equipment = new Equipment(new List<WeaponProficiency>(), new List<ArmorProficiency>(), view.Object, events);
            return equipment;
        }
    }
}
