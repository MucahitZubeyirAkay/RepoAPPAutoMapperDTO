using BLL.Operation.Interface;
using DAL.Implementations;
using DAL.Interface.Concrete;
using DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Operation.Implementations
{
   
    public class TeacherService : ITeacherService
    {
        private readonly ITeacherRepository _teacherRepository;

        public TeacherService(ITeacherRepository teacherRepository)
        {
            _teacherRepository = teacherRepository;
        }

        public async Task Create(TeacherDTO teacherDTO)
        {
            await _teacherRepository.Create(teacherDTO);
        }

        public async Task Delete(int id)
        {
            await _teacherRepository.Delete(id);
        }

        public async Task<List<TeacherDTO>> GetAll()
        {
            return await _teacherRepository.GetAll();
        }

        public async Task<TeacherDTO> GetById(int id)
        {
            return await _teacherRepository.GetById(id);
        }

        public async Task Update(TeacherDTO teacherDTO)
        {
            await _teacherRepository.Update(teacherDTO);
        }
    }
}
