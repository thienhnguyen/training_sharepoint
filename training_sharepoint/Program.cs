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

namespace training_sharepoint
{
    class Program
    {
        static void Main(string[] args)
        {
            //Authentication
            ClientContext context = Authentication.GetAuthentication();

            //Site
            var site = new ITSite(context);
            site.CreateSite();

            //Field
            var fi = new ITField(context);
            fi.CreateField();

            //Content Type
            var ct = new ITContentType(context);
            ct.CreateContentType();
            ct.AddFieldToContentType();


            //List
            var li = new ITList(context);
            li.CreateListAndLib();
            //li.GetData();
            //li.AddData();
            //li.EditData(1);
            //li.DeleteData(1);

            context.ExecuteQuery();

            Console.WriteLine("Success");

            Console.ReadLine();
        }
    }
}
