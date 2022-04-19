using MongoDB.Driver;
using ProjectCafeApi.Models;
using System.Collections.Generic;
using System.Linq;
using System;

namespace ProjectCafeApi.Services
{
    public class EmployeeService
    {
        private readonly IMongoCollection<Employee> _employee;
        public EmployeeService(DatabaseSettings settings)
        {
            var Client = new MongoClient(settings.ConnectionString);
            var database = Client.GetDatabase(settings.DatabaseName);
            _employee = database.GetCollection<Employee>(settings.EmployeeCollection);
        }

        public Employee CreateEmployee(Employee employee)
        {
             _employee.InsertOne(employee);
             return employee;
        }

        public List<Employee> GetEmployeeAll() => _employee.Find(emp => emp.Status.ToLower() != "deleted").ToList();

        public Employee GetEmployeeById(string id) => _employee.Find(emp => emp.Emp_Id == id).FirstOrDefault();

        public List<Employee> GetAllForApi() => _employee.Find(emp => true).ToList();

        public void UpdateEmployee(string id, Employee employee) => _employee.ReplaceOne(emp => emp.Emp_Id == id, employee);
    }
}