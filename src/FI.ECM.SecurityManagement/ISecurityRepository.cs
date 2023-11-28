using System;
using FI.ECM.SecurityManagement.Exceptions;
using FI.ECM.SecurityManagement.SecurityManagementSoa;

namespace FI.ECM.SecurityManagement
{
    /// <summary>
    /// An interface defining access to a repository containing security information.
    /// </summary>
    public interface ISecurityRepository
    {
        /// <summary>
        /// Retrieves security info from the security repository.
        /// </summary>
        /// <param name="traceId">A unique identifier for tracing this request to the security repository.</param>
        /// <returns>A <see cref="SecurityData"/> object containing the authentication token upon success or an error code and description upon failure.</returns>
        /// <exception cref="RepositoryException">Thrown if there was an error when working with the security repository.</exception>
        SecurityData GetSecurityInfo(Guid traceId);
    }
}
