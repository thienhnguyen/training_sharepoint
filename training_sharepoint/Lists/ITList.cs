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

        public void CreateListAndLib()
        {
            ContentTypeCollection contentTypeCollection = _context.Site.RootWeb.ContentTypes;
            _context.Load(contentTypeCollection);
            _context.ExecuteQuery();

            //Emp
            //Create list
            ListCreationInformation emplci = new ListCreationInformation
            {
                Title = "IT Employees List",
                TemplateType = 100,
            };
            List empList = _context.Web.Lists.Add(emplci);
            empList.ContentTypesEnabled = true;
            empList.Update();

            //Add Content type to list
            ContentType empContentType = (from c in contentTypeCollection
                                          where c.Name == "IT Employees"
                                          select c).FirstOrDefault();

            List empTargetList = _context.Web.Lists.GetByTitle("IT Employees List");
            empTargetList.ContentTypes.AddExistingContentType(empContentType);
            empTargetList.Update();


            //Projects
            ListCreationInformation projlci = new ListCreationInformation
            {
                Title = "IT Projects List",
                TemplateType = 100,
            };
            List projList = _context.Web.Lists.Add(projlci);
            projList.ContentTypesEnabled = true;
            projList.Update();

            //Add Content type to list
            ContentType projContentType = (from c in contentTypeCollection
                                          where c.Name == "IT Projects"
                                          select c).FirstOrDefault();

            List projTargetList = _context.Web.Lists.GetByTitle("IT Projects List");
            projTargetList.ContentTypes.AddExistingContentType(projContentType);
            projTargetList.Update();

            //Doc
            ListCreationInformation doclci = new ListCreationInformation
            {
                Title = "IT Documents List",
                TemplateType = 101,
            };
            List docList = _context.Web.Lists.Add(doclci);
            docList.ContentTypesEnabled = true;
            docList.Update();

            //Add Content type to list
            ContentType docContentType = (from c in contentTypeCollection
                                           where c.Name == "IT Documents"
                                           select c).FirstOrDefault();

            List docTargetList = _context.Web.Lists.GetByTitle("IT Documents List");
            docTargetList.ContentTypes.AddExistingContentType(docContentType);
            docTargetList.Update();

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
