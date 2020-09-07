using Microsoft.SharePoint.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using training_sharepoint.Models;

namespace training_sharepoint.Lists
{
	public class EmpList
	{
        private ClientContext _context { get; set; }

        public EmpList(ClientContext context)
        {
            _context = context;
        }

        public void CreateList()
        {
            //Create list
            ListCreationInformation lci = new ListCreationInformation
            {
                Title = Constants.LI_NAME,
                TemplateType = 100,
            };
            List lib = _context.Web.Lists.Add(lci);
            lib.ContentTypesEnabled = true;

            lib.Update();

            //Add Content type to list
            ContentTypeCollection contentTypeCollection;

            contentTypeCollection = _context.Site.RootWeb.ContentTypes;

            _context.Load(contentTypeCollection);
            _context.ExecuteQuery();

            ContentType liContentType = (from c in contentTypeCollection
                                         where c.Name == Constants.LI_CONT_NAME
                                         select c).FirstOrDefault();

            List targetList = _context.Web.Lists.GetByTitle(Constants.LI_NAME);
            targetList.ContentTypes.AddExistingContentType(liContentType);
            targetList.Update();

            _context.Web.Update();
        }

        public void GetData()
        {
            CamlQuery query = CamlQuery.CreateAllItemsQuery();

            List targetList = _context.Web.Lists.GetByTitle(Constants.LI_NAME);
            ListItemCollection listItem = targetList.GetItems(query);

            _context.Load(listItem);
            _context.ExecuteQuery();

            foreach (ListItem item in listItem)
            {
                Console.WriteLine(item["ID"].ToString());
            }
        }

        public void AddData()
        {
            var data = new MockData();

            foreach (var item in data.EmpMockData())
            {
                ListItemCreationInformation listCreationInformation = new ListItemCreationInformation();
                List targetList = _context.Web.Lists.GetByTitle(Constants.LI_NAME);
                ListItem listItem = targetList.AddItem(listCreationInformation);
                listItem["FirstName"] = item.FirstName;
                listItem["Last_x0020_Name"] = item.LastName;
                listItem["EMail"] = item.Email;
                listItem["CellPhone"] = item.PhoneNumber;
                listItem.Update();
            }
        }

        public void EditData(int id)
        {
            List targetList = _context.Web.Lists.GetByTitle(Constants.LI_NAME);
            ListItem listItem = targetList.GetItemById(id);
            listItem["FirstName"] = "Hello World";
            listItem.Update();
        }

        public void DeleteData(int id)
        {
            List targetList = _context.Web.Lists.GetByTitle(Constants.LI_NAME);
            ListItem listItem = targetList.GetItemById(id);
            listItem.DeleteObject();
        }
    }
}
