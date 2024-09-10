using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using Developertest.Models;
using Microsoft.Data.SqlClient;
namespace Developertest.Repos
{
    public class TeacherRepository
    {
        private readonly string _connectionString;

        public TeacherRepository(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection");
        }

        public void AddTeacher(Teacher teacher)
        {
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                using (SqlCommand cmd = new SqlCommand("AddTeacher", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@Name", teacher.Name);
                    cmd.Parameters.AddWithValue("@Age", teacher.Age);
                    cmd.Parameters.AddWithValue("@ImagePath", teacher.ImagePath);
                    cmd.Parameters.AddWithValue("@Sex", teacher.Sex);

                    conn.Open();
                    cmd.ExecuteNonQuery();
                }
            }
        }

        public List<Teacher> GetAllTeachers()
        {
            List<Teacher> teachers = new List<Teacher>();

            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                using (SqlCommand cmd = new SqlCommand("GetAllTeachers", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    conn.Open();
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            teachers.Add(new Teacher
                            {
                                Id = (int)reader["Id"],
                                Name = reader["Name"].ToString(),
                                Age = (int)reader["Age"],
                                ImagePath = reader["ImagePath"].ToString(),
                                Sex = reader["Sex"].ToString()
                            });
                        }
                    }
                }
            }

            return teachers;
        }
    }
}
