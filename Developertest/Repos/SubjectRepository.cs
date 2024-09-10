using System.Collections.Generic;
using System.Data;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using Developertest.Models;

namespace Developertest.Repos
{
    public class SubjectRepository
    {
        private readonly string _connectionString;

        public SubjectRepository(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection");
        }

        public void AddSubject(Subject subject)
        {
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                using (SqlCommand cmd = new SqlCommand("AddSubject", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@Name", subject.Name);
                    cmd.Parameters.AddWithValue("@Class", subject.Class);
                    cmd.Parameters.AddWithValue("@Languages", subject.Languages);

                    conn.Open();
                    cmd.ExecuteNonQuery();
                }
            }
        }

        public List<Subject> GetAllSubjects()
        {
            List<Subject> subjects = new List<Subject>();

            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                using (SqlCommand cmd = new SqlCommand("GetAllSubjects", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    conn.Open();
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            subjects.Add(new Subject
                            {
                                Id = (int)reader["Id"],
                                Name = reader["Name"].ToString(),
                                Class = reader["Class"].ToString(),
                                Languages = reader["Languages"].ToString()
                            });
                        }
                    }
                }
            }

            return subjects;
        }
    }
}
