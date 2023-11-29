using FI.ECM.SecurityManagement;

namespace FI.ECM.SecurityManagment.Tests
{
    public class SecurityManagementSoaRepositoryTests
    {

        public static readonly TheoryData<Func<SecurityManagementSoaRepository>> credentialConstructorSet = new()
        {
             () => new SecurityManagementSoaRepository(EnvironmentConfiguration.FIDEV, null),
             () => new SecurityManagementSoaRepository("https://test.com", null),
             () => new SecurityManagementSoaRepository(new Uri("https://test.com"), null),
             () => new SecurityManagementSoaRepository(EnvironmentConfiguration.FIDEV, EndpointConfiguration.SecurityManagementSoap, null),
             () => new SecurityManagementSoaRepository("https://test.com", EndpointConfiguration.SecurityManagementSoap, null),
             () => new SecurityManagementSoaRepository("https://test.com", new System.ServiceModel.BasicHttpBinding(), null),
             () => new SecurityManagementSoaRepository(new Uri("https://test.com"), EndpointConfiguration.SecurityManagementSoap, null),
             () => new SecurityManagementSoaRepository(new Uri("https://test.com"), new System.ServiceModel.BasicHttpBinding(), null)
        };

        public static readonly TheoryData<Func<SecurityManagementSoaRepository>> missingUriEndpointConstructorSet = new()
        {
            () => new SecurityManagementSoaRepository((Uri)null, new CredentialInfo("validUser", "password")),
            () => new SecurityManagementSoaRepository((Uri)null, EndpointConfiguration.SecurityManagementSoap, new CredentialInfo("validUser", "password")),
            () => new SecurityManagementSoaRepository((Uri)null, new System.ServiceModel.BasicHttpBinding(), new CredentialInfo("validUser", "password"))
        };

        public static readonly TheoryData<Func<SecurityManagementSoaRepository>> invalidUriEndpointConstructorSet = new()
        {
            () => new SecurityManagementSoaRepository(new Uri("https://helloworld.com/path??/file name"), new CredentialInfo("validUser", "password")),
            () => new SecurityManagementSoaRepository(new Uri("https://helloworld.com/path??/file name"), EndpointConfiguration.SecurityManagementSoap, new CredentialInfo("validUser", "password")),
            () => new SecurityManagementSoaRepository(new Uri("https://helloworld.com/path??/file name"), new System.ServiceModel.BasicHttpBinding(), new CredentialInfo("validUser", "password")),
            () => new SecurityManagementSoaRepository(new Uri("file:///C:/file.txt"), new CredentialInfo("valdUser", "password")),
            () => new SecurityManagementSoaRepository(new Uri("file:///C:/file.txt"), EndpointConfiguration.SecurityManagementSoap, new CredentialInfo("validUser", "password")),
            () => new SecurityManagementSoaRepository(new Uri("file:///C:/file.txt"), new System.ServiceModel.BasicHttpBinding(), new CredentialInfo("validUser", "password")),
            () => new SecurityManagementSoaRepository(new Uri("test.com", UriKind.Relative), new CredentialInfo("validuser", "password")),
            () => new SecurityManagementSoaRepository(new Uri("test.com", UriKind.Relative), EndpointConfiguration.SecurityManagementSoap, new CredentialInfo("validUSer", "password")),
            () => new SecurityManagementSoaRepository(new Uri("test.com", UriKind.Relative), new System.ServiceModel.BasicHttpBinding(), new CredentialInfo("validUser", "password"))
        };

