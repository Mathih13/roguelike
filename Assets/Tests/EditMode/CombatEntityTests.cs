using System.Collections;
using System.Collections.Generic;
using Moq;
using NUnit.Framework;
using Rougelike.EventLog;
using UnityEngine;
using UnityEngine.TestTools;

namespace Tests
{
    public class CombatEntityTests
    {
        [Test]
        public void CombatEntityTakesDamage()
        {
            var charControllerMock = new Mock<CharacterController>();
            var combatEntity = new CombatEntity("Test", null, null, charControllerMock.Object, new CombatEntityEvents());
            combatEntity.MaxHP = 20;
            combatEntity.HP = 20;

            var attackResult = new AttackCalculationDataFake(null, combatEntity, 2).Execute();
            combatEntity.TakeDamage(attackResult);
            Assert.AreEqual(18, combatEntity.HP);
        }

        [Test]
        public void CombatEntityDiesWhenDamageIsMoreThanRemainingHealth()
        {
            var charControllerMock = new Mock<CharacterController>();
            var onDeathCalled = false;
            var combatEntity = new CombatEntity("Test", null, null, charControllerMock.Object,
                new CombatEntityEvents
                { onDeath = (CombatEntity entity, AttackCalculationData data) => onDeathCalled = true }
            );
            combatEntity.HP = 2;

            var attackResult = new AttackCalculationDataFake(null, combatEntity, 3).Execute();
            combatEntity.TakeDamage(attackResult);
            Assert.AreEqual(true, onDeathCalled);
        }

        [Test]
        public void CombatEntityHeals()
        {
            var charControllerMock = new Mock<CharacterController>();
            var combatEntity = new CombatEntity("Test", null, null, charControllerMock.Object, new CombatEntityEvents());
            combatEntity.MaxHP = 20;
            combatEntity.HP = 5;

            combatEntity.Heal(new HealData(5, combatEntity));

            Assert.AreEqual(10, combatEntity.HP);
        }

        [Test]
        public void CombatEntityStopsHealingAtMaxHealth()
        {
            var charControllerMock = new Mock<CharacterController>();
            var combatEntity = new CombatEntity("Test", null, null, charControllerMock.Object, new CombatEntityEvents());
            combatEntity.MaxHP = 20;
            combatEntity.HP = 18;

            combatEntity.Heal(new HealData(3, combatEntity));

            Assert.AreEqual(20, combatEntity.HP);
        }

        [Test]
        public void CombatEntityUpdatesArmorClassWhenArmorChanges()
        {


            var data = SetupCombatEntityWithStats(new CharacterStats());

            var pItems = data.Items;
            var combatEntity = data.CombatEntity;

            Assert.AreEqual(10, combatEntity.ArmorClass);

            var armorToEquip = new Armor(18, false, 0, ArmorProficiency.Heavy, null);
            pItems.Inventory.Place(armorToEquip);
            pItems.Equipment.BodyArmor.Equip(armorToEquip);

            Assert.AreEqual(18, combatEntity.ArmorClass);
        }

        [Test]
        public void CalculateArmorClassReturnsBaseACWithNoArmor()
        {


            var stats = new CharacterStats { Dexterity = new CharacterStat { Value = 10 } };
            var data = SetupCombatEntityWithStats(stats);

            Assert.AreEqual(10, data.CombatEntity.ArmorClass);
        }

        [Test]
        public void CalculateArmorClassReturnsPureACWhenArmorHasNoDexModifier()
        {
            var stats = new CharacterStats { Dexterity = new CharacterStat { Value = 10 } };
            var data = SetupCombatEntityWithStats(stats);
            data.Items.Equipment.BodyArmor = new ArmorSlot(null, null, new List<ArmorProficiency> { ArmorProficiency.Heavy });


            data.Items.Equipment.BodyArmor.Equip(new Armor(18, false, 0, ArmorProficiency.Heavy, null));

            Assert.AreEqual(18, data.CombatEntity.ArmorClass);
        }

