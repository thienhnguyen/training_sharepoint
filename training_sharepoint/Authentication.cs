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
        //using(var context = new ClientContext(site))
        //{
        //    context.Credentials = new SharePointOnlineCredentials(userName, securePassword);
        //}
        public static ClientContext GetAuthentication()
        {
            var authenticationManager = new OfficeDevPnP.Core.AuthenticationManager();
            ClientContext context = authenticationManager.GetWebLoginClientContext(Constants.SITE_URL.ToString(), null);
            return context;
        }
    }
}
