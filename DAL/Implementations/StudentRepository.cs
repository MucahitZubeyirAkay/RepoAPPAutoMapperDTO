using AutoMapper;
using DAL.Context;
using DAL.Interface.Abstract;
using DTOs;
using Microsoft.AspNetCore.Http;
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

        public async Task<List<StudentDTO>> GetAll()
        {
            var students = await _dbContext.Students.ToListAsync();
            return _mapper.Map<List<StudentDTO>>(students);
        }

        /* public async Task<List<StudentDTO>> GetAll()
         {

             var students = _dbContext.Students.ToListAsync();  // 5
             var students1 = _dbContext.Students.ToListAsync(); // 5
             var students2 = _dbContext.Students.ToListAsync(); // 5
             var students3 = _dbContext.Students.ToListAsync(); // 10

             await Task.WhenAll(students, students1, students2, students3); // 10

             //Task.WaitAll(students, students1, students2, students3); // 5

             return _mapper.Map<List<StudentDTO>>(students);
         }
        */

        // client aldığı
        public async Task<List<StudentSmallDTO>> GetAllSmall()
        {
            var query = _dbContext.Students.AsQueryable();
            var list = _mapper.ProjectTo<List<StudentSmallDTO>>(query);
            var students = await list.ToListAsync();

            return _mapper.Map<List<StudentSmallDTO>>(students);
        }

        public async Task<StudentDTO> GetById(int id)
        {
            var student = await _dbContext.Students.FindAsync(id);
            return _mapper.Map<StudentDTO>(student);
        }

        public async Task Create(StudentDTO studentDTO)
        {
            var student = _mapper.Map<Student>(studentDTO);
            _dbContext.Students.Add(student);
            await _dbContext.SaveChangesAsync();
        }

        public async Task Update(StudentDTO studentDTO)
        {
            var student = _mapper.Map<Student>(studentDTO);
            _dbContext.Entry(student).State = EntityState.Modified;
            await _dbContext.SaveChangesAsync();
        }

        public async Task Delete(int id)
        {
            var student = await _dbContext.Students.FindAsync(id);
            _dbContext.Students.Remove(student);
            await _dbContext.SaveChangesAsync();
        }

       
    }
}
