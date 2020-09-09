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
        static public readonly string SITE_URL = "https://thienhnguyen.sharepoint.com";
        static public readonly string SITE_ADMIN_URL = "https://thienhnguyen-admin.sharepoint.com";
        static public readonly string SITE_COLLECTION = @"/sites/Thnit";
        static public readonly string SUBSITE = @"HrSubSite";

        static public readonly string USERNAME = "thienhnguyen@thienhnguyen.onmicrosoft.com";
        static public readonly string PASSWORD = @"{S$t5rN$";

        static public SecureString SecurePasswordString()
        {
            var res = new SecureString();
            PASSWORD.ToCharArray().ToList().ForEach(c => res.AppendChar(c));
            return res;
        }
    }
}
