using StudentRegistration.Models;
using StudentRegistration.ViewModel;

namespace StudentRegistration.Repositories
{
    public interface IStudentRepository
    {
        Task<StudentViewModel> GetByIdAsync(int id);
        Task<List<StudentViewModel>> GetAllAsync();
        Task AddAsync(StudentViewModel student);
        Task UpdateAsync(StudentViewModel student);
        Task DeleteAsync(int id);
    }
}
