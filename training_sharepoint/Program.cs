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
            #region Full Site
            ////Site
            //var site = new ITSite();
            //site.CreateSite();

            ////Get context
            //ClientContext context = Context.GetClientContext(Constants.SITE_URL + Constants.SITE_COLLECTION);

            //#region Sub Site
            ////Sub Site
            //site.CreateSubSite(context);

            //context = Context.GetClientContext(Constants.SITE_URL + Constants.SITE_COLLECTION + "/" + Constants.SUBSITE);
            //#endregion

            ////Field
            //var fi = new ITField(context);
            //fi.CreateField("emp");
            //fi.CreateField("proj");
            //fi.CreateField("doc");


            ////Content Type
            //var ct = new ITContentType(context);
            //ct.CreateContentType(empContentType, "Item");
            //ct.CreateContentType(projContentType, "Item");
            //ct.CreateContentType(docContentype, "Document");

            //ct.AddFieldToContentType(empContentType, empFields);
            //ct.AddFieldToContentType(projContentType, projectFields);
            //ct.AddFieldToContentType(docContentype, docFields);

            ////List
            //var li = new ITList(context);
            //li.CreateListAndLib(empList, 100, empContentType);
            //li.CreateListAndLib(projList, 100, projContentType);
            //li.CreateListAndLib(docLib, 101, docContentype);

            //fi.CreateLookupField();

            ////View
            //var view = new ITView(context);
            //view.CreateView(empList, "IT Employees View", empFields);
            //view.CreateView(projList, "IT Projects View", projectFields);
            //view.CreateView(docLib, "IT Documents View", docFields);

            //context.ExecuteQuery();
            #endregion

            #region App Start

            string opt;
            string url;
            do
            {
                Display();
                opt = Console.ReadLine();
                switch (opt)
                {
                    case "1":
                        Console.Clear();
                        Console.Write("Please input your site url: ");

                        url = Console.ReadLine();

                        Console.WriteLine("Please Wait...");

                        ClientContext context = Context.GetClientContext(url.Trim());
                        //Field
                        var fi = new ITField(context);
                        fi.CreateField("emp");

                        //Content Type
                        var ct = new ITContentType(context);
                        ct.CreateContentType(empContentType, "Item");
                        ct.AddFieldToContentType(empContentType, empFields);

                        //List
                        var li = new ITList(context);
                        li.CreateListAndLib(empList, 100, empContentType);

                        //View
                        var view = new ITView(context);
                        view.CreateView(empList, "IT Employees View", empFields);

                        context.ExecuteQuery();

                        Console.WriteLine("Finish! Happy Play :)))))");
                        System.Threading.Thread.Sleep(5000);
                        Console.Clear();

                        break;
                    case "2":
                        Console.Clear();
                        Console.Write("Please input your site url: ");

                        url = Console.ReadLine();

                        Console.WriteLine("Please Wait...");

                        context = Context.GetClientContext(url.Trim());
                        //Field
                        fi = new ITField(context);
                        fi.CreateField("proj");

                        //Content Type
                        ct = new ITContentType(context);
                        ct.CreateContentType(projContentType, "Item");
                        ct.AddFieldToContentType(projContentType, projectFields);

                        //List
                        li = new ITList(context);
                        li.CreateListAndLib(projList, 100, projContentType);
                        fi.CreateLookupField();

                        //View
                        view = new ITView(context);
                        view.CreateView(projList, "IT Projects View", projectFields);

                        context.ExecuteQuery();

                        Console.WriteLine("Finish! Happy Play :)))))");
                        System.Threading.Thread.Sleep(5000);
                        Console.Clear();
                        break;
                    case "3":
                        break;
                    case "4":
                        break;
                    case "0":
                        break;
                }
            } while (opt != "0");

            #endregion
        }

        static void Display()
        {
            Console.WriteLine("---------------------------------------------------------------------");
            Console.WriteLine("Hi there, welcome to my console app use for Sharepoint");
            Console.WriteLine("");

            Console.WriteLine("Please choose your option below and press ENTER");
            Console.WriteLine("1. Create a new Employee List to your site");
            Console.WriteLine("2. Create a new Projects List to your site");
            Console.WriteLine("3. Create a new Document Library to your site");
            Console.WriteLine("4. Create a new Site with the lists above");
            Console.WriteLine("0. Exit");

            Console.WriteLine("---------------------------------------------------------------------");
        }
    }
}
