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

        public void CreateSite(out string siteAddress, string siteName = "IT Site create from CSOM")
        {
            siteAddress = siteName.Replace(" ", "");
            using (ClientContext tenantContext = new ClientContext(Constants.SITE_ADMIN_URL))
            {
                tenantContext.Credentials = new SharePointOnlineCredentials(Constants.USERNAME, Constants.SecurePasswordString());

                var tenant = new Tenant(tenantContext);

                var siteCreationProperties = new SiteCreationProperties
                {
                    Url = Constants.SITE_URL + @"/sites/" + siteAddress,
                    Title = siteName,
                    Owner = Constants.USERNAME,
                    Template = "STS#0", //Team site template
                    StorageMaximumLevel = 100,
                    UserCodeMaximumLevel = 50
                };

                SpoOperation spo = tenant.CreateSite(siteCreationProperties);

                tenantContext.Load(tenant);

                tenantContext.Load(spo, i => i.IsComplete);

                tenantContext.ExecuteQuery();

                while (!spo.IsComplete)
                {
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
                Url = Constants.SUBSITE,
                Title = "HR Subsite",
                UseSamePermissionsAsParentSite = true,
                WebTemplate = "STS#0"   // 'Team Site' template
            };

            context.Site.RootWeb.Webs.Add(webCreationInformation);

            context.ExecuteQuery();
        }
    }
}
