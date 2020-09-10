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
using OfficeDevPnP.Core.Extensions;

namespace training_sharepoint
{
    class Program
    {
        static void Main(string[] args)
        {
            #region Variables
            var empContentType = "IT Employees";
            var projContentType = "IT Projects";
            var docContentype = "IT Documents";

            var empList = "IT Employees List";
            var projList = "IT Projects List";
            var docLib = "IT Documents Library";

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
                "ITDocTypeOfDoc"
            };
            #endregion

            #region App Start
            while (true)
            {
                try
                {
                    Console.Clear();
                    Play();
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    Console.ReadLine();
                }
            }
            #endregion

            void Display()
            {
                Console.WriteLine("---------------------------------------------------------------------");
                Console.WriteLine();
                Console.WriteLine("Hi there, welcome to my console app used for SharePoint :))))");
                Console.WriteLine();
                Console.WriteLine("Please input your option below and press ENTER:");
                Console.WriteLine("1. Create a new Employees List to your site");
                Console.WriteLine("2. Create a new Projects List to your site");
                Console.WriteLine("3. Create a new Documents Library to your site");
                Console.WriteLine("4. Create a new Site with the lists above");
                Console.WriteLine("5. Add mock data to Employees List");
                Console.WriteLine("6. Add mock data to Projects List (In-development)");
                Console.WriteLine();
                Console.WriteLine("---------------------------------------------------------------------");
            }

            void Play()
            {
                string opt;
                string url;
                do
                {
                    Display();
                    opt = Console.ReadLine().Trim();
                    Console.Clear();
                    switch (opt)
                    {
                        case "1":
                            Console.Write("Please input your site url: ");

                            url = Console.ReadLine().Trim();

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
                            Console.Write("Please input your site url: ");

                            url = Console.ReadLine().Trim();

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
                            Console.Write("Please input your site url: ");

                            url = Console.ReadLine().Trim();

                            Console.WriteLine("Please Wait...");

                            context = Context.GetClientContext(url.Trim());
                            //Field
                            fi = new ITField(context);
                            fi.CreateField("doc");

                            //Content Type
                            ct = new ITContentType(context);
                            ct.CreateContentType(docContentype, "Document");
                            ct.AddFieldToContentType(docContentype, docFields);

                            //List
                            li = new ITList(context);
                            li.CreateListAndLib(docLib, 101, docContentype);

                            //View
                            view = new ITView(context);
                            view.CreateView(docLib, "IT Documents View", docFields);

                            context.ExecuteQuery();

                            Console.WriteLine("Finish! Happy Play :)))))");
                            System.Threading.Thread.Sleep(5000);
                            Console.Clear();

                            break;
                        case "4":
                            Console.WriteLine("Default site name: IT Site create from CSOM");
                            Console.Write("or Change it if you want: ");

                            var siteName = Console.ReadLine().Trim();
                            string siteAddress;

                            Console.WriteLine("Please Wait...");

                            //Site
                            var site = new ITSite();
                            if (string.IsNullOrEmpty(siteName))
                            {
                                site.CreateSite(out siteAddress);
                            }
                            else
                            {
                                site.CreateSite(out siteAddress, siteName);
                            }

                            Console.WriteLine("Please be patient... Few more seconds :)))");

                            //Get context
                            context = Context.GetClientContext(Constants.SITE_URL + "/sites/" + siteAddress);

                            #region Sub Site
                            //Sub Site
                            site.CreateSubSite(context);

                            context = Context.GetClientContext(Constants.SITE_URL + "/sites/" + siteAddress + "/" + Constants.SUBSITE);
                            #endregion

                            //Field
                            fi = new ITField(context);
                            fi.CreateField("emp");
                            fi.CreateField("proj");
                            fi.CreateField("doc");


                            //Content Type
                            ct = new ITContentType(context);
                            ct.CreateContentType(empContentType, "Item");
                            ct.CreateContentType(projContentType, "Item");
                            ct.CreateContentType(docContentype, "Document");

                            ct.AddFieldToContentType(empContentType, empFields);
                            ct.AddFieldToContentType(projContentType, projectFields);
                            ct.AddFieldToContentType(docContentype, docFields);

                            //List
                            li = new ITList(context);
                            li.CreateListAndLib(empList, 100, empContentType);
                            li.CreateListAndLib(projList, 100, projContentType);
                            li.CreateListAndLib(docLib, 101, docContentype);

                            fi.CreateLookupField();

                            //View
                            view = new ITView(context);
                            view.CreateView(empList, "IT Employees View", empFields);
                            view.CreateView(projList, "IT Projects View", projectFields);
                            view.CreateView(docLib, "IT Documents View", docFields);

                            context.ExecuteQuery();

                            Console.WriteLine("Finish! Happy Play :)))))");
                            System.Threading.Thread.Sleep(5000);
                            Console.Clear();

                            break;

                        case "5":
                            Console.WriteLine("Please input your site url having the employees list: ");

                            url = Console.ReadLine().Trim();

                            Console.WriteLine("Please Wait...");

                            context = Context.GetClientContext(url.Trim());

                            var mockData = new MockData(context);
                            mockData.AddDataToEmployeesList(empList);
                            context.ExecuteQuery();

                            Console.WriteLine("Finish! Happy Play :)))))");
                            System.Threading.Thread.Sleep(5000);
                            Console.Clear();
                            break;

                        case "6":
                            Console.WriteLine("Please input your site url having the projects list: ");

                            url = Console.ReadLine().Trim();

                            Console.WriteLine("Please Wait...");

                            context = Context.GetClientContext(url.Trim());

                            mockData = new MockData(context);
                            mockData.AddDataToProjectsList(projList);
                            context.ExecuteQuery();

                            Console.WriteLine("Finish! Happy Play :)))))");
                            System.Threading.Thread.Sleep(5000);
                            Console.Clear();
                            break;
                    }
                } while (opt != "0");
            }
        }
    }
}
