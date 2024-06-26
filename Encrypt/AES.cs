﻿/// <summary>
/// 
/// Author:       Trenton Stratton
/// Date started: 25-MAR-2024
/// Last updated: 10-APR-2024
///
/// File Contents: 
///     AES key generation
///     AES key setting with a key from a different source
///     get the primary AES key
///     key expansion
///     encryption of byte arrays
///     encryption of UTF-8 strings
///     decryption of byte arrays
///     decryption of Base64 strings 
///     add round key to data
///     subdivide data into 16 byte increments
///     create state array (byte array to 4x4 matrix)
///     revert from state array (4x4 matrix to byte array)
///     byte substitution of a 4x4 matrix
///     row shifting
///     modular arithmetic
///     column mixing
///     
/// Notes:
///     Need to refactor SubBytes and other
///     methods that have inverse for more DRY code.
/// 
/// </summary>

using System.Collections;
using System.Security.Cryptography;
using System.Text;

namespace Encrypt
{
    
    /// <summary>
    ///     This class provides AES encryption for byte arrays and 
    ///     UTF-8 text. UTF-8 text is returned as Base 64 text when
    ///     encrypted.
    ///     AES 128, 192, and 256 bit keys can be generated or 
    ///     set using a prior generated key.
    /// </summary>
    public class AES
    {
        private int _numBits;        // number of encryption bits... 128, 192, 256, 512... -- 16, 32, 64 bytes
        private int _numRounds;      // number of encryption rounds- depends on how many encryption bits
        private byte[] _key;         // the primary AES key
        private byte[][] _roundKeys; // all round keys generated from expanding the primary key

        private readonly byte[,] _mixColumnsMatrix = new byte[,]
            {
                { 0x02, 0x03, 0x01, 0x01 },
                { 0x01, 0x02, 0x03, 0x01 },
                { 0x01, 0x01, 0x02, 0x03 },
                { 0x03, 0x01, 0x01, 0x02 }
            };
        
        private readonly byte[] _sBox = new byte[]
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

        private readonly byte[] _inverseSbox =
            {
                0x52, 0x09, 0x6a, 0xd5, 0x30, 0x36, 0xa5, 0x38, 0xbf, 0x40, 0xa3, 0x9e, 0x81, 0xf3, 0xd7, 0xfb,
                0x7c, 0xe3, 0x39, 0x82, 0x9b, 0x2f, 0xff, 0x87, 0x34, 0x8e, 0x43, 0x44, 0xc4, 0xde, 0xe9, 0xcb,
                0x54, 0x7b, 0x94, 0x32, 0xa6, 0xc2, 0x23, 0x3d, 0xee, 0x4c, 0x95, 0x0b, 0x42, 0xfa, 0xc3, 0x4e,
                0x08, 0x2e, 0xa1, 0x66, 0x28, 0xd9, 0x24, 0xb2, 0x76, 0x5b, 0xa2, 0x49, 0x6d, 0x8b, 0xd1, 0x25,
                0x72, 0xf8, 0xf6, 0x64, 0x86, 0x68, 0x98, 0x16, 0xd4, 0xa4, 0x5c, 0xcc, 0x5d, 0x65, 0xb6, 0x92,
                0x6c, 0x70, 0x48, 0x50, 0xfd, 0xed, 0xb9, 0xda, 0x5e, 0x15, 0x46, 0x57, 0xa7, 0x8d, 0x9d, 0x84,
                0x90, 0xd8, 0xab, 0x00, 0x8c, 0xbc, 0xd3, 0x0a, 0xf7, 0xe4, 0x58, 0x05, 0xb8, 0xb3, 0x45, 0x06,
                0xd0, 0x2c, 0x1e, 0x8f, 0xca, 0x3f, 0x0f, 0x02, 0xc1, 0xaf, 0xbd, 0x03, 0x01, 0x13, 0x8a, 0x6b,
                0x3a, 0x91, 0x11, 0x41, 0x4f, 0x67, 0xdc, 0xea, 0x97, 0xf2, 0xcf, 0xce, 0xf0, 0xb4, 0xe6, 0x73,
                0x96, 0xac, 0x74, 0x22, 0xe7, 0xad, 0x35, 0x85, 0xe2, 0xf9, 0x37, 0xe8, 0x1c, 0x75, 0xdf, 0x6e,
                0x47, 0xf1, 0x1a, 0x71, 0x1d, 0x29, 0xc5, 0x89, 0x6f, 0xb7, 0x62, 0x0e, 0xaa, 0x18, 0xbe, 0x1b,
                0xfc, 0x56, 0x3e, 0x4b, 0xc6, 0xd2, 0x79, 0x20, 0x9a, 0xdb, 0xc0, 0xfe, 0x78, 0xcd, 0x5a, 0xf4,
                0x1f, 0xdd, 0xa8, 0x33, 0x88, 0x07, 0xc7, 0x31, 0xb1, 0x12, 0x10, 0x59, 0x27, 0x80, 0xec, 0x5f,
                0x60, 0x51, 0x7f, 0xa9, 0x19, 0xb5, 0x4a, 0x0d, 0x2d, 0xe5, 0x7a, 0x9f, 0x93, 0xc9, 0x9c, 0xef,
                0xa0, 0xe0, 0x3b, 0x4d, 0xae, 0x2a, 0xf5, 0xb0, 0xc8, 0xeb, 0xbb, 0x3c, 0x83, 0x53, 0x99, 0x61,
                0x17, 0x2b, 0x04, 0x7e, 0xba, 0x77, 0xd6, 0x26, 0xe1, 0x69, 0x14, 0x63, 0x55, 0x21, 0x0c, 0x7d
            };


