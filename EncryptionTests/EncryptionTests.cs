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

        // --- RSA TESTS ---
        // soon...
    }
}