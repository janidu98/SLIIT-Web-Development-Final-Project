using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;

namespace StudentManager.Pages.Students
{
    public class IndexModel : PageModel
    {
        public List<StudentInfo> listStudents = new List<StudentInfo>();
        public void OnGet()
        {
            try
            {
                string connectionString = "Data Source=DESKTOP-HTENID4;Initial Catalog=StudentManagement;Integrated Security=True";

                using(SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    string sql = "SELECT students.studentId, students.name, students.city, courses.name as course_name FROM students JOIN courses ON students.courseId = courses.courseId;";

                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                StudentInfo studentInfo = new StudentInfo();

                                studentInfo.id = "" + reader.GetInt32(0);
                                studentInfo.studentName = reader.GetString(1);
                                studentInfo.city = reader.GetString(2);
                                studentInfo.courseName = reader.GetString(3);

                                listStudents.Add(studentInfo);
                            }
                        }
                    }
                }
            }catch(Exception ex)
            {
                Console.WriteLine("Exception : " + ex.ToString());
            }
        }
    }

    public class StudentInfo
    {
        public string id;
        public string studentName;
        public string city;
        public string courseName;
        public string courseId;
    }
}
