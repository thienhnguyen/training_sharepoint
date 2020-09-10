using Microsoft.SharePoint.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using training_sharepoint.Models;

namespace training_sharepoint.Lists
{
	public class ITList
	{
        private ClientContext _context { get; set; }

        public ITList(ClientContext context)
        {
            _context = context;
        }

        public void CreateListAndLib(string listName, int templateType, string contentTypeName)
        {
            ContentTypeCollection contentTypeCollection = _context.Site.RootWeb.ContentTypes;
            _context.Load(contentTypeCollection);
            _context.ExecuteQuery();

            //Create list
            ListCreationInformation lci = new ListCreationInformation
            {
                Title = listName,
                TemplateType = templateType,
            };
            List itList = _context.Web.Lists.Add(lci);
            itList.ContentTypesEnabled = true;
            itList.Update();

            //Add Content type to list
            ContentType contentType = (from c in contentTypeCollection
                                       where c.Name == contentTypeName
                                       select c).FirstOrDefault();

            List targetList = _context.Web.Lists.GetByTitle(listName);
            targetList.ContentTypes.AddExistingContentType(contentType);
            targetList.Update();

            _context.Web.Update();
        }

        //public void GetData()
        //{
        //    CamlQuery query = CamlQuery.CreateAllItemsQuery();

        //    List targetList = _context.Web.Lists.GetByTitle(Constants.LI_NAME);
        //    ListItemCollection listItem = targetList.GetItems(query);

        //    _context.Load(listItem);
        //    _context.ExecuteQuery();

        //    foreach (ListItem item in listItem)
        //    {
        //        Console.WriteLine(item["ID"].ToString());
        //    }
        //}



        //public void EditData(int id)
        //{
        //    List targetList = _context.Web.Lists.GetByTitle(Constants.LI_NAME);
        //    ListItem listItem = targetList.GetItemById(id);
        //    listItem["FirstName"] = "Hello World";
        //    listItem.Update();
        //}

        //public void DeleteData(int id)
        //{
        //    List targetList = _context.Web.Lists.GetByTitle(Constants.LI_NAME);
        //    ListItem listItem = targetList.GetItemById(id);
        //    listItem.DeleteObject();
        //}
    }
}