        /// <summary>
        ///     Generates an AES key of the number of bits specified.
        /// </summary>
        /// <param name="numBits">Number of bits the key has.</param>
        public AES(int numBits)
        {
            //if (not a valid encryption level) {
            //throw new ArgumentException("AES encryption level not valid.");
            //}
            this._numBits = numBits;
            _numRounds = (((numBits / 64) - 2) * 2) + 10; //TODO: nine rounds for 128?
            _roundKeys = new byte[_numRounds+1][];

            GenerateNewKey(numBits);

            ExpandKey();
        }

        /// <summary>
        ///     Accepts a prior generated AES key of a valid number of bits.
        /// </summary>
        /// <param name="key">The key represented as a UTF-8 string</param>
        public AES(string key)
        {
            //if (not a valid key) {
            //throw new ArgumentException("Key is not valid.");
            //}

            this._key = Encoding.UTF8.GetBytes(key);

            ExpandKey();
        }

        /// <summary>
        ///     Parameter is 128, 192, 256.
        /// </summary>
        /// <param name="numBits">Number of bits the key has</param>
        public void GenerateNewKey(int numBits)
        {
            byte[] randomBytes = new byte[numBits / 8];

            using (RandomNumberGenerator rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(randomBytes);
            }

            this._key = randomBytes;

            //TODO: need to reset other fields- rounds, etc...
        }


