using Microsoft.SharePoint.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Security;
using System.Text;
using System.Threading.Tasks;
using training_sharepoint.Models;
using training_sharepoint.Lists;
using training_sharepoint.Fields;
using training_sharepoint.Content_Types;
using training_sharepoint.Sites;
using training_sharepoint.Views;

namespace training_sharepoint
{
    class Program
    {
        static void Main(string[] args)
        {
            var empContentType = "IT Employees";
            var projContentType = "IT Projects";
            var docContentype = "IT Documents";

            var empList = "IT Employees List";
            var projList = "IT Projects List";
            var docLib = "IT Documents List";

            var empFields = new string[]
            {
                "ITEmpFirstName",
                "ITEmpLastName",
                "ITEmpEmail",
                "ITEmpShortDescription",
                "ITEmpProgrammingLanguages"
            };
            var projectFields = new string[]
            {
                "ITProjectName",
                "ITProjectLeader",
                "ITProjectMembers",
                "ITProjectStartDate",
                "ITProjectEndDate",
                "ITProjectDescription",
                "ITProjectState"
            };
            var docFields = new string[]
            {
                "ITDocTitle",
                "ITDocDescription",
                "ITDocLinkedProjectItem",
                "ITProjectDescription",
                "ITDocTypeOfDoc"
            };

            //Site
            var site = new ITSite();
            site.CreateSite();

            //Get context
            ClientContext context = Context.GetClientContext(Constants.SITE_URL + Constants.SITE_COLLECTION);

            #region Sub Site
            //Sub Site
            site.CreateSubSite(context);

            context = Context.GetClientContext(Constants.SITE_URL + Constants.SITE_COLLECTION + "/" + Constants.SUBSITE);
            #endregion

            //Field
            var fi = new ITField(context);
            fi.CreateField("emp");
            fi.CreateField("proj");
            fi.CreateField("doc");


            //Content Type
            var ct = new ITContentType(context);
            ct.CreateContentType(empContentType, "Item");
            ct.CreateContentType(projContentType, "Item");
            ct.CreateContentType(docContentype, "Document");

            ct.AddFieldToContentType(empContentType, empFields);
            ct.AddFieldToContentType(projContentType, projectFields);
            ct.AddFieldToContentType(docContentype, docFields);

            //List
            var li = new ITList(context);
            li.CreateListAndLib(empList, 100, empContentType);
            li.CreateListAndLib(projList, 100, projContentType);
            li.CreateListAndLib(docLib, 101, docContentype);

            fi.CreateLookupField();

            //View
            var view = new ITView(context);
            view.CreateView(empList, "IT Employees View", empFields);
            view.CreateView(projList, "IT Projects View", projectFields);
            view.CreateView(docLib, "IT Documents View", docFields);

            context.ExecuteQuery();

            Console.WriteLine("Success");

            Console.ReadLine();
        }
    }
}
