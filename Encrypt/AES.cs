/// <summary>
/// 
/// Author:       Trenton Stratton
/// Date started: 25-MAR-2024
/// Last updated: 25-MAR-2024
///
/// File Contents: 
///     AES key generation
///     encryption
///     decryption
///     key expansion
///     adding a key
///     byte substitution
///     row shifting
///     column mixing
///     
/// Notes:
///     
/// 
/// </summary>

namespace Encrypt
{
    public class AES
    //internal class AES
    {
        public AES() { }

        /// <summary>
        ///     Parameter is 128, 256, 512, etc...
        /// </summary>
        /// <param name="security"></param>
        /// <returns></returns>
        public string GenerateKey(int security)
        {

        }
        
        public string Encrypt(string key, string plainText)
        {
            throw new NotImplementedException();
        }

        public string Decrypt(string key, string cipherText)
        {
            throw new NotImplementedException();
        }

        public Array ExpandKey()
        {
            throw new NotImplementedException();
        }

        public byte[][] AddRoundKey()
        {
            throw new NotImplementedException();
        }

        public byte[][] SubBytes()
        {
            throw new NotImplementedException();
        }

        public byte[][] ShiftRows()
        {
            throw new NotImplementedException();
        }

        public byte [][] MixColumns()
        {
            throw new NotImplementedException();
        }
    }
}
