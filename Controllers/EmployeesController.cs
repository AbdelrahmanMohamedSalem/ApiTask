using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using ApiDay1.Models;

namespace ApiDay1.Controllers
{
    public class EmployeesController : ApiController
    {
        MyDataBaseEntities db = new MyDataBaseEntities();

        // Get All Employees

        //public List<Employee> GetAllEmployees()
        //{
        //    return db.Employees.ToList();
        //}       

        public IHttpActionResult GetAllEmployees()
        {
            var emps = db.Employees.Select(x => new EmployeeDTO()
            {
                EmpID = x.EmpID,
                EmpName = x.EmpName,
                DeptName = x.Department.DeptName
            }).ToList();

            return Ok(emps);
        }

        // Get Employee By Id

        public IHttpActionResult GetEmployeeById(int id)
        {
            EmployeeDTO emp = db.Employees.Select(x => new EmployeeDTO()
            {
                EmpID = x.EmpID,
                EmpName = x.EmpName,
                DeptName = x.Department.DeptName
            }).FirstOrDefault(x => x.EmpID == id);
            if (emp == null)
            {
                return NotFound();
            }
            return Ok(emp);
        }

        // Add New Employee

        public IHttpActionResult PostEmployee(Employee employee)
        {
            if (employee == null)
            {
                return BadRequest();
            }
            else
            {
                db.Employees.Add(employee);
                db.SaveChanges();
                return Created("Employee Class", employee);
            }
        }

        // Edit Employee
        
        public IHttpActionResult PutEmployee(Employee employee)
        {
            db.Entry(employee).State = EntityState.Modified;
            return Ok(employee);
        }

        // Delete Employee

        public IHttpActionResult DeleteEmployee(int id)
        {
            Employee emp = db.Employees.Find(id);
            if (emp == null)
            {
                return BadRequest();
            }
            else
            {
                db.Employees.Remove(emp);
                db.SaveChanges();
                return Ok(db.Employees.ToList());
            }
        }
    }
}
