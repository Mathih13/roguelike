using System;
using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

namespace Tests
{
    public class AttackTests
    {
        // A UnityTest behaves like a coroutine in Play Mode. In Edit Mode you can use
        // `yield return null;` to skip a frame.
        [UnityTest]
        public IEnumerator AttackTestsWithEnumeratorPasses()
        {
            yield return null;
            var gameObject = new GameObject();
            var controller1 = gameObject.AddComponent<CharacterController>();

            BoardManager.Instance.AddCharacterToGameBoard(controller1, new Vector3Int(0, 0, 0));
            controller1.MoveOrAttack(Direction.East);

            var x = BoardManager.Instance.GetPositionOfCharacter(controller1);

            // Use the Assert class to test conditions.
            // Use yield to skip a frame.
            
        }
    }
}
