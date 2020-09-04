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

            var docLibraryName = "SharePoint API Document Library";
            var liName = "SharePoint API List";

            var docLibContentTypeName = "Project Documents Library";
            var liContentTypeName = "Employees";

            var securePassword = new SecureString();
            password.ToCharArray().ToList().ForEach(c => securePassword.AppendChar(c));

            #region Authentication
            //using(var context = new ClientContext(site))
            //{
            //    context.Credentials = new SharePointOnlineCredentials(userName, securePassword);
            //}

            var authenticationManager = new OfficeDevPnP.Core.AuthenticationManager();
            ClientContext context = authenticationManager.GetWebLoginClientContext(site.ToString(), null);

            #endregion

            #region Create new Document Library and new List
            ListCreationInformation lci = new ListCreationInformation
            {
                Title = docLibraryName,
                TemplateType = 101,
            };
            List lib = context.Web.Lists.Add(lci);
            lib.ContentTypesEnabled = true;

            lci = new ListCreationInformation
            {
                Title = liName,
                TemplateType = 100,
            };
            lib = context.Web.Lists.Add(lci);
            lib.ContentTypesEnabled = true;

            lib.Update();

            #endregion

            #region Add Content Type to Document Library
            ContentTypeCollection contentTypeCollection;

            contentTypeCollection = context.Site.RootWeb.ContentTypes;

            context.Load(contentTypeCollection);
            context.ExecuteQuery();

            ContentType docLibContentType = (from c in contentTypeCollection
                                             where c.Name == docLibContentTypeName
                                             select c).FirstOrDefault();

            List targetDocLib = context.Web.Lists.GetByTitle(docLibraryName);
            targetDocLib.ContentTypes.AddExistingContentType(docLibContentType);
            targetDocLib.Update();

            context.Web.Update();

            #endregion

            #region Add Content Type to List
            ContentType liContentType = (from c in contentTypeCollection
                                             where c.Name == liContentTypeName
                                             select c).FirstOrDefault();

            List targetList = context.Web.Lists.GetByTitle(liName);
            targetList.ContentTypes.AddExistingContentType(liContentType);
            targetList.Update();

            context.Web.Update();
            #endregion
            context.ExecuteQuery();

            //https://www.c-sharpcorner.com/UploadFile/sagarp/create-update-delete-a-list-using-client-object-model-cso/
        }
    }
}
