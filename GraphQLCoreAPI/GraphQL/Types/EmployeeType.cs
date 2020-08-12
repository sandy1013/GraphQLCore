using GraphQL.Types;
using GraphQLCore.Business.Entities.DataModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;

namespace GraphQLCoreAPI.GraphQL.Types
{
    public class EmployeeType : ObjectGraphType<EmployeeDataModel>
    {
        public EmployeeType()
        {
            Field(x => x.EmployeeId, type: typeof(IntGraphType)).Description("The Id of the employee.");
            Field(x => x.FirstName, type: typeof(StringGraphType)).Description("The first name of the employee.");
            Field(x => x.LastName, type: typeof(StringGraphType)).Description("The last name of the employee.");
            Field(x => x.DateOfBirth, type: typeof(DateGraphType)).Description("The date of birth of the employee.");
            Field(x => x.Email, type: typeof(StringGraphType)).Description("The email of the employee.");
        }
    }
}
