/// <summary>
/// 
/// Author:       Trenton Stratton
/// Date started: 23-MAR-2024
/// Last updated: 10-APR-2024
///
/// File Contents:
///     encrypt
///     decrypt
///     change shift amount
///
/// </summary>

using System.Text;

namespace Encrypt
{
    /// <summary>
    ///     This class encrypts and decrypts Ceasar Ciphers
    ///     using the selected shift amount (an integer from
    ///     1 to 94 inclusive).
    ///     Allowed characters to encrypt/decrypt are all the
    ///     ASCII characters from 32 to 126 (inclusive).
    /// </summary>
    public class CeasarCipher
    {
        private static int _lower = 32;                  // lower end of range of allowed ASCII values
        private static int _upper = 126;                 // upper end of range of allowed ASCII values
        private static int _range = _upper - _lower + 1; // number of allowed ASCII values
        private int _shift;                              // amount of encrypt/decrypt offset


        /// <summary>
        ///     Allows for setting a character offset value which
        ///     is an integer from 1 to 94 (inclusive).
        /// </summary>
        /// <param name="shift">Amount that the message letters shift when encrypted.</param>
        public CeasarCipher(int shift)
        {
            if (shift % _range == 0)
                throw new ArgumentException("A multiple of " + _range + " won't encrypt the message.");
            else
                this._shift = shift % _range;  
        }

        
        /// <summary>
        ///     Encrypts using the chosen shift value.
        /// </summary>
        /// <param name="plainText">plain text message</param>
        /// <returns>A string that is the message encrypted.</returns>
        public string Encrypt(string plainText)
        {
            StringBuilder cipherText = new StringBuilder(); // encrypted plain text
            int charNum;                                    // ASCII number of a character

            for (int i = 0; i < plainText.Length; i++)
            {
                charNum = (int)plainText[i];

                if (charNum < _lower || charNum > _upper)
                    throw new ArgumentException("Message contains a character that is not allowed for encryption.");

                charNum += this._shift;

                if (charNum < _lower) { charNum += _range; }
                if (charNum > _upper) { charNum -= _range; }

                cipherText.Append((char)charNum);
            }

            return cipherText.ToString();
        }


        /// <summary>
        ///     Decrypts using the chosen shift value.
        /// </summary>
        /// <param name="cipherText">cipher text message</param>
        /// <returns>A string that is the message decrypted.</returns>
        public string Decrypt(string cipherText)
        {
            StringBuilder plainText = new StringBuilder(); // decrypted cipher text
            int charNum;                                   // ASCII number of a character

            for (int i = 0; i < cipherText.Length; i++)
            {
                charNum = (int)cipherText[i];

                if (charNum < _lower || charNum > _upper)
                    throw new ArgumentException("Encrypted message contains a character that is not allowed for decryption.");

                charNum -= this._shift;

                if (charNum < _lower) { charNum += _range; }
                if (charNum > _upper) { charNum -= _range; }

                plainText.Append((char)charNum);
            }

            return plainText.ToString();
        }


        /// <summary>
        ///     Changes the amount of encrypt/decrypt shift.
        ///     An integer value from 1 to 94 (inclusive).
        /// </summary>
        /// <param name="shift">the amount of message character offset when encrypted.</param>
        public void ChangeShift(int shift)
        {
            this._shift = shift;
        }

    }
}
