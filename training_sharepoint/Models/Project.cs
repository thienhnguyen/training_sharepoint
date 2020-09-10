using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace training_sharepoint.Models
{
    class Project
    {
        public string ProjectName { get; set; }

        public string[] Leader { get; set; }

        public string[] Members { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }

        public string Description{ get; set; }

        public string State { get; set; }
    }
}