        [Test]
        public void CalculateArmorClassReturnsCorrectACWithDexModifier()
        {
            // Test if armor allows dex mod bonus
            var stats = new CharacterStats { Dexterity = new CharacterStat { Value = 14 } };
            var data = SetupCombatEntityWithStats(stats);

            data.Items.Equipment.BodyArmor = new ArmorSlot(null, null, new List<ArmorProficiency> { ArmorProficiency.Medium });
            data.Items.Equipment.BodyArmor.Equip(new Armor(11, true, 2, ArmorProficiency.Medium, null));

            Assert.AreEqual(13, data.CombatEntity.ArmorClass);



            // Test that armor caps on max dex bonus
            stats = new CharacterStats { Dexterity = new CharacterStat { Value = 14 } };
            data = SetupCombatEntityWithStats(stats);

            data.Items.Equipment.BodyArmor = new ArmorSlot(null, null, new List<ArmorProficiency> { ArmorProficiency.Light });
            data.Items.Equipment.BodyArmor.Equip(new Armor(11, true, 2, ArmorProficiency.Light, null));

            Assert.AreEqual(13, data.CombatEntity.ArmorClass);
        }

        [Test]
        public void CalculateArmorClassReturnsCorrectACWithNoDexCap()
        {
            var stats = new CharacterStats { Dexterity = new CharacterStat { Value = 20 } };
            var data = SetupCombatEntityWithStats(stats);
            data.Items.Equipment.BodyArmor = new ArmorSlot(null, null, new List<ArmorProficiency> { ArmorProficiency.Light });
            data.Items.Equipment.BodyArmor.Equip(new Armor(11, true, 0, ArmorProficiency.Light, null));

            // Test if armor allows dex mod bonus
            Assert.AreEqual(16, data.CombatEntity.ArmorClass);


        }


        private CombatEntityTestData SetupCombatEntityWithStats(CharacterStats stats)
        {
            var charControllerMock = new Mock<CharacterController>();
            var inventoryViewMock = new Mock<IItemContainerView<Inventory>>();
            var equipmentViewMock = new Mock<IEquipmentView>();
            var charSheetViewMock = new Mock<ICharacterSheetView>();

            var data = new CombatEntityTestData();

            var charSheet = new CharacterSheet(charSheetViewMock.Object);
            charSheet.SetStats(stats);

            charSheet.SetClass(MakeTestClass(charSheet));

            data.Items = new PlayerItems(charSheet, inventoryViewMock.Object, equipmentViewMock.Object);
            data.CombatEntity = new CombatEntity("Test", data.Items.Equipment, charSheet.Stats, null, new CombatEntityEvents());
            charSheet.CharacterClass.SetCombatEntity(data.CombatEntity);
            return data;
        }

        private Class MakeTestClass(CharacterSheet charSheet)
        {
            return new Class(
                  "Test",
                  10,
                  DiceType.d10,
                  new Proficiencies
                  {
                      Armor = new List<ArmorProficiency>
                          {
                            ArmorProficiency.Heavy,
                            ArmorProficiency.Light,
                            ArmorProficiency.Medium,
                            ArmorProficiency.Shields
                          },
                      Weapon = new List<WeaponProficiency>
                          {
                            WeaponProficiency.Exotic,
                            WeaponProficiency.Martial,
                            WeaponProficiency.Simple
                          }
                  },
                  charSheet.Stats
                  );
        }

        private struct CombatEntityTestData
        {
            public PlayerItems Items { get; set; }
            public CombatEntity CombatEntity { get; set; }
        }

        public class AttackCalculationDataFake : AttackCalculationData
        {
            private int _fakeDamage = 0;
            public AttackCalculationDataFake(CombatEntity attacker, CombatEntity target, int fakeDamage) : base(attacker, target)
            {
                _fakeDamage = fakeDamage;
            }

            public override AttackCalculationData Execute()
            {
                Hit = true;
                Damage = _fakeDamage;
                return this;
            }
        }
    }
}
