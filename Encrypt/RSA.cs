﻿/// <summary>
/// 
/// Author:       Trenton Stratton
/// Date started: 23-MAR-2024
/// Last updated: 24-MAR-2024
///
/// File Contents: 
///     RSA key generation
///     encryption
///     decryption
///     check if a number is prime
///     list all primes less than a number n
///     square root approximation
///     modular multiplicative inverse
///     
/// Notes:
///     Need create a way to generate random numbers that 
///     are closer to 30 digits long (for 512-bit RSA keys).
///     Method that takes in byte length of desired
///     random number?
///     Currently encryption/decryption return byte arrays,
///     this may change to strings or allow the user to
///     select whichever.
///     Need to figure out a more efficient way to check
///     if a number is prime, as more than 10 digit primes
///     begin to lag significantly.
///     Need to add a check to ensure p and q aren't too
///     close together.
///     Need to check to ensure data doesn't exceed the 
///     modulus size.
///     Check if byte[] padding is a problem.
///     Sometimes ModularInverse throws the exception that
///     e isn't invertible.
/// 
/// </summary>

using System.Collections;
using System.Numerics;
using System.Text;

namespace Encrypt
{
    //internal class RSA
    public class RSA
    {
        public RSA() { }
        
        /// <summary>
        ///     Generates 64-bit RSA keys. (WOW! Much secure!)
        ///     Each prime is 32/3.32 (10) digits.
        ///     
        ///     Need a different random number generator.
        /// </summary>
        /// <returns></returns>
        public List<string> GenerateKeysRSA()
        {
            //string[] keys = new string[3]; // public, private
            List<string> keys = new List<string>(3); // public, private

            //(int min, int max) = (100, 999);
            //(int min, int max) = (1_000, 9_999);
            //(int min, int max) = (100_000, 999_999);
            //(int min, int max) = (100_000_000, 999_999_999);
            (int min, int max) = (500_000_000, 2_147_483_647);

            
            Random random = new Random();
            BigInteger randomNum = random.Next(min, max);
            //BigInteger randomNum = random.Next(100_000_000, 999_999_999);
            
            BigInteger p = 0;   // random prime 1
            BigInteger q = 0;   // random prime 2
            BigInteger phi;     // euler totient
            BigInteger n;       // modulus
            int e;              // public exponent
            BigInteger d;       // private key


            // generate two large, similar bit length prime numbers that are far apart
            while (p == 0)
            {
                if (IsPrime(randomNum))
                {
                    p = randomNum;
                }

                randomNum = random.Next(min, max);
            }

            while (q == 0)
            {
                if (IsPrime(randomNum)) { q = randomNum; }
                else { randomNum = random.Next(min, max); }
            }


            // euler totient
            phi = (p - 1) * (q - 1);

            // modulus
            n = p * q;

            // public exponent (65537)
            //e = 65537;
            //e = 97;
            e = 11;

            // private exponent
            //d = (phi + 1) / e;
            d = ModInverse(e, phi);

            keys.Add(d.ToString());
            keys.Add(n.ToString());
            keys.Add(e.ToString());

            // for testing
            keys.Add(p.ToString());
            keys.Add(q.ToString());
            keys.Add(phi.ToString());

            return keys;
        } // end of GenerateKeysRSA()


        //public string EncryptRSA(string publicExponent, string modulus, string plainText)
        //{
        //    string cipherText = "";

        //    // message to ASCII
        //    //BigInteger m = int.Parse(plainText);

        //    //foreach (char c in plainText)
        //    //{

        //    //}

        //    byte[] bytes = Encoding.UTF8.GetBytes(plainText);
        //    BigInteger m = new BigInteger(bytes);

        //    int e = int.Parse(publicExponent);
        //    BigInteger n = BigInteger.Parse(modulus);

        //    // message to the e mod n

        //    cipherText = (BigInteger.Pow(m, e) % n).ToString();

        //    return cipherText;
        //}

