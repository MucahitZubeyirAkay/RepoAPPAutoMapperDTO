using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Operation.Interface
{
    public interface IGenericService<T>
    {
        Task<List<T>> GetAllStudents();
        Task<T> GetStudentById(int id);
        Task CreateStudent(T studentDTO);
        Task UpdateStudent(T studentDTO);
        Task DeleteStudent(int id);
    }
}
