using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

namespace Tests
{
    public class NewTestScript
    {
        [Test]
        public void TestIncrement()
        {
            //Assign
            var counter = new TestScript(0);
            //Act
            counter.Increment();
            //Assert
            Assert.AreEqual(1, counter.Count);
        }

    }
}