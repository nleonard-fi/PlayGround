﻿using System;
using System.Diagnostics;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using System.ServiceModel.Channels;
using FI.ECM.SecurityManagement.Exceptions;
using Microsoft.Extensions.Logging.Abstractions;
using FI.ECM.SecurityManagement.SecurityManagementSoa;
using System.Security.Cryptography.Pkcs;

namespace FI.ECM.SecurityManagement
{
    public class SecurityManagementSoaRepository : ISecurityRepository, IAysncSecurityRepository
    {
        internal SecurityManagementSoapClient _soa;
        private readonly ILogger<SecurityManagementSoaRepository> _logger;
        private readonly CredentialInfo _credentialInfo;

        /// <summary>
        /// Initializes a new instance of the <see cref="SecurityManagementSoaRepository"/> class with a <paramref name="logger"/> to be used in the repository, the
        /// <paramref name="environmentConfiguration"/> to determine the binding information, and the <paramref name="accessCredentials"/> for the endpoint. 
        /// The endpoint defaults to <see cref="EndpointConfiguration.SecurityManagementSoap12"/> SOAP version.
        /// </summary>
        /// <param name="logger">The <see cref="ILogger{TCategoryName}"/> to be used in the repository. Defaults to <see cref="NullLogger{T}"/> when a null value is passed.</param>
        /// <param name="environmentConfiguration">The <see cref="EnvironmentConfiguration"/> to be used for determining endpoint binding information.</param>
        /// <param name="accessCredentials">The <see cref="CredentialInfo"/> for authenticating with the security repository.</param>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="accessCredentials"/> are missing.</exception>
        public SecurityManagementSoaRepository(EnvironmentConfiguration environmentConfiguration, CredentialInfo accessCredentials, 
            ILogger<SecurityManagementSoaRepository> logger = null)
        {
            _logger = logger ?? NullLogger<SecurityManagementSoaRepository>.Instance;
            _credentialInfo = accessCredentials ?? throw new ArgumentNullException(nameof(accessCredentials), "SoaSecurity access credentials are required.");
            _soa = new SecurityManagementSoapClient(EndpointConfiguration.SecurityManagementSoap12, environmentConfiguration);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="SecurityManagementSoaRepository"/> class with a <paramref name="logger"/> to be used in the repository, an <paramref name="environmentConfiguration"/>
        /// used to determine the binding information, an <paramref name="endpointConfiguration"/> to specify the SOAP version for the endpoint, 
        /// and the <paramref name="accessCredentials"/> for the endpoint.
        /// </summary>
        /// <param name="logger">The <see cref="ILogger{TCategoryName}"/> to be used in the repository. Defaults to <see cref="NullLogger{T}"/> when a null value is passed.</param>
        /// <param name="environmentConfiguration">The <see cref="EnvironmentConfiguration"/> to be used for determining endpoint binding information.</param>
        /// <param name="endpointConfiguration">The <see cref="EndpointConfiguration"/> specifying the SOAP version to be used for the endpoint in the repository.</param>
        /// <param name="accessCredentials">The <see cref="CredentialInfo"/> for authenticating with the security repository.</param>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="accessCredentials"/> are missing.</exception>
        public SecurityManagementSoaRepository(EnvironmentConfiguration environmentConfiguration, EndpointConfiguration endpointConfiguration, CredentialInfo accessCredentials, 
            ILogger<SecurityManagementSoaRepository> logger = null)
        {
            _logger = logger ?? NullLogger<SecurityManagementSoaRepository>.Instance;
            _credentialInfo = accessCredentials ?? throw new ArgumentNullException(nameof(accessCredentials), "SoaSecurity access credentials are required.");
            _soa = new SecurityManagementSoapClient(endpointConfiguration, environmentConfiguration);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="SecurityManagementSoaRepository"/> class with a <paramref name="logger"/> to be used in the repository, the
        /// <paramref name="endpointAddress"/> for the repository, and the <paramref name="accessCredentials"/> for the endpoint. The endpoint defaults to 
        /// <see cref="EndpointConfiguration.SecurityManagementSoap12"/> SOAP version.
        /// </summary>
        /// <param name="logger">The <see cref="ILogger{TCategoryName}"/> to be used in the repository. Defaults to <see cref="NullLogger{T}"/> when a null value is passed.</param>
        /// <param name="endpointAddress">The <see cref="Uri"/> specifying the endpoint address to be used in the repository. <see cref="Uri.IsAbsoluteUri"/> must be true and 
        /// <see cref="Uri.IsFile"/> must be false.</param>
        /// <param name="accessCredentials">The <see cref="CredentialInfo"/> for authenticating with the security repository.</param>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="endpointAddress"/> or <paramref name="accessCredentials"/> are missing.</exception>
        /// <exception cref="ArgumentException">Thrown when <paramref name="endpointAddress"/> is not in a correct <see cref="Uri"/> non-file format.</exception>
        public SecurityManagementSoaRepository(Uri endpointAddress, CredentialInfo accessCredentials, ILogger<SecurityManagementSoaRepository> logger = null)
        {
            _logger = logger ?? NullLogger<SecurityManagementSoaRepository>.Instance;
            _credentialInfo = accessCredentials ?? throw new ArgumentNullException(nameof(accessCredentials), "SoaSecurity access credentials are required.");

            if (endpointAddress == null)
                throw new ArgumentNullException(nameof(endpointAddress), "SoaSecurity repository endpoint is required.");
            else if(!endpointAddress.IsWellFormedOriginalString() || !endpointAddress.IsAbsoluteUri || endpointAddress.IsFile) 
                throw new ArgumentException("The SoaSecurity repository parameter is not in the correct format.", nameof(endpointAddress));
            else
                _soa = new SecurityManagementSoapClient(EndpointConfiguration.SecurityManagementSoap12, endpointAddress.AbsoluteUri);
        }

        /// <summary>
        /// Initialzes a new instance of the <see cref="SecurityManagementSoaRepository"/> class with a <paramref name="logger"/> to be used in the repository, the
        /// <paramref name="endpointAddress"/> for the repository, a the <paramref name="endpointConfiguration"/> specifying the SOAP version of the endpoint, and the 
        /// <paramref name="accessCredentials"/> for the endpoint.
        /// </summary>
        /// <param name="logger">The <see cref="ILogger{TCategoryName}"/> to be used in the repository. Defaults to <see cref="NullLogger{T}"/> when a null value is passed.</param>
        /// <param name="endpointAddress">The <see cref="Uri"/> specifying the endpoint address to be used in the repository. <see cref="Uri.IsAbsoluteUri"/> must be true and 
        /// <see cref="Uri.IsFile"/> must be false.</param>
        /// <param name="endpointConfiguration">The <see cref="EndpointConfiguration"/> specifying the SOAP version to be used for the endpoint in the repository.</param>
        /// <param name="accessCredentials">The <see cref="CredentialInfo"/> for authenticating with the security repository.</param>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="endpointAddress"/> or <paramref name="accessCredentials"/> are missing.</exception>
        /// <exception cref="ArgumentException">Thrown when <paramref name="endpointAddress"/> is not in a correct <see cref="Uri"/> non-file format.</exception>
        public SecurityManagementSoaRepository(Uri endpointAddress, EndpointConfiguration endpointConfiguration, CredentialInfo accessCredentials, 
            ILogger<SecurityManagementSoaRepository> logger = null)
        {
            _logger = logger ?? NullLogger<SecurityManagementSoaRepository>.Instance;
            _credentialInfo = accessCredentials ?? throw new ArgumentNullException(nameof(accessCredentials), "SoaSecurity access credentials are required.");

            if (endpointAddress == null)
                throw new ArgumentNullException(nameof(endpointAddress), "SoaSecurity repository endpoint is required.");
            else if (!endpointAddress.IsWellFormedOriginalString() || !endpointAddress.IsAbsoluteUri || endpointAddress.IsFile)
                throw new ArgumentException("The SoaSecurity repository parameter is not in the correct format.", nameof(endpointAddress));
            else
                _soa = new SecurityManagementSoapClient(endpointConfiguration, endpointAddress.AbsoluteUri);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="SecurityManagementSoaRepository"/> class with a <paramref name="logger"/> to be used in the repository, the
        /// <paramref name="endpointAddress"/> for the repository, the <paramref name="binding"/> settings for the endpoint, 
        /// and the <paramref name="accessCredentials"/> for the endpoint.
        /// </summary>
        /// <param name="logger">The <see cref="ILogger{TCategoryName}"/> to be used in the repository. Defaults to <see cref="NullLogger{T}"/> when a null value is passed.</param>
        /// <param name="endpointAddress">The <see cref="Uri"/> specifying the endpoint address to be used in the repository. <see cref="Uri.IsAbsoluteUri"/> must be true and 
        /// <see cref="Uri.IsFile"/> must be false.</param>
        /// <param name="binding">The <see cref="Binding"/> containing the binding settings for the endpoint.</param>
        /// <param name="accessCredentials">The <see cref="CredentialInfo"/> for authenticating with the security repository.</param>
        /// <exception cref="ArgumentNullException">Thrown if <paramref name="binding"/>, <paramref name="endpointAddress"/>, 
        /// or <paramref name="accessCredentials"/> are missing.</exception>
        /// <exception cref="ArgumentException">Thrown if <paramref name="endpointAddress"/> is not in a correct <see cref="Uri"/> non-file format.</exception>
        public SecurityManagementSoaRepository(Uri endpointAddress, Binding binding, CredentialInfo accessCredentials, ILogger<SecurityManagementSoaRepository> logger = null)
        {
            _logger = logger ?? NullLogger<SecurityManagementSoaRepository>.Instance;
            _credentialInfo = accessCredentials ?? throw new ArgumentNullException(nameof(accessCredentials), "SoaSecurity access credentials are required.");

            if (endpointAddress == null)
                throw new ArgumentNullException(nameof(endpointAddress), "SoaSecurity repository endpoint is required.");
            else if (!endpointAddress.IsWellFormedOriginalString() || !endpointAddress.IsAbsoluteUri || endpointAddress.IsFile)
                throw new ArgumentException("The SoaSecurity repository parameter is not in the correct format.", nameof(endpointAddress));

            if (binding == null) throw new ArgumentNullException(nameof(binding), "The SoaSecurity repository parameter is required.");

            _soa = new SecurityManagementSoapClient(binding, new System.ServiceModel.EndpointAddress(endpointAddress.AbsoluteUri));
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="SecurityManagementSoaRepository"/> class with a <paramref name="logger"/> to be used in the repository, 
        /// the <paramref name="endpointAddress"/> for the repository, and the <paramref name="accessCredentials"/> for the endpoint. The endpoint defaults to 
        /// <see cref="EndpointConfiguration.SecurityManagementSoap12"/> SOAP version.
        /// </summary>
        /// <param name="logger">The <see cref="ILogger{TCategoryName}"/> to be used in the repository. Defaults to <see cref="NullLogger{T}"/> when a null value is passed.</param>
        /// <param name="endpointAddress">A <see cref="string"/> representing the address to be used for the endpoint. It must be convertable to a valid <see cref="Uri"/> object 
        /// that has <see cref="Uri.IsAbsoluteUri"/> set to true and <see cref="Uri.IsFile"/> set to false.</param>
        /// <param name="accessCredentials">The <see cref="CredentialInfo"/> for authenticating with the security repository.</param>
        /// <exception cref="ArgumentNullException">Thrown if <paramref name="endpointAddress"/> or <paramref name="accessCredentials"/> are missing.</exception>
        /// <exception cref="ArgumentException">Thrown if <paramref name="endpointAddress"/> is not in a correct <see cref="Uri"/> non-file format.</exception>
        public SecurityManagementSoaRepository(string endpointAddress, CredentialInfo accessCredentials, ILogger<SecurityManagementSoaRepository> logger = null)
        {
            _logger = logger ?? NullLogger<SecurityManagementSoaRepository>.Instance;
            _credentialInfo = accessCredentials ?? throw new ArgumentNullException(nameof(accessCredentials), "SoaSecurity access credentials are required.");

            if (string.IsNullOrWhiteSpace(endpointAddress))
                throw new ArgumentNullException(nameof(endpointAddress), "SoaSecurity repository endpoint is required.");
            
            bool created = Uri.TryCreate(endpointAddress, UriKind.Absolute, out Uri endpoint);
            if (!created || !endpoint.IsWellFormedOriginalString() || endpoint.IsFile)
                throw new ArgumentException("The SoaSecurity repository parameter is not in the correct format.", nameof(endpointAddress));
            else
                _soa = new SecurityManagementSoapClient(EndpointConfiguration.SecurityManagementSoap12, endpoint.AbsoluteUri);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="SecurityManagementSoaRepository"/> class with a <paramref name="logger"/> to be used in the repository, 
        /// the <paramref name="endpointAddress"/>  for the repository, the <paramref name="endpointConfiguration"/> to determine the binding information,
        /// and the <paramref name="accessCredentials"/> for the endpoint.
        /// </summary>
        /// <param name="logger">The <see cref="ILogger{TCategoryName}"/> to be used in the repository. Defaults to <see cref="NullLogger{T}"/> when a null value is passed.</param>
        /// <param name="endpointAddress">A <see cref="string"/> representing the address to be used for the endpoint. It must be convertable to a valid <see cref="Uri"/> object 
        /// that has <see cref="Uri.IsAbsoluteUri"/> set to true and <see cref="Uri.IsFile"/> set to false.</param>
        /// <param name="endpointConfiguration">The <see cref="EndpointConfiguration"/> specifying the SOAP version to be used for the endpoint in the repository.</param>
        /// <param name="accessCredentials">The <see cref="CredentialInfo"/> for authenticating with the security repository.</param>
        /// <exception cref="ArgumentNullException">Thrown if <paramref name="endpointAddress"/> or <paramref name="accessCredentials"/> are missing.</exception>
        /// <exception cref="ArgumentException">Thrown if <paramref name="endpointAddress"/> is not in a correct <see cref="Uri"/> non-file format.</exception>
        public SecurityManagementSoaRepository(string endpointAddress, EndpointConfiguration endpointConfiguration, 
            CredentialInfo accessCredentials, ILogger<SecurityManagementSoaRepository> logger = null)
        {
            _logger = logger ?? NullLogger<SecurityManagementSoaRepository>.Instance;
            _credentialInfo = accessCredentials ?? throw new ArgumentNullException(nameof(accessCredentials), "SoaSecurity access credentials are required.");

            if (string.IsNullOrWhiteSpace(endpointAddress))
                throw new ArgumentNullException(nameof(endpointAddress), "SoaSecurity repository endpoint is required.");

            bool created = Uri.TryCreate(endpointAddress, UriKind.Absolute, out Uri endpoint);
            if (!created || !endpoint.IsWellFormedOriginalString() || endpoint.IsFile)
                throw new ArgumentException("The SoaSecurity repository parameter is not in the correct format.", nameof(endpointAddress));
            else
                _soa = new SecurityManagementSoapClient(endpointConfiguration, endpointAddress);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="SecurityManagementSoaRepository"/> class with a <paramref name="logger"/> to be used in the repository, 
        /// the <paramref name="endpointAddress"/>  for the repository, the <paramref name="binding"/> information for the endpoint, 
        /// and the <paramref name="accessCredentials"/> for the endpoint.
        /// </summary>
        /// <param name="logger">The <see cref="ILogger{TCategoryName}"/> to be used in the repository. Defaults to <see cref="NullLogger{T}"/> when a null value is passed.</param>
        /// <param name="endpointAddress">A <see cref="string"/> representing the address to be used for the endpoint. It must be convertable to a valid <see cref="Uri"/> object 
        /// that has <see cref="Uri.IsAbsoluteUri"/> set to true and <see cref="Uri.IsFile"/> set to false.</param>
        /// <param name="binding">The <see cref="Binding"/> containing the binding settings for the endpoint.</param>
        /// <param name="accessCredentials">The <see cref="CredentialInfo"/> for authenticating with the security repository.</param>
        /// <exception cref="ArgumentNullException">Thrown if <paramref name="binding"/>, <paramref name="endpointAddress"/>,
        /// <paramref name="accessCredentials"/> are missing.</exception>
        /// <exception cref="ArgumentException">Thrown if <paramref name="endpointAddress"/> is not in a correct <see cref="Uri"/> non-file format.</exception>
        public SecurityManagementSoaRepository(string endpointAddress, Binding binding, CredentialInfo accessCredentials, ILogger<SecurityManagementSoaRepository> logger = null)
        {
            _logger = logger ?? NullLogger<SecurityManagementSoaRepository>.Instance;
            _credentialInfo = accessCredentials ?? throw new ArgumentNullException(nameof(accessCredentials), "SoaSecurity access credentials are required.");

            if (string.IsNullOrWhiteSpace(endpointAddress))
                throw new ArgumentNullException(nameof(endpointAddress), "SoaSecurity repository endpoint is required.");

            bool created = Uri.TryCreate(endpointAddress, UriKind.Absolute, out Uri endpoint);
            if (!created || !endpoint.IsWellFormedOriginalString() || endpoint.IsFile)
                throw new ArgumentException("The SoaSecurity repository parameter is not in the correct format.", nameof(endpointAddress));

            if (binding == null) throw new ArgumentNullException(nameof(binding), "The SoaSecurity repository parameter is required.");

            _soa = new SecurityManagementSoapClient(binding, new System.ServiceModel.EndpointAddress(endpoint.AbsoluteUri));
        }

        /// <inheritdoc />
        public SecurityData GetSecurityInfo(Guid traceId)
        {
            _logger.LogInformation("Retrieving security info from Soa service. TraceId: {TraceId}", traceId);
            Stopwatch sw = Stopwatch.StartNew();

            var tokenData = new SecurityData() { MessageDescription = "Failed to retrieve a token. Unknown reason.", MessageStatus = -1 };

            try
            {
                if (!_credentialInfo.IsEncrypted)
                {
                    tokenData = _soa.RequestToken(_credentialInfo.User, _credentialInfo.Password);
                }
                else
                {
                    tokenData = _soa.RequestToken(_credentialInfo.Decryptor(_credentialInfo.User), _credentialInfo.Decryptor(_credentialInfo.Password));
                }
            }
            catch(Exception ex)
            {
                _logger.LogError("Error retrieving token. TraceId: {TraceId}. Error - {Error}", traceId, ex);
                throw new RepositoryException("Repository execution failed.", ex);
            }

            sw.Stop();
            _logger.LogDebug("Finished retrieving security info from Soa service. Took {TimeTaken} ms. TraceId: {TraceId}", sw.ElapsedMilliseconds, traceId);

            return tokenData;
        }

        /// <inheritdoc />
        public async Task<SecurityData> GetSecurityInfoAsync(Guid traceId)
        {
            _logger.LogInformation("Retrieving security info from Soa service. TraceId: {TraceId}", traceId);
            Stopwatch sw = Stopwatch.StartNew();

            var tokenData = new SecurityData() { MessageDescription = "Failed to retrieve a token. Unknown reason.", MessageStatus = -1 };

            try
            {
                if (!_credentialInfo.IsEncrypted)
                {
                    tokenData = await _soa.RequestTokenAsync(_credentialInfo.User, _credentialInfo.Password);
                }
                else
                {
                    tokenData = await _soa.RequestTokenAsync(_credentialInfo.Decryptor(_credentialInfo.User), _credentialInfo.Decryptor(_credentialInfo.Password));
                }
            }
            catch(Exception ex)
            {
                _logger.LogError("Error retrieving token. TraceId: {TraceId}. Error - {Error}", traceId, ex);
                throw new RepositoryException("Rpository execution failed.", ex);
            }

            sw.Stop();
            _logger.LogDebug("Finished retrieving security info from Soa service. Took {TimeTaken} ms. TraceId: {TraceId}", sw.ElapsedMilliseconds, traceId);

            return tokenData;
        }
    }
}
