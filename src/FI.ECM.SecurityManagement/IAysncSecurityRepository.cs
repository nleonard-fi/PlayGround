using System;
using System.Threading.Tasks;
using FI.ECM.SecurityManagement.SecurityManagementSoa;

namespace FI.ECM.SecurityManagement
{
    public interface IAysncSecurityRepository
    {
        Task<SecurityData> GetSecurityInfoAsync(Guid traceId);
    }
}
