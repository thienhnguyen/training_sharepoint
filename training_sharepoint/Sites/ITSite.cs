using Microsoft.Azure.ActiveDirectory.GraphClient;
using Microsoft.Online.SharePoint.TenantAdministration;
using Microsoft.SharePoint.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security;
using System.Text;
using System.Threading.Tasks;

namespace training_sharepoint.Sites
{
    class ITSite
    {
        private ClientContext _context { get; set; }

        public ITSite()
        {
        }

        public void CreateSite()
        {
            using (ClientContext tenantContext = new ClientContext(Constants.SITE_ADMIN_URL))
            {
                tenantContext.Credentials = new SharePointOnlineCredentials(Constants.USERNAME, Constants.SecurePasswordString());

                var tenant = new Tenant(tenantContext);

                //Properties of the New SiteCollection
                var siteCreationProperties = new SiteCreationProperties
                {

                    //New SiteCollection Url
                    Url = Constants.SITE_URL + Constants.SITE_COLLECTION,

                    //Title of the Root Site
                    Title = "IT Site create from CSOM",

                    //Login name of Owner
                    Owner = Constants.USERNAME,

                    //Template of the Root Site. Using Team Site for now.
                    Template = "STS#0",

                    //Storage Limit in MB
                    StorageMaximumLevel = 100,

                    //UserCode Resource Points Allowed
                    UserCodeMaximumLevel = 50
                };

                //Create the SiteCollection
                SpoOperation spo = tenant.CreateSite(siteCreationProperties);

                tenantContext.Load(tenant);

                //We will need the IsComplete property to check if the provisioning of the Site Collection is complete.
                tenantContext.Load(spo, i => i.IsComplete);

                tenantContext.ExecuteQuery();

                //Check if provisioning of the SiteCollection is complete.
                while (!spo.IsComplete)
                {
                    //Wait for 30 seconds and then try again
                    System.Threading.Thread.Sleep(30000);
                    spo.RefreshLoad();
                    tenantContext.ExecuteQuery();
                }

                Console.WriteLine("SiteCollection Created.");
            }
        }

        public void CreateSubSite(ClientContext context)
        {
            WebCreationInformation webCreationInformation = new WebCreationInformation
            {
                // This is relative URL of the url provided in context
                Url = Constants.SUBSITE,
                Title = "HR Subsite",

                // This will inherit permission from parent site
                UseSamePermissionsAsParentSite = true,

                // "STS#0" is the code for 'Team Site' template
                WebTemplate = "STS#0"
            };

            context.Site.RootWeb.Webs.Add(webCreationInformation);

            context.ExecuteQuery();
        }
    }
}
