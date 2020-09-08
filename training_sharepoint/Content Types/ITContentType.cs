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

        private readonly string[] empFields = new string[]
        {
            "ITEmpFirstName",
            "ITEmpLastName",
            "ITEmpEmail",
            "ITEmpShortDescription",
            "ITEmpProgrammingLanguages"
        };
        private readonly string[] projectFields = new string[]
        {
            "ITProjectName",
            "ITProjectStartDate",
            "ITProjectEndDate",
            "ITProjectDescription",
            "ITProjectState"
        };
        private readonly string[] docFields = new string[]
        {
            "ITDocTitle",
            "ITDocDescription",
            "ITDocLinkedProjectItem",
            "ITProjectDescription",
            "ITDocTypeOfDoc"
        };

        public ITContentType(ClientContext context)
        {
            _context = context;
        }

        public void CreateContentType()
        {
            ContentTypeCollection collection = _context.Site.RootWeb.ContentTypes;

            _context.Load(collection);
            _context.ExecuteQuery();

            //Emp
            ContentType empParentContentType = (from c in collection
                                             where c.Name == "Item"
                                             select c).FirstOrDefault();

            ContentTypeCreationInformation empContentType = new ContentTypeCreationInformation
            {
                Name = "IT Employees",
                Group = "IT Content Type",
                ParentContentType = empParentContentType
            };

            //Project
            ContentType projParentContentType = (from c in collection
                                 where c.Name == "Item"
                                 select c).FirstOrDefault();

            ContentTypeCreationInformation projContentType = new ContentTypeCreationInformation
            {
                Name = "IT Projects",
                Group = "IT Content Type",
                ParentContentType = projParentContentType
            };

            //Doc
            ContentType docParentContentType = (from c in collection
                                                 where c.Name == "Document"
                                                select c).FirstOrDefault();

            ContentTypeCreationInformation docContentType = new ContentTypeCreationInformation
            {
                Name = "IT Documents",
                Group = "IT Content Type",
                ParentContentType = docParentContentType
            };

            collection.Add(empContentType);
            collection.Add(projContentType);
            collection.Add(docContentType);

            _context.ExecuteQuery();
        }

        public void AddFieldToContentType()
        {
            // Get all the content types from current site
            ContentTypeCollection collection = _context.Site.RootWeb.ContentTypes;
            _context.Load(collection);
            _context.ExecuteQuery();

            // Emp
            ContentType empContentType = (from c in collection
                                          where c.Name == "IT Employees"
                                          select c).FirstOrDefault();

            foreach (var item in empFields)
            {
                Field targetField = _context.Web.AvailableFields.GetByInternalNameOrTitle(item);
                FieldLinkCreationInformation fldLink = new FieldLinkCreationInformation();
                fldLink.Field = targetField;
                fldLink.Field.Required = false;
                fldLink.Field.Hidden = false;

                empContentType.FieldLinks.Add(fldLink);
                empContentType.Update(false);
            }

            // Project
            ContentType projContentType = (from c in collection
                              where c.Name == "IT Projects"
                              select c).FirstOrDefault();

            foreach (var item in projectFields)
            {
                Field targetField = _context.Web.AvailableFields.GetByInternalNameOrTitle(item);
                FieldLinkCreationInformation fldLink = new FieldLinkCreationInformation();
                fldLink.Field = targetField;
                fldLink.Field.Required = false;
                fldLink.Field.Hidden = false;

                projContentType.FieldLinks.Add(fldLink);
                projContentType.Update(false);
            }

            // Doc
            ContentType docContentType = (from c in collection
                              where c.Name == "IT Documents"
                              select c).FirstOrDefault();

            foreach (var item in docFields)
            {
                Field targetField = _context.Web.AvailableFields.GetByInternalNameOrTitle(item);
                FieldLinkCreationInformation fldLink = new FieldLinkCreationInformation();
                fldLink.Field = targetField;
                fldLink.Field.Required = false;
                fldLink.Field.Hidden = false;

                docContentType.FieldLinks.Add(fldLink);
                docContentType.Update(false);
            }
        }
    }
}
