using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using Microsoft.Data.SqlClient;

namespace WebApplication1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ToDoAppController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        public ToDoAppController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        [HttpGet]
        [Route("GetNotes")]
        public JsonResult getNotes()
        {
            string query = "select * from dbo.Notes";
            DataTable table = new DataTable();
            string? conStr = _configuration.GetConnectionString("ToDoAppCon");
            SqlDataReader dr;
            using (SqlConnection con=new SqlConnection(conStr))
            {
                con.Open();
                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    dr = cmd.ExecuteReader();
                    table.Load(dr);
                    dr.Close();
                    con.Close();
                }
            }
            return new JsonResult(table);
        }
        [HttpPost]
        [Route("AddNotes")]
        public JsonResult addNotes([FromForm] string newNotes)
        {
            string query = "insert into dbo.Notes values(@newNotes)";
            DataTable table = new DataTable();
            string? conStr = _configuration.GetConnectionString("ToDoAppCon");
            SqlDataReader dr;
            using (SqlConnection con = new SqlConnection(conStr))
            {
                con.Open();
                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    cmd.Parameters.AddWithValue("@newNotes", newNotes);
                    dr = cmd.ExecuteReader();
                    table.Load(dr);
                    dr.Close();
                    con.Close();
                }
            }
            return new JsonResult("Added Successfully");
        }

        [HttpDelete]
        [Route("DeleteNotes")]
        public JsonResult deleteNotes(int id)
        {
            string query = "delete from dbo.Notes where id=@id";
            DataTable table = new DataTable();
            string? conStr = _configuration.GetConnectionString("ToDoAppCon");
            SqlDataReader dr;
            using (SqlConnection con = new SqlConnection(conStr))
            {
                con.Open();
                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    cmd.Parameters.AddWithValue("@id", id);
                    dr = cmd.ExecuteReader();
                    dr.Close();
                    con.Close();
                }
            }
            return new JsonResult("Deleted Successfully");
        }
    }
}
