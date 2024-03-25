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
///     I am not sure I am happy about the valid characters, so I 
///     may want to change that to include all ASCII.
///     Maybe prevent user from choosing an offset that
///     doesn't actually encrypt?
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
        /// <param name="plainText"></param>
        /// <returns></returns>
        public string Encrypt(int offset, string plainText)
        {
            offset %= 91;
            
            StringBuilder sb = new StringBuilder();
            int charNum; // ASCII number of a character

            for (int i = 0; i < plainText.Length; i++)
            {
                charNum = (int)plainText[i];

                if (charNum < 32 || charNum > 122)
                    throw new ArgumentException("Message contains a character that is not allowed for encryption.");

                charNum += offset; // ceasar slide

                if (charNum < 32) { charNum += 91; }
                if (charNum > 122) { charNum -= 91; }

                sb.Append((char)charNum);
            }

            return sb.ToString();
        }

        /// <summary>
        ///     Decrypts using an offset from 1-90 (this is the
        ///     same number as what was used to encrypt).
        /// </summary>
        /// <param name="offset"></param>
        /// <param name="cipherText"></param>
        /// <returns></returns>
        public string Decrypt(int offset, string cipherText)
        {
            offset %= 91;

            StringBuilder sb = new StringBuilder();
            int charNum; // ASCII number of a character

            for (int i = 0; i < cipherText.Length; i++)
            {
                charNum = (int)cipherText[i];

                if (charNum < 32 || charNum > 122)
                    throw new ArgumentException("Encrypted message contains a character that is not allowed for decryption.");

                charNum -= offset; // ceasar slide

                if (charNum < 32) { charNum += 91; }
                if (charNum > 122) { charNum -= 91; }

                sb.Append((char)charNum);
            }

            return sb.ToString();
        }

    }
}
