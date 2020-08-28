using System.Collections;
using System.Collections.Generic;
using Moq;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

namespace Tests
{
    public class DiceRollTests
    {
        [Test]
        public void RollsCorrectValuesForDiceD4()
        {
            var diceRoll = new DiceRoll(DiceType.d4, 1, 0);

            AssertDiceRoll(diceRoll, 1, 4);

            var diceRollModifier = new DiceRoll(DiceType.d4, 1, 2);
            AssertDiceRollWithModifier(diceRollModifier, 1, 4);
        }

        [Test]
        public void RollsCorrectValuesForDiceD6()
        {
            var diceRoll = new DiceRoll(DiceType.d6, 1, 0);

            AssertDiceRoll(diceRoll, 1, 6);

            var diceRollModifier = new DiceRoll(DiceType.d6, 1, 2);
            AssertDiceRollWithModifier(diceRollModifier, 1, 6);
        }


        [Test]
        public void RollsCorrectValuesForDiceD8()
        {
            var diceRoll = new DiceRoll(DiceType.d8, 1, 0);

            AssertDiceRoll(diceRoll, 1, 8);

            var diceRollModifier = new DiceRoll(DiceType.d8, 1, 2);
            AssertDiceRollWithModifier(diceRollModifier, 1, 8);
        }

        [Test]
        public void RollsCorrectValuesForDiceD10()
        {
            var diceRoll = new DiceRoll(DiceType.d10, 1, 0);

            AssertDiceRoll(diceRoll, 1, 10);

            var diceRollModifier = new DiceRoll(DiceType.d10, 1, 2);
            AssertDiceRollWithModifier(diceRollModifier, 1, 10);
        }

        [Test]
        public void RollsCorrectValuesForDiceD12()
        {
            var diceRoll = new DiceRoll(DiceType.d12, 1, 0);

            AssertDiceRoll(diceRoll, 1, 12);

            var diceRollModifier = new DiceRoll(DiceType.d12, 1, 2);
            AssertDiceRollWithModifier(diceRollModifier, 1, 12);
        }

        [Test]
        public void RollsCorrectValuesForDiceD20()
        {
            var diceRoll = new DiceRoll(DiceType.d20, 1, 0);

            AssertDiceRoll(diceRoll, 1, 20);

            var diceRollModifier = new DiceRoll(DiceType.d20, 1, 2);
            AssertDiceRollWithModifier(diceRollModifier, 1, 20);
        }


        private void AssertDiceRoll(DiceRoll roll, int min, int max)
        {
            Assert.GreaterOrEqual(roll.Result, min);
            Assert.LessOrEqual(roll.Result, max);
        }

        private void AssertDiceRollWithModifier(DiceRoll roll, int min, int max)
        {
            if (roll.Modifier != 0)
            {
                min += roll.Modifier;
                max += roll.Modifier;
            }

            Assert.GreaterOrEqual(roll.Result, min);
            Assert.LessOrEqual(roll.Result, max);
        }
    }
}
