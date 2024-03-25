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
///     adding a key to plain text
///     byte substitution
///     row shifting
///     column mixing
///     
/// Notes:
///     
/// 
/// </summary>

using System.Collections;
using System.Linq.Expressions;
using System.Security.Cryptography;

namespace Encrypt
{
    
    /// <summary>
    ///     This class implements an AES encryption.
    /// </summary>
    public class AES
    //internal class AES
    {
        private int encryptionLevel;
        private int rounds;
        private byte[] key;
        private byte[][] roundKeys;
        
        /// <summary>
        ///     Sets the key on instantiation.
        /// </summary>
        /// <param name="encryptionLevel"></param>
        public AES(int encryptionLevel)
        {
            // verify selected encryption level
            GenerateNewKey(encryptionLevel);
        }

        /// <summary>
        ///     Parameter is 128, 192, 256, 320, 512, etc...
        /// </summary>
        /// <param name="security"></param>
        /// <returns></returns>
        public void GenerateNewKey(int encryptionLevel)
        {
            this.encryptionLevel = encryptionLevel;
            this.rounds = (( (encryptionLevel / 64) - 2 ) * 2) + 10;
            byte[] randomBytes = new byte[encryptionLevel / 8];

            using (RandomNumberGenerator rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(randomBytes);
            }

            this.key = randomBytes;
        }

        /// <summary>
        ///     Provides the encryption key in the specified format.
        ///     Valid formats are "byte", "hex", and "base64" for 
        ///     representations that are an array of bytes, a
        ///     hexadecimal string, or Base64 string respectively.
        /// </summary>
        /// <param name="format"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentException"></exception>
        public object GetKey(string format)
        {
            switch (format) {
                case "bytes":
                    return this.key;
                case "hex":
                    return BitConverter.ToString(this.key).Replace("-", "");
                case "base64":
                    return Convert.ToBase64String(this.key);
                default:
                    throw new ArgumentException("Invalid format requested.");
            }   
        }

        private ArrayList SubdivideData()
        {
            throw new NotImplementedException();

        }
        
        public string Encrypt(string key, string plainText)
        {
            throw new NotImplementedException();
            string cipherText = "";
            // convert to bytes
            
            ExpandKey();

            ArrayList dataPackets = SubdivideData();

            for (int i = 0; i < rounds; i++)
            {
                SubBytes();
                ShiftRows();
                MixColumns();
                AddRoundKey(roundKeys[i + 1]);
            }

            return cipherText;
        }

        public string Decrypt(string key, string cipherText)
        {
            throw new NotImplementedException();
        }

        private byte[][] ExpandKey()
        {
            throw new NotImplementedException();
        }

        private byte[][] AddRoundKey()
        {
            throw new NotImplementedException();

            // xor
        }

        private byte[][] SubBytes()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        ///     Shifts each element in a row to the left the
        ///     number of times of the row number it is in.
        ///     
        ///     i.e. row zero doesn't shift, row 3 shifts left 3, etc.
        /// </summary>
        /// <param name="state"></param>
        /// <returns></returns>
        private byte[][] ShiftRows(byte[][] state)
        {
            for (int i = 1; i < state.Length; i++)
            {
                Shift(state, i, i);
            }

            return state;
        }

        private byte[][] Shift(byte[][] state, int row, int shift)
        {
            byte temp = state[0][row];

            for (int i = 0; i < state[0].Length-1; i++)
            {
                state[i][row] = state[i + 1][row];
            }

            state[3][row] = temp;

            return state;
        }

        private byte [][] MixColumns()
        {
            throw new NotImplementedException();
        }
    }
}