        /// <summary>
        ///     Provides the encryption key in the specified format.
        ///     Valid formats are "byte", "hex", and "base64" for 
        ///     representations that are an array of bytes, a
        ///     hexadecimal string, or Base64 string respectively.
        /// </summary>
        /// <param name="format"></param>
        /// <returns>Returns the key as an object</returns>
        /// <exception cref="ArgumentException"></exception>
        public object GetKey(string format)
        {
            switch (format)
            {
                case "bytes":
                    return this._key;
                case "hex":
                    return BitConverter.ToString(this._key).Replace("-", "");
                case "base64":
                    return Convert.ToBase64String(this._key);
                default:
                    throw new ArgumentException("Invalid format requested.");
            }
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
        private void ExpandKey()
        {
            int wordLength = _numBits / 32;                 // number of bytes in each word
            byte[,] words = new byte[4, wordLength];        // four words of each key (changes each round)
            byte[] lastRoundKey = _key;                     // previous round key (begins with the unexpanded key)
            byte[] nextRoundKey = new byte[wordLength * 4]; // current round key

            byte[] roundConstants = new byte[] { 0x01, 0x02, 0x04, 0x08, 0x10, 0x20, 0x40, 0x80, 0x1B, 0x36, 0x6C, 0xD8, 0xAB, 0x4D };
            
            for (int round = 0; round < _numRounds; round++)
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

                        words[i, j] = _sBox[highNibble * 16 + lowNibble];
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

                byte[] copy = new byte[wordLength * 4];
                Array.Copy(nextRoundKey, copy, nextRoundKey.Length);
                _roundKeys[round] = copy;

                Array.Copy(nextRoundKey, lastRoundKey, nextRoundKey.Length);
            }
        }


        /// <summary>
        ///     Encrypts a byte array, returning it as a byte array.
        ///     
        ///     Encrypts data using the following steps:
        ///         1. subdivide plain text into 16 byte state arrays
        ///         2. XOR a state array with the first round key
        ///         3. main rounds (9 times for AES-128):
        ///             a. substitute state array with S-box bytes
        ///             b. shift rows
        ///             c. mix columns
        ///             d. add round key
        ///         4. final round:
        ///             a. substitute state array with S-box bytes
        ///             b. shift rows
        ///             c. add round key
        ///         5. repeat 2-4 with each state array created from the plain text
        /// </summary>
        /// <param name="plainTextBytes"></param>
        /// <returns>Encrypted byte array</returns>
        public byte[] Encrypt(byte[] plainTextBytes)
        {
            ArrayList stateArrays = SubdivideData(plainTextBytes);     // data represented as bytes broken into 16 byte matrices
            

            foreach (byte[,] stateArray in stateArrays)
            {
                AddRoundKey(1, stateArray);                            // add key to plain text before beginning rounds

                for (int i = 2; i < _numRounds+1; i++)
                {
                    SubBytes(stateArray, false);
                    ShiftRows(stateArray, false);
                    if (i != _numRounds+1) { MixColumns(stateArray); } // last round doesn't mix columns
                    AddRoundKey(i, stateArray);
                }
            }

            byte[] cipherTextBytes = new byte[stateArrays.Count * 16];

            for (int i = 0; i < stateArrays.Count; i++)
            {
                byte[] revertedStateArray = RevertStateArray((byte[,])stateArrays[i]);

                for (int j = 0; j < 16; j++)
                {
                    cipherTextBytes[(i * 16) + j] = revertedStateArray[j];
                }
            }

            return cipherTextBytes;
        }


        /// <summary>   
        ///     Encrypts a UTF-8 string, returning it as a Base64 string.  
        /// </summary>
        /// <param name="plainText"></param>
        /// <returns>Encrypted (Base64) text.</returns>
        public string Encrypt(string plainText)
        {
            byte[] plainTextBytes = Encoding.UTF8.GetBytes(plainText);

            return Convert.ToBase64String(Encrypt(plainTextBytes));
        }


        /// <summary>
        ///     Decrypts a byte array, returning it as a byte array.
        /// </summary>
        /// <param name="cipherText">The cipher text to be decrypted.</param>
        /// <returns>An array of bytes.</returns>
        public byte[] Decrypt(byte[] cipherTextBytes)
        {
            ArrayList stateArrays = SubdivideData(cipherTextBytes);   // data represented as bytes broken into 16 byte matrices

            foreach (byte[,] stateArray in stateArrays)
            {
                AddRoundKey(_roundKeys.Length - 1, stateArray);       // add last key to cipher text before beginning rounds

                for (int i = _numRounds-1; i > 0; i--)                     // go through round keys in reverse order
                {
                    SubBytes(stateArray, true);
                    ShiftRows(stateArray, true);
                    if (i != _numRounds - 2) { MixColumns(stateArray); } // last round doesn't mix columns
                    AddRoundKey(i, stateArray);
                }
            }

            byte[] plainTextBytes = new byte[stateArrays.Count * 16];

            for (int i = 0; i < stateArrays.Count; i++)
            {
                byte[] revertedStateArray = RevertStateArray((byte[,])stateArrays[i]);
                
                for (int j = 0; j < 16; j++)
                {
                    plainTextBytes[(i * 16) + j] = revertedStateArray[j];
                }
            }

            return plainTextBytes;
        }


