using AutoMapper;
using DAL.Context;
using DAL.Interface.Abstract;
using DTOs;
using Microsoft.EntityFrameworkCore;
using MODEL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Interface.Concrete
{
    public class StudentRepository : IStudentRepository
    {
        private readonly RepositoryDbContext _dbContext;
        private readonly IMapper _mapper;

        public StudentRepository(RepositoryDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public async Task<List<StudentDTO>> GetAllStudents()
        {
            var students = await _dbContext.Students.ToListAsync();
            return _mapper.Map<List<StudentDTO>>(students);
        }

        public async Task<StudentDTO> GetStudentById(int id)
        {
            var student = await _dbContext.Students.FindAsync(id);
            return _mapper.Map<StudentDTO>(student);
        }

        public async Task CreateStudent(StudentDTO studentDTO)
        {
            var student = _mapper.Map<Student>(studentDTO);
            _dbContext.Students.Add(student);
            await _dbContext.SaveChangesAsync();
        }

        public async Task UpdateStudent(StudentDTO studentDTO)
        {
            var student = _mapper.Map<Student>(studentDTO);
            _dbContext.Entry(student).State = EntityState.Modified;
            await _dbContext.SaveChangesAsync();
        }

        public async Task DeleteStudent(int id)
        {
            var student = await _dbContext.Students.FindAsync(id);
            _dbContext.Students.Remove(student);
            await _dbContext.SaveChangesAsync();
        }
    }
}
