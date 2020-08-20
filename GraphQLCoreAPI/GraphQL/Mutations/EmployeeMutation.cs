using EntityFrameworkCore.DbContextScope;
using GraphQL;
using GraphQL.Types;
using GraphQLCore.Business.Entities.DataModels.Mutations;
using GraphQLCore.Data.Access.IRepository;
using GraphQLCoreAPI.Authentication;
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
        private AuthenticationUtility _authUtility;
        public EmployeeMutation(IEmployeeRepository employeeRepository, IDbContextScopeFactory dbContextScope)
        {
            _authUtility = new AuthenticationUtility();

            FieldAsync<EmployeeType>("addEmployee",
            arguments: new QueryArguments {
                new QueryArgument<NonNullGraphType<AddEmployeeType>> { Name = "employeeData" }
            },
            resolve: async context =>
            {
                if (!_authUtility.ValidateContext(context))
                {
                    throw new ExecutionError("ErrorCode: UNAUTHORIZED_USER, Message: 401 Unautherized error.");
                }

                AddEmployeeDataModel employeeData = context.GetArgument<AddEmployeeDataModel>("employeeData");

                using (dbContextScope.Create(DbContextScopeOption.ForceCreateNew))
                {
                    return await employeeRepository.AddEmployee(employeeData);
                }
            });
        }
    }
}
