/// <summary>
/// 
/// Author:       Trenton Stratton
/// Date started: 23-MAR-2024
/// Last updated: 24-MAR-2024
///
/// File Contents: 
///     RSA key generation
///     encryption
///     decryption
///     square root approximation
///     modular multiplicative inverse
///     check if a number is prime
///     list all primes less than a number n
///     
/// Notes:
///     Need to find a different way to generate random
///     numbers that are closer to 30 digits long (for
///     512-bit RSA keys).
///     Currently encryption/decryption return byte arrays,
///     this may change to strings or allow the user to
///     select whichever.
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

            Random random = new Random();
            BigInteger randomNum = random.Next(1_000, 9_999);
            //BigInteger randomNum = random.Next(100_000_000, 999_999_999);
            BigInteger p = 0; // random prime 1
            BigInteger q = 0; // random prime 2
            BigInteger phi;    // euler totient
            BigInteger n; // modulus
            int e; // public exponent
            BigInteger d; // private key


            // generate 2 large, similar bit length prime numbers that are far apart
            while (p == 0)
            {
                if (IsPrime(randomNum))
                {
                    p = randomNum;
                }

                randomNum = random.Next(1_000, 9_999);
            }

            while (q == 0)
            {
                if (IsPrime(randomNum)) { q = randomNum; }
                else { randomNum = random.Next(1_000, 9_999); }
            }


            // euler totient
            phi = (p - 1) * (q - 1);

            // modulus
            n = p * q;

            // public exponent (65537)
            //e = 65537;
            e = 97;

            // private exponent
            //d = (phi + 1) / e;
            d = ModularInverse(e, phi);

            keys.Add(d.ToString());
            keys.Add(n.ToString());
            keys.Add(e.ToString());

            keys.Add(p.ToString());
            keys.Add(q.ToString());
            keys.Add(phi.ToString());

            return keys;
        } // end of GenerateKeysRSA()


        public BigInteger ModularInverse(BigInteger e, BigInteger phi)
        {

        }


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

        public byte[] EncryptRSA(string publicExponent, string modulus, string plainText)
        {
            // string
            // byte array
            // encrypt
            // return byte array

            byte[] plainTextBytes = Encoding.UTF8.GetBytes(plainText);
            BigInteger m = new BigInteger(plainTextBytes);

            int e = int.Parse(publicExponent);
            BigInteger n = BigInteger.Parse(modulus);

            byte[] cipherBytes = BigInteger.ModPow(m, e, n).ToByteArray();

            return cipherBytes;
        }

        public byte[] DecryptRSA(string privateExponent, string modulus, byte[] cipherText)
        {
            // byte array
            // decrypt
            // return string

            BigInteger c = new BigInteger(cipherText);

            BigInteger d = BigInteger.Parse(privateExponent);
            BigInteger n = BigInteger.Parse(modulus);

            //plainText = (BigInteger.ModPow(c, d, null) % n).ToString();

            BigInteger mathDecrypt = BigInteger.ModPow(c, d, n);

            byte[] decryptedBytes = mathDecrypt.ToByteArray();

            StringBuilder sb = new StringBuilder();

            return decryptedBytes;
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
                digitSum += BigInteger.Parse(c.ToString());

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

        /// <summary>
        ///     Testing with ints.
        /// </summary>
        /// <param name="n"></param>
        /// <returns></returns>
        //public int Sqrt(int n)
        //{
        //    int nextApprox = 0;
        //    int approx = n;

        //    while (true)
        //    {
        //        nextApprox = (approx + (n / approx)) / 2;

        //        if (nextApprox - approx < 1 && approx - nextApprox < 1)
        //            break;

        //        approx = nextApprox;
        //    }

        //    return nextApprox;
        //} // end of Sqrt()

        /// <summary>
        ///     Gets list of primes up to the number n.
        /// </summary>
        /// <param name="n"></param>
        /// <returns></returns>
        public ArrayList PrimesLessThan(BigInteger n)
        {
            ArrayList primes = new();
            primes.Add(new BigInteger(2));
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

                if (!divides) { primes.Add(new BigInteger(i)); }

                divides = false;
            }
            return primes;
        } // end of PrimesLessThan()
    }
}