        /// <summary>
        ///     Decrypts a Base64 string, returning it as a UTF-8 string.
        /// </summary>
        /// <param name="cipherText">The cipher text to be decrypted.</param>
        /// <returns>Cipher text</returns>
        public string Decrypt(string cipherText)
        {
            byte[] cipherTextBytes = Convert.FromBase64String(cipherText);

            return Encoding.UTF8.GetString(Decrypt(cipherTextBytes));
        }


        /// <summary>
        ///     XORs each byte of the provided state array with each
        ///     byte of the current round key.
        /// </summary>
        /// <param name="roundNumber">The number of the current round.</param>
        /// <param name="stateArray">The 4x4 matrix being encrypted.</param>
        /// <returns></returns>
        private byte[,] AddRoundKey(int roundNumber, byte[,] stateArray)
        {
            for (int i = 0; i < 4; i++)
            {
                for (int j = 0; j < 4; j++)
                {
                    stateArray[i,j] = (byte)(stateArray[i,j] ^ _roundKeys[roundNumber-1][(i * 4) + j]);
                }
            }

            return stateArray;
        }


        /// <summary>
        ///     Divides the data into 16 byte state arrays.
        ///     Padding (space characters) is added to the final 
        ///     state array if it is not a multiple of 16.
        /// </summary>
        /// <returns>An ArrayList of the subdivided data.</returns>
        private ArrayList SubdivideData(byte[] dataBytes)
        {
            ArrayList dividedData = new ArrayList();

            int numChars = dataBytes.Length;
            byte[] dataBytesPadded;

            if (numChars % 16 != 0) // add padding if not a multiple of 16
            {   
                int arrayLength = numChars + (16 - numChars % 16);
                dataBytesPadded = new byte[arrayLength];

                for (int i = 0; i < numChars; i++)
                {
                    dataBytesPadded[i] = dataBytes[i];
                }

                for (int i = numChars; i < arrayLength; i++)
                {
                    dataBytesPadded[i] = 0x20;
                }
            }
            else                    // no padding needed
            {
                dataBytesPadded = dataBytes;
            }

            //TODO: would be more efficient to save in ArrayList immediately

            // break into 16 byte chunks
            for (int i = 0; i < dataBytesPadded.Length; i += 16)
            {
                byte[] stateBlock = new byte[16];

                for (int j = 0; j < 16; j++)
                {
                    stateBlock[j] = dataBytesPadded[i + j];
                }
                dividedData.Add(CreateStateArray(stateBlock));
            }

            return dividedData;
        }


        /// <summary>
        ///     Turns a byte array of 16 elements into a 4x4 matrix.
        /// </summary>
        /// <param name="bytes"></param>
        /// <returns>The data arranged in a 4x4 matrix.</returns>
        private byte[,] CreateStateArray(byte[] bytes)
        {
            byte[,] stateBlock = new byte[4,4];

            for (int i = 0; i < 4; i++)
            {
                for (int j = 0; j < 4; j++)
                {
                    stateBlock[i,j] = bytes[i * 4 + j];
                }
            }

            return stateBlock;
        }


        /// <summary>
        ///     Converts a 4x4 byte matrix to a byte array of 16 elements.
        /// </summary>
        /// <param name="stateArray"></param>
        /// <returns></returns>
        private byte[] RevertStateArray(byte[,] stateArray)
        {
            byte[] bytes = new byte[16];

            for (int i = 0; i < 4; i++)
            {
                for (int j = 0; j < 4; j++)
                {
                    bytes[(i * 4) + j] = stateArray[i,j];
                }
            }

            return bytes;
        }


