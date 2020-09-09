using Microsoft.SharePoint.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace training_sharepoint.Content_Types
{
    class ITContentType
    {
        private ClientContext _context { get; set; }

        public ITContentType(ClientContext context)
        {
            _context = context;
        }

        public void CreateContentType(string contentTypeName, string parentContentTypeName)
        {
            ContentTypeCollection collection = _context.Site.RootWeb.ContentTypes;

            _context.Load(collection);
            _context.ExecuteQuery();

            //Emp
            ContentType parentContentType = (from c in collection
                                             where c.Name == parentContentTypeName
                                             select c).FirstOrDefault();

            ContentTypeCreationInformation contentType = new ContentTypeCreationInformation
            {
                Name = contentTypeName,
                Group = "IT Content Type",
                ParentContentType = parentContentType
            };

            collection.Add(contentType);

            _context.ExecuteQuery();
        }

        public void AddFieldToContentType(string contentTypeName, string[] fields)
        {
            // Get all the content types from current site
            ContentTypeCollection collection = _context.Site.RootWeb.ContentTypes;
            _context.Load(collection);
            _context.ExecuteQuery();

            ContentType contentType = (from c in collection
                                          where c.Name == contentTypeName
                                          select c).FirstOrDefault();

            foreach (var item in fields)
            {
                if (item.Contains("Leader") || item.Contains("Members"))
                {
                    continue;
                }
                Field targetField = _context.Web.AvailableFields.GetByInternalNameOrTitle(item);
                FieldLinkCreationInformation fldLink = new FieldLinkCreationInformation
                {
                    Field = targetField
                };
                fldLink.Field.Required = false;
                fldLink.Field.Hidden = false;

                contentType.FieldLinks.Add(fldLink);
                contentType.Update(false);
            }
        }
    }
}
