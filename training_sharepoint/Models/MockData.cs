using Microsoft.ProjectServer.Client;
using Microsoft.SharePoint.Client;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel.Syndication;
using System.Text;
using System.Threading.Tasks;

namespace training_sharepoint.Models
{
    public class MockData
    {
        private ClientContext _context { get; set; }

        private readonly Employee[] empList = new Employee[]
        {
            new Employee { FirstName = "test1", LastName = "test1", EmailAddress = "test1@gmail.com",ShortDescription=" test1", ProgrammingLanguage = new string[]{ "C#", "Java" } },

            new Employee { FirstName = "test2", LastName = "test2", EmailAddress = "test2@gmail.com",ShortDescription=" test2", ProgrammingLanguage = new string[]{ "VueJS", "F#" } },

            new Employee { FirstName = "test3", LastName = "test3", EmailAddress = "test3@gmail.com", ShortDescription = "This is test3", ProgrammingLanguage = new string[]{ "Other" } }
        };

        private readonly Project[] projList = new Project[]
        {
            new Project { ProjectName = "Proj1", Leader = new string[] {"test1", "test2"}, Members = new string[] { "test1", "test2", "test3" }, Description = "Proj1Desc", StartDate = DateTime.Now, EndDate = DateTime.Now, State = "Signed" },

            new Project { ProjectName = "Proj2", Leader = new string[] {"test2"}, Members = new string[] { "test2", "test3" }, Description = "Proj2Desc", StartDate = DateTime.Now, EndDate = DateTime.Now, State = "Development" },

            new Project { ProjectName = "Proj3", Leader = new string[] {"test3", "test2"}, Members = new string[] { "test1", "test3" }, Description = "Proj3Desc", StartDate = DateTime.Now, EndDate = DateTime.Now, State = "Maintenance" },
        };

        public MockData(ClientContext context)
        {
            _context = context;
        }

        public void AddDataToEmployeesList(string empList)
        {

            foreach (var item in this.empList)
            {
                ListItemCreationInformation listCreationInformation = new ListItemCreationInformation();
                List targetList = _context.Web.Lists.GetByTitle(empList);
                ListItem listItem = targetList.AddItem(listCreationInformation);
                listItem["ITEmpFirstName"] = item.FirstName;
                listItem["ITEmpLastName"] = item.LastName;
                listItem["ITEmpEmail"] = item.EmailAddress;
                listItem["ITEmpProgrammingLanguages"] = item.ProgrammingLanguage;
                listItem["ITEmpShortDescription"] = item.ShortDescription;

                listItem.Update();
            }
        }

        public void AddDataToProjectsList(string projList)
        {
            foreach (var projItem in this.projList)
            {
                ListItemCreationInformation listCreationInformation = new ListItemCreationInformation();
                List targetList = _context.Web.Lists.GetByTitle(projList);
                ListItem listItem = targetList.AddItem(listCreationInformation);

                var lookUpValues = new ArrayList();

                foreach (var iProjectItem in projItem.Leader)
                {
                    lookUpValues.Add(GetLookupValue(iProjectItem, "ITEmpFirstName", "text", "IT Employees List"));
                }
                listItem["Leader"] = lookUpValues.ToArray();

                listItem.Update();

                foreach (var iProjectItem in projItem.Members)
                {
                    lookUpValues.Add(GetLookupValue(iProjectItem, "ITEmpFirstName", "text", "IT Employees List"));
                }
                listItem["Members"] = lookUpValues.ToArray();

                listItem["ITProjectName"] = projItem.ProjectName;
                listItem["ITProjectStartDate"] = projItem.StartDate;
                listItem["ITProjectEndDate"] = projItem.EndDate;
                listItem["ITProjectDescription"] = projItem.Description;
                listItem["ITProjectState"] = projItem.State;

                listItem.Update();
            }
        }

        public FieldLookupValue GetLookupValue(string value, string lookupFieldName, string lookupFieldType, string lookupListName)
        {
            List list = null;
            FieldLookupValue lookupValue = null;

            list = _context.Site.RootWeb.Lists.GetByTitle(lookupListName);

            if (list != null)
            {
                CamlQuery camlQueryForItem = new CamlQuery
                {
                    ViewXml = string.Format(@"
                    <View>
                        <Query>
                            <Where>
                                <Eq>
                                    <FieldRef Name='{0}'/>
                                    <Value Type='{1}'>{2}</Value>
                                </Eq>
                            </Where>
                        </Query>
                    </View>",
                    lookupFieldName, lookupFieldType, value)
                };

                ListItemCollection listItems = list.GetItems(camlQueryForItem);
                _context.Load(listItems, items => items.Include
                                                  (listItem => listItem["ID"],
                                                   listItem => listItem[lookupFieldName]));
                _context.ExecuteQuery();

                if (listItems != null)
                {
                    ListItem item = listItems[0];
                    lookupValue = new FieldLookupValue
                    {
                        LookupId = int.Parse(item["ID"].ToString()),
                    };
                }
            }

            return lookupValue;
        }
    }
}
