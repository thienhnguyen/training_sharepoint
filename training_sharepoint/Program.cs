﻿using Microsoft.SharePoint.Client;
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
            li.CreateList();
            //li.GetData();
            //li.AddData();
            //li.EditData(1);
            //li.DeleteData(1);

            #region training
            //string schemaTextField = @"<Field Type='Text' Name='TestTest' StaticName='TestTest' DisplayName='TestTest' />";
            //Web rootWeb = context.Site.RootWeb;
            //rootWeb.Fields.AddFieldAsXml(schemaTextField, true, AddFieldOptions.AddToDefaultContentType);

            //Field session = rootWeb.Fields.GetByInternalNameOrTitle("TestTest");
            //ContentType sessionContentType = rootWeb.ContentTypes.GetById("0x0106001B7740E400FDDE4F9AFC3CB95139FA75");
            //sessionContentType.FieldLinks.Add(new FieldLinkCreationInformation
            //{
            //    Field = session,
            //});
            //sessionContentType.Update(true);
            #endregion

            context.ExecuteQuery();

            Console.WriteLine("Success");

            Console.ReadLine();
        }
    }
}