        public static readonly TheoryData<Func<SecurityManagementSoaRepository>> missingStringEndpointConstructorSet = new()
        {
            () => new SecurityManagementSoaRepository((string)null, new CredentialInfo("validUser", "password")),
            () => new SecurityManagementSoaRepository("", new CredentialInfo("validUser", "pasword")),
            () => new SecurityManagementSoaRepository("  ", new CredentialInfo("validUSer", "password")),
            () => new SecurityManagementSoaRepository((string)null, EndpointConfiguration.SecurityManagementSoap, new CredentialInfo("validUser", "password")),
            () => new SecurityManagementSoaRepository("", EndpointConfiguration.SecurityManagementSoap, new CredentialInfo("validUSer", "password")),
            () => new SecurityManagementSoaRepository("   ", EndpointConfiguration.SecurityManagementSoap, new CredentialInfo("validuser", "password")),
            () => new SecurityManagementSoaRepository((string)null, new System.ServiceModel.BasicHttpBinding(), new CredentialInfo("validUSEr", "pasword")),
            () => new SecurityManagementSoaRepository("", new System.ServiceModel.BasicHttpBinding(), new CredentialInfo("validUser", "password")),
            () => new SecurityManagementSoaRepository("  ", new System.ServiceModel.BasicHttpBinding(), new CredentialInfo("validUSer", "password"))
        };

        public static readonly TheoryData<Func<SecurityManagementSoaRepository>> invalidStringEndpontConstructorSet = new()
        {
            
            () => new SecurityManagementSoaRepository("test", new CredentialInfo("validUser", "password")),
            () => new SecurityManagementSoaRepository("https://helloworld.com/path??/file name", new CredentialInfo("validUser", "password")),
            () => new SecurityManagementSoaRepository("file:///C:/file.txt", new CredentialInfo("validUser", "password")),
            () => new SecurityManagementSoaRepository("https://helloworld.com/path??/file name", EndpointConfiguration.SecurityManagementSoap, new CredentialInfo("validUser", "password")),
            () => new SecurityManagementSoaRepository("file:///C:/file.txt", EndpointConfiguration.SecurityManagementSoap, new CredentialInfo("validUser", "password")),
            () => new SecurityManagementSoaRepository("test", EndpointConfiguration.SecurityManagementSoap, new CredentialInfo("validUser", "password")),
            () => new SecurityManagementSoaRepository("https://helloworld.com/path??/file name", new System.ServiceModel.BasicHttpBinding(), new CredentialInfo("validUser", "password")),
            () => new SecurityManagementSoaRepository("file:///C:/file.txt", new System.ServiceModel.BasicHttpBinding(), new CredentialInfo("validUser", "password")),
            () => new SecurityManagementSoaRepository("test", new System.ServiceModel.BasicHttpBinding(), new CredentialInfo("validUser", "password")),
        };

        public static readonly TheoryData<Func<SecurityManagementSoaRepository>> missingBindingConstructorSet = new()
        {
            () => new SecurityManagementSoaRepository("https://test.com", null, new CredentialInfo("validUser", "password")),
            () => new SecurityManagementSoaRepository(new Uri("https://test.com"), null, new CredentialInfo("validUser", "password"))
        };

        public static readonly TheoryData<Func<SecurityManagementSoaRepository>> validInputsConstructorSet = new()
        {
            () => new SecurityManagementSoaRepository(EnvironmentConfiguration.FIDEV, new CredentialInfo("validUser", "password")),
            () => new SecurityManagementSoaRepository("https://test.com", new CredentialInfo("validUser", "password")),
            () => new SecurityManagementSoaRepository(new Uri("https://test.com"), new CredentialInfo("validUser", "password")),
            () => new SecurityManagementSoaRepository(EnvironmentConfiguration.FIDEV, EndpointConfiguration.SecurityManagementSoap, new CredentialInfo("validUser", "password")),
            () => new SecurityManagementSoaRepository("https://test.com", EndpointConfiguration.SecurityManagementSoap, new CredentialInfo("validUser", "password")),
            () => new SecurityManagementSoaRepository("https://test.com", new System.ServiceModel.BasicHttpBinding(), new CredentialInfo("validUser", "password")),
            () => new SecurityManagementSoaRepository(new Uri("https://test.com"), EndpointConfiguration.SecurityManagementSoap, new CredentialInfo("validUSer", "password")),
            () => new SecurityManagementSoaRepository(new Uri("https://test.com"), new System.ServiceModel.BasicHttpBinding(), new CredentialInfo("vaidUser", "password"))
        };

