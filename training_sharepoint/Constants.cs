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
        static public readonly string SITE_URL = "https://thienhnguyen.sharepoint.com/sites/Home/Hr";
        static public readonly string USERNAME = "thienhnguyen@thienhnguyen.onmicrosoft.com";
        static public readonly string PASSWORD = "{S$t5rN$";

        static public readonly string DOC_LIB_NAME = "SharePoint API Document Library";
        static public readonly string LI_NAME = "SharePoint API List";

        static public readonly string DOC_LIB_CONT_NAME = "Project Documents Library";
        static public readonly string LI_CONT_NAME = "Employees";

        static public SecureString SECURE_PASSWORD = new SecureString();

        static public SecureString SecurePasswordString()
        {
            PASSWORD.ToCharArray().ToList().ForEach(c => SECURE_PASSWORD.AppendChar(c));
            return SECURE_PASSWORD;
        }
    }
}
