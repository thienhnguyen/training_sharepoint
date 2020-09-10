using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace training_sharepoint.Models
{
	public class Employee
	{
		public string FirstName { get; set; }

		public string LastName { get; set; }

		public string EmailAddress { get; set; }

		public string ShortDescription { get; set; }

		public string[] ProgrammingLanguage { get; set; }
	}
}
