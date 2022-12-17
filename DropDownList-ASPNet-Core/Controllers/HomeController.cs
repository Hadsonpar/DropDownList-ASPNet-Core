using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using DropDownList_ASPNet_Core.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Configuration;
using System.Data.SqlClient;

namespace DropDownList_ASPNet_Core.Controllers
{
    public class HomeController : Controller
    {        
        private IConfiguration Configuration;

        public HomeController(IConfiguration _configuration)
        {
            Configuration = _configuration;
        }
                
        public IActionResult Index()
        {
            ViewBag.Departments = GetDepartmentDetails();
            return View();
        }
       
        private List<ClsNivelSeguridad> GetDepartmentDetails()
        {   
            string connectionString = Configuration["ConnectionStrings:DefaultConnection"];

            List<ClsNivelSeguridad> listClsNivelSeguridad = new List<ClsNivelSeguridad>();
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                string query = "SELECT * FROM NivelSeguridad";
                using (SqlCommand cmd = new SqlCommand(query))
                {
                    cmd.Connection = con;
                    con.Open();
                    using (SqlDataReader dataReader = cmd.ExecuteReader())
                    {
                        while (dataReader.Read())
                        {
                            ClsNivelSeguridad clsNivelSeguridad = new ClsNivelSeguridad();

                            clsNivelSeguridad.Id = Convert.ToInt32(dataReader["ID"]);
                            clsNivelSeguridad.Nivel = dataReader["NIVEL"].ToString();
                            listClsNivelSeguridad.Add(clsNivelSeguridad);
                        }
                    }
                    con.Close();
                }
            }
            return listClsNivelSeguridad;
        }

        public IActionResult Acercade()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
