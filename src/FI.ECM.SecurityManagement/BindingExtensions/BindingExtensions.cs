using System;
using System.Collections.Generic;
using System.Text;
using System.ServiceModel.Channels;
using System.Runtime.CompilerServices;

namespace FI.ECM.SecurityManagement.BindingExtensions
{
    public static class BindingExtensions
    {
        public static void ConfigureEndpointBinding(this SoaSecurityRepository securityRepository, Binding binding)
        {
            if(binding != null) securityRepository._soa.Endpoint.Binding = binding;
        }
    }
}
