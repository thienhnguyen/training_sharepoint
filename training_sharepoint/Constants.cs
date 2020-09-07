using System;
using System.Collections.Generic;
using System.Linq;
using System.Security;
using System.Text;
using System.Threading.Tasks;

namespace training_sharepoint
{
	public static class Constants
	{
        public static readonly string SITE_URL = "https://thienhnguyen.sharepoint.com/sites/Home/Hr";
        public static readonly string USERNAME = "thienhnguyen@thienhnguyen.onmicrosoft.com";
        public static readonly string PASSWORD = "{S$t5rN$";

        public static readonly string DOC_LIB_NAME = "SharePoint API Document Library";
        public static readonly string LI_NAME = "SharePoint API List";

        public static readonly string DOC_LIB_CONT_NAME = "Project Documents Library";
        public static readonly string LI_CONT_NAME = "Employees";

        public static SecureString SECURE_PASSWORD = new SecureString();

        static public SecureString SecurePasswordString()
        {
            PASSWORD.ToCharArray().ToList().ForEach(c => SECURE_PASSWORD.AppendChar(c));
            return SECURE_PASSWORD;
        }
    }
}
