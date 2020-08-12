using System;
using System.Collections.Generic;
using System.Text;

namespace GraphQLCore.Business.Entities.Filters
{
    public class EmployeeModelFilter
    {
        public int EmployeeId { get; set; }

        public string FirstName { get; set; }

        public string Email { get; set; }
    }
}
