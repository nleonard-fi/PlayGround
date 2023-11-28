using FI.ECM.SecurityManagement;

namespace FI.ECM.SecurityManagment.Tests
{
    public class CredentialInfoTests
    {
        private readonly string _user;
        private readonly string _password;

        public CredentialInfoTests()
        {
            _user = "validUser";
            _password = "password";
        }

        [Theory]
        [InlineData("")]
        [InlineData("   ")]
        [InlineData(null)]
        public void CredentialInfo_InvalidUser_ThrowsArgumentNullException(string user)
        {
            var sut = () => new CredentialInfo(user, _password);

            sut.Should().Throw<ArgumentNullException>("we passed an invalid value")
                .WithMessage("The parameter is required to connect to the repository.*", "that is the exception message")
                .WithParameterName(nameof(user), "that is the name of the constructor parameter");
        }

        [Theory]
        [InlineData("")]
        [InlineData("  ")]
        [InlineData(null)]
        public void CredentialInfo_InvalidPassword_ThrowsArgumentNullException(string password)
        {
            var sut = () => new CredentialInfo(_user, password);

            sut.Should().Throw<ArgumentNullException>("we passed an invalid value")
                .WithMessage("the parameter is required to connect to the repository.*", "that is the exception message")
                .WithParameterName(nameof(password), "that is the name of the constructor parameter");
        }

        [Fact]
        public void CredentialInfo_InvalidDecryptor_ThrowsArgumentNullException()
        {
            var sut = () => new CredentialInfo(_user, _password, null);

            sut.Should().Throw<ArgumentNullException>("we passed an invalid value")
                .WithMessage("The parameter is required to convert the security credentials to plain text values.*")
                .WithParameterName("decryptor", "that is the name of the constructor parameter");
        }

        [Fact]
        public void CredentialInfo_ValidPlainTextProperties_HasIsEncryptedSetToFalse()
        {
            var sut = new CredentialInfo(_user, _password);

            sut.IsEncrypted.Should().BeFalse("we passed plain text values");
        }

        [Fact]
        public void CredentialInfo_ValidEncryptedProperties_HasIsEncryptedSetToTrue()
        {
            var sut = new CredentialInfo(_user, _password, input => input);

            sut.IsEncrypted.Should().BeTrue("we passed encrypted values");
        }
    }
}