using Microsoft.SharePoint.Client;
using System;
using System.Linq;
using System.Net;
using System.Security;

namespace SharePoint
{
    class Program
    {
        static void Main(string[] args)
        {
            Uri site = new Uri("https://thienhnguyen.sharepoint.com/sites/Home/Hr");
            string user = "thienhnguyen@thienhnguyen.onmicrosoft.com";
            var password = "Abcd@2411";

            var securePassword = new SecureString();
            password.ToCharArray().ToList().ForEach(c => securePassword.AppendChar(c));

            // Note: The PnP Sites Core AuthenticationManager class also supports this
            using var authenticationManager = new AuthenticationManager();
            using var context = authenticationManager.GetContext(site, user, securePassword);
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
