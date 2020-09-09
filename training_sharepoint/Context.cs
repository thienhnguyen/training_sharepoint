using Microsoft.SharePoint.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace training_sharepoint
{
	public static class Context
	{
        public static ClientContext GetClientContext(string url)
        {
            using (var context = new ClientContext(url))
            {
                context.Credentials = new SharePointOnlineCredentials(Constants.USERNAME, Constants.SecurePasswordString());
                return context;
            }

            //If Default Security of Azure Enabled, use this:
            //var authenticationManager = new OfficeDevPnP.Core.AuthenticationManager();
            //ClientContext context = authenticationManager.GetWebLoginClientContext(Constants.SITE_URL.ToString(), null);
            //return context;
        }
    }
}
