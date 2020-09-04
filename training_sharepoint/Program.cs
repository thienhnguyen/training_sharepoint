using Microsoft.SharePoint.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Security;
using System.Text;
using System.Threading.Tasks;

namespace training_sharepoint
{
    class Program
    {
        static void Main(string[] args)
        {
            Uri site = new Uri("https://thienhnguyen.sharepoint.com/sites/Home/Hr");
            string userName = "thienhnguyen@thienhnguyen.onmicrosoft.com";
            var password = "{S$t5rN$";

            var securePassword = new SecureString();
            password.ToCharArray().ToList().ForEach(c => securePassword.AppendChar(c));

            using(var context = new ClientContext(site))
            {

                context.Credentials = new SharePointOnlineCredentials(userName, securePassword);
                context.RequestTimeout = -1;
                ListCreationInformation lci = new ListCreationInformation
                {
                    Description = "Library used to hold Dynamics CRM documents",
                    Title = "Test",
                    TemplateType = 101,

                };

                List lib = context.Web.Lists.Add(lci);
                lib.ContentTypesEnabled = true;
                lib.Update();
                context.Load(lib);
                context.ExecuteQuery();
            }
        }
    }
}
