using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;

namespace StudentManager.Pages.Courses
{
    public class IndexModel : PageModel
    {
        public List<CourseInfo> listCourses = new List<CourseInfo>();
        public void OnGet()
        {
            try
            {
                string connectionString = "Data Source=DESKTOP-HTENID4;Initial Catalog=StudentManagement;Integrated Security=True";

                using(SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    String sql = "SELECT * FROM courses";
                    using(SqlCommand command = new SqlCommand(sql, connection))
                    {
                        using(SqlDataReader reader = command.ExecuteReader())
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
    }

    public class CourseInfo
    {
        public string id;
        public string courseName;
        public string lectureName;
    }
}