        [Theory]
        [MemberData(nameof(credentialConstructorSet))]
        public void SecurityManagementSoaRepository_MissingAccessCredentials_ThrowsArgumentNullException(Func<SecurityManagementSoaRepository> constructorUnderTest)
        {
            var soa = constructorUnderTest;

            soa.Should().Throw<ArgumentNullException>("the access credentials are missing").WithMessage("SoaSecurity access credentials are required.*", "that is the exception message")
                .WithParameterName("accessCredentials", "that is the name of the constructor parameter");
        }

        [Theory]
        [MemberData(nameof(missingUriEndpointConstructorSet))]
        public void SecurityManagementSoaRepository_MissingUriEndpointAddress_ThrowsArgumentNullException(Func<SecurityManagementSoaRepository> constructorUnderTest)
        {
            var soa = constructorUnderTest;

            soa.Should().Throw<ArgumentNullException>("the Uri is missing").WithMessage("SoaSecurity repository endpoint is required.*", "that is the exception message")
                .WithParameterName("endpointAddress", "that is the name of the constructor parameter");
        }

        [Theory]
        [MemberData(nameof(invalidUriEndpointConstructorSet))]
        public void SecurityManagementSoaRepository_InvalidUriEndpointAddress_ThrowsArgumentException(Func<SecurityManagementSoaRepository> constructorUnderTest)
        {
            var soa = constructorUnderTest;

            soa.Should().Throw<ArgumentException>("the Uri is invalid").WithMessage("The SoaSecurity repository parameter is not in the correct format.*", "that is the exception message")
                .WithParameterName("endpointAddress", "that is the name of the constructor parameter");
        }

        [Theory]
        [MemberData(nameof(missingStringEndpointConstructorSet))]
        public void SecurityManagmentSoaRepository_MissingStringEndpointAddress_ThrowsArgumentNullException(Func<SecurityManagementSoaRepository> constructorUnderTest)
        {
            var soa = constructorUnderTest;

            soa.Should().Throw<ArgumentNullException>("the endpoint string is missing").WithMessage("SoaSecurity repository endpoint is required.*", "that is the exception message")
                .WithParameterName("endpointAddress", "that is the name of the constructor parameter");
        }

        [Theory]
        [MemberData(nameof(invalidStringEndpontConstructorSet))]
        public void SecurityManagementSoaRepository_InvalidStringEndpointAddress_ThrowsArgumentException(Func<SecurityManagementSoaRepository> constructorUnderTest)
        {
            var soa = constructorUnderTest;

            soa.Should().Throw<ArgumentException>("the endpoint string is not in the correct format").WithMessage("The SoaSecurity repository parameter is not in the correct format.*", "that is the exception message")
                .WithParameterName("endpointAddress", "that is the name of the constructor parameter");
        }

        [Theory]
        [MemberData(nameof(missingBindingConstructorSet))]
        public void SecurityManagementSoaRepository_MissingBinding_ThrowsArgumentNullException(Func<SecurityManagementSoaRepository> constructorUnderTest)
        {
            var soa = constructorUnderTest;

            soa.Should().Throw<ArgumentNullException>("the binding information is missing").WithMessage("The SoaSecurity repository parameter is required.*", "that is the exception message")
                .WithParameterName("binding", "that is the name of the constructor parameter");
        }

        [Theory]
        [MemberData(nameof(validInputsConstructorSet))]
        public void SecurityManagementSoaRepository_ValidInputs_CreatesANewInstance(Func<SecurityManagementSoaRepository> constructorUnderTest)
        {
            var soa = constructorUnderTest;

            soa.Should().NotThrow("all values are valid").And.NotBeNull("we created a new instance");
        }
    }
}
