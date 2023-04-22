using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using StudentsApi.Context;
using StudentsApi.Models;

namespace StudentsApi.Services
{
    public class StudentService : IStudentService
    {
        private readonly AppDbContext _context;

        public StudentService(AppDbContext context)
        {
            _context = context;
        }

        public async Task CreateStudent(Student student)
        {
            this._context.Add(student);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteStudent(Student student)
        {
            this._context.Remove(student);
            await _context.SaveChangesAsync();
        }

        public async Task<Student> GetStudent(int id)
        {
            var student = await _context.Students.FindAsync(id);

            if (student == null) throw new Exception("Student not found");

            return student;
        }

        public async Task<IEnumerable<Student>> GetStudents()
        {
            try
            {
                return await this._context.Students.ToListAsync();
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task<IEnumerable<Student>> GetStudentsByName(string? name)
        {
            IEnumerable<Student> students;
            if (!name.IsNullOrEmpty())
            {
                students = await this._context.Students
                    .Where(student => student.Name.Contains(name))
                    .ToListAsync();
            }
            else
            {
                students = await this.GetStudents();
            }
            return students;
        }

        public async Task UpdateStudent(Student student)
        {
            _context.Students.Entry(student).State = EntityState.Modified;
            await _context.SaveChangesAsync();

        }
    }
}
