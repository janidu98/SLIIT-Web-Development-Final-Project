using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using StudentManager.Pages.Students;
using System.Data.SqlClient;

namespace StudentManager.Pages.Courses
{
    public class CreateModel : PageModel
    {
        public CourseInfo courseInfo = new CourseInfo();
        public string errorMessage = "";
        public string successMessage = "";
        

        public void OnGet()
        {
            
        }

        public void OnPost() 
        {
            courseInfo.id = Request.Form["id"];
            courseInfo.courseName = Request.Form["coursename"];
            courseInfo.lectureName = Request.Form["lecturename"];

            if(courseInfo.id.Length == 0 || courseInfo.courseName.Length == 0 || courseInfo.lectureName.Length == 0)
            {
                errorMessage = "All fields are required!";
                return;
            }

            try
            {
                string connectionString = "Data Source=DESKTOP-HTENID4;Initial Catalog=StudentManagement;Integrated Security=True";
                using(SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    string sql = "INSERT INTO courses VALUES (@courseId, @name, @lectureName);";

                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        command.Parameters.AddWithValue("@courseId", courseInfo.id);
                        command.Parameters.AddWithValue("@name", courseInfo.courseName);
                        command.Parameters.AddWithValue("@lectureName", courseInfo.lectureName);

                        command.ExecuteNonQuery();
                    }
                }
            } 
            catch(Exception ex)
            {
                errorMessage = ex.Message;
                return;
            }

            courseInfo.id = "";
            courseInfo.courseName = "";
            courseInfo.lectureName = "";

            successMessage = "New Course is Added successfully";

            Response.Redirect("/Courses/Index");
        }
    }
}
