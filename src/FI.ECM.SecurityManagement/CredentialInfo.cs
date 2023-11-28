using System;

namespace FI.ECM.SecurityManagement
{
    /// <summary>
    /// A class representing credentials required to connect to the security repository.
    /// </summary>
    public sealed class CredentialInfo
    {
        /// <summary>
        /// Gets the identifier used to authenticate with the security repository.
        /// </summary>
        public string User { get; }

        /// <summary>
        /// Gets the code used to authenticate with the security repository.
        /// </summary>
        public string Password { get; }

        /// <summary>
        /// Indicates whether User and Password are plain text or encrypted.
        /// </summary>
        public bool IsEncrypted { get; }

        /// <summary>
        /// Gets the function used to decrypt the User and Password values if they are encrypted as indicated by the IsEncrypted property.
        /// </summary>
        public Func<string, string> Decryptor { get; }

        /// <summary>
        /// Initializes a new instance of the <see cref="CredentialInfo"/> class with the passed <paramref name="user"/> and <paramref name="password"/> values.
        /// These values are assumed to be unencrypted and in plain text.
        /// </summary>
        /// <param name="user">The identifier used to authenticate with the security repository.</param>
        /// <param name="password">The unique code used to authenticate with the security repository.</param>
        /// <exception cref="ArgumentNullException">Thrown if <paramref name="user"/> or <paramref name="password"/> are null or invalid.</exception>
        public CredentialInfo(string user, string password)
        {
            if (string.IsNullOrWhiteSpace(user)) throw new ArgumentNullException(nameof(user), "The parameter is required to connect to the repository.");
            if (string.IsNullOrWhiteSpace(password)) throw new ArgumentNullException(nameof(password), "The parameter is required to connect to the repository.");
            
            User = user;
            Password = password;
            IsEncrypted = false;
            Decryptor = input => input;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="CredentialInfo"/> class with the <paramref name="user"/>, <paramref name="password"/>, and <paramref name="decryptor"/>
        /// used to convert <paramref name="user"/> and <paramref name="password"/> to plain text values and authenticate with the security repository.
        /// </summary>
        /// <param name="user">The identifier used to authenticate with the security repository. This is not assumed to be in plain text.</param>
        /// <param name="password">The unique code used to authenticate with the security repository. This is not assumed to be in plain text.</param>
        /// <param name="decryptor">The <see cref="Delegate"/> used to convert <paramref name="user"/> and <paramref name="password"/> to their plain text values.</param>
        /// <exception cref="ArgumentNullException">Thrown if <paramref name="user"/>, <paramref name="password"/>, and <paramref name="decryptor"/> are null
        /// or invalid.</exception>
        public CredentialInfo(string user, string password, Func<string, string> decryptor)
        {
            if (string.IsNullOrWhiteSpace(user)) throw new ArgumentNullException(nameof(user), "The parameter is required to connect to the repository.");
            if (string.IsNullOrWhiteSpace(password)) throw new ArgumentNullException(nameof(password), "The parameter is required to connect to the repository.");

            User = user;
            Password = password;
            IsEncrypted = true;
            Decryptor = decryptor ?? throw new ArgumentNullException(nameof(decryptor), "The parameter is required to convert the security credentials to plain text values.");
        }
    }
}
