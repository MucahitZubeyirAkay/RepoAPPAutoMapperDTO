using AutoMapper;
using DAL.Context;
using DAL.Interface.Concrete;
using DTOs;
using Microsoft.EntityFrameworkCore;
using MODEL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Implementations
{
    public class TeacherRepository : ITeacherRepository
    {
        private readonly RepositoryDbContext _dbContext;
        private readonly IMapper _mapper;

        public TeacherRepository(RepositoryDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public async Task Create(TeacherDTO teacherDTO)
        {
            var teacher = _mapper.Map<Teacher>(teacherDTO);
            _dbContext.Teachers.Add(teacher);
            await _dbContext.SaveChangesAsync();
        }

        public async Task Delete(int id)
        {
            var teacher = await _dbContext.Teachers.FindAsync(id);
            _dbContext.Teachers.Remove(teacher);
            await _dbContext.SaveChangesAsync();
        }

        public async Task<List<TeacherDTO>> GetAll()
        {
            var teacher = await _dbContext.Teachers.ToListAsync();
            return _mapper.Map<List<TeacherDTO>>(teacher);
        }

        public async Task<TeacherDTO> GetById(int id)
        {
            var teacher = await _dbContext.Teachers.FindAsync(id);
            return _mapper.Map<TeacherDTO>(teacher);
        }

        public async Task Update(TeacherDTO teacherDTO)
        {
            var teacher = _mapper.Map<Teacher>(teacherDTO);
            _dbContext.Entry(teacher).State = EntityState.Modified;
            await _dbContext.SaveChangesAsync();
        }
    }
}
