using System;
using System.Collections.Generic;
using System.Text;
using System.Security.Cryptography;
using HarmonyLib;
using System.Reflection;

namespace MonsterTrainModdingAPI.Managers
{
    public class GUIDManager
    {
        public static MD5CryptoServiceProvider provider = new MD5CryptoServiceProvider();
        public static string byteString = "";
        public static void ResetProvider()
        {
            provider = new MD5CryptoServiceProvider();
        }
        public static string GenerateDeterministicGUID(string Key)
        {
            //Generate a Byte Array from a string using default encoding
            byte[] inputBytes = Encoding.Default.GetBytes(Key);
            //Use Bytes to Generate a Byte Array of Length 16
            byte[] hashBytes = provider.ComputeHash(inputBytes);

            hashBytes[7] = (byte)(0x40 | ((int)hashBytes[7] & 0xf));
            hashBytes[8] = (byte)(0x80 | ((int)hashBytes[8] & 0x3f));

            Guid guid = new Guid(hashBytes);
            return guid.ToString();
        }
        /// <summary>
        /// Tests to See if GUID generation meets all demands required
        /// </summary>
        public static void DetTest()
        {
            ResetTest();
            ConcurrenceTest();
            UUIDVersion4Test();
            //OutputKeyForFormatReasons();
        }
        /// <summary>
        /// If the Provider is Reset and given the same input, the strings generated should be equal 
        /// </summary>
        public static void ResetTest()
        {
            for (int i = 0; i < 100; i++)
            {
                string number = i.ToString("X");
                ResetProvider();
                string result1 = GenerateDeterministicGUID(number);
                ResetProvider();
                string result2 = GenerateDeterministicGUID(number);
                if (result1 != result2)
                {
                    MonsterTrainModdingAPI.API.Log(BepInEx.Logging.LogLevel.Error, $"Reset: {result1} != {result2}");
                    break;
                }
            }
        }
        /// <summary>
        /// Generating from the Same key should have the same result no matter how many times its generated
        /// </summary>
        public static void ConcurrenceTest()
        {
            ResetProvider();
            for (int i = 0; i < 100; i++)
            {
                string genKey = i.ToString("X");
                string gen = GenerateDeterministicGUID(genKey);
                for (int j = 0; j < 100; j++)
                {
                    string gen2 = GenerateDeterministicGUID(genKey);
                    if (gen != gen2)
                    {
                        MonsterTrainModdingAPI.API.Log(BepInEx.Logging.LogLevel.Error, $"Con: {gen} != {gen2}");
                        break;
                    }
                }
            }

        }

        public static void UUIDVersion4Test()
        {
            Random random = new Random();
            for (int i = 0; i < 100; i++)
            {
                byte[] buffer = new byte[16];
                random.NextBytes(buffer);
                string conversion = Encoding.UTF8.GetString(buffer);
                string gen = GenerateDeterministicGUID(conversion);

                bool value1 = gen[14] != '4';
                if (value1)
                {
                    MonsterTrainModdingAPI.API.Log(BepInEx.Logging.LogLevel.Info, $"Format Failed, no 4: {value1}:{gen}");
                }
                char testchar = gen[19];
                if(testchar != 'a' && testchar != 'b' && testchar != '8' && testchar != '9')
                {
                    MonsterTrainModdingAPI.API.Log(BepInEx.Logging.LogLevel.Info, $"Format Failed, no {testchar}: {gen}");
                }
                



            }
        }
        /// <summary>
        /// Goes through a couple of Random strings and outputs them to make sure format looks correct
        /// </summary>
        public static void OutputKeyForFormatReasons()
        {
            Random random = new Random();
            for (int i = 0; i < 100; i++)
            {
                byte[] buffer = new byte[16];
                random.NextBytes(buffer);
                string conversion = Encoding.UTF8.GetString(buffer);
                string gen = GenerateDeterministicGUID(conversion);
                MonsterTrainModdingAPI.API.Log(BepInEx.Logging.LogLevel.Info, $"{conversion} becomes {gen}");

            }
        }
    }
}
