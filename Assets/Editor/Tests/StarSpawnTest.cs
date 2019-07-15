using NUnit.Framework;
using UnityEngine;

namespace Editor.Tests
{
    public class StarSpawnTest
    {
        [Test]
        public void StarSpawnTestSimplePasses()
        {
            var starsGenerator = new GameObject();
            starsGenerator.AddComponent<StarsGeneration>();
            starsGenerator.GetComponent<StarsGeneration>().Awake();
            var starSum = starsGenerator.GetComponent<StarsGeneration>().mainStar + starsGenerator.GetComponent<StarsGeneration>().otherStar;
            Assert.AreEqual(5000, starSum); 

        }

    }
}
