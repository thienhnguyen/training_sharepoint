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


namespace training_sharepoint
{
    class Program
    {
        static void Main(string[] args)
        {
            //Authentication
            ClientContext context = Authentication.GetAuthentication();

            //Document Library
            //var doli = new DocumentLibrary(context);
            //doli.CreateDocumentLibrary();


            //List
            var li = new EmpList(context);
            //li.CreateList();
            li.AddData();
            //li.EditData(1);
            //li.DeleteData(4);

            context.ExecuteQuery();

            Console.WriteLine("Success");
        }
    }
}
