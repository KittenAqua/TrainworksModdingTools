using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MonsterTrainModdingAPI.Utilities;

namespace MonsterTrainModdingAPIUnitTests
{
    [TestClass]
    public class GUIDGeneratorTests
    {
        /// <summary>
        /// If the provider is reset and given the same input, the strings generated should be equal 
        /// </summary>
        [TestMethod]
        public void ResetGUIDTest()
        {
            for (int i = 0; i < 100; i++)
            {
                string number = i.ToString("X");
                GUIDGenerator.ResetProvider();
                string result1 = GUIDGenerator.GenerateDeterministicGUID(number);
                GUIDGenerator.ResetProvider();
                string result2 = GUIDGenerator.GenerateDeterministicGUID(number);
                Assert.AreEqual(result1, result2);
            }
        }

        /// <summary>
        /// Generating from the same key should have the same result no matter how many times its generated
        /// </summary>
        [TestMethod]
        public void ConsistencyTest()
        {
             GUIDGenerator.ResetProvider();
            for (int i = 0; i< 100; i++)
            {
                string genKey = i.ToString("X");
                string gen = GUIDGenerator.GenerateDeterministicGUID(genKey);
                for (int j = 0; j< 100; j++)
                {
                    string gen2 = GUIDGenerator.GenerateDeterministicGUID(genKey);
                    Assert.AreEqual(gen, gen2);
                }
            }
        }

        /// <summary>
        /// Makes sure that the GUID generated is UUID v4 compliant
        /// </summary>
        [TestMethod]
        public void UUIDVersion4Test()
        {
            Random random = new Random();
            for (int i = 0; i < 100; i++)
            {
                byte[] buffer = new byte[16];
                random.NextBytes(buffer);
                string conversion = System.Text.Encoding.UTF8.GetString(buffer);
                string gen = GUIDGenerator.GenerateDeterministicGUID(conversion);
                
                // Check if Index 14 is 4
                Assert.AreEqual(gen[14], '4');
                
                // Check if Index 19 is a,b,8,or 9
                char testchar = gen[19];
                if (testchar != 'a' && testchar != 'b' && testchar != '8' && testchar != '9')
                {
                    Assert.IsTrue(testchar == 'a' || testchar == 'b' || testchar == '8' || testchar == '9');
                }
            }
        }
    }
}
