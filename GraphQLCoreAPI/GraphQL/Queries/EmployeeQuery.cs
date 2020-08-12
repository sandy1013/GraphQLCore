using EntityFrameworkCore.DbContextScope;
using GraphQL.Types;
using GraphQLCore.Business.Entities.DataModels;
using GraphQLCore.Business.Entities.Filters;
using GraphQLCore.Data.Access.Data;
using GraphQLCore.Data.Access.IRepository;
using GraphQLCoreAPI.GraphQL.Types;
using GraphQLCoreAPI.GraphQL.Types.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GraphQLCoreAPI.GraphQL.Queries
{
    public class EmployeeQuery : ObjectGraphType
    {
        public EmployeeQuery(IEmployeeRepository employeeRepository, IDbContextScopeFactory dbContextScope)
        {
            FieldAsync<ListGraphType<EmployeeType>>(Name = "employees", resolve: async context =>
            {
                using(dbContextScope.CreateReadOnly())
                {
                    return await employeeRepository.GetEmployees();
                }
            });

            FieldAsync<EmployeeType>(Name = "employee", 
            arguments: new QueryArguments {
                new QueryArgument<NonNullGraphType<EmployeeFilterType>> { Name = "employeeFilter" }
            },
            resolve: async context =>
            {
                EmployeeModelFilter employeeFilter = context.GetArgument<EmployeeModelFilter>("employeeFilter");
                using (dbContextScope.CreateReadOnly())
                {
                    return await employeeRepository.GetEmployee(employeeFilter.EmployeeId);
                }
            });
        }
    }
}
