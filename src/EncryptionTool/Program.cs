// See https://aka.ms/new-console-template for more information
using FI.ECM.Security.Decryption;
using FI.ECM.Security.Encryption;
using FI.ECM.Security.Encryption.Legacy;
using FI.ECM.Security.Decryption.Legacy;

IDecryptor decryptor = new LegacyDecryption();
IEncryptor encryptor = new LegacyEncryption();

bool go = true;

while (go)
{
    Console.WriteLine("What needs doing today? [Encryption|E|e] or [Decryption|D|d]?");

    string? input = Console.ReadLine();

    while (!string.Equals(input, "encryption", StringComparison.OrdinalIgnoreCase) && !string.Equals(input, "e", StringComparison.OrdinalIgnoreCase) &&
        !string.Equals(input, "decryption", StringComparison.OrdinalIgnoreCase) && !string.Equals(input, "d", StringComparison.OrdinalIgnoreCase))
    {
        Console.WriteLine("Not a valid input. Please try again buster.");
        input = Console.ReadLine();
    }

    if (string.Equals(input, "decryption", StringComparison.OrdinalIgnoreCase) || string.Equals(input, "d", StringComparison.OrdinalIgnoreCase))
    {
        Console.WriteLine("Beginning decryption.");
        try
        {
            string processThis = WhereToReadAndGetContents();
            string decrypted = decryptor.Decrypt(processThis);
            WhereToDisplay(decrypted);

            Console.WriteLine("All done.");
            go = StartOver();
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Failed to decrypt value. Error - {ex}");
            go = StartOver();
        }
    }
    else
    {
        Console.WriteLine("Beginning encryption.");
        try
        {
            string processThis = WhereToReadAndGetContents();
            string encrypted = encryptor.Encrypt(processThis);
            WhereToDisplay(encrypted);

            Console.WriteLine("All done.");
            go = StartOver();
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Failed to encrypt value. Error - {ex}");
            go = StartOver();
        }
    }
}


void WhereToDisplay(string input)
{
    Console.WriteLine("Done. Where would you like the value displayed? [Console|C|c] or [File|F|f]?");

    string? displayLocation = Console.ReadLine();

    while (!string.Equals(displayLocation, "console", StringComparison.OrdinalIgnoreCase) && !string.Equals(displayLocation, "c", StringComparison.OrdinalIgnoreCase) &&
        !string.Equals(displayLocation, "file", StringComparison.OrdinalIgnoreCase) && !string.Equals(displayLocation, "f", StringComparison.OrdinalIgnoreCase))
    {
        Console.WriteLine("Invalid option. Please try again.");
        displayLocation = Console.ReadLine();
    }

    if(string.Equals(displayLocation, "console", StringComparison.OrdinalIgnoreCase) || string.Equals(displayLocation, "c", StringComparison.OrdinalIgnoreCase))
    {
        Console.WriteLine($"Your value: {input}");
    }
    else
    {
        string path = GetFilePath();
        DisplayToFile(input, path);
    }
}

string WhereToReadAndGetContents()
{
    Console.WriteLine("Where should I find the data? [File|F|f] or [Console|C|c]?");
    string? readLocation = Console.ReadLine();

    while(!string.Equals(readLocation, "file", StringComparison.OrdinalIgnoreCase) && !string.Equals(readLocation, "f", StringComparison.OrdinalIgnoreCase) &&
        !string.Equals(readLocation, "console", StringComparison.OrdinalIgnoreCase) && !string.Equals(readLocation, "c", StringComparison.OrdinalIgnoreCase))
    {
        Console.WriteLine("Invalid input. Please try again.");
        readLocation = Console.ReadLine();
    }

    string? inputToProcess;

    if(string.Equals(readLocation, "file", StringComparison.OrdinalIgnoreCase) || string.Equals(readLocation, "f", StringComparison.OrdinalIgnoreCase))
    {
        string path = GetFilePath();
        return ReadFromFile(path);
    }
    else
    {
        Console.WriteLine("Please enter the value to be consumed.");
        inputToProcess = Console.ReadLine();
        while(string.IsNullOrEmpty(inputToProcess) && string.IsNullOrWhiteSpace(inputToProcess))
        {
            Console.WriteLine("Invalid input.");
            inputToProcess = Console.ReadLine();
        }
        return inputToProcess;
    }
}

string GetFilePath()
{
    Console.WriteLine("Please input a valid file path.");
    string? filePath = Console.ReadLine();

    Uri? localFile;
    while (!Uri.TryCreate(filePath, UriKind.Absolute, out localFile) && !localFile.IsFile)
    {
        Console.WriteLine("Invalid file path. Please try again?");
        filePath = Console.ReadLine();
    }
    return localFile.AbsolutePath;
}

string ReadFromFile(string filePath)
{
    try
    {
        return File.ReadAllText(filePath);
    }
    catch(Exception ex)
    {
        Console.WriteLine($"Failed to read from file. Error - {ex}");
        throw;
    }
}

void DisplayToFile(string input, string filePath)
{
    try
    {
        File.WriteAllText(filePath, input);
    }
    catch(Exception ex)
    {
        Console.WriteLine($"Failed to write to file. Error - {ex}");
        throw;
    }
}
 
bool StartOver()
{
    Console.WriteLine("Would you like to start over? [Yes|Y|y] or [No|N|n]?");

    string? input = Console.ReadLine();
    while(!string.Equals(input, "yes", StringComparison.OrdinalIgnoreCase) && !string.Equals(input, "y", StringComparison.OrdinalIgnoreCase) &&
        !string.Equals(input, "no", StringComparison.OrdinalIgnoreCase) && !string.Equals(input, "n", StringComparison.OrdinalIgnoreCase))
    {
        Console.WriteLine("Invalid input. Please try again.");
        input = Console.ReadLine();
    }

    return string.Equals(input, "yes", StringComparison.OrdinalIgnoreCase) || string.Equals(input, "y", StringComparison.OrdinalIgnoreCase);
}