using Rideshare.Uber.Sdk.Models;

namespace Rideshare.Uber.Sdk
{
    public class ServerAuthenticatedUberRiderService : BaseUberRiderService
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="serverToken"></param>
        /// <param name="baseUri"></param>
        public ServerAuthenticatedUberRiderService(string serverToken, string baseUri = "https://api.uber.com")
            : base(AccessTokenType.Server, serverToken, baseUri)
        { }
    }
}
