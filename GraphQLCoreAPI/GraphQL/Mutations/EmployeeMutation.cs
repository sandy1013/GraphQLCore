using EntityFrameworkCore.DbContextScope;
using GraphQL.Types;
using GraphQLCore.Business.Entities.DataModels.Mutations;
using GraphQLCore.Data.Access.IRepository;
using GraphQLCoreAPI.GraphQL.Types;
using GraphQLCoreAPI.GraphQL.Types.Mutations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GraphQLCoreAPI.GraphQL.Mutations
{
    public class EmployeeMutation: ObjectGraphType
    {
        public EmployeeMutation(IEmployeeRepository employeeRepository, IDbContextScopeFactory dbContextScope)
        {
            FieldAsync<EmployeeType>("addEmployee",
            arguments: new QueryArguments {
                new QueryArgument<AddEmployeeType> { Name = "employeeData" }
            },
            resolve: async context =>
            {
                AddEmployeeDataModel employeeData = context.GetArgument<AddEmployeeDataModel>("employeeData");

                using (dbContextScope.Create(DbContextScopeOption.ForceCreateNew))
                {
                    return await employeeRepository.AddEmployee(employeeData);
                }
            });
        }
    }
}
