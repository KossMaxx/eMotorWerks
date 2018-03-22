using System.Collections.Generic;
using Modules.WebApi.Shared.Responses;

namespace Modules.WebApi.Shared.Identity.Responses
{
    public class IdentityResponse : OkResponse
    {
        public IdentityResponse(List<string> data)
        {
            Data = data;
        }

        public List<string> Data { get; }
    }
}
