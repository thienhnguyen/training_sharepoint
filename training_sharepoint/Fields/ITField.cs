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
        string[] ITFields = new string[]
        {
            // Employee
            @"<Field ID='" + Guid.NewGuid() + "' Type='Text' Name='ITEmpFirstName' DisplayName='First Name' Group='IT Field' />",

            @"<Field ID='" + Guid.NewGuid() + "' Type='Text' Name='ITEmpLastName' DisplayName='Last Name' Group='IT Field' />",

            @"<Field ID='" + Guid.NewGuid() + "' Type='Text' Name='ITEmpEmail' DisplayName='Email Address' Group='IT Field' />",

            @"<Field ID='" + Guid.NewGuid() + "' Type='HTML' RichText='TRUE' RichTextMode='FullHTML' Name='ITEmpShortDescription' DisplayName='Short Description' Group='IT Field' />",

            @"<Field ID='" + Guid.NewGuid() + "' Type='MultiChoice' Name='ITEmpProgrammingLanguages' DisplayName='Programming Languages' Group='IT Field'>" +
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
        //};
        //string[] ITProjectFields = new string[]
        //{
            //Project
            @"<Field ID='" + Guid.NewGuid() + "' Type='Text' Name='ITProjectName' DisplayName='Name of Project' Group='IT Field' />",

            @"<Field ID='" + Guid.NewGuid() + "' Type='DateTime' Name='ITProjectStartDate' DisplayName='Start Date' Group='IT Field'><Default>[Today]</Default></Field>",

            @"<Field ID='" + Guid.NewGuid() + "' Type='DateTime' Name='ITProjectEndDate' DisplayName='End Date' Group='IT Field'><Default>[Today]</Default></Field>",

            @"<Field ID='" + Guid.NewGuid() + "' Type='Text' Name='ITProjectDescription' DisplayName='Description' Group='IT Field' />",

            @"<Field ID='" + Guid.NewGuid() + "' Type='Choice' Name='ITProjectState' DisplayName='State' Format='Dropdown' Group='IT Field'>" +
            "<CHOICES>" +
                "<CHOICE>Signed</CHOICE>" +
                "<CHOICE>Design</CHOICE>" +
                "<CHOICE>Development</CHOICE>" +
                "<CHOICE>Maintenance</CHOICE>" +
                "<CHOICE>Closed</CHOICE>" +
            "</CHOICES>" +
            "</Field>",
        //};
        //string[] ITDocFields = new string[]
        //{
            //Document
            @"<Field ID='" + Guid.NewGuid() + "' Type='Text' Name='ITDocTitle' DisplayName='Title' Group='IT Field' />",

            @"<Field ID='" + Guid.NewGuid() + "' Type='Text' Name='ITDocDescription' DisplayName='Description' Group='IT Field' />",

            @"<Field ID='" + Guid.NewGuid() + "' Type='Text' Name='ITDocLinkedProjectItem' DisplayName='Linked project item' Group='IT Field' />",

            @"<Field ID='" + Guid.NewGuid() + "' Type='Choice' Name='ITDocTypeOfDoc' DisplayName='Type of Document' Format='Dropdown' Group='IT Field'>" +
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

        public void CreateField()
        {
            Web rootWeb = _context.Site.RootWeb;
            foreach (var item in ITFields)
            {
                rootWeb.Fields.AddFieldAsXml(item, true, AddFieldOptions.AddToDefaultContentType);
            }
        }
    }
}
