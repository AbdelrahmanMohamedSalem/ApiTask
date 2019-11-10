using ApiDay1.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace ApiDay1.Controllers
{
    public class DepartmentsController : ApiController
    {
        MyDataBaseEntities db = new MyDataBaseEntities();

        // Get All Departments

        //public List<Department> GetAllDepartments()
        //{
        //    return db.Departments.ToList();
        //}

        public IHttpActionResult GetAllDepartments()
        {
            var Depts = db.Departments.Select(x => new DepartmentDTO()
            {
                DeptID = x.DeptID,
                DeptName = x.DeptName
            }).ToList();
            return Ok(Depts);
        }

        // Get Department By Id

        public IHttpActionResult GetDepartmentById(int id)
        {
            DepartmentDTO dept = db.Departments.Select(x => new DepartmentDTO()
            {
                DeptID = x.DeptID,
                DeptName = x.DeptName
            }).FirstOrDefault(x => x.DeptID == id);
            if (dept == null)
            {
                return NotFound();
            }
            return Ok(dept);
        }

        // Add New Department

        public IHttpActionResult PostDepartment(Department department)
        {
            if (department == null)
            {
                return BadRequest();
            }
            else
            {
                db.Departments.Add(department);
                db.SaveChanges();
                return Created("Employee Class", department);
            }
        }

        // Edit Department

        public IHttpActionResult PutDepartment(Department department)
        {
            db.Entry(department).State = EntityState.Modified;
            return Ok(department);
        }

        // Delete Department

        public IHttpActionResult DeleteDepartment(int id)
        {
            Department dept = db.Departments.Find(id);
            if (dept == null)
            {
                return BadRequest();
            }
            else
            {
                db.Departments.Remove(dept);
                db.SaveChanges();
                return Ok(db.Departments.ToList());
            }
        }
    }
}

