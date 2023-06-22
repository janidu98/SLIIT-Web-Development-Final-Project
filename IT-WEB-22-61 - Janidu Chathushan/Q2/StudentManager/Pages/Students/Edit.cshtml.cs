using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using StudentManager.Pages.Courses;
using System.Data.SqlClient;

namespace StudentManager.Pages.Students
{
    public class EditModel : PageModel
    {
        public StudentInfo studentInfo = new StudentInfo();
        public List<CourseInfo> listCourses = new List<CourseInfo>();
        public string errorMessage = "";
        public string successMessage = "";
        
        public void OnGet()
        {
            string id = Request.Query["id"];
            try
            {
                string connectionString = "Data Source=DESKTOP-HTENID4;Initial Catalog=StudentManagement;Integrated Security=True";

                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    string sql = "SELECT * FROM students WHERE studentId=@id";
                    string sql1 = "SELECT * FROM courses";

                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        command.Parameters.AddWithValue("@id", id);
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                studentInfo.id = "" + reader.GetInt32(0);
                                studentInfo.studentName = reader.GetString(1);
                                studentInfo.city = reader.GetString(2);
                                studentInfo.courseId = "" + reader.GetInt32(3);

                            }
                        }
                    }

                    using (SqlCommand command = new SqlCommand(sql1, connection))
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
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception : " + ex.ToString());
            }
        }

        public void OnPost()
        {
            studentInfo.id = Request.Form["id"];
            studentInfo.studentName = Request.Form["studentname"];
            studentInfo.city = Request.Form["city"];
            studentInfo.courseId = Request.Form["courseid"];

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
                    string sql = "UPDATE students SET name=@studentName, city=@city, courseId=@courseId WHERE studentId=@id";

                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        command.Parameters.AddWithValue("@id", studentInfo.id);
                        command.Parameters.AddWithValue("@studentName", studentInfo.studentName);
                        command.Parameters.AddWithValue("@city", studentInfo.city);
                        command.Parameters.AddWithValue("@courseId", Int32.Parse(studentInfo.courseId));

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

            successMessage = "Student is Updated successfully";

            Response.Redirect("/Students/Index");

        }
    }
}
