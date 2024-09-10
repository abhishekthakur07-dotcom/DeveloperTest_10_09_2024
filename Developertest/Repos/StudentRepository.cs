using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using Developertest.Models;
using Microsoft.Data.SqlClient;

namespace Developertest.Repos
{
    public class StudentRepository
    {
        private readonly string _connectionString;

        public StudentRepository(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection");
        }

        public void AddStudent(Student student)
        {
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                using (SqlCommand cmd = new SqlCommand("AddStudent", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@Name", student.Name);
                    cmd.Parameters.AddWithValue("@Age", student.Age);
                    cmd.Parameters.AddWithValue("@ImagePath", student.ImagePath);
                    cmd.Parameters.AddWithValue("@Class", student.Class);
                    cmd.Parameters.AddWithValue("@RollNumber", student.RollNumber);

                    conn.Open();
                    cmd.ExecuteNonQuery();
                }
            }
        }

        public List<Student> GetAllStudents()
        {
            List<Student> students = new List<Student>();

            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                using (SqlCommand cmd = new SqlCommand("GetAllStudents", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    conn.Open();
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            students.Add(new Student
                            {
                                Id = (int)reader["Id"],
                                Name = reader["Name"].ToString(),
                                Age = (int)reader["Age"],
                                ImagePath = reader["ImagePath"].ToString(),
                                Class = Convert.ToInt32(reader["Class"]),
                                RollNumber = reader["RollNumber"].ToString()
                            });
                        }
                    }
                }
            }

            return students;
        }


    }
}
