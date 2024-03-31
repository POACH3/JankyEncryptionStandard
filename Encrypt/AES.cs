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
using System.ComponentModel;
using System.Linq.Expressions;
using System.Security.Cryptography;
using System.Text;

namespace Encrypt
{
    
    /// <summary>
    ///     This class implements an AES encryption.
    /// </summary>
    public class AES
    //internal class AES
    {
        private int encryptLevel;   // 128, 256, 512... number of bits, 16, 32, 64 bytes
        private int rounds;         // number of encryption rounds- depends on encryptionLevel
        private byte[] originalKey; // the AES key
        private byte[][] roundKeys; // all round keys

        /// <summary>
        ///     Sets the key on instantiation.
        /// </summary>
        /// <param name="encryptLevel"></param>
        public AES(int encryptLevel)
        {
            //if (not a valid encryption level) {
            //throw new ArgumentException("AES encryption level not valid.");
            //}
            this.encryptLevel = encryptLevel;
            rounds = (((encryptLevel / 64) - 2) * 2) + 10;
            roundKeys = new byte[rounds][];

            GenerateNewKey(encryptLevel);
        }

        /// <summary>
        ///     Parameter is 128, 192, 256, 320, 512, etc...
        /// </summary>
        /// <param name="security"></param>
        /// <returns></returns>
        public void GenerateNewKey(int encryptLevel)
        {
            byte[] randomBytes = new byte[encryptLevel / 8];

            using (RandomNumberGenerator rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(randomBytes);
            }

            this.originalKey = randomBytes;
        }

        /// <summary>
        ///     Performs key expansion to generate all round keys.
        ///     
        ///     Steps performed to create the key for each round:
        ///         1. previous round key is subdivided into four words
        ///         2. each word is rotated one byte to the left
        ///         3. each byte of each word is substituted with the S-box
        ///         4. round constant is added to the first byte of the first word
        ///         5. words are combined
        ///         6. combined words is XORed with the previous round key
        /// </summary>
        public void ExpandKey() //private
        {
            int wordLength = encryptLevel / 32;             // number of bytes in each word
            byte[,] words = new byte[4, wordLength];        // four words of each key (changes each round)
            byte[] lastRoundKey = originalKey;              // previous round key
            byte[] nextRoundKey = new byte[wordLength * 4]; // current round key

            byte[] roundConstants = new byte[] { 0x01, 0x02, 0x04, 0x08, 0x10, 0x20, 0x40, 0x80, 0x1B, 0x36, 0x6C, 0xD8, 0xAB, 0x4D };
            byte[] sBox = new byte[]
            {
                0x63, 0x7C, 0x77, 0x7B, 0xF2, 0x6B, 0x6F, 0xC5, 0x30, 0x01, 0x67, 0x2B, 0xFE, 0xD7, 0xAB, 0x76,
                0xCA, 0x82, 0xC9, 0x7D, 0xFA, 0x59, 0x47, 0xF0, 0xAD, 0xD4, 0xA2, 0xAF, 0x9C, 0xA4, 0x72, 0xC0,
                0xB7, 0xFD, 0x93, 0x26, 0x36, 0x3F, 0xF7, 0xCC, 0x34, 0xA5, 0xE5, 0xF1, 0x71, 0xD8, 0x31, 0x15,
                0x04, 0xC7, 0x23, 0xC3, 0x18, 0x96, 0x05, 0x9A, 0x07, 0x12, 0x80, 0xE2, 0xEB, 0x27, 0xB2, 0x75,
                0x09, 0x83, 0x2C, 0x1A, 0x1B, 0x6E, 0x5A, 0xA0, 0x52, 0x3B, 0xD6, 0xB3, 0x29, 0xE3, 0x2F, 0x84,
                0x53, 0xD1, 0x00, 0xED, 0x20, 0xFC, 0xB1, 0x5B, 0x6A, 0xCB, 0xBE, 0x39, 0x4A, 0x4C, 0x58, 0xCF,
                0xD0, 0xEF, 0xAA, 0xFB, 0x43, 0x4D, 0x33, 0x85, 0x45, 0xF9, 0x02, 0x7F, 0x50, 0x3C, 0x9F, 0xA8,
                0x51, 0xA3, 0x40, 0x8F, 0x92, 0x9D, 0x38, 0xF5, 0xBC, 0xB6, 0xDA, 0x21, 0x10, 0xFF, 0xF3, 0xD2,
                0xCD, 0x0C, 0x13, 0xEC, 0x5F, 0x97, 0x44, 0x17, 0xC4, 0xA7, 0x7E, 0x3D, 0x64, 0x5D, 0x19, 0x73,
                0x60, 0x81, 0x4F, 0xDC, 0x22, 0x2A, 0x90, 0x88, 0x46, 0xEE, 0xB8, 0x14, 0xDE, 0x5E, 0x0B, 0xDB,
                0xE0, 0x32, 0x3A, 0x0A, 0x49, 0x06, 0x24, 0x5C, 0xC2, 0xD3, 0xAC, 0x62, 0x91, 0x95, 0xE4, 0x79,
                0xE7, 0xC8, 0x37, 0x6D, 0x8D, 0xD5, 0x4E, 0xA9, 0x6C, 0x56, 0xF4, 0xEA, 0x65, 0x7A, 0xAE, 0x08,
                0xBA, 0x78, 0x25, 0x2E, 0x1C, 0xA6, 0xB4, 0xC6, 0xE8, 0xDD, 0x74, 0x1F, 0x4B, 0xBD, 0x8B, 0x8A,
                0x70, 0x3E, 0xB5, 0x66, 0x48, 0x03, 0xF6, 0x0E, 0x61, 0x35, 0x57, 0xB9, 0x86, 0xC1, 0x1D, 0x9E,
                0xE1, 0xF8, 0x98, 0x11, 0x69, 0xD9, 0x8E, 0x94, 0x9B, 0x1E, 0x87, 0xE9, 0xCE, 0x55, 0x28, 0xDF,
                0x8C, 0xA1, 0x89, 0x0D, 0xBF, 0xE6, 0x42, 0x68, 0x41, 0x99, 0x2D, 0x0F, 0xB0, 0x54, 0xBB, 0x16
            };


            for (int round = 0; round < rounds; round++)
            {
                // key subdivision
                for (int i = 0; i < 4; i++)
                {
                    for (int j = 0; j < wordLength; j++)
                    {
                        words[i, j] = lastRoundKey[(i * 4) + j];
                    }
                }

                // rotate bytes in each word
                byte temp;

                for (int i = 0; i < 4; i++)
                {
                    temp = words[i, 0];

                    for (int j = 0; j < wordLength; j++)
                    {
                        if (j < wordLength - 1)
                            words[i, j] = words[i, j + 1];
                        else
                            words[i, wordLength - 1] = temp;
                    }
                }

                // s-box byte substitution
                int lowNibble;  // lower order half of the byte - column index of s-box
                int highNibble; // higher order half of the byte - row index of s-box

                for (int i = 0; i < 4; i++) // each word
                {
                    for (int j = 0; j < wordLength; j++) // byte in the word
                    {
                        lowNibble = words[i, j] & 0x0F;
                        //highNibble = words[i, j] & 0xF0 >> 4;
                        highNibble = (byte)(words[i, j] >> 4) & 0x0F;

                        words[i, j] = sBox[highNibble * 16 + lowNibble];
                    }
                }

                // add round constant
                words[0, 0] = (byte)(words[0, 0] + roundConstants[round]);

                // combine words
                for (int i = 0; i < 4; i++)
                {
                    for (int j = 0; j < 4; j++)
                    {
                        nextRoundKey[(i * 4) + j] = words[i, j];
                    }
                }

                // XOR with previous round key
                for (int i = 0; i < nextRoundKey.Length; i++)
                {
                    nextRoundKey[i] = (byte)(nextRoundKey[i] ^ lastRoundKey[i]);
                }

                //roundKeys[round] = nextRoundKey;
                byte[] copy = new byte[wordLength * 4];
                Array.Copy(nextRoundKey, copy, nextRoundKey.Length);
                roundKeys[round] = copy;

                Array.Copy(nextRoundKey, lastRoundKey, nextRoundKey.Length);
                //lastRoundKey = nextRoundKey;
            }
        }


        // METHODS FOR EXPERIMENTING WITH TESTS USING REFLECTION
        //private byte[] XOR(byte[] a1, byte[] a2)
        //{
        //    byte[] result = new byte[a1.Length];

        //    for (int i = 0; i < a1.Length; i++)
        //    {
        //        result[i] = (byte)(a1[i] ^ a2[i]);
        //    }

        //    return result;
        //}

        //private void XOR2()
        //{
        //    encryptionLevel = 129;
        //}

        public byte[] Bitwise(byte[] input1, byte[] input2)
        {
            byte[] result = new byte[input1.Length];

            for (int i = 0; i < input1.Length; i++)
            {
                result[i] = (byte)(input1[i] ^ input2[i]);
            }

            return result;
        }

        public byte[] ToBytes(string input)
        {
            byte[] array = new byte[input.Length];
            
            for (int i  = 0; i < array.Length; i++)
            {
                array[i] = (byte)Convert.ToInt16(input[i].ToString(), 2);
            }

            return array;
        }

        public string ToStr(byte[] bytes)
        {
            StringBuilder sb = new StringBuilder();
            
            foreach (byte b in bytes)
            {
                sb.Append(b);
            }

            return sb.ToString();
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
                    return this.originalKey;
                case "hex":
                    return BitConverter.ToString(this.originalKey).Replace("-", "");
                case "base64":
                    return Convert.ToBase64String(this.originalKey);
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
            
            //string cipherText = "";
            //// convert to bytes
            
            //ExpandKey();

            //ArrayList dataPackets = SubdivideData();

            //AddRoundKey(); // always add key to plain text before beginning rounds

            //for (int i = 0; i < rounds; i++)
            //{  
            //    SubBytes();
            //    ShiftRows();
            //    if (i != rounds - 1) { MixColumns(); } // last round doesn't mix columns
            //    AddRoundKey(roundKeys[i + 1]);
            //}

            //return cipherText;
        }

        public string Decrypt(string key, string cipherText)
        {
            throw new NotImplementedException();
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
