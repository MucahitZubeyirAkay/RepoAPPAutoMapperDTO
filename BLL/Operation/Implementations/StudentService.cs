using BLL.Operation.Interface;
using DAL.Interface.Concrete;
using DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Operation.Implementations
{
    public class StudentService : IStudentService
    {
        private readonly IStudentRepository _studentRepository;

        public StudentService(IStudentRepository studentRepository)
        {
            _studentRepository = studentRepository;
        }

        public async Task<List<StudentDTO>> GetAllStudents()
        {
            return await _studentRepository.GetAllStudents();
        }

        public async Task<StudentDTO> GetStudentById(int id)
        {
            return await _studentRepository.GetStudentById(id);
        }

        public async Task CreateStudent(StudentDTO studentDTO)
        {
            await _studentRepository.CreateStudent(studentDTO);
        }

        public async Task UpdateStudent(StudentDTO studentDTO)
        {
            await _studentRepository.UpdateStudent(studentDTO);
        }

        public async Task DeleteStudent(int id)
        {
            await _studentRepository.DeleteStudent(id);
        }
    }
}
