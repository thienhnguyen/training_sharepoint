using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace training_sharepoint.Models
{
	public class MockData
	{
		public Employee[] EmpMockData()
		{
			return new Employee[] {
				new Employee { FirstName = "test1", LastName = "test1", Email = "test1@gmail.com", PhoneNumber = "123456778" },
				new Employee { FirstName = "test2", LastName = "test2", Email = "test2@gmail.com", PhoneNumber = "145283957" },
				new Employee { FirstName = "test3", LastName = "test3", Email = "test3@gmail.com", PhoneNumber = "918789455" }
			};
		}
	}
}
