using Microsoft.SharePoint.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace training_sharepoint
{
	public static class Authentication
	{
        public static ClientContext GetAuthentication()
        {
            using (var context = new ClientContext(Constants.SITE_URL))
            {
                context.Credentials = new SharePointOnlineCredentials(Constants.USERNAME, Constants.SecurePasswordString());
                return context;
            }

            //var authenticationManager = new OfficeDevPnP.Core.AuthenticationManager();
            //ClientContext context = authenticationManager.GetWebLoginClientContext(Constants.SITE_URL.ToString(), null);
            //return context;
        }
    }
}
