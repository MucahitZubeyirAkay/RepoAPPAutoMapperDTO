using BLL.Operation.Interface;
using DAL.Interface.Concrete;
using DTOs;
using Microsoft.AspNetCore.Http;
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
        private readonly IFileRepository _fileRepository;

       
        public StudentService(IStudentRepository studentRepository, IFileRepository fileRepository)
        {
            _studentRepository = studentRepository;
            _fileRepository = fileRepository;
        }

        public async Task<List<StudentDTO>> GetAll()
        {
            return await _studentRepository.GetAll();
        }

        public async Task<StudentDTO> GetById(int id)
        {
            return await _studentRepository.GetById(id);
        }

        public async Task Create(StudentDTO studentDTO)
        {
            await _studentRepository.Create(studentDTO);
        }

        public async Task Update(StudentDTO studentDTO)
        {
            await _studentRepository.Update(studentDTO);
        }

        public async Task Delete(int id)
        {
            await _studentRepository.Delete(id);
        }

        public async Task<string> UploadFile(IFormFile file)
        {
            if (file == null || file.Length == 0)
            {
                throw new ArgumentException("Dosya geçersiz.");
            }

            var fileName = await _fileRepository.UploadFile(file);

            return fileName;
        }
       
    }
}
