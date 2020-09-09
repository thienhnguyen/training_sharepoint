using Microsoft.SharePoint.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace training_sharepoint.Fields
{
    class ITField
    {
        private ClientContext _context { get; set; }
        string[] ITEmployeeFields = new string[]
        {
            // Employee
            @"<Field ID='" + Guid.NewGuid() + "' Type='Text' Name='ITEmpFirstName' DisplayName='First Name' StaticName='ITEmpFirstName' Group='IT Field' />",

            @"<Field ID='" + Guid.NewGuid() + "' Type='Text' Name='ITEmpLastName' DisplayName='Last Name' StaticName='ITEmpLastName' Group='IT Field' />",

            @"<Field ID='" + Guid.NewGuid() + "' Type='Text' Name='ITEmpEmail' DisplayName='Email Address' StaticName='ITEmpEmail'  Group='IT Field' />",

            @"<Field ID='" + Guid.NewGuid() + "' Type='HTML' RichText='TRUE' RichTextMode='FullHTML' StaticName='ITEmpShortDescription' Name='ITEmpShortDescription' DisplayName='Short Description' Group='IT Field' />",

            @"<Field ID='" + Guid.NewGuid() + "' Type='MultiChoice' Name='ITEmpProgrammingLanguages' DisplayName='Programming Languages' StaticName='ITEmpProgrammingLanguages' Group='IT Field'>" +
            "<CHOICES>" +
                "<CHOICE>C#</CHOICE>" +
                "<CHOICE>F#</CHOICE>" +
                "<CHOICE>Visual Basic</CHOICE>" +
                "<CHOICE>Java</CHOICE>" +
                "<CHOICE>Jquery</CHOICE>" +
                "<CHOICE>AngularJS</CHOICE>" +
                "<CHOICE>VueJS</CHOICE>" +
                "<CHOICE>Other</CHOICE>" +
            "</CHOICES>" +
            "</Field>",
        };
        string[] ITProjectFields = new string[]
        {
            //Project
            @"<Field ID='" + Guid.NewGuid() + "' Type='Text' Name='ITProjectName' DisplayName='Name of Project' StaticName='ITProjectName' Group='IT Field' />",

            @"<Field ID='" + Guid.NewGuid() + "' Type='DateTime' Name='ITProjectStartDate' DisplayName='Start Date' StaticName='ITProjectStartDate' Group='IT Field'><Default>[Today]</Default></Field>",

            @"<Field ID='" + Guid.NewGuid() + "' Type='DateTime' Name='ITProjectEndDate' DisplayName='End Date' StaticName='ITProjectEndDate' Group='IT Field'><Default>[Today]</Default></Field>",

            @"<Field ID='" + Guid.NewGuid() + "' Type='Text' Name='ITProjectDescription' DisplayName='Description' StaticName='ITProjectDescription' Group='IT Field' />",

            @"<Field ID='" + Guid.NewGuid() + "' Type='Choice' Name='ITProjectState' DisplayName='State' StaticName='ITProjectState' Format='Dropdown' Group='IT Field'>" +
            "<CHOICES>" +
                "<CHOICE>Signed</CHOICE>" +
                "<CHOICE>Design</CHOICE>" +
                "<CHOICE>Development</CHOICE>" +
                "<CHOICE>Maintenance</CHOICE>" +
                "<CHOICE>Closed</CHOICE>" +
            "</CHOICES>" +
            "</Field>",
        };
        string[] ITDocFields = new string[]
        {
            //Document
            @"<Field ID='" + Guid.NewGuid() + "' Type='Text' Name='ITDocTitle' DisplayName='Title' StaticName='ITDocTitle' Group='IT Field' />",

            @"<Field ID='" + Guid.NewGuid() + "' Type='Text' Name='ITDocDescription' DisplayName='Description' StaticName='ITDocDescription' Group='IT Field' />",

            @"<Field ID='" + Guid.NewGuid() + "' Type='Text' Name='ITDocLinkedProjectItem' DisplayName='Linked project item' StaticName='ITDocLinkedProjectItem' Group='IT Field' />",

            @"<Field ID='" + Guid.NewGuid() + "' Type='Choice' Name='ITDocTypeOfDoc' DisplayName='Type of Document' Format='Dropdown' StaticName='ITDocTypeOfDoc' Group='IT Field'>" +
            "<CHOICES>" +
                "<CHOICE>Business requirement</CHOICE>" +
                "<CHOICE>Technical document</CHOICE>" +
                "<CHOICE>User guide</CHOICE>" +
            "</CHOICES>" +
            "</Field>",
        };

        public ITField(ClientContext context)
        {
            _context = context;
        }

        public void CreateField(string field)
        {
            Web rootWeb = _context.Site.RootWeb;
            switch (field)
            {
                case "emp":
                    foreach (var item in ITEmployeeFields)
                    {
                        rootWeb.Fields.AddFieldAsXml(item, true, AddFieldOptions.AddToDefaultContentType);
                    }
                    break;
                case "proj":
                    foreach (var item in ITProjectFields)
                    {
                        rootWeb.Fields.AddFieldAsXml(item, true, AddFieldOptions.AddToDefaultContentType);
                    }
                    break;
                case "doc":
                    foreach (var item in ITDocFields)
                    {
                        rootWeb.Fields.AddFieldAsXml(item, true, AddFieldOptions.AddToDefaultContentType);
                    }
                    break;
            }
        }

        public void CreateLookupField()
        {
            List empList = _context.Web.Lists.GetByTitle("IT Employees List");
            List projList = _context.Web.Lists.GetByTitle("IT Projects List");
            Web rootWeb = _context.Site.RootWeb;

            _context.Load(empList, e => e.Id);
            _context.ExecuteQuery();

            string leaderField = @"<Field ID='" + Guid.NewGuid() + "' Type='LookupMulti' Name='ITProjectLeader' DisplayName='Leader' StaticName='ITProjectLeader' List='" + empList.Id + "' ShowField='ITEmpFirstName' Mult='TRUE' Group='IT Field' />";

            string membersField = @"<Field ID='" + Guid.NewGuid() + "' Type='LookupMulti' Name='ITProjectMembers' DisplayName='Members' StaticName='ITProjectMembers' List='" + empList.Id + "' ShowField='ITEmpFirstName' Mult='TRUE' Group='IT Field' />";

            //Add to content type
            rootWeb.Fields.AddFieldAsXml(leaderField, true, AddFieldOptions.DefaultValue);
            rootWeb.Fields.AddFieldAsXml(membersField, true, AddFieldOptions.DefaultValue);

            //Add to list
            projList.Fields.AddFieldAsXml(leaderField, true, AddFieldOptions.DefaultValue);
            projList.Fields.AddFieldAsXml(membersField, true, AddFieldOptions.DefaultValue);

            //_context.Load(leaderLookup);
            _context.ExecuteQuery();
        }
    }
}
