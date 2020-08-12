using System;
using System.Collections.Generic;
using System.Text;

namespace GraphQLCore.Business.Entities.DataModels
{
    public class EmployeeDataModel
    {
        public int EmployeeId { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Email { get; set; }

        public DateTime DateOfBirth { get; set; }
    }
}
