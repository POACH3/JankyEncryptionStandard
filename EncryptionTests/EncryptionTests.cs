/// <summary>
/// 
/// Author:       Trenton Stratton
/// Date started: 23-MAR-2024
/// Last updated: 30-MAR-2024
///
/// File Contents
///     
///     Ceasar Cipher tests
///     RSA tests (eventually)
///     AES tests (eventually)
///     
/// Notes:
/// 
/// </summary> 

using Encrypt;
using System.ComponentModel;
using System.Reflection;

namespace EncryptionTests
{
    [TestClass]
    public class EncryptionTests
    {
        
        // --- CEASAR CIPHER TESTS ---
        [TestMethod]
        public void EncryptLowerEndShiftToUpperEnd()
        {
            CeasarCipher ceasar = new CeasarCipher(-1);
            Assert.AreEqual("~", ceasar.Encrypt(" "));
        }

        [TestMethod]
        public void EncryptUpperEndShiftToLowerEnd()
        {
            CeasarCipher ceasar = new CeasarCipher(1);
            Assert.AreEqual(" ", ceasar.Encrypt("~"));
        }

        [TestMethod]
        public void EncryptPositiveShiftInMiddle()
        {
            CeasarCipher ceasar = new CeasarCipher(1);
            Assert.AreEqual("B", ceasar.Encrypt("A"));
        }

        [TestMethod]
        public void EncryptNegativeShiftInMiddle()
        {
            CeasarCipher ceasar = new CeasarCipher(-1);
            Assert.AreEqual("@", ceasar.Encrypt("A"));
        }

        [TestMethod]
        public void EncryptSimpleMessage()
        {
            CeasarCipher ceasar = new CeasarCipher(1);
            Assert.AreEqual("Fu!uv-!Csvuf@", ceasar.Encrypt("Et tu, Brute?"));
        }

        [TestMethod]
        public void DecryptLowerEndShiftToUpperEnd()
        {
            CeasarCipher ceasar = new CeasarCipher(-1);
            Assert.AreEqual(" ", ceasar.Decrypt("~"));
        }

        [TestMethod]
        public void DecryptUpperEndShiftToLowerEnd()
        {
            CeasarCipher ceasar = new CeasarCipher(1);
            Assert.AreEqual("~", ceasar.Decrypt(" "));
        }

        [TestMethod]
        public void DecryptPositiveShiftInMiddle()
        {
            CeasarCipher ceasar = new CeasarCipher(1);
            Assert.AreEqual("A", ceasar.Decrypt("B"));
        }

        [TestMethod]
        public void DecryptNegativeShiftInMiddle()
        {
            CeasarCipher ceasar = new CeasarCipher(-1);
            Assert.AreEqual("A", ceasar.Decrypt("@"));
        }

        [TestMethod]
        public void DecryptSimpleMessage()
        {
            CeasarCipher ceasar = new CeasarCipher(1);
            Assert.AreEqual("Et tu, Brute?", ceasar.Decrypt("Fu!uv-!Csvuf@"));
        }

        [TestMethod]
        public void EncryptAndDecryptMessage()
        {
            string s = "Julius Ceasar was stabbed 23 times-- wild! In my opinion, he probably should have picked better friends...";

            CeasarCipher ceasar = new CeasarCipher(37);
            Assert.AreEqual(s, ceasar.Decrypt(ceasar.Encrypt(s)));
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void InvalidOffset()
        {
            new CeasarCipher(95);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void InvalidCharacterEncrypt()
        {
            CeasarCipher ceasar = new CeasarCipher(1);
            ceasar.Encrypt("€");
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void InvalidCharacterDecrypt()
        {
            CeasarCipher ceasar = new CeasarCipher(1);
            ceasar.Decrypt("€");
        }



        // --- RSA TESTS ---
        // soon...



        // --- AES TESTS ---

        // using reflection to access a private method
        [TestMethod]
        public void Test1()
        {
            AES aes = new AES(128);
            Type type = aes.GetType();
            MethodInfo method = type.GetMethod("XOR", BindingFlags.NonPublic | BindingFlags.Instance);

            byte[] data1 = aes.ToBytes("1010");
            byte[] data2 = aes.ToBytes("0101");

            byte[] result = (byte[])method.Invoke(aes, new object[] { data1, data2 });

            Assert.AreEqual("1111", aes.ToStr(result));
        }

        // using reflection to access a private field and a private method
        [TestMethod]
        public void Test2()
        {
            AES aes = new AES(128);
            Type type = aes.GetType();

            // access private method directly using reflection
            MethodInfo method = type.GetMethod("XOR2", BindingFlags.NonPublic | BindingFlags.Instance);
            method.Invoke(aes, null);

            // access private field directly using reflection
            FieldInfo value1Field = type.GetField("encryptionLevel", BindingFlags.NonPublic | BindingFlags.Instance);
            int value1 = (int)value1Field.GetValue(aes);

            Assert.AreEqual(129, value1);
        }


        


    }
}