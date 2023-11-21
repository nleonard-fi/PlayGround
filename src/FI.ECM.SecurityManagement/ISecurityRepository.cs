using FI.ECM.SecurityManagement.SecurityManagementSoa;
using System;

namespace FI.ECM.SecurityManagement
{
    public interface ISecurityRepository
    {
        SecurityData GetSecurityInfo(Guid traceId);
    }
}
