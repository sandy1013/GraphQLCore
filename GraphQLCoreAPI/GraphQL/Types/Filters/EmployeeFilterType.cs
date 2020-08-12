using GraphQL.Instrumentation;
using GraphQL.Types;
using GraphQLCore.Business.Entities.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GraphQLCoreAPI.GraphQL.Types.Filters
{
    public class EmployeeFilterType: InputObjectGraphType<EmployeeModelFilter>
    {
        public EmployeeFilterType()
        {
            Name = "employeeFilter";
            Field<NonNullGraphType<IntGraphType>>("employeeId", description: "fetch employee by id.");
            Field<StringGraphType>("firstName", description: "fetch employee by first name.");
            Field<StringGraphType>("email", description: "fetch employee by email.");
        }
    }
}
