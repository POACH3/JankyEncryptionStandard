/// <summary>
/// 
/// Author:     Trenton Stratton
/// Date:       23-MAR-2024
///  
/// Notes:
///     
/// 
/// </summary>

using Encrypt;

string userCont = "";
bool run = true;

Console.WriteLine("CEASAR CIPHER");
CeasarCipher ceasar = new CeasarCipher();

while (run)
{
    Console.WriteLine("\nEnter an encryption value (1-90): ");
    int offset = int.Parse(Console.ReadLine());

    Console.WriteLine("Enter a message to encrypt: ");
    string message = Console.ReadLine();

    string encryptedMessage = ceasar.Encrypt(offset, message);

    Console.WriteLine("\nEncrypted: " + encryptedMessage);
    Console.WriteLine("Decrypted: " + ceasar.Decrypt(offset, encryptedMessage));


    while (userCont != "Y" || userCont != "N" || userCont != "y" || userCont != "n")
    {
        Console.WriteLine("\nEncrypt another message? (Y/N)");
        userCont = Console.ReadLine();
        if (userCont == "Y" || userCont == "y")
        {
            break;
        }
        else if (userCont == "N" || userCont == "n")
        {
            run = false;
            break;
        }
        else
        {
            Console.WriteLine("Invalid command.");
        }
    }

} // end of program while loop