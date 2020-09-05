using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using training_sharepoint;
using Microsoft.SharePoint.Client;

namespace training_sharepoint.Lists
{
	public class DocumentLibrary
	{
        private ClientContext _context { get; set; }

        public DocumentLibrary(ClientContext context)
        {
            _context = context;
        }
        public void CreateDocumentLibrary()
		{
            //Create Document Lib
            ListCreationInformation lci = new ListCreationInformation
            {
                Title = Constants.DOC_LIB_NAME,
                TemplateType = 101,
            };

            List lib = _context.Web.Lists.Add(lci);
            lib.ContentTypesEnabled = true;

            //Add content type to doc lib
            ContentTypeCollection contentTypeCollection;

            contentTypeCollection = _context.Site.RootWeb.ContentTypes;

            _context.Load(contentTypeCollection);
            _context.ExecuteQuery();

            ContentType docLibContentType = (from c in contentTypeCollection
                                             where c.Name == Constants.DOC_LIB_CONT_NAME
                                             select c).FirstOrDefault();

            List targetDocLib = _context.Web.Lists.GetByTitle(Constants.DOC_LIB_NAME);
            targetDocLib.ContentTypes.AddExistingContentType(docLibContentType);
            targetDocLib.Update();

            _context.Web.Update();
        }
	}
}
