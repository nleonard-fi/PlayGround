using System;
using System.Diagnostics;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using System.ServiceModel.Channels;
using Microsoft.Extensions.Logging.Abstractions;
using FI.ECM.SecurityManagement.SecurityManagementSoa;

namespace FI.ECM.SecurityManagement
{
    public class SecurityManagementSoaRepository : ISecurityRepository, IAysncSecurityRepository
    {
        private readonly ILogger<SecurityManagementSoaRepository> _logger;
        internal SecurityManagementSoapClient _soa;

        /// <summary>
        /// Initializes a new instance of the <see cref="SecurityManagementSoaRepository"/> class with an <see cref="ILogger{TCategoryName}"/> to be used in the repository and an
        /// <see cref="EnvironmentConfiguration"/> to be used for determing endpoint binding information. The endpoint defaults to <see cref="EndpointConfiguration.SecurityManagementSoap12"/> SOAP version.
        /// </summary>
        /// <param name="logger">The <see cref="ILogger{TCategoryName}"/> to be used in the repository. Defaults to <see cref="NullLogger{T}"/> when a null value is passed.</param>
        /// <param name="environmentConfiguration">The <see cref="EnvironmentConfiguration"/> to be used for determining endpoint binding information.</param>
        public SecurityManagementSoaRepository(ILogger<SecurityManagementSoaRepository> logger, EnvironmentConfiguration environmentConfiguration)
        {
            _logger = logger ?? NullLogger<SecurityManagementSoaRepository>.Instance;
            _soa = new SecurityManagementSoapClient(EndpointConfiguration.SecurityManagementSoap12, environmentConfiguration);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="SecurityManagementSoaRepository"/> class with an <see cref="ILogger{TCategoryName}"/> to be used in the repository, an <see cref="EnvironmentConfiguration"/>
        /// to be used for determing binding information, and an <see cref="EndpointConfiguration"/> specifying the SOAP version to be used for the endpoint in the repository.
        /// </summary>
        /// <param name="logger">The <see cref="ILogger{TCategoryName}"/> to be used in the repository. Defaults to <see cref="NullLogger{T}"/> when a null value is passed.</param>
        /// <param name="environmentConfiguration">The <see cref="EnvironmentConfiguration"/> to be used for determining endpoint binding information.</param>
        /// <param name="endpointConfiguration">The <see cref="EndpointConfiguration"/> specifying the SOAP version to be used for the endpoint in the repository.</param>
        public SecurityManagementSoaRepository(ILogger<SecurityManagementSoaRepository> logger, EnvironmentConfiguration environmentConfiguration, EndpointConfiguration endpointConfiguration)
        {
            _logger = logger ?? NullLogger<SecurityManagementSoaRepository>.Instance;
            _soa = new SecurityManagementSoapClient(endpointConfiguration, environmentConfiguration);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="SecurityManagementSoaRepository"/> class with an <see cref="ILogger{TCategoryName}"/> to be used in the repository and a <see cref="Uri"/>
        /// specifying the address for the endpoint to be used in the repository. The endpoint defaults to <see cref="EndpointConfiguration.SecurityManagementSoap12"/> SOAP version.
        /// </summary>
        /// <param name="logger">The <see cref="ILogger{TCategoryName}"/> to be used in the repository. Defaults to <see cref="NullLogger{T}"/> when a null value is passed.</param>
        /// <param name="endpointAddress">The <see cref="Uri"/> specifying the endpoint address to be used in the repository. <see cref="Uri.IsAbsoluteUri"/> must be true and 
        /// <see cref="Uri.IsFile"/> must be false.</param>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="endpointAddress"/> is missing.</exception>
        /// <exception cref="ArgumentException">Thrown when <paramref name="endpointAddress"/> is not in a correct <see cref="Uri"/> non-file format.</exception>
        public SecurityManagementSoaRepository(ILogger<SecurityManagementSoaRepository> logger, Uri endpointAddress)
        {
            _logger = logger ?? NullLogger<SecurityManagementSoaRepository>.Instance;
            if (endpointAddress == null)
                throw new ArgumentNullException(nameof(endpointAddress), "SoaSecurity repository endpoint is required.");
            else if(!endpointAddress.IsWellFormedOriginalString() || endpointAddress.IsFile || !endpointAddress.IsAbsoluteUri) 
                throw new ArgumentException("The SoaSecurity repository parameter is not in the correct format.", nameof(endpointAddress));
            else
                _soa = new SecurityManagementSoapClient(EndpointConfiguration.SecurityManagementSoap12, endpointAddress.AbsoluteUri);
        }

        /// <summary>
        /// Initialzes a new instance of the <see cref="SecurityManagementSoaRepository"/> class with an <see cref="ILogger{TCategoryName}"/> to be used in the repository, an <see cref="Uri"/>
        /// specifying the address for the endpoint to be used in the repository, and an <see cref="EndpointConfiguration"/> specifying the SOAP version to be used for the endpoint.
        /// </summary>
        /// <param name="logger">The <see cref="ILogger{TCategoryName}"/> to be used in the repository. Defaults to <see cref="NullLogger{T}"/> when a null value is passed.</param>
        /// <param name="endpointAddress">The <see cref="Uri"/> specifying the endpoint address to be used in the repository. <see cref="Uri.IsAbsoluteUri"/> must be true and 
        /// <see cref="Uri.IsFile"/> must be false.</param>
        /// <param name="endpointConfiguration">The <see cref="EndpointConfiguration"/> specifying the SOAP version to be used for the endpoint in the repository.</param>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="endpointAddress"/> is missing.</exception>
        /// <exception cref="ArgumentException">Thrown when <paramref name="endpointAddress"/> is not in a correct <see cref="Uri"/> non-file format.</exception>
        public SecurityManagementSoaRepository(ILogger<SecurityManagementSoaRepository> logger, Uri endpointAddress, EndpointConfiguration endpointConfiguration)
        {
            _logger = logger ?? NullLogger<SecurityManagementSoaRepository>.Instance;
            if (endpointAddress == null)
                throw new ArgumentNullException(nameof(endpointAddress), "SoaSecurity repository endpoint is required.");
            else if (!endpointAddress.IsWellFormedOriginalString() || endpointAddress.IsFile || !endpointAddress.IsAbsoluteUri)
                throw new ArgumentException("The SoaSecuirty repository parameter is not in the correct format.", nameof(endpointAddress));
            else
                _soa = new SecurityManagementSoapClient(endpointConfiguration, endpointAddress.AbsoluteUri);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="SecurityManagementSoaRepository"/> class with an <see cref="ILogger{TCategoryName}"/> to be used in the repository, an <see cref="Uri"/>
        /// specifying the address for the enpoint to be used in the repository, and a <see cref="Binding"/> containing the binding settings to be used for the endpoint in the repository.
        /// </summary>
        /// <param name="logger">The <see cref="ILogger{TCategoryName}"/> to be used in the repository. Defaults to <see cref="NullLogger{T}"/> when a null value is passed.</param>
        /// <param name="endpointAddress">The <see cref="Uri"/> specifying the endpoint address to be used in the repository. <see cref="Uri.IsAbsoluteUri"/> must be true and 
        /// <see cref="Uri.IsFile"/> must be false.</param>
        /// <param name="binding">The <see cref="Binding"/> containing the binding settings for the endpoint.</param>
        /// <exception cref="ArgumentNullException">Thrown if <paramref name="binding"/> or <paramref name="endpointAddress"/> are missing.</exception>
        /// <exception cref="ArgumentException">Thrown if <paramref name="endpointAddress"/> is not in a correct <see cref="Uri"/> non-file format.</exception>
        public SecurityManagementSoaRepository(ILogger<SecurityManagementSoaRepository> logger, Uri endpointAddress, Binding binding)
        {
            _logger = logger ?? NullLogger<SecurityManagementSoaRepository>.Instance;
            if (endpointAddress == null)
                throw new ArgumentNullException(nameof(endpointAddress), "SoaSecurity repository endpoint is required.");
            else if (!endpointAddress.IsWellFormedOriginalString() || endpointAddress.IsFile || !endpointAddress.IsAbsoluteUri)
                throw new ArgumentException("The SoaSecurity repository parameter is not in the correct format.", nameof(endpointAddress));

            if (binding == null) throw new ArgumentNullException(nameof(binding), "The SoaSecurity repository parameter is required.");

            _soa = new SecurityManagementSoapClient(binding, new System.ServiceModel.EndpointAddress(endpointAddress.AbsoluteUri));
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="SecurityManagementSoaRepository"/> class with an <see cref="ILogger{TCategoryName}"/> to be used in the repository and a 
        /// <see cref="string"/> containing the address for the endpoint to be used in the repository. The endpoint defaults to <see cref="EndpointConfiguration.SecurityManagementSoap12"/>.
        /// </summary>
        /// <param name="logger">The <see cref="ILogger{TCategoryName}"/> to be used in the repository. Defaults to <see cref="NullLogger{T}"/> when a null value is passed.</param>
        /// <param name="endpointAddress">A <see cref="string"/> representing the address to be used for the endpoint. It must be convertable to a valid <see cref="Uri"/> object 
        /// that has <see cref="Uri.IsAbsoluteUri"/> set to true and <see cref="Uri.IsFile"/> set to false.</param>
        /// <exception cref="ArgumentNullException">Thrown if <paramref name="endpointAddress"/> is missing.</exception>
        /// <exception cref="ArgumentException">Thrown if <paramref name="endpointAddress"/> is not in a correct <see cref="Uri"/> non-file format.</exception>
        public SecurityManagementSoaRepository(ILogger<SecurityManagementSoaRepository> logger, string endpointAddress)
        {
            _logger = logger ?? NullLogger<SecurityManagementSoaRepository>.Instance;
            if (string.IsNullOrWhiteSpace(endpointAddress))
                throw new ArgumentNullException(nameof(endpointAddress), "SoaSecurity repository endpoint is required.");
            
            var endpoint = new Uri(endpointAddress);
            if (!endpoint.IsWellFormedOriginalString() || endpoint.IsFile || !endpoint.IsAbsoluteUri)
                throw new ArgumentException("The SoaSecurity repository parameter is not in the correct format.", nameof(endpointAddress));
            else
                _soa = new SecurityManagementSoapClient(EndpointConfiguration.SecurityManagementSoap12, endpoint.AbsoluteUri);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="SecurityManagementSoaRepository"/> class with an <see cref="ILogger{TCategoryName}"/> to be used in the repository, a 
        /// <see cref="string"/> containing the address for the endpoint to be used in the repository, and an <see cref="EndpointConfiguration"/> to determine the binding
        /// information to be used for the endpoint.
        /// </summary>
        /// <param name="logger">The <see cref="ILogger{TCategoryName}"/> to be used in the repository. Defaults to <see cref="NullLogger{T}"/> when a null value is passed.</param>
        /// <param name="endpointAddress">A <see cref="string"/> representing the address to be used for the endpoint. It must be convertable to a valid <see cref="Uri"/> object 
        /// that has <see cref="Uri.IsAbsoluteUri"/> set to true and <see cref="Uri.IsFile"/> set to false.</param>
        /// <param name="endpointConfiguration">The <see cref="EndpointConfiguration"/> specifying the SOAP version to be used for the endpoint in the repository.</param>
        /// <exception cref="ArgumentNullException">Thrown if <paramref name="endpointAddress"/> is missing.</exception>
        /// <exception cref="ArgumentException">Thrown if <paramref name="endpointAddress"/> is not in a correct <see cref="Uri"/> non-file format.</exception>
        public SecurityManagementSoaRepository(ILogger<SecurityManagementSoaRepository> logger, string endpointAddress, EndpointConfiguration endpointConfiguration)
        {
            _logger = logger ?? NullLogger<SecurityManagementSoaRepository>.Instance;
            if (string.IsNullOrWhiteSpace(endpointAddress))
                throw new ArgumentNullException(nameof(endpointAddress), "SoaSecurity repository endpoint is required.");

            var endpoint = new Uri(endpointAddress);
            if (!endpoint.IsWellFormedOriginalString() || endpoint.IsFile || !endpoint.IsAbsoluteUri)
                throw new ArgumentException("The SoaSecurity repository parameter is not in the correct format.", nameof(endpointAddress));
            else
                _soa = new SecurityManagementSoapClient(endpointConfiguration, endpointAddress);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="SecurityManagementSoaRepository"/> class with an <see cref="ILogger{TCategoryName}"/> to be used in the repository, a
        /// <see cref="string"/> containing the address for the endpoint to be used in the repository, and a <see cref="Binding"/> containing the binding information to be used for the endpoint.
        /// </summary>
        /// <param name="logger">The <see cref="ILogger{TCategoryName}"/> to be used in the repository. Defaults to <see cref="NullLogger{T}"/> when a null value is passed.</param>
        /// <param name="endpointAddress">A <see cref="string"/> representing the address to be used for the endpoint. It must be convertable to a valid <see cref="Uri"/> object 
        /// that has <see cref="Uri.IsAbsoluteUri"/> set to true and <see cref="Uri.IsFile"/> set to false.</param>
        /// <param name="binding">The <see cref="Binding"/> containing the binding settings for the endpoint.</param>
        /// <exception cref="ArgumentNullException">Thrown if <paramref name="binding"/> or <paramref name="endpointAddress"/> is missing.</exception>
        /// <exception cref="ArgumentException">Thrown if <paramref name="endpointAddress"/> is not in a correct <see cref="Uri"/> non-file format.</exception>
        public SecurityManagementSoaRepository(ILogger<SecurityManagementSoaRepository> logger, string endpointAddress, Binding binding)
        {
            _logger = logger ?? NullLogger<SecurityManagementSoaRepository>.Instance;
            if (string.IsNullOrWhiteSpace(endpointAddress))
                throw new ArgumentNullException(nameof(endpointAddress), "SoaSecurity repository endpoint is required.");

            var endpoint = new Uri(endpointAddress);
            if (!endpoint.IsWellFormedOriginalString() || endpoint.IsFile || !endpoint.IsAbsoluteUri)
                throw new ArgumentException("The SoaSecurity repository parameter is not in the correct format.", nameof(endpointAddress));

            if (binding == null) throw new ArgumentNullException(nameof(binding), "The SoaSecurity repository parameter is required.");

            _soa = new SecurityManagementSoapClient(binding, new System.ServiceModel.EndpointAddress(endpoint.AbsoluteUri));
        }

        /// <inheritdoc />
        public SecurityData GetSecurityInfo(Guid traceId)
        {
            _logger.LogInformation("Retrieving security info from soa service. TraceId: {TraceId}", traceId);
            Stopwatch sw = Stopwatch.StartNew();


            throw new NotImplementedException();
        }

        /// <inheritdoc />
        public Task<SecurityData> GetSecurityInfoAsync(Guid traceId)
        {
            throw new NotImplementedException();
        }
    }
}
