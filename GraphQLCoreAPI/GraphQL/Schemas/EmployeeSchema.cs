using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GraphQL;
using GraphQL.Types;
using GraphQLCoreAPI.GraphQL.Mutations;
using GraphQLCoreAPI.GraphQL.Queries;

namespace GraphQLCoreAPI.GraphQL.Schemas
{
    public class EmployeeSchema: Schema
    {
        public EmployeeSchema(IDependencyResolver resolver): base(resolver)
        {
            Query = resolver.Resolve<EmployeeQuery>();
            Mutation = resolver.Resolve<EmployeeMutation>();
        }
    }
}
