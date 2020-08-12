using GraphQLCore.Business.Entities.DataModels;
using GraphQLCore.Business.Entities.DataModels.Mutations;
using GraphQLCore.Data.Models.EntityModels;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace GraphQLCore.Data.Access.IRepository
{
    public interface IEmployeeRepository
    {
        Task<List<EmployeeDataModel>> GetEmployees();

        Task<EmployeeDataModel> GetEmployee(int employeeId);

        Task<EmployeeDataModel> AddEmployee(AddEmployeeDataModel employee);
    }
}
