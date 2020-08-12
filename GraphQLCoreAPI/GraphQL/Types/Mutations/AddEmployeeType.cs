using GraphQL.Types;
using GraphQLCore.Business.Entities.DataModels.Mutations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GraphQLCoreAPI.GraphQL.Types.Mutations
{
    public class AddEmployeeType : InputObjectGraphType<AddEmployeeDataModel>
    {
        public AddEmployeeType()
        {
            Field(x => x.EmployeeId, type: typeof(IntGraphType)).Description("The id of the employee.");
            Field(x => x.FirstName, type: typeof(StringGraphType)).Description("The first name of the employee.");
            Field(x => x.LastName, type: typeof(StringGraphType)).Description("The last name of the employee.");
            Field(x => x.DateOfBirth, type: typeof(DateGraphType)).Description("The date of birth of the employee.");
            Field(x => x.PhoneNumber, type: typeof(StringGraphType)).Description("The phone number of the employee.");
            Field(x => x.Email, type: typeof(StringGraphType)).Description("The email of the employee.");
        }
    }
}
