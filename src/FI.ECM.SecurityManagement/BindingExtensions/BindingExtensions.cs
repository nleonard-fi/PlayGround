using System.ServiceModel.Channels;

namespace FI.ECM.SecurityManagement.BindingExtensions
{
    public static class BindingExtensions
    {
        public static void ConfigureEndpointBinding(this SecurityManagementSoaRepository securityRepository, Binding binding)
        {
            if(binding != null) securityRepository._soa.Endpoint.Binding = binding;
        }
    }
}
