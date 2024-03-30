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
            CeasarCipher ceasar = new CeasarCipher();
            Assert.AreEqual("~", ceasar.Encrypt(-1, " "));
        }

        [TestMethod]
        public void EncryptUpperEndShiftToLowerEnd()
        {
            CeasarCipher ceasar = new CeasarCipher();
            Assert.AreEqual(" ", ceasar.Encrypt(1, "~"));
        }

        [TestMethod]
        public void EncryptPositiveShiftInMiddle()
        {
            CeasarCipher ceasar = new CeasarCipher();
            Assert.AreEqual("B", ceasar.Encrypt(1, "A"));
        }

        [TestMethod]
        public void EncryptNegativeShiftInMiddle()
        {
            CeasarCipher ceasar = new CeasarCipher();
            Assert.AreEqual("@", ceasar.Encrypt(-1, "A"));
        }

        [TestMethod]
        public void EncryptSimpleMessage()
        {
            CeasarCipher ceasar = new CeasarCipher();
            Assert.AreEqual("Fu!uv-!Csvuf@", ceasar.Encrypt(1, "Et tu, Brute?"));
        }

        [TestMethod]
        public void DecryptLowerEndShiftToUpperEnd()
        {
            CeasarCipher ceasar = new CeasarCipher();
            Assert.AreEqual(" ", ceasar.Decrypt(-1, "~"));
        }

        [TestMethod]
        public void DecryptUpperEndShiftToLowerEnd()
        {
            CeasarCipher ceasar = new CeasarCipher();
            Assert.AreEqual("~", ceasar.Decrypt(1, " "));
        }

        [TestMethod]
        public void DecryptPositiveShiftInMiddle()
        {
            CeasarCipher ceasar = new CeasarCipher();
            Assert.AreEqual("A", ceasar.Decrypt(1, "B"));
        }

        [TestMethod]
        public void DecryptNegativeShiftInMiddle()
        {
            CeasarCipher ceasar = new CeasarCipher();
            Assert.AreEqual("A", ceasar.Decrypt(-1, "@"));
        }

        [TestMethod]
        public void DecryptSimpleMessage()
        {
            CeasarCipher ceasar = new CeasarCipher();
            Assert.AreEqual("Et tu, Brute?", ceasar.Decrypt(1, "Fu!uv-!Csvuf@"));
        }

        [TestMethod]
        public void EncryptAndDecryptMessage()
        {
            string s = "Julius Ceasar was stabbed 23 times-- wild! In my opinion, he probably should have picked better friends...";

            CeasarCipher ceasar = new CeasarCipher();
            Assert.AreEqual(s, ceasar.Decrypt(37, ceasar.Encrypt(37, s)));
        }

        // --- RSA TESTS ---
        // soon...
    }
}