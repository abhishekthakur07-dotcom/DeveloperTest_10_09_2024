namespace Developertest.Models
{
    public class TeacherSubjects
    {
        public string SubjectName { get; set; }
        public List<Teacher> Teachers { get; set; } = new List<Teacher>();
    }
}
