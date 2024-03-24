/// <summary>
/// 
/// Author:       Trenton Stratton
/// Date started: 23-MAR-2024
/// Last updated: 24-MAR-2024
///
/// File Contents:
///     encryption
///     decryption
///     
/// Notes:
///     I am not sure I am happy about the valid characters. I 
///     may want to change that to include all ASCII.
/// 
/// </summary>

using System.Text;

namespace Encrypt
{
    /// <summary>
    ///     This class encrypts and decrypts Ceasar Ciphers of
    ///     a custom offset.
    ///     Valid characters to encrypt/decrypt are all 
    ///     ASCII characters from 32 to 122.
    /// </summary>
    public class CeasarCipher
    {
        /// <summary>
        ///     Encrypts using a chosen offset value from 1-90.
        /// </summary>
        /// <param name="offset"></param>
        /// <param name="data"></param>
        /// <returns></returns>
        public string Encrypt(int offset, string data)
        {
            offset = offset % 91;
            StringBuilder sb = new StringBuilder();
            int charNum;

            for (int i = 0; i < data.Length; i++)
            {
                charNum = (int)data[i];

                if (charNum < 32 || charNum > 122)
                {
                    //TODO: invalid character
                }

                charNum = charNum + offset; // ceasar slide

                if (charNum < 32) { charNum = charNum + 91; }
                if (charNum > 122) { charNum = charNum - 91; }

                sb.Append((char)charNum);
            }

            return sb.ToString();
        }

        /// <summary>
        ///     Decrypts using an offset from 1-90 (this is the
        ///     same number as what was used to encrypt).
        /// </summary>
        /// <param name="offset"></param>
        /// <param name="data"></param>
        /// <returns></returns>
        public string Decrypt(int offset, string data)
        {
            offset = offset % 91;
            StringBuilder sb = new StringBuilder();
            int charNum;

            for (int i = 0; i < data.Length; i++)
            {
                charNum = (int)data[i];

                if (charNum < 32 || charNum > 122)
                {
                    //TODO: invalid character
                }

                charNum = charNum - offset; // ceasar slide

                if (charNum < 32) { charNum = charNum + 91; }
                if (charNum > 122) { charNum = charNum - 91; }

                sb.Append((char)charNum);
            }

            return sb.ToString();
        }
    }
}
