using Microsoft.EntityFrameworkCore;
using StudentRegistration.Data;
using StudentRegistration.Models;
using StudentRegistration.ViewModel;

namespace StudentRegistration.Repositories
{
    public class StudentRepository : IStudentRepository
    {
        private readonly AppDbContext _dbContext;
        public StudentRepository(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        //Add Student record to database
        public async Task AddAsync(StudentViewModel student)
        {
            var newStudent = new Student()
            {
                FirstName = student.FirstName,
                LastName = student.LastName,
                DateOfBirth = student.DateOfBirth,
                PhoneNumber = student.PhoneNumber,
                Gender = student.Gender,
                Email = student.Email,
                Address = student.Address,
            };
            await _dbContext.Students.AddAsync(newStudent);
            await _dbContext.SaveChangesAsync();
        }

        //Delete Students record
        public async Task DeleteAsync(int id)
        {
            Student student = await _dbContext.Students.FindAsync(id);
            _dbContext.Students.Remove(student);
            await _dbContext.SaveChangesAsync();
        }

        //Get all students record
        public async Task<List<StudentViewModel>> GetAllAsync()
        {
            List<Student> students = await _dbContext.Students.ToListAsync();
            List<StudentViewModel> studentViewModels = new List<StudentViewModel>();

            foreach (var student in students)
            {
                var studentViewModel = new StudentViewModel
                {
                    StudentId = student.StudentId,
                    FirstName = student.FirstName,
                    LastName = student.LastName,
                    DateOfBirth = student.DateOfBirth,
                    Gender = student.Gender,
                    Email = student.Email,
                    PhoneNumber = student.PhoneNumber,
                    Address = student.Address,
                };
                studentViewModels.Add(studentViewModel);
            }
            return studentViewModels;
        }

        //Get students record by id
        public async Task<StudentViewModel> GetByIdAsync(int id)
        {
            var student = await _dbContext.Students.FindAsync(id);
            var studentViewModel = new StudentViewModel
            {
                StudentId = student.StudentId,
                FirstName = student.FirstName,
                LastName = student.LastName,
                DateOfBirth = student.DateOfBirth,
                Gender = student.Gender,
                Email = student.Email,
                PhoneNumber = student.PhoneNumber,
                Address = student.Address
            };
            return studentViewModel;
        }

        //Update students record
        public async Task UpdateAsync(StudentViewModel studentUpdated)
        {
            var students = await _dbContext.Students.FindAsync(studentUpdated.StudentId);
            students.FirstName = studentUpdated.FirstName;
            students.LastName = studentUpdated.LastName;
            students.DateOfBirth = studentUpdated.DateOfBirth;
            students.Gender = studentUpdated.Gender;
            students.Email = studentUpdated.Email;
            students.PhoneNumber = studentUpdated.PhoneNumber;
            students.Address = studentUpdated.Address;

            _dbContext.Students.Update(students);

            await _dbContext.SaveChangesAsync();
        }
    }
}
