using System;
using System.Collections.Generic;

namespace NIntegrate.Configuration
{
    public interface IEndpointProvider
    {
        IList<Endpoint> GetServerEndpoints(Type serviceContract);
        IList<Endpoint> GetClientEndpoints(Type serviceContract);
    }
}
