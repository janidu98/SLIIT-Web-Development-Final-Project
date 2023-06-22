using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;

namespace StudentManager.Pages.Courses
{
    public class EditModel : PageModel
    {
        public CourseInfo courseInfo = new CourseInfo();
        public string errorMessage = "";
        public string successMessage = "";

        public void OnGet()
        {
            string id = Request.Query["id"];
            try
            {
                string connectionString = "Data Source=DESKTOP-HTENID4;Initial Catalog=StudentManagement;Integrated Security=True";

                using(SqlConnection  connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    string sql = "SELECT * FROM courses WHERE courseId=@id";

                    using(SqlCommand command = new SqlCommand(sql, connection))
                    {
                        command.Parameters.AddWithValue("@id", id);
                        using(SqlDataReader reader = command.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                courseInfo.id = "" + reader.GetInt32(0);
                                courseInfo.courseName = reader.GetString(1);
                                courseInfo.lectureName = reader.GetString(2);  
                            }
                        }
                    }
                }
            }
            catch(Exception ex)
            {
                Console.WriteLine("Exception : " + ex.ToString());
            }
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
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    string sql = "UPDATE courses SET name=@courseName, lectureName=@lectureName WHERE courseId=@id";

                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        command.Parameters.AddWithValue("@id", courseInfo.id);
                        command.Parameters.AddWithValue("@courseName", courseInfo.courseName);
                        command.Parameters.AddWithValue("@lectureName", courseInfo.lectureName);

                        command.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                errorMessage = ex.Message;
                return;
            }

            courseInfo.id = "";
            courseInfo.courseName = "";
            courseInfo.lectureName = "";

            successMessage = "Course is Updated successfully";

            Response.Redirect("/Courses/Index");
        
        }
    }
}
