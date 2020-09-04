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

            var listDocName = "SharePoint API";
            var contentTypeName = "Project Documents Library";

            var securePassword = new SecureString();
            password.ToCharArray().ToList().ForEach(c => securePassword.AppendChar(c));

            #region Authentication
            //using(var context = new ClientContext(site))
            //{

            //    context.Credentials = new SharePointOnlineCredentials(userName, securePassword);
            //    ListCreationInformation lci = new ListCreationInformation
            //    {
            //        Description = "Library used to hold Dynamics CRM documents",
            //        Title = "Test",
            //        TemplateType = 101,

            //    };

            //    List lib = context.Web.Lists.Add(lci);
            //    lib.ContentTypesEnabled = true;
            //    lib.Update();
            //    context.Load(lib);
            //    context.ExecuteQuery();
            //}

            var authenticationManager = new OfficeDevPnP.Core.AuthenticationManager();
            ClientContext context = authenticationManager.GetWebLoginClientContext(site.ToString(), null);

            #endregion

            #region Create New Document List
            ListCreationInformation lci = new ListCreationInformation
            {
                Title = listDocName,
                TemplateType = 101,

            };

            List lib = context.Web.Lists.Add(lci);
            lib.ContentTypesEnabled = true;
            lib.Update();

            #endregion

            #region Add Content Type to List
            ContentTypeCollection contentTypeCollection;

            contentTypeCollection = context.Site.RootWeb.ContentTypes;

            context.Load(contentTypeCollection);
            context.ExecuteQuery();

            ContentType targetContentType = (from c in contentTypeCollection
                                             where c.Name == contentTypeName
                                             select c).FirstOrDefault();

            List targetList = context.Web.Lists.GetByTitle(listDocName);
            targetList.ContentTypes.AddExistingContentType(targetContentType);
            targetList.Update();

            context.Web.Update();

            #endregion

            context.ExecuteQuery();
        }
    }
}
