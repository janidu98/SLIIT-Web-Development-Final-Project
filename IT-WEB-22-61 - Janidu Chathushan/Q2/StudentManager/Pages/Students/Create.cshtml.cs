using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using StudentManager.Pages.Courses;
using System.Data.SqlClient;

namespace StudentManager.Pages.Students
{
    public class CreateModel : PageModel
    {
        public StudentInfo studentInfo = new StudentInfo();
        public List<CourseInfo> listCourses = new List<CourseInfo>();
        

        public string errorMessage = "";
        public string successMessage = "";

        public void OnGet()
        {
            string connectionString = "Data Source=DESKTOP-HTENID4;Initial Catalog=StudentManagement;Integrated Security=True";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                string sql = "SELECT * FROM courses";

                using (SqlCommand command = new SqlCommand(sql, connection))
                {
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            CourseInfo courseInfo = new CourseInfo();

                            courseInfo.id = "" + reader.GetInt32(0);
                            courseInfo.courseName = reader.GetString(1);
                            courseInfo.lectureName = reader.GetString(2);


                            listCourses.Add(courseInfo);
                        }
                    }
                }

                connection.Close();
            }
        }

        public void OnPost() 
        {
            studentInfo.id = Request.Form["id"];
            studentInfo.studentName = Request.Form["studentname"];
            studentInfo.city = Request.Form["city"];
            studentInfo.courseId = Request.Form["courseId"];

            if (studentInfo.id.Length == 0 || studentInfo.studentName.Length == 0 || studentInfo.city.Length == 0 || studentInfo.courseId.Length == 0)
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
                    string sql = "INSERT INTO students VALUES (@studentId, @name, @city, @courseId);";

                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        command.Parameters.AddWithValue("@studentId", studentInfo.id);
                        command.Parameters.AddWithValue("@name", studentInfo.studentName);
                        command.Parameters.AddWithValue("@city", studentInfo.city);
                        command.Parameters.AddWithValue("@courseId", studentInfo.courseId);

                        command.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                errorMessage = ex.Message;
                return;
            }

            studentInfo.id = "";
            studentInfo.studentName = "";
            studentInfo.city = "";
            studentInfo.courseId = "";

            successMessage = "New Student is Added successfully";

            Response.Redirect("/Students/Index");
        }
    }
}
