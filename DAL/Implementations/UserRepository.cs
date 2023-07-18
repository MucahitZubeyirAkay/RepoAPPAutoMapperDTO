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
    public class UserRepository : IUserRepository
    {


        private readonly RepositoryDbContext _dbContext;
        private readonly IMapper _mapper;

        public UserRepository(RepositoryDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public async Task Create(UserDTO userDTO)
        {
            var user = _mapper.Map<User>(userDTO);
            _dbContext.Users.Add(user);
            await _dbContext.SaveChangesAsync();
        }

        public async Task Delete(int id)
        {
            var user = await _dbContext.Users.FindAsync(id);
            _dbContext.Users.Remove(user);
            await _dbContext.SaveChangesAsync();
        }

        public async Task<List<UserDTO>> GetAll()
        {
            var user = await _dbContext.Users.ToListAsync();
            return _mapper.Map<List<UserDTO>>(user);
        }

        public async Task<UserDTO> GetById(int id)
        {
            var user = await _dbContext.Users.FindAsync(id);
            return _mapper.Map<UserDTO>(user);
        }

        public async Task Update(UserDTO userDTO)
        {
            var user = _mapper.Map<User>(userDTO);
            _dbContext.Entry(user).State = EntityState.Modified;
            await _dbContext.SaveChangesAsync();
        }



        //public Task<UserDTO> GetUser(string username, string password)
        //{
        //    var kullanici = _dbContext.Users.FirstOrDefault(x => x.UserName == username && x.UserPassword== password);
        //    return Task.FromResult(_mapper.Map<UserDTO>(kullanici));
        //}

        public async Task<UserDTO> GetUser(string username, string password)
        {
            var kullanici = await _dbContext.Users.FirstOrDefaultAsync(x => x.UserName == username && x.UserPassword == password);
            return _mapper.Map<UserDTO>(kullanici);
        }

    }
}
