using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Kubernox.Service.Host.Core
{
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class CoreController : ControllerBase
    {
        #region Get Name Identifier
        private const string OBJECT_ID_CLAIM = "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/name";
        public string UserId
        {
            get
            {
                return User.Claims.First(c => c.Type.Equals(OBJECT_ID_CLAIM, StringComparison.InvariantCultureIgnoreCase)).Value;
            }
        }

        public string GetObjectIdAsNull
        {
            get
            {
                try
                {
                    return User.Claims.First(c => c.Type.Equals(OBJECT_ID_CLAIM, StringComparison.InvariantCultureIgnoreCase)).Value;
                }
                catch (Exception)
                {
                    return null;
                }
            }
        }
        #endregion
    }
}
