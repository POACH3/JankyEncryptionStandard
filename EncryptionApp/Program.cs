/// <summary>
/// 
/// Author:       Trenton Stratton
/// Date started: 23-MAR-2024
/// Last updated: 31-MAR-2024
///  
/// Notes:
///     Need to create a menu to select
///     encryption type.
/// 
/// </summary>

using Encrypt;
using System.Numerics;
using System.Text;
using System.Transactions;

string userCont = "";
bool continueRun = true;

//Console.WriteLine("RSA");
//RSA rsa = new RSA();
//List<string> keys = rsa.GenerateKeysRSA();

//string d = keys[0];
//string n = keys[1];
//string e = keys[2];

//string p = keys[3];
//string q = keys[4];
//string phi = keys[5];

//Console.WriteLine("  private exponent: " + d);
//Console.WriteLine("  modulus: " + n);
//Console.WriteLine("  public exponent: " + e);

//Console.WriteLine("  p: " + p);
//Console.WriteLine("  q: " + q);
//Console.WriteLine("  totient: " + phi);

//Console.WriteLine("AES");
//AES aes = new AES(128);
//Console.WriteLine(aes.GetKey("bytes").ToString());
//Console.WriteLine(aes.GetKey("hex"));
//Console.WriteLine(aes.GetKey("base64"));






while (continueRun)
{
    // === MORSE STUFF ===

    Console.WriteLine();
    Console.WriteLine("\nEnter a speed: ");
    int speed = int.Parse(Console.ReadLine());
    Morse morse = new Morse(600, speed, false);

    Console.WriteLine();
    Console.WriteLine("\nEnter a message: ");
    string plainText = Console.ReadLine();
    Console.WriteLine();

    string cipherText = morse.Encrypt(plainText);
    Console.WriteLine("ENCRYPTED:  " + cipherText);

    string decipherText = morse.Decrypt(cipherText);
    Console.WriteLine("DECRYPTED:  " + decipherText);

    morse.PlayMessageTones(cipherText);


    // === AES STUFF ===

    //Console.WriteLine();
    //Console.WriteLine("\nEnter a message: ");
    //string plainText = Console.ReadLine();
    //Console.WriteLine();

    //string cipherText = aes.Encrypt(plainText);
    //Console.WriteLine("ENCRYPTED:  " + cipherText);

    //string decipherText = aes.Decrypt(cipherText);
    //Console.WriteLine("DECRYPTED:  " + decipherText);

    // === RSA STUFF ===

    //Console.WriteLine("\nEnter a message: ");
    //string message = Console.ReadLine();
    //byte[] messageBytes = Encoding.UTF8.GetBytes(message);

    //Console.WriteLine("\n ENCRYPTED MESSAGE:");
    //byte[] cipherTextBytes = rsa.Encrypt(e, n, message);
    //string cipherText = Encoding.UTF8.GetString(cipherTextBytes);
    //Console.WriteLine(" " + cipherText);

    //Console.WriteLine("\n DECRYPTED MESSAGE:");
    //byte[] plainTextBytes = rsa.Decrypt(d, n, cipherTextBytes);
    //string plainText = Encoding.UTF8.GetString(plainTextBytes);
    //Console.WriteLine(" " + plainText);


    //Console.WriteLine("\n PLAINTEXT CHARS:");
    //foreach (byte b in messageBytes)
    //{
    //    Console.WriteLine("  " + b + " " + Convert.ToChar(b));
    //}
    //Console.WriteLine("\n CIPHERTEXT CHARS:");
    //byte[] cipherTextBytes = rsa.Encrypt(e, n, message);
    //foreach (byte b in cipherTextBytes)
    //{
    //    Console.WriteLine("  " + b + " " + Convert.ToChar(b));
    //}
    //Console.WriteLine("\n DECRYPTED CHARS:");
    //byte[] plainTextBytes = rsa.Decrypt(d, n, cipherTextBytes);
    //foreach (byte b in plainTextBytes)
    //{
    //    Console.WriteLine("  " + b + " " + Convert.ToChar(b));
    //}

    //Console.WriteLine("\nMODULAR INVERSE:");

    //try
    //{
    //    Console.WriteLine("Enter n:");
    //    int num = int.Parse(Console.ReadLine());
    //    Console.WriteLine("Enter modulo:");
    //    int mod = int.Parse(Console.ReadLine());

    //    BigInteger result1 = rsa.ModInverse(num, mod);
    //    //int result2 = rsa.GCD(num, mod);
    //    Console.WriteLine("MOD Result: " + result1);
    //    //Console.WriteLine("GCD Result: " + result2);
    //}
    //catch (Exception e)
    //{
    //    Console.WriteLine(e.Message);
    //}







    // === CEASAR CIPHER STUFF ===

    //Console.WriteLine("CEASAR CIPHER");

    //Console.WriteLine("\nEnter an encryption value (1-94): ");
    //int offset = int.Parse(Console.ReadLine());

    //try
    //{
    //    CeasarCipher ceasar = new CeasarCipher(offset);

    //    Console.WriteLine("Enter a message to encrypt: ");
    //    string message = Console.ReadLine();

    //    string encryptedMessage = ceasar.Encrypt(message);

    //    Console.WriteLine("\nEncrypted: " + encryptedMessage);
    //    Console.WriteLine("Decrypted: " + ceasar.Decrypt(encryptedMessage));
    //}
    //catch (Exception ex)
    //{
    //    Console.WriteLine(ex.Message);
    //}


    //while (userCont != "Y" || userCont != "N" || userCont != "y" || userCont != "n")
    //{
    //    Console.WriteLine("\nEncrypt another message? (Y/N)");
    //    userCont = Console.ReadLine();
    //    if (userCont == "Y" || userCont == "y")
    //    {
    //        break;
    //    }
    //    else if (userCont == "N" || userCont == "n")
    //    {
    //        continueRun = false;
    //        break;
    //    }
    //    else
    //    {
    //        Console.WriteLine("Invalid command.");
    //    }
    //}

} // end of program while loop