using System.Collections;
using System.Collections.Generic;
using Moq;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

namespace Tests
{
    public class Classtests
    {
        [Test]
        public void ClassSetsCorrectValuesOnCreation()
        {
            var proficiencies = new Proficiencies { Armor = new List<ArmorProficiency> { ArmorProficiency.Light }, Weapon = new List<WeaponProficiency>() };
            var stats = new CharacterStats { Constitution = new CharacterStat { Value = 14 } };
            CombatEntity combatEntity = GenerateCombatEntity();

            var classToTest = new Class("Barbarian", 12, DiceType.d12, proficiencies, stats);
            classToTest.SetCombatEntity(combatEntity);
            Assert.AreEqual(1, classToTest.Level);
            Assert.AreEqual(DiceType.d12, classToTest.PerLevelHP);
            Assert.AreEqual(true, classToTest.Proficiencies.Armor.Contains(ArmorProficiency.Light));
            Assert.AreEqual(false, classToTest.Proficiencies.Armor.Contains(ArmorProficiency.Heavy));
            Assert.AreEqual(14, combatEntity.HP);
        }

        [Test]
        public void ClassAddsProficienciesWhenItDoesntContainIt()
        {
            var proficiencies = new Proficiencies { Armor = new List<ArmorProficiency> { ArmorProficiency.Light }, Weapon = new List<WeaponProficiency>() };
            var stats = new CharacterStats { Constitution = new CharacterStat { Value = 14 } };
            CombatEntity combatEntity = GenerateCombatEntity();

            var classToTest = new Class("Barbarian", 12, DiceType.d12, proficiencies, stats);

            classToTest.AddProficiency(WeaponProficiency.Simple);
            Assert.AreEqual(true, classToTest.Proficiencies.Weapon.Contains(WeaponProficiency.Simple));

            classToTest.AddProficiency(ArmorProficiency.Light);
            Assert.AreEqual(true, classToTest.Proficiencies.Armor.Contains(ArmorProficiency.Light));
        }

        [Test]
        public void ClassIncreasesMaxHPOnLevelUp()
        {
            var proficiencies = new Proficiencies { Armor = new List<ArmorProficiency> { ArmorProficiency.Light }, Weapon = new List<WeaponProficiency>() };
            var stats = new CharacterStats { Constitution = new CharacterStat { Value = 14 } };
            CombatEntity combatEntity = GenerateCombatEntity();

            var classToTest = new Class("Barbarian", 12, DiceType.d12, proficiencies, stats);
            classToTest.SetCombatEntity(combatEntity);

            classToTest.LevelUp();
            Assert.GreaterOrEqual(combatEntity.MaxHP, 17);
            Assert.LessOrEqual(combatEntity.MaxHP, 28);
        }


        private CombatEntity GenerateCombatEntity()
        {
            var charControllerMock = new Mock<CharacterController>();
            var combatEntity = new CombatEntity("Test", null, null, charControllerMock.Object, new CombatEntityEvents());
            return combatEntity;
        }
    }
}
