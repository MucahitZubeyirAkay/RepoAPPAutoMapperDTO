using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Interface.Abstract
{
    public interface IGenericRepository<T>
    {
        Task<List<T>> GetAllStudents();
        Task<T> GetStudentById(int id);
        Task CreateStudent(T entity);
        Task UpdateStudent(T entity);
        Task DeleteStudent(int id);
    }
}
