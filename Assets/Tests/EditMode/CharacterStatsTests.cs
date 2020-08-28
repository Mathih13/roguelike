using System.Collections;
using System.Collections.Generic;
using Moq;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

namespace Tests
{
    public class CharacterStatsTests
    {
        [Test]
        public void CharacterStatsReturnsCorrectModifierForValue()
        {
            var stats = new CharacterStats();
            stats.Charisma = new CharacterStat { Value = 10 };

            Assert.AreEqual(0, stats.Charisma.Modifier);

            stats.Charisma.Value = 31;
            Assert.AreEqual(10, stats.Charisma.Modifier);

            stats.Charisma.Value = 0;
            Assert.AreEqual(-5, stats.Charisma.Modifier);

            stats.Charisma.Value = 16;
            Assert.AreEqual(3, stats.Charisma.Modifier);

            stats.Charisma.Value = 11;
            Assert.AreEqual(0, stats.Charisma.Modifier);

            stats.Charisma.Value = 13;
            Assert.AreEqual(1, stats.Charisma.Modifier);
        }


    }
}
