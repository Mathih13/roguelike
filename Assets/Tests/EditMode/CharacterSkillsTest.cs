using UnityEngine;
using System.Collections;
using NUnit.Framework;
using Rougelike.EventLog;
using static Tests.CombatEntityTests;
using Moq;

namespace Tests
{
    public class CharacterSkillsTest
    {

        private IPlayerEventsProvider eventsProvider() => new PlayerEventsProvider();

        [Test]
        public void CharacterSkillCanBeCreated()
        {
            var skillToTest = new OneHandedSkill(eventsProvider());
            Assert.AreNotEqual(null, skillToTest);
        }


        [Test]
        public void CharacterSkillLevelCannotBeSetOutsideOfLevelBoundaries()
        {
            var skillToTest = new OneHandedSkill(eventsProvider());
            skillToTest.SetLevel(100);
            Assert.AreEqual(10, skillToTest.Level);

            skillToTest = new OneHandedSkill(eventsProvider());
            skillToTest.SetLevel(-1);
            Assert.AreEqual(1, skillToTest.Level);
        }

        [Test]
        public void CharacterSkillLevelIsOneIfNotSet()
        {
            var skillToTest = new OneHandedSkill(eventsProvider());

            Assert.AreEqual(1, skillToTest.Level);
        }

        [Test]
        public void CharacterSkillLevelCanBeSet()
        {
            var skillToTest = new OneHandedSkill(eventsProvider());
            skillToTest.SetLevel(2);
            Assert.AreEqual(2, skillToTest.Level);
        }

        [Test]
        public void CharacterSkillLevelCanIncreaseCurrentXP()
        {
            var skillToTest = new OneHandedSkill(eventsProvider());
            skillToTest.IncreaseXP(10);
            Assert.AreEqual(10, skillToTest.GetCurrentXP());
        }

        [Test]
        public void CharacterSkillLevelIncreasesWhenThresholdIsMet()
        {
            var skillToTest = new OneHandedSkill(eventsProvider());
            skillToTest.IncreaseXP(250);
            Assert.AreEqual(2, skillToTest.Level);
        }

        [Test]
        public void CharacterSkillCurrentXPResetsOnLevelUp()
        {
            var skillToTest = new OneHandedSkill(eventsProvider());
            skillToTest.IncreaseXP(250);
            Assert.AreEqual(0, skillToTest.GetCurrentXP());
        }

        [Test]
        public void CharacterSkillDoesNotTryToLevelPastMaxLevel()
        {
            var skillToTest = new OneHandedSkill(eventsProvider());
            skillToTest.SetLevel(20);
            skillToTest.IncreaseXP(10000);
            Assert.AreEqual(10, skillToTest.Level);
        }


        [Test]
        public void CharacterSkillDoesNotIncreaseXPWhenAtMaxLevel()
        {
            var skillToTest = new OneHandedSkill(eventsProvider());
            skillToTest.IncreaseXP(100);
            Assert.AreEqual(0, skillToTest.GetCurrentXP());
        }
        
    }
}
