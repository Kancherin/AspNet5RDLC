using AspNet5RDLC.Web.Models;
using AspNetCore.Reporting;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace AspNet5RDLC.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public HomeController(ILogger<HomeController> logger, IWebHostEnvironment webHostEnvironment)
        {
            _logger = logger;
            _webHostEnvironment = webHostEnvironment;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        public IActionResult Imprimir()
        {
            System.Text.Encoding.RegisterProvider(System.Text.CodePagesEncodingProvider.Instance);
            DataTable dt = new DataTable();
            dt.Columns.Add("Id");
            dt.Columns.Add("Name");
            dt.Columns.Add("Gender");
            dt.Columns.Add("Company");
            dt.Columns.Add("Department");

            for (int i = 1; i < 101; i++)
            {
                dt.Rows.Add(i, "Empleado " + i, "Male", "Administración Pública", "Desarrollo");
            }

            var filePath = $"{_webHostEnvironment.WebRootPath}\\Reports\\rptEmpleados.rdl";
            var local = new LocalReport(filePath);

            local.AddDataSource("DataSet1", dt);

            var rpt = local.Execute(RenderType.Pdf);
            return File(rpt.MainStream, "application/pdf");
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
