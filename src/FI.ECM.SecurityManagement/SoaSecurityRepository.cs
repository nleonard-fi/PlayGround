using System;
using System.Collections.Generic;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;
using FI.ECM.SecurityManagement.SecurityManagementSoa;
using Microsoft.Extensions.Logging;

namespace FI.ECM.SecurityManagement
{
    public class SoaSecurityRepository : ISecurityRepository, IAysncSecurityRepository
    {
        private readonly ILogger<SoaSecurityRepository> _logger;
        internal SecurityManagementSoapClient _soa;

        public SoaSecurityRepository(ILogger<SoaSecurityRepository> logger, EnvironmentConfiguration environmentConfiguration)
        {
            _logger = logger;
            _soa = new SecurityManagementSoapClient(SecurityManagementSoapClient.EndpointConfiguration.SecurityManagementSoap12, environmentConfiguration);
        }

        public SoaSecurityRepository(ILogger<SoaSecurityRepository> logger, Uri endpointAddress)
        {
            _logger = logger;
            if (endpointAddress == null)
                throw new ArgumentNullException(nameof(endpointAddress), "SoaSecurity repository endpoint is required.");
            else if(!endpointAddress.IsWellFormedOriginalString() || endpointAddress.IsFile || endpointAddress.IsAbsoluteUri) 
                throw new ArgumentException("The SoaSecurity repository parameter is not in the correct format.", nameof(endpointAddress));
            else
                _soa = new SecurityManagementSoapClient(SecurityManagementSoapClient.EndpointConfiguration.SecurityManagementSoap12, endpointAddress.ToString());
        }

        public SoaSecurityRepository(ILogger<SoaSecurityRepository> logger, string endpointAddress)
        {
            _logger = logger;
            if (string.IsNullOrWhiteSpace(endpointAddress) || string.IsNullOrEmpty(endpointAddress))
                throw new ArgumentNullException(nameof(endpointAddress), "SoaSecurity repository endpoint is required.");
            
            var endpoint = new Uri(endpointAddress);
            if (!endpoint.IsWellFormedOriginalString() || endpoint.IsFile || endpoint.IsAbsoluteUri)
                throw new ArgumentException("The SoaSecurity repository parameter is not in the correct format.", nameof(endpointAddress));
            else
                _soa = new SecurityManagementSoapClient(SecurityManagementSoapClient.EndpointConfiguration.SecurityManagementSoap12, endpoint.ToString());
        }

        /// <inheritdoc />
        public SecurityData GetSecurityInfo(Guid traceId)
        {
            _logger.LogInformation("Retrieving security info from soa service. TraceId: {TraceId}", traceId);

            throw new NotImplementedException();
        }

        /// <inheritdoc />
        public Task<SecurityData> GetSecurityInfoAsync(Guid traceId)
        {
            throw new NotImplementedException();
        }
    }
}
