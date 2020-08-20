using AutoMapper;
using EntityFrameworkCore.DbContextScope;
using GraphQLCore.Business.Entities.DataModels;
using GraphQLCore.Business.Entities.DataModels.Mutations;
using GraphQLCore.Data.Access.IRepository;
using GraphQLCore.Data.Models.EntityModels;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GraphQLCore.Data.Access.Data
{
    public class EmployeeDataService : IEmployeeRepository
    {

        private readonly IAmbientDbContextLocator _ambientDbContextLocator;

        private GraphQLCoreDbContext _context
        {
            get
            {
                var databaseContext = _ambientDbContextLocator.Get<GraphQLCoreDbContext>();

                if (databaseContext == null)
                    throw new InvalidOperationException("No ambient DbContext of type EmployeePortfolioContext found. This means that this repository method has been called outside of the scope of a DbContextScope. A repository must only be accessed within the scope of a DbContextScope, which takes care of creating the DbContext instances that the repositories need and making them available as ambient contexts. This is what ensures that, for any given DbContext-derived type, the same instance is used throughout the duration of a business transaction. To fix this issue, use IDbContextScopeFactory in your top-level business logic service method to create a DbContextScope that wraps the entire business transaction that your service method implements. Then access this repository within that scope. Refer to the comments in the IDbContextScope.cs file for more details.");

                return databaseContext;
            }
        }

        public EmployeeDataService(IAmbientDbContextLocator ambientDbContextLocator)
        {
            _ambientDbContextLocator = ambientDbContextLocator;
        }

        public async Task<List<EmployeeDataModel>> GetEmployees()
        {
            List<Employee> empolyees = await _context.Employees.ToListAsync<Employee>();

            MapperConfiguration config = new MapperConfiguration(mc => mc.CreateMap<Employee, EmployeeDataModel>());
            Mapper mapper = new Mapper(config);
            List<EmployeeDataModel> resource = mapper.Map<List<Employee>, List<EmployeeDataModel>>(empolyees);

            return resource;
        }

        public async Task<EmployeeDataModel> GetEmployee(int employeeId)
        {
            Employee employee = await _context.Employees.SingleOrDefaultAsync(o => o.EmployeeId == employeeId);

            MapperConfiguration config = new MapperConfiguration(mc => mc.CreateMap<Employee, EmployeeDataModel>());
            Mapper mapper = new Mapper(config);
            EmployeeDataModel resource = mapper.Map<Employee, EmployeeDataModel>(employee);

            return resource;
        }

        public async Task<EmployeeDataModel> AddEmployee(AddEmployeeDataModel employee)
        {
            MapperConfiguration config = new MapperConfiguration(mc => mc.CreateMap<AddEmployeeDataModel, Employee>());
            Mapper mapper = new Mapper(config);
            Employee newEmployee = mapper.Map<AddEmployeeDataModel, Employee>(employee);

            var phoneNumber = newEmployee.PhoneNumber;

            var _ = await _context.Employees.AddAsync(newEmployee);

            var __ = await _context.SaveChangesAsync();
            //EmployeeDataModel

            MapperConfiguration config_1 = new MapperConfiguration(mc => mc.CreateMap<Employee, EmployeeDataModel>());
            Mapper mapper_1 = new Mapper(config_1);
            EmployeeDataModel resource = mapper_1.Map<Employee, EmployeeDataModel>(newEmployee);

            return resource;
        }
    }
}
