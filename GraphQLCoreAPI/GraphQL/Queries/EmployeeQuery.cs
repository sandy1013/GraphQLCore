using EntityFrameworkCore.DbContextScope;
using GraphQL;
using GraphQL.Types;
using GraphQLCore.Business.Entities.DataModels;
using GraphQLCore.Business.Entities.Filters;
using GraphQLCore.Data.Access.Data;
using GraphQLCore.Data.Access.IRepository;
using GraphQLCoreAPI.GraphQL.Types;
using GraphQLCoreAPI.GraphQL.Types.Filters;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using GraphQLCoreAPI.Authentication;

namespace GraphQLCoreAPI.GraphQL.Queries
{
    public class EmployeeQuery : ObjectGraphType
    {
        private AuthenticationUtility _authUtility;

        public EmployeeQuery(IEmployeeRepository employeeRepository, IDbContextScopeFactory dbContextScope)
        {
            _authUtility = new AuthenticationUtility();

            FieldAsync<ListGraphType<EmployeeType>>(Name = "employees", resolve: async context =>
            {
                if (!_authUtility.ValidateContext(context))
                {
                    throw new ExecutionError("ErrorCode: UNAUTHORIZED_USER, Message: 401 Unautherized error.");
                }

                using (dbContextScope.CreateReadOnly())
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
                if (!_authUtility.ValidateContext(context))
                {
                    throw new ExecutionError("ErrorCode: UNAUTHORIZED_USER, Message: 401 Unautherized error.");
                }

                EmployeeModelFilter employeeFilter = context.GetArgument<EmployeeModelFilter>("employeeFilter");
                using (dbContextScope.CreateReadOnly())
                {
                    return await employeeRepository.GetEmployee(employeeFilter.EmployeeId);
                }
            });
        }
    }
}
