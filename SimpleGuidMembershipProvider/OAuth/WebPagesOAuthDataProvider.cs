using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Security;
using nonintanon.Security.SimpleGuidMembershipProvider.Resources;
using DotNetOpenAuth.AspNet;

namespace nonintanon.Security.OAuth
{
    internal class WebPagesOAuthDataProvider : IOpenAuthDataProvider
    {
        private static GuidExtendedMembershipProvider VerifyProvider()
        {
            var provider = Membership.Provider as GuidExtendedMembershipProvider;
            if (provider == null) {
                throw new InvalidOperationException(OAuthResources.Security_NoExtendedMembershipProvider);
            }
            return provider;
        }

        public string GetUserNameFromOpenAuth(string openAuthProvider, string openAuthId)
        {
            GuidExtendedMembershipProvider provider = VerifyProvider();

            Guid userId = provider.GetUserIdFromOAuth(openAuthProvider, openAuthId);
            if (userId == Guid.Empty) {
                return null;
            }

            return provider.GetUserNameFromId(userId);
        }
    }
}