        /// <summary>
        ///     Substitutes an entire 4x4 matrix of bytes.
        /// </summary>
        /// <returns></returns>
        private byte[,] SubBytes(byte[,] stateArray, bool inverse)
        {
            if (inverse)
            {
                for (int i = 0; i < 4; i++)
                {
                    for (int j = 0; j < 4; j++)
                    {
                        int lowNibble = stateArray[i,j] & 0x0F;               // lower order half of the byte - column index of s-box
                        int highNibble = (byte)(stateArray[i,j] >> 4) & 0x0F; // higher order half of the byte - row index of s-box

                        stateArray[i,j] = _inverseSbox[highNibble * 16 + lowNibble];
                    }
                }
            }
            else
            {
                for (int i = 0; i < 4; i++)
                {
                    for (int j = 0; j < 4; j++)
                    {
                        int lowNibble = stateArray[i,j] & 0x0F;               // lower order half of the byte - column index of s-box
                        int highNibble = (byte)(stateArray[i,j] >> 4) & 0x0F; // higher order half of the byte - row index of s-box

                        stateArray[i,j] = _sBox[highNibble * 16 + lowNibble];
                    }
                }
            }

            return stateArray;
        }


        /// <summary>
        ///     Shifts each element in a row to the left the
        ///     number of times of the row number it is in.
        ///     
        ///     i.e. row zero doesn't shift, row 3 shifts left 3, etc.
        /// </summary>
        /// <param name="state"></param>
        /// <returns></returns>
        private byte[,] ShiftRows(byte[,] state, bool inverse)
        {
            //int direction = inverse ? -1 : 1; // determines the direction rows shift
            
            for (int i = 1; i < 4; i++)
            {
                //Shift(state, i, i * direction);
                //TODO: refactor to make DRY
                if (inverse)
                {
                    byte[] rowCopy = new byte[4];

                    for (int k = 0; k < 4; k++)
                    {
                        rowCopy[k] = state[i, k];
                    }

                    for (int j = 0; j < 4; j++)
                    {  
                        state[i,j] = rowCopy[Mod((j - i), 4)];
                    }
                }
                else
                {
                    byte[] rowCopy = new byte[4];

                    for (int k = 0; k < 4; k++)
                    {
                        rowCopy[k] = state[i, k];
                    }

                    for (int j = 0; j < 4; j++)
                    {
                        state[i,j] = rowCopy[(i + j) % 4];
                    }  
                }
            }

            return state;
        }


        /// <summary>
        ///     Performs modular arithmetic, functioning for negative 
        ///     integers as well.
        /// </summary>
        /// <param name="a"></param>
        /// <param name="m"></param>
        /// <returns></returns>
        private int Mod(int a, int m)
        {
            return (a%m + m) % m;
            //return a - m * Math.Floor(a / m);
        }


        /// <summary>
        ///     Replaces the state array with one with mixed columns.
        ///     This is done through matrix multiplication and XOR.
        /// </summary>
        /// <param name="stateArray"></param>
        /// <returns></returns>
        private byte[,] MixColumns(byte[,] stateArray)
        {
            byte[,] mixedCols = new byte[4,4];     // replacement for the state array passed in
            byte[] matrixMultResult = new byte[4]; // intermediate result of matrix multiplication for a single row
            
            for (int i = 0; i < 4; i++)            // for each column
            {
                for (int j = 0; j < 4; j++)        // for each byte of the column
                {
                    matrixMultResult[j] = (byte)(_mixColumnsMatrix[i,j] * stateArray[j,i]);
                    mixedCols[i,j] = (byte)(matrixMultResult[0] ^ matrixMultResult[1] ^ matrixMultResult[2] ^ matrixMultResult[3]);
                }
            }

            return mixedCols;
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



    }
}
