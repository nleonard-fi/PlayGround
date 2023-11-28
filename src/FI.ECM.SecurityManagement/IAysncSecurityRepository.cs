using System;
using System.Threading.Tasks;
using FI.ECM.SecurityManagement.Exceptions;
using FI.ECM.SecurityManagement.SecurityManagementSoa;

namespace FI.ECM.SecurityManagement
{
    /// <summary>
    /// An interface defining asynchronous access to a repository containing security information.
    /// </summary>
    public interface IAysncSecurityRepository
    {
        /// <summary>
        /// Asynchronously retrieves security info from the security repository.
        /// </summary>
        /// <param name="traceId">A unique identifier for tracing this request to the security repository.</param>
        /// <returns>A <see cref="Task{TResult}"/> of <see cref="SecurityData"/> containing the authentication token upon success or 
        /// an error code and description upon failure.</returns>
        /// <exception cref="RepositoryException">Thrown if there was an error when working with the security repository.</exception>
        Task<SecurityData> GetSecurityInfoAsync(Guid traceId);
    }
}
