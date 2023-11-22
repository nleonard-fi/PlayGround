using System;
using System.Security;
using FI.ECM.Security.Decryption;
using FI.ECM.Security.Decryption.Legacy;

namespace FI.ECM.SecurityManagement
{
    /// <summary>
    /// Credentials required to connect to the security repository.
    /// </summary>
    public readonly struct CredentialInfo
    {
        public string User { get; }

        public string Password { get; }

        public bool IsEncrypted { get; }

        public IDecryptor Decryptor { get; }

        /// <summary>
        /// Initializes a new instance of the <see cref="CredentialInfo"/> structure to the passed <paramref name="user"/> and <paramref name="password"/> values.
        /// These values are assumed to be unencrypted and in plain text.
        /// </summary>
        /// <param name="user">The </param>
        /// <param name="password"></param>
        /// <exception cref="ArgumentNullException"></exception>
        public CredentialInfo(string user, string password)
        {
            if (string.IsNullOrWhiteSpace(user)) throw new ArgumentNullException(nameof(user), "The parameter is required to connect to the repository.");
            if (string.IsNullOrWhiteSpace(password)) throw new ArgumentNullException(nameof(password), "The parameter is required to connect to the repository.");
            // FINSIHE THE COMMENTS FOR THE CONSTRUCTORS AND THE LOGIC.
            User = user;
            Password = password;
            IsEncrypted = false;
            Decryptor = new LegacyDecryption();
        }

        public CredentialInfo(string user, string password, bool isEncrypted, IDecryptor decryptor)
        {
            if (string.IsNullOrEmpty(user)) throw new ArgumentNullException(nameof(user), "The user is required to connect to the repository.");
            if(string.IsNullOrWhiteSpace(password)) throw new ArgumentNullException(nameof(password), "The password is required to connect to the repository.");
            if (isEncrypted == true && decryptor == null) throw new ArgumentNullException(nameof(decryptor), "Decryptor is required if the credential values are encrypted.");

            User = user;
            Password = password;
            IsEncrypted = isEncrypted;
            Decryptor = decryptor;
        }
    }
}
