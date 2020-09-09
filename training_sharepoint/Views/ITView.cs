using Microsoft.SharePoint.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace training_sharepoint.Views
{
    class ITView
    {
        private ClientContext _context { get; set; }

        public ITView(ClientContext context)
        {
            _context = context;
        }

        public void CreateView(string listTitle, string viewTitle, string[] fields)
        {
            //Emp
            List targetList = _context.Web.Lists.GetByTitle(listTitle);

            ViewCollection viewCollection = targetList.Views;
            _context.Load(viewCollection);

            ViewCreationInformation viewCreationInformation = new ViewCreationInformation
            {
                Title = viewTitle,
                RowLimit = 10,
                SetAsDefaultView = true,
                ViewFields = fields
            };

            View listView = viewCollection.Add(viewCreationInformation);
            listView.Update();
        }
    }
}
