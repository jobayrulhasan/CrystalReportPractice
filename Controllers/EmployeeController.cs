using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CrystalDecisions.CrystalReports.Engine;
using CrystalReportPractice.Controllers;
using CrystalReportPractice.Models;

namespace CrystalReportPractice.Controllers
{
    public class EmployeeController : Controller
    {
        private MyvivaEntities db = new MyvivaEntities();
        // GET: Employee
        public ActionResult Index()
        {
            return View(db.Employees.ToList());
        }

        public ActionResult Employee()
        {
            List<Employee> allCustomer = new List<Employee>();
            allCustomer = db.Employees.ToList();


            ReportDocument rd = new ReportDocument();
            rd.Load(Path.Combine(Server.MapPath("~/Report"), "Employee.rpt"));

            //rd.SetDataSource(ListToDataTable);

            Response.Buffer = false;
            Response.ClearContent();
            Response.ClearHeaders();


            Stream stream = rd.ExportToStream(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat);
            stream.Seek(0, SeekOrigin.Begin);
            return File(stream, "application/pdf", "EmployeeList.pdf");
        }

    }
}  
   