        public byte[] EncryptRSA(string publicExponent, string modulus, string plainTextMessage)
        {
            // string
            // byte array
            // encrypt
            // return byte array

            byte[] plainText = Encoding.UTF8.GetBytes(plainTextMessage);
            BigInteger m = new BigInteger(plainText);                           // byte array is interpreted as unsigned big-endian
            int e = int.Parse(publicExponent);
            BigInteger n = BigInteger.Parse(modulus);

            byte[] cipherBytes = BigInteger.ModPow(m, e, n).ToByteArray();      // byte array is in little-endian order

            return cipherBytes;
        }

        /// <summary>
        ///     Returns decrypted byte array
        /// </summary>
        /// <param name="privateExponent"></param>
        /// <param name="modulus"></param>
        /// <param name="cipherText"></param>
        /// <returns></returns>
        public byte[] DecryptRSA(string privateExponent, string modulus, byte[] cipherText)
        {
            // byte array
            // decrypt
            // return byte array or string

            BigInteger c = new BigInteger(cipherText);
            BigInteger d = BigInteger.Parse(privateExponent);
            BigInteger n = BigInteger.Parse(modulus);

            BigInteger decryptFunction = BigInteger.ModPow(c, d, n);

            return decryptFunction.ToByteArray();
        }


        /// <summary>
        ///     Checks to see if a given number is a prime.
        ///     About 10 digits seems to be the max before it
        ///     gets super laggy (2 minutes for 12 digits).
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        public bool IsPrime(BigInteger x)
        {
            // case: ends with 0,2,4,6,8
            if (x % 2 == 0 && x > 2) { return false; }

            // case: i am lazy
            if (x == 2 || x == 3) { return true; }

            // case: sum of digits divisible by 3
            string num = x.ToString();
            BigInteger digitSum = 0;

            foreach (char c in num)
                digitSum += BigInteger.Parse(c.ToString()); // TODO: need to be BigInteger?

            if (digitSum % 3 == 0) { return false; }

            // case: square is divisible by a prime
            //int root = Sqrt(x);
            BigInteger root = SquareRoot(x);
            foreach (BigInteger prime in PrimesLessThan(root))
            {
                if (x % prime == 0)
                    return false;
            }

            return true;
        } // end of IsPrime()


        /// <summary>
        ///     Gets list of primes up to the number n.
        /// </summary>
        /// <param name="n"></param>
        /// <returns></returns>
        public ArrayList PrimesLessThan(BigInteger n)
        {
            ArrayList primes = new();
            primes.Add(new BigInteger(2)); //TODO
            //primes.Add(2);

            bool divides = false;

            for (int i = 3; i < n; i++)
            {
                foreach (BigInteger prime in primes)
                {
                    if (i % prime == 0)
                    {
                        divides = true;
                        break;
                    }
                }

                if (!divides) { primes.Add(new BigInteger(i)); } //TODO: faster to not make all BigInteger?

                divides = false;
            }
            return primes;
        } // end of PrimesLessThan()


        /// <summary>
        ///     Approximates roots using the Newton-Raphson Method.
        /// </summary>
        /// <param name="n"></param>
        /// <returns></returns>
        public BigInteger SquareRoot(BigInteger n)
        {
            BigInteger nextApprox = 0;
            BigInteger approx = n;

            while (true)
            {
                nextApprox = (approx + (n / approx)) / 2;

                if (nextApprox - approx < 1 && approx - nextApprox < 1)
                    break;

                approx = nextApprox;
            }

            return nextApprox;
        } // end of SquareRoot()



        /// <exception cref="Exception"></exception>
        public BigInteger ModInverse(BigInteger e, BigInteger phi)
        {
            // e and phi being coprime is a condition for mod inverse to exist (gdc = 1)

            BigInteger a = 0;
            BigInteger newA = 1;
            BigInteger b = phi;
            BigInteger newB = e;

            while (newB != 0)
            {
                BigInteger quotient = b / newB;

                a = newA;
                newA = a - quotient * newA;
                b = newB;
                newB = b - quotient * newB;
            }

            if (b > 1)
                throw new Exception("The selected value for e is not invertible.");
            
            if (a < 0)
                a += phi;

            return a;
        }

    }
}
