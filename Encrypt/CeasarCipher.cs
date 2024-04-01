/// <summary>
/// 
/// Author:       Trenton Stratton
/// Date started: 23-MAR-2024
/// Last updated: 31-MAR-2024
///
/// File Contents:
///     encrypt
///     decrypt
///     change shift amount
///     
/// Notes:
/// 
/// </summary>

using System.Text;

namespace Encrypt
{
    /// <summary>
    ///     This class encrypts and decrypts Ceasar Ciphers of
    ///     a custom offset.
    ///     Allowed characters to encrypt/decrypt are all the
    ///     ASCII characters from 32 to 126 (inclusive).
    /// </summary>
    public class CeasarCipher
    {
        private static int lower = 32;                // lower end of range of allowed ASCII values
        private static int upper = 126;               // upper end of range of allowed ASCII values
        private static int range = upper - lower + 1; // number of allowed ASCII values
        private int offset;                           // amount of encrypt/decrypt shift


        /// <summary>
        ///     Allows for setting a custom offset value which
        ///     is an integer from 1 to 94 (inclusive).
        /// </summary>
        /// <param name="offset">Amount that the message letters shift when encrypted.</param>
        public CeasarCipher(int offset)
        {
            if (offset % range == 0)
                throw new ArgumentException("A multiple of " + range + " won't encrypt the message.");
            else
                this.offset = offset % range;  
        }
        
        /// <summary>
        ///     Encrypts using the chosen offset value.
        /// </summary>
        /// <param name="plainText">plain text message</param>
        /// <returns>A string that is the message encrypted.</returns>
        public string Encrypt(string plainText)
        {
            StringBuilder sb = new StringBuilder();
            int charNum; // ASCII number of a character

            for (int i = 0; i < plainText.Length; i++)
            {
                charNum = (int)plainText[i];

                if (charNum < lower || charNum > upper)
                    throw new ArgumentException("Message contains a character that is not allowed for encryption.");

                charNum += offset; // ceasar slide

                if (charNum < lower) { charNum += range; }
                if (charNum > upper) { charNum -= range; }

                sb.Append((char)charNum);
            }

            return sb.ToString();
        }

        /// <summary>
        ///     Decrypts using the chosen offset value.
        /// </summary>
        /// <param name="cipherText">cipher text message</param>
        /// <returns>A string that is the message decrypted.</returns>
        public string Decrypt(string cipherText)
        {
            StringBuilder sb = new StringBuilder();
            int charNum; // ASCII number of a character

            for (int i = 0; i < cipherText.Length; i++)
            {
                charNum = (int)cipherText[i];

                if (charNum < lower || charNum > upper)
                    throw new ArgumentException("Encrypted message contains a character that is not allowed for decryption.");

                charNum -= offset; // ceasar slide

                if (charNum < lower) { charNum += range; }
                if (charNum > upper) { charNum -= range; }

                sb.Append((char)charNum);
            }

            return sb.ToString();
        }


        /// <summary>
        ///     Change the amount of encrypt/decrypt shift.
        ///     An integer value from 1 to 94 (inclusive).
        /// </summary>
        /// <param name="offset">Amount that the message letters shift when encrypted.</param>
        public void ChangeOffset(int offset)
        {
            this.offset = offset;
        }

    }
}
