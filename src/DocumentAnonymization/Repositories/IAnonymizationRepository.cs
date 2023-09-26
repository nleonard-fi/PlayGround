using DocumentAnonymization.Models;

namespace DocumentAnonymization.Repositories
{
    public interface IAnonymizationRepository
    {
        /// <summary>
        /// Insert a client anonymization request for the anonymization service to process.
        /// </summary>
        /// <param name="anonymizationRequest">The request details needed by the anonymization service.</param>
        /// <returns></returns>
        Task<Guid> InsertAnonymizationRequest(AnonymizationRequestDto anonymizationRequest);
    }
}
