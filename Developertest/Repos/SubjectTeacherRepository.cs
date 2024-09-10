using System.Collections.Generic;
using System.Data;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using Developertest.Models;

namespace Developertest.Repos
{
    public class SubjectTeacherRepository
    {
        private readonly string _connectionString;

        public SubjectTeacherRepository(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection");
        }

        public List<TeacherSubjects> GetSubjectsWithTeachers()
        {
            List<TeacherSubjects> result = new List<TeacherSubjects>();

            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                using (SqlCommand cmd = new SqlCommand("GetSubjectsWithTeachers", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    conn.Open();
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        var subjectDictionary = new Dictionary<string, TeacherSubjects>();

                        while (reader.Read())
                        {
                            string subjectName = reader["SubjectName"].ToString();
                            string teacherName = reader["TeacherName"].ToString();

                            if (!subjectDictionary.TryGetValue(subjectName, out var subjectViewModel))
                            {
                                subjectViewModel = new TeacherSubjects
                                {
                                    SubjectName = subjectName,
                                    Teachers = new List<Teacher>()
                                };
                                subjectDictionary.Add(subjectName, subjectViewModel);
                            }

                            subjectViewModel.Teachers.Add(new Teacher { Name = teacherName });
                        }

                        result.AddRange(subjectDictionary.Values);
                    }
                }
            }

            return result;
        }
    }
}